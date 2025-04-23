package com.example.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

import com.example.model.CompanyInfo;
import java.util.Optional;

@Repository
@Transactional
public interface CompanyRepository extends JpaRepository<CompanyInfo, Integer> {
    public CompanyInfo findByEmail(String email);
    //public Optional<CompanyInfo> findByEmail(String email);
    
}
