package com.example.model;

import java.math.BigDecimal;
import java.sql.Date;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.ManyToOne;
import jakarta.persistence.Table;
import jakarta.persistence.Temporal;
import jakarta.persistence.TemporalType;

@Entity
@Table(name = "Invoice")
public class Invoice {
	
	@Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int invId;

    //@Temporal(TemporalType.DATE)
    //@Column(nullable = false)
    private Date invDate;

    //@ManyToOne
    //@JoinColumn(name = "model_id", nullable = false)
    //private Model model;

    private String componentDetails;

    private BigDecimal totalAmt;

    private BigDecimal taxAmt;

    // Getters and Setters
    public int getInvId() {
        return invId;
    }

    public void setInvId(int invId) {
        this.invId = invId;
    }

    public Date getInvDate() {
        return invDate;
    }

    public void setInvDate(Date invDate) {
        this.invDate = invDate;
    }

//    public Model getModel() {
//        return model;
//    }
//
//    public void setModel(Model model) {
//        this.model = model;
//    }

    public String getComponentDetails() {
        return componentDetails;
    }

    public void setComponentDetails(String componentDetails) {
        this.componentDetails = componentDetails;
    }

    public BigDecimal getTotalAmt() {
        return totalAmt;
    }

    public void setTotalAmt(BigDecimal totalAmt) {
        this.totalAmt = totalAmt;
    }

    public BigDecimal getTaxAmt() {
        return taxAmt;
    }

    public void setTaxAmt(BigDecimal taxAmt) {
        this.taxAmt = taxAmt;
    }

}
