package com.example.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.web.multipart.MultipartFile;

import com.example.mail.EmailRegSender;
import com.example.mail.EmailSender;

@Service
public class EmailServiceImpl implements EmailService 
{
	@Autowired
	private EmailSender sender;
	
	@Autowired
	private EmailRegSender regsender;

	@Override
	public void sendEmail(String subject,MultipartFile filename, String message) 
	{
		sender.send(subject,filename, message);
	}
	@Override
	public void sendEmailReg(String subject, String message,String receiptemail) 
	{
		regsender.send(subject, message,receiptemail);
	}

}
