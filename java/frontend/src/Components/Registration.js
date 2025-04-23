import React, { useState } from "react";
import "./Registration.css";

const Registration = () => {
  const [formData, setFormData] = useState({
    companyName: "",
    addressLine1: "",
    addressLine2: "",
    areaCity: "",
    state: "",
    pinCode: "",
    tel: "",
    fax: "",
    holdingType: "",
    authorizedPersonName: "",
    designation: "",
    authorizedTel: "",
    authorizedCell: "",
    stNo: "",
    vatRegNo: "",
    panNo: "",
    email: "",
    password: ""
  });

  const [error, setError] = useState("");

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };

  const sendEmail = async () => {
    try {
      await fetch("http://localhost:8080/email/reg", {
        method: "POST",
        headers: {
          "Content-Type": "application/x-www-form-urlencoded",
        },
        body: new URLSearchParams({
          subject: "Registration Successful",
          message: `Dear ${formData.authorizedPersonName}, Your company ${formData.companyName} has been registered successfully!`,
          email: formData.email,
        }),
      });
    } catch (error) {
      console.error("Error sending email:", error);
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
  
    // Validation: Check if all required fields are filled
    if (
      !formData.companyName ||
      !formData.addressLine1 ||
      !formData.areaCity ||
      !formData.state ||
      !formData.pinCode ||
      !formData.holdingType ||
      !formData.authorizedPersonName ||
      !formData.designation ||
      !formData.email ||
      !formData.password
    ) {
      setError("Please fill all mandatory fields marked with *.");
      return;
    }
    // Pin Code validation (only 6-digit numbers)
    if (!/^\d{6}$/.test(formData.pinCode)) {
      setError("Pin Code must be exactly 6 digits.");
      return;
    }

    // Telephone and Fax should be numeric (if entered)
    if (formData.tel && !/^\d+$/.test(formData.tel)) {
      setError("Telephone number must contain only digits.");
      return;
    }

    if (formData.fax && !/^\d+$/.test(formData.fax)) {
      setError("Fax number must contain only digits.");
      return;
    }

    // PAN Number validation (if entered)
    if (formData.panNo && !/^[A-Z]{5}[0-9]{4}[A-Z]{1}$/.test(formData.panNo)) {
      setError("Invalid PAN Number format.");
      return;
    }


    // Validate Area / City (Only letters and spaces)
  const cityRegex = /^[A-Za-z\s]+$/;  // Only letters and spaces allowed
  if (!formData.areaCity || !cityRegex.test(formData.areaCity)) {
    let isValid = false;
    let errorMessages = []; 
     errorMessages.push("Area/City should only contain alphabetic characters and spaces.");
  }
    setError("");
  
    try {
      // Send data to the  backend (adjust the endpoint URL as necessary)
      const response = await fetch("http://localhost:8080/api/new_Company", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(formData),
      });
  
      if (response.ok) {
        const result = await response.json().catch((error) => {
          console.error("Failed to parse JSON:", error);
          return {}; // Return an empty object if JSON parsing fails
        });
  
        // Ensure result contains expected data
       
          alert("Registration successful");
          await sendEmail();
        
  
        // Clear form after successful submission
        setFormData({
          companyName: "",
          addressLine1: "",
          addressLine2: "",
          areaCity: "",
          state: "",
          pinCode: "",
          tel: "",
          fax: "",
          holdingType: "",
          authorizedPersonName: "",
          designation: "",
          authorizedTel: "",
          authorizedCell: "",
          stNo: "",
          vatRegNo: "",
          panNo: "",
          email: "",
          password: ""
        });
      } else {
        alert("Failed to register. Please try again.");
      }
    } catch (error) {
      console.error("Error submitting registration:", error);
      alert("An error occurred while registering. Please try again later.");
    }
  };

  const handleClear = () => {
    setFormData({
      companyName: "",
      addressLine1: "",
      addressLine2: "",
      areaCity: "",
      state: "",
      pinCode: "",
      tel: "",
      fax: "",
      holdingType: "",
      authorizedPersonName: "",
      designation: "",
      authorizedTel: "",
      authorizedCell: "",
      stNo: "",
      vatRegNo: "",
      panNo: "",
      email: "",
      password: ""
    });
    setError("");
  };

  return (
    <div className="registration-container">
      <h2>Company Registration</h2>
      {error && <p className="error-message">{error}</p>}
      <form onSubmit={handleSubmit} className="registration-form">
        <div className="form-group">
          <label>
            Company Name: <span className="mandatory">*</span>
          </label>
          <input
            type="text"
            name="companyName"
            value={formData.companyName}
            onChange={handleChange}
          />
        </div>

        <div className="form-group">
          <label>
            Address Line 1: <span className="mandatory">*</span>
          </label>
          <input
            type="text"
            name="addressLine1"
            value={formData.addressLine1}
            onChange={handleChange}
          />
        </div>

        <div className="form-group">
          <label>Address Line 2:</label>
          <input
            type="text"
            name="addressLine2"
            value={formData.addressLine2}
            onChange={handleChange}
          />
        </div>

        <div className="form-group">
          <label>
            Area / City: <span className="mandatory">*</span>
          </label>
          <input
            type="text"
            name="areaCity"
            value={formData.areaCity}
            onChange={handleChange}
          />
        </div>

        <div className="form-group">
          <label>
            State: <span className="mandatory">*</span>
          </label>
          <input
            type="text"
            name="state"
            value={formData.state}
            onChange={handleChange}
          />
        </div>

        <div className="form-group">
          <label>
            Pin Code: <span className="mandatory">*</span>
          </label>
          <input
            type="text"
            name="pinCode"
            value={formData.pinCode}
            onChange={handleChange}
          />
        </div>

        <div className="form-group">
          <label>Telephone: <span className="mandatory">*</span></label>
          <input
            type="text"
            name="tel"
            value={formData.tel}
            onChange={handleChange}
          />
        </div>

        <div className="form-group">
          <label>Fax:</label>
          <input
            type="text"
            name="fax"
            value={formData.fax}
            onChange={handleChange}
          />
        </div>

        <div className="form-group">
          <label>
            Holding Type: <span className="mandatory">*</span>
          </label>
          <select
            name="holdingType"
            value={formData.holdingType}
            onChange={handleChange}
          >
            <option value="">Select</option>
            <option value="Proprietary">Proprietary</option>
            <option value="Pvt. Ltd">Pvt. Ltd</option>
            <option value="Ltd">Ltd</option>
          </select>
        </div>

        <div className="form-group">
          <label>
            Authorized Person Name: <span className="mandatory">*</span>
          </label>
          <input
            type="text"
            name="authorizedPersonName"
            value={formData.authorizedPersonName}
            onChange={handleChange}
          />
        </div>

        <div className="form-group">
          <label>
            Designation: <span className="mandatory">*</span>
          </label>
          <input
            type="text"
            name="designation"
            value={formData.designation}
            onChange={handleChange}
          />
        </div>

        <div className="form-group">
          <label>Authorized Telephone:</label>
          <input
            type="text"
            name="authorizedTel"
            value={formData.authorizedTel}
            onChange={handleChange}
          />
        </div>

        <div className="form-group">
          <label>Authorized Cell:</label>
          <input
            type="text"
            name="authorizedCell"
            value={formData.authorizedCell}
            onChange={handleChange}
          />
        </div>

        <div className="form-group">
          <label>Company's ST No:</label>
          <input
            type="text"
            name="stNo"
            value={formData.stNo}
            onChange={handleChange}
          />
        </div>

        <div className="form-group">
          <label>Company VAT Reg. No:</label>
          <input
            type="text"
            name="vatRegNo"
            value={formData.vatRegNo}
            onChange={handleChange}
          />
        </div>

        <div className="form-group">
          <label>PAN No:</label>
          <input
            type="text"
            name="panNo"
            value={formData.panNo}
            onChange={handleChange}
          />
        </div>

        <div className="form-group">
          <label>Email:<span className="mandatory">*</span></label>
          <input
            type="text"
            name="email"
            value={formData.email}
            onChange={handleChange}
          />
        </div>
        <div className="form-group">
          <label>Password:<span className="mandatory">*</span></label>
          <input
            type="password"
            name="password"
            value={formData.password}
            onChange={handleChange}
          />
        </div>

        <div className="form-buttons">
          <button type="submit">Submit</button>
          <button type="button" onClick={handleClear}>
            Clear
          </button>
        </div>
      </form>
    </div>
  );
};

export default Registration;
