package com.example.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

import com.example.model.CompanyInfo;

@Repository
@Transactional
public interface RegistrationRepository extends JpaRepository<CompanyInfo,Integer>{
	
}
