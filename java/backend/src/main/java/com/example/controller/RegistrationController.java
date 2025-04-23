package com.example.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

import com.example.model.CompanyInfo;
import com.example.service.RegisterManager;

@RestController
@CrossOrigin("*")
public class RegistrationController {
	@Autowired
	RegisterManager manager;
	
	@PostMapping(value="api/new_Company")
	public void afterSubmit(@RequestBody CompanyInfo company)
	{
		
		manager.addCompany(company);
	}
	
}

