package com.example.service;

import org.springframework.web.multipart.MultipartFile;

public interface EmailService 
{
	void sendEmail(String subject,MultipartFile filename,String message);
	void sendEmailReg(String subject,String message,String receiptemail);
}
