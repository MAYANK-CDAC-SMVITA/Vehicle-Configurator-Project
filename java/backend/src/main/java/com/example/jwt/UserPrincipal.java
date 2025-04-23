package com.example.jwt;

import java.util.Collection;
import java.util.Collections;
import java.util.Optional;

import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.authority.SimpleGrantedAuthority;
import org.springframework.security.core.userdetails.UserDetails;

import com.example.model.CompanyInfo;





public class UserPrincipal implements UserDetails {

	
	private CompanyInfo companyinfo;
	
	public UserPrincipal( CompanyInfo companyinfo)
	{
		this.companyinfo = companyinfo;
	}
	
	@Override
	public Collection<? extends GrantedAuthority> getAuthorities() {
		
		return Collections.singleton(new SimpleGrantedAuthority("USER"));
	}

	@Override
	public String getPassword() {

		return companyinfo.getPassword();
	}

	@Override
	public String getUsername() {

		return companyinfo.getEmail();
	}

}