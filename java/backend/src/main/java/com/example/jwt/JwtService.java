package com.example.jwt;

import java.security.Key;
import java.security.NoSuchAlgorithmException;
import java.security.PublicKey;
import java.util.Base64;
import java.util.Date;
import java.util.HashMap;
import java.util.Map;
import java.util.function.Function;

import javax.crypto.KeyGenerator;
import javax.crypto.SecretKey;

import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.stereotype.Service;

import io.jsonwebtoken.ClaimJwtException;
import io.jsonwebtoken.Claims;
import io.jsonwebtoken.Jwts;
import io.jsonwebtoken.SignatureAlgorithm;
import io.jsonwebtoken.io.Decoders;
import io.jsonwebtoken.security.Keys;

@Service
public class JwtService {

	private String secretKey;

	public JwtService() {
		this.secretKey = generateSecretkey();
	}

	public String generateSecretkey() {

		try {

			KeyGenerator keyGen = KeyGenerator.getInstance("HmacSHA256");

			SecretKey secretkey = keyGen.generateKey();

			System.out.println("Secret Key:" + secretkey.toString());

			return Base64.getEncoder().encodeToString(secretkey.getEncoded());

		} catch (NoSuchAlgorithmException e) {

			throw new RuntimeException("Error generating secret key", e);

		}

	}

	public String generateToken(String username) {
		// TODO Auto-generated method stub
		Map<String, Object> claims = new HashMap<>();

		return Jwts.builder().setClaims(claims).setSubject(username).setIssuedAt(new Date(System.currentTimeMillis()))
				.setExpiration(new Date(System.currentTimeMillis() + 1000 * 60 * 3))
				.signWith(getKey(), SignatureAlgorithm.HS256).compact();

	}

	private Key getKey() {
		byte[] keyBytes = Decoders.BASE64.decode(secretKey);

		return Keys.hmacShaKeyFor(keyBytes);
	}

	public String extractUsername(String token) {

		return extractclaim(token, Claims::getSubject);
	}

	private <T> T extractclaim(String token, Function<Claims, T> claimResolver) {
		final Claims claims = extractAllClaims(token);
		return claimResolver.apply(claims);
	}

	private Claims extractAllClaims(String token) {

		return Jwts.parserBuilder().setSigningKey(getKey()).build().parseClaimsJws(token).getBody();
	}

	public boolean validateToken(String token, UserDetails userDetails) {
		final String userName = extractUsername(token);
		return (userName.equals(userDetails.getUsername()) && !isTokenExpired(token));
	}

	private boolean isTokenExpired(String token) {
		
		return extractExpiration(token).before(new Date());
	}

	private Date extractExpiration(String token) {

		return extractclaim(token, Claims::getExpiration);
	}

}