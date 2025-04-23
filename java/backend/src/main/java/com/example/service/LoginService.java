package com.example.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import com.example.model.CompanyInfo;
import com.example.repository.CompanyRepository;
import java.util.Optional;

@Service
public class LoginService {

    @Autowired
    private CompanyRepository companyRepository;

    public String authenticateUser(String email, String password) {
    	System.out.println("email is \t"+email);
    	System.out.println("password is\t"+password);
       
        CompanyInfo companyInfo = companyRepository.findByEmail(email);

        if (companyInfo==null) {
            return "Invalid user";
        }


       
        else if (!companyInfo.getPassword().equals(password)) {
            return "Incorrect password";
        }

        return "Success";
    }
}

