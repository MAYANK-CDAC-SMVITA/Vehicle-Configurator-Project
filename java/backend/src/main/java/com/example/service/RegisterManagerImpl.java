package com.example.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.example.model.CompanyInfo;
import com.example.repository.RegistrationRepository;
@Service
public class RegisterManagerImpl implements RegisterManager {
	@Autowired
	RegistrationRepository repository;
	
	@Override
	public CompanyInfo addCompany(CompanyInfo p) {
		return repository.save(p);
		
	}
	
}
