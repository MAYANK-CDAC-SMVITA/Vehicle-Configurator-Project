package com.example.model;

import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import jakarta.validation.constraints.Email;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Pattern;
import jakarta.validation.constraints.Size;
@Entity
@Table(name="CompanyInfo")
public class CompanyInfo {
	private int companyId;

    @NotBlank(message = "Company Name is required")
    @Size(max = 255, message = "Company Name must be at most 255 characters")
    //@Column(name = "company_name")
    private String companyName;
    
    @NotBlank(message = "Address Line 1 is required")
    @Size(max = 200, message = "Address must not exceed 200 characters")
    private String addressLine1;
    
    @Size(max = 200, message = "Address must not exceed 200 characters")

    private String addressLine2;
    
    @NotBlank(message = "City is required")
    @Pattern(regexp = "^[A-Za-z ]+$", message = "City should contain only letters")
    private String areaCity;
    
    @NotBlank(message = "State is required")
    @Pattern(regexp = "^[A-Za-z ]+$", message = "State should contain only letters")
    private String state;
    
    @NotBlank(message = "Pin Code is required")
    @Pattern(regexp = "\\d{6}", message = "Pin Code must be exactly 6 digits")
    private String pinCode;
    
    @NotBlank(message = "Phone number is required")
    @Pattern(regexp = "\\d{10}", message = "Telephone number must be exactly 10 digits")
    private String tel;
    
    @Pattern(regexp = "\\d{10}", message = "Fax number must be exactly 10 digits")
    private String fax;
    
    
    private String holdingType; // Can be "Proprietary", "Pvt. Ltd", or "Ltd"
    
    @NotBlank(message = "Authorized Person Name is required")
    @Pattern(regexp = "^[A-Za-z ]+$", message = "Authorized Person Name should contain only letters")
    private String authorizedPersonName;
    
    @NotBlank(message = "Designation is required")
    @Pattern(regexp = "^[A-Za-z ]+$", message = "Designation should contain only letters")
    private String designation;
   
     @Pattern(regexp = "\\d{10}", message = "Authorized Telephone must be exactly 10 digits")
    private String authorizedTel;
    
    
    @Pattern(regexp = "\\d{10}", message = "Authorized Cell must be exactly 10 digits")
    private String authorizedCell;
    
    @Pattern(regexp = "^[A-Z0-9]{8,15}$", message = "Invalid Sales Tax Number format")
    private String stNo; // Sales Tax Number
    
    
    @Pattern(regexp = "[A-Z0-9]{11,15}", message = "Invalid     VAT Registration Number format")
    private String vatRegNo; // VAT Registration Number
    
    //@NotBlank(message = "PAN Number is required")
    @Pattern(regexp = "^[A-Z]{5}[0-9]{4}[A-Z]{1}$", message = "Invalid PAN Number format")
    private String panNo; 
    
    @Email(message = "Invalid email format")
    @NotBlank(message = "Email is required")
    private String email;
    
    @NotBlank(message = "password is required")    
    @Size(min = 6, message = "Password must be at least 6 characters")
    private String password;

    public String getEmail() {
		return email;
	}

	public void setEmail(String email) {
		this.email = email;
	}

	public String getPassword() {
		return password;
	}

	public void setPassword(String password) {
		this.password = password;
	}

	// Getters and Setters
    @Id
    @GeneratedValue(strategy=GenerationType.IDENTITY)
    public int getCompanyId() {
        return companyId;
    }

    public void setCompanyId(int companyId) {
        this.companyId = companyId;
    }

    public String getCompanyName() {
        return companyName;
    }

    public void setCompanyName(String companyName) {
        this.companyName = companyName;
    }

    public String getAddressLine1() {
        return addressLine1;
    }

    public void setAddressLine1(String addressLine1) {
        this.addressLine1 = addressLine1;
    }

    public String getAddressLine2() {
        return addressLine2;
    }

    public void setAddressLine2(String addressLine2) {
        this.addressLine2 = addressLine2;
    }

    public String getAreaCity() {
        return areaCity;
    }

    public void setAreaCity(String areaCity) {
        this.areaCity = areaCity;
    }

    public String getState() {
        return state;
    }

    public void setState(String state) {
        this.state = state;
    }

    public String getPinCode() {
        return pinCode;
    }

    public void setPinCode(String pinCode) {
        this.pinCode = pinCode;
    }

    public String getTel() {
        return tel;
    }

    public void setTel(String tel) {
        this.tel = tel;
    }

    public String getFax() {
        return fax;
    }

    public void setFax(String fax) {
        this.fax = fax;
    }

    public String getHoldingType() {
        return holdingType;
    }

    public void setHoldingType(String holdingType) {
        this.holdingType = holdingType;
    }

    public String getAuthorizedPersonName() {
        return authorizedPersonName;
    }

    public void setAuthorizedPersonName(String authorizedPersonName) {
        this.authorizedPersonName = authorizedPersonName;
    }

    public String getDesignation() {
        return designation;
    }

    public void setDesignation(String designation) {
        this.designation = designation;
    }

    public String getAuthorizedTel() {
        return authorizedTel;
    }

    public void setAuthorizedTel(String authorizedTel) {
        this.authorizedTel = authorizedTel;
    }

    public String getAuthorizedCell() {
        return authorizedCell;
    }

    public void setAuthorizedCell(String authorizedCell) {
        this.authorizedCell = authorizedCell;
    }

    public String getStNo() {
        return stNo;
    }

    public void setStNo(String stNo) {
        this.stNo = stNo;
    }

    public String getVatRegNo() {
        return vatRegNo;
    }

    public void setVatRegNo(String vatRegNo) {
        this.vatRegNo = vatRegNo;
    }

    public String getPanNo() {
        return panNo;
    }

    public void setPanNo(String panNo) {
        this.panNo = panNo;
    }

    @Override
    public String toString() {
        return "CompanyInfo{" +
                "companyId=" + companyId +
                ", companyName='" + companyName + '\'' +
                ", addressLine1='" + addressLine1 + '\'' +
                ", addressLine2='" + addressLine2 + '\'' +
                ", areaCity='" + areaCity + '\'' +
                ", state='" + state + '\'' +
                ", pinCode='" + pinCode + '\'' +
                ", tel='" + tel + '\'' +
                ", fax='" + fax + '\'' +
                ", holdingType='" + holdingType + '\'' +
                ", authorizedPersonName='" + authorizedPersonName + '\'' +
                ", designation='" + designation + '\'' +
                ", authorizedTel='" + authorizedTel + '\'' +
                ", authorizedCell='" + authorizedCell + '\'' +
                ", stNo='" + stNo + '\'' +
                ", vatRegNo='" + vatRegNo + '\'' +
                ", panNo='" + panNo + '\'' +
                '}';
    }
}

