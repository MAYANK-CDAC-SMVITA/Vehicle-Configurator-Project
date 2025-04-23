package com.example.controller;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.HttpStatusCode;
import org.springframework.http.ResponseEntity;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.Authentication;
import org.springframework.web.bind.annotation.*;

import com.example.jwt.JwtService;
import com.example.model.CompanyInfo;
import com.example.service.RegisterManager;

@RestController
@RequestMapping("/api/auth")
//@CrossOrigin(origins = "*")
public class LoginController {

	@Autowired 
	private RegisterManager register;
    
    @Autowired
    private AuthenticationManager authmanager;
    
    @Autowired
    private JwtService jwtService;
    @PostMapping(value = "/login")
    public ResponseEntity<String> login(@RequestBody CompanyInfo user) {
        try {
            // Authenticate the user
            Authentication authentication = authmanager.authenticate(
                    new UsernamePasswordAuthenticationToken(user.getEmail(), user.getPassword()));
            
            if (authentication.isAuthenticated()) {
                // Generate JWT token for the authenticated user
                String jwt = jwtService.generateToken(user.getEmail());
                return ResponseEntity.status(HttpStatus.OK).body(jwt);
            } else {
                return ResponseEntity.status(HttpStatus.UNAUTHORIZED)
                                     .body("Authentication failed, please check your credentials.");
            }
        } catch (Exception e) {
            // Return Unauthorized if an exception occurs
            return ResponseEntity.status(HttpStatus.UNAUTHORIZED)
                                 .body("Error occurred during authentication: " + e.getMessage());
        }
    }
    
    
    @PostMapping(value = "/signup")
	public ResponseEntity<CompanyInfo> registerCompany(@RequestBody CompanyInfo user) {
		try {

			CompanyInfo createdUser = register.addCompany(user);
			return ResponseEntity.status(HttpStatus.CREATED).body(createdUser);
		} catch (Exception e) {

			return ResponseEntity.status(HttpStatus.BAD_REQUEST).build();
		}
	}
}

