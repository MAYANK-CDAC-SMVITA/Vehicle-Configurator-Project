package com.example.controller;

import java.util.List;
import java.util.Optional;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

import com.example.model.Invoice;
import com.example.service.InvoiceService;


@RestController

public class InvoiceController {
	
	 @Autowired
	    private InvoiceService invoiceService;
	    
	    
	    @GetMapping("/invoices")
	    public List<Invoice> getAllInvoices() {
	        return invoiceService.getAllInvoices();
	    }
	    
	    
	    @GetMapping("/invoices/{id}")
	    public ResponseEntity<Invoice> getInvoiceById(@PathVariable int id) {
	        Optional<Invoice> invoice = invoiceService.getInvoiceById(id);
	        return invoice.map(ResponseEntity::ok).orElseGet(() -> ResponseEntity.notFound().build());
	    }
	    
	    
	    @PostMapping("/invoices")
	    public ResponseEntity<Invoice> createInvoice(@RequestBody Invoice invoice) {
	        Invoice createdInvoice = invoiceService.createInvoice(invoice);
	        return ResponseEntity.status(HttpStatus.CREATED).body(createdInvoice);
	    }

}
