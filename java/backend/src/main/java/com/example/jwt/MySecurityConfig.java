package com.example.jwt;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.AuthenticationProvider;
import org.springframework.security.authentication.dao.DaoAuthenticationProvider;
import org.springframework.security.config.Customizer;
import org.springframework.security.config.annotation.authentication.configuration.AuthenticationConfiguration;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.config.http.SessionCreationPolicy;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.web.DefaultSecurityFilterChain;
import org.springframework.security.web.SecurityFilterChain;
import org.springframework.security.web.authentication.UsernamePasswordAuthenticationFilter;
import org.springframework.web.cors.CorsConfiguration;
import org.springframework.web.cors.CorsConfigurationSource;
import org.springframework.web.cors.UrlBasedCorsConfigurationSource;

import java.util.Arrays;

@Configuration
@EnableWebSecurity
public class MySecurityConfig {

	@Autowired
	private UserDetailsService userDetailsService;
	
	@Autowired
	private JwtFilter jwtFilter;

	@Bean
	public BCryptPasswordEncoder passwordEncoder() {
		return new BCryptPasswordEncoder();
	}

	@Bean
	public SecurityFilterChain securityFilterChain(HttpSecurity http) throws Exception {

		http.csrf(customizer -> customizer.disable()).authorizeHttpRequests(request -> request
//		 		.requestMatchers("/public").permitAll()
//		 			
				.requestMatchers("/api/auth/login").permitAll()
				.anyRequest().permitAll())
				.httpBasic(Customizer.withDefaults())
				.sessionManagement(session -> session.sessionCreationPolicy(SessionCreationPolicy.STATELESS))
				.addFilterBefore(jwtFilter, UsernamePasswordAuthenticationFilter.class);
				
				
		return http.build();
	}

	@Bean
	public AuthenticationProvider authenticationProvider() {

		DaoAuthenticationProvider provider = new DaoAuthenticationProvider();
		provider.setUserDetailsService(userDetailsService);
		provider.setPasswordEncoder(new BCryptPasswordEncoder());
		return provider;

	}
	
	@Bean
	public AuthenticationManager autenAuthenticationManager(AuthenticationConfiguration config) throws Exception
	{
		return config.getAuthenticationManager();
	}
	

}