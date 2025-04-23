package com.example.service;

import java.util.List;
import java.util.Optional;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.example.model.Invoice;
import com.example.repository.InvoiceRepository;

@Service
public class InvoiceService {
	
	@Autowired
    private InvoiceRepository invoiceRepository;
    
    // Method to get all invoices
    public List<Invoice> getAllInvoices() {
        return invoiceRepository.findAll();
    }
    
    // Method to get an invoice by ID
    public Optional<Invoice> getInvoiceById(int invId) {
        return invoiceRepository.findById(invId);
    }
    
    // Method to create an invoice
    public Invoice createInvoice(Invoice invoice) {
        return invoiceRepository.save(invoice);
    }

}
