import React, { useState, useEffect } from "react";
import { useLocation } from "react-router-dom";
import jsPDF from "jspdf";
import "jspdf-autotable";
import "./Invoice.css";

const Invoice = () => {
  const location = useLocation();
  const [invoiceId, setInvoiceId] = useState("");
  const [companyEmail, setCompanyEmail] = useState("Loading...");

  useEffect(() => {
    setInvoiceId(`INV-${Date.now()}-${Math.floor(Math.random() * 1000)}`);
    const storedEmail = sessionStorage.getItem("email");

    setCompanyEmail(storedEmail || "No Email Available");
  }, []);

  const {
    selectedAlternates = {},
    vehicleDetails = {},
    totalPrice = 0,
    nonConfigurableComponents = [],
    standardComponents = [],
    interiorComponents = [],
    exteriorComponents = [],
    date = new Date().toLocaleDateString(),
  } = location.state || {};

  const { modName = "Unknown Model" } = vehicleDetails;

  const allComponents = [
    ...nonConfigurableComponents.map((comp) => comp.component?.componentName || "Unknown Component"),
    ...standardComponents.map((comp) => selectedAlternates[comp.component.id]?.altComponentName || comp.component.componentName),
    ...interiorComponents.map((comp) => selectedAlternates[comp.component.id]?.altComponentName || comp.component.componentName),
    ...exteriorComponents.map((comp) => selectedAlternates[comp.component.id]?.altComponentName || comp.component.componentName),
  ];

  const gstAmount = totalPrice * 0.18;
  const totalWithGST = totalPrice + gstAmount;

  // Function to generate PDF
  const generatePDF = () => {
    const doc = new jsPDF();

    doc.setFontSize(18);
    doc.text("Vehicle Invoice", 14, 20);

    doc.setFontSize(12);
    doc.text(`Invoice ID: ${invoiceId}`, 14, 30);
    doc.text(`Date: ${date}`, 14, 40);
    doc.text(`Model: ${modName}`, 14, 50);
    doc.text(`Email: ${companyEmail}`, 14, 70);

    doc.text("Selected Components:", 14, 85);
    doc.autoTable({
      startY: 90,
      head: [["Component Name"]],
      body: allComponents.map((comp) => [comp]),
    });

    const finalY = doc.lastAutoTable.finalY || 100;
    doc.text(`Total Price (Excl. GST): Rs.${totalPrice.toFixed(2)}`, 14, finalY + 10);
    doc.text(`GST (18%): Rs.${gstAmount.toFixed(2)}`, 14, finalY + 20);
    doc.text(`Total Price (Incl. GST): Rs.${totalWithGST.toFixed(2)}`, 14, finalY + 30);

    return doc;
  };

  // Function to save invoice and send PDF via email
  const handleSaveInvoice = async () => {
    const invoiceData = {
      invoiceId,
      date,
      modelName: modName,
      totalPrice,
      gstAmount,
      totalWithGST,
      email: companyEmail,
      components: JSON.stringify(allComponents),
    };

    try {
      // Save invoice in the database
      const saveResponse = await fetch("http://localhost:8083/api/invoices", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(invoiceData),
      });

      if (!saveResponse.ok) {
        const errorText = await saveResponse.text();
        throw new Error(`Invoice save failed: ${errorText || saveResponse.statusText}`);
      }

      const saveResult = await saveResponse.json();
      console.log("Invoice saved successfully:", saveResult);

      // Generate PDF as Blob
      const doc = generatePDF();
      const pdfBlob = doc.output("blob");

      // Create FormData to send PDF as attachment
      const formData = new FormData();
      formData.append("email", invoiceData.email);
      formData.append("invoiceId", invoiceId);
      formData.append("file", pdfBlob, `Invoice_${invoiceId}.pdf`);

      // Send email with PDF
      const emailResponse = await fetch("http://localhost:8083/api/invoice/send-email", {
        method: "POST",
        body: formData,
      });

      if (!emailResponse.ok) {
        const errorText = await emailResponse.text();
        throw new Error(`Email send failed: ${errorText || emailResponse.statusText}`);
      }

      const emailResult = await emailResponse.json();
      console.log("Email sent successfully:", emailResult);
      alert("Invoice saved & email sent successfully!");
    } catch (error) {
      console.error("Error:", error);
      // Display a more detailed error message
      alert(`An error occurred: ${error.message || 'Please try again later.'}`);
    }
  };

  return (
    <div className="invoice">
      <div className="section">
        <h2>Invoice</h2>
        <p><strong>Invoice ID:</strong> {invoiceId}</p>
        <p><strong>Date:</strong> {date}</p>
        <p><strong>Model:</strong> {modName}</p>
      </div>

      <div className="section">
        <p><strong>Email:</strong> {companyEmail}</p>
      </div>

      <div className="section">
        <h2>Selected Components</h2>
        <ul>
          {allComponents.length === 0 ? <li>No components selected.</li> : allComponents.map((comp, index) => (
            <li key={index}>{comp}</li>
          ))}
        </ul>
      </div>

      <div className="section">
        <p><strong>Total Price (Excl. GST):</strong> Rs.{totalPrice.toFixed(2)}</p>
        <p><strong>GST (18%):</strong> Rs.{gstAmount.toFixed(2)}</p>
        <p><strong>Total Price (Incl. GST):</strong> Rs.{totalWithGST.toFixed(2)}</p>
      </div>

      <button onClick={handleSaveInvoice} className="button save-btn">Save Invoice & Send Email</button>
      <button onClick={() => generatePDF().save("Invoice.pdf")} className="button pdf-btn">Download PDF</button>
    </div>
  );
};

export default Invoice;
