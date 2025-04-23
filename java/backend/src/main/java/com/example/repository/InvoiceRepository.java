package com.example.repository;

import org.springframework.data.jpa.repository.JpaRepository;

import com.example.model.Invoice;

public interface InvoiceRepository extends JpaRepository<Invoice, Integer>{

}
