package com.example.jwt;
import java.util.Optional;

import org.springframework.beans.factory.annotation.Autowired;

import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;

import org.springframework.stereotype.Service;

import com.example.model.CompanyInfo;
import com.example.repository.CompanyRepository;

@Service
public class MyUserDetailService implements UserDetailsService {

	@Autowired
	private CompanyRepository companyrepository;

	@Override
	public UserDetails loadUserByUsername(String email) throws UsernameNotFoundException {

		CompanyInfo user = companyrepository.findByEmail(email);
		if (user == null) {

			throw new UsernameNotFoundException("not found");
		}

		return new UserPrincipal(user);
	}

}
