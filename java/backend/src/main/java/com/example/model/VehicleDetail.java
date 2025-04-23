package com.example.model;

import jakarta.persistence.CascadeType;
import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.ManyToOne;
import jakarta.persistence.Table;

@Entity
@Table(name = "vehicle_details")
public class VehicleDetail {
    
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "confi_id")
    private Long id;

    @ManyToOne(cascade = CascadeType.ALL)
    @JoinColumn(name = "model_id", nullable = false)
    private Model model;

    @ManyToOne(cascade = CascadeType.ALL)
    @JoinColumn(name = "comp_id", nullable = false)
    private Component component;

    @Column(name = "comp_type", nullable = false, length = 1)
    private char compType; // C=Core, S=Standard, I=Interior, E=Exterior

    @Column(name = "is_configurable", nullable = false, length = 1)
    private char isConfigurable; // Y/N

    

    public VehicleDetail() {}

    public Long getId() {
        return id;
    }

    public Model getModel() {
        return model;
    }

    public void setModel(Model model) {
        this.model = model;
    }

    public Component getComponent() {
        return component;
    }

    public void setComponent(Component component) {
        this.component = component;
    }

    public char getCompType() {
        return compType;
    }

    public void setCompType(char compType) {
        this.compType = compType;
    }

    public char getIsConfigurable() {
        return isConfigurable;
    }

    public void setIsConfigurable(char isConfigurable) {
        this.isConfigurable = isConfigurable;
    }
    
}
