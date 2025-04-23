package com.example.mail;


import java.util.Properties;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Component;

import jakarta.mail.BodyPart;
import jakarta.mail.Message;
import jakarta.mail.Multipart;
import jakarta.mail.PasswordAuthentication;
import jakarta.mail.Session;
import jakarta.mail.Transport;
import jakarta.mail.internet.InternetAddress;
import jakarta.mail.internet.MimeBodyPart;
import jakarta.mail.internet.MimeMessage;
import jakarta.mail.internet.MimeMultipart;
@Component
public class EmailRegSender {
	@Value("${file.path}")
	private String path;
	public void send(String subject,String message,String receiptemail)
	{
		final String username = "vehicleconfigurator@gmail.com";
		final String password = "ihhi nuro srmz ltpr";
 
		Properties props = new Properties();
		props.put("mail.smtp.auth", "true");
		props.put("mail.smtp.starttls.enable", "true");
		props.put("mail.smtp.host", "smtp.gmail.com");
		props.put("mail.smtp.port", "587");
 
		Session session = Session.getInstance(props,
		  new jakarta.mail.Authenticator() {
			protected PasswordAuthentication getPasswordAuthentication() {
				return new PasswordAuthentication(username, password);
			}
		  });
 
		try {
 
			MimeMessage message1 = new MimeMessage(session);
			message1.setRecipients(Message.RecipientType.TO,
			InternetAddress.parse(receiptemail));
			message1.setSubject(subject);

			
			 
			
			BodyPart part2 = new MimeBodyPart();  
		    part2.setText(message);  
			
			Multipart multipart = new MimeMultipart();  
			
			multipart.addBodyPart(part2);
			
			message1.setContent(multipart);
			Transport.send(message1);
 
			System.out.println("Done completely");
 
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
}
