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
@Table(name = "alt_components")
public class AltComponent {
    
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "alt_id")
    private Long id;

    @ManyToOne(cascade = CascadeType.ALL)
    @JoinColumn(name = "model_id", nullable = false)
    private Model model;

    @ManyToOne(cascade = CascadeType.ALL)
    @JoinColumn(name = "comp_id", nullable = false)
    private Component component;

    @ManyToOne(cascade = CascadeType.ALL)
    @JoinColumn(name = "alt_comp_id", nullable = false)
    private Component altComponent; // This component is an alternative for comp_id

    @Column(name = "price_variation", nullable = false)
    private Double priceVariation; // Price difference between comp_id and alt_comp_id

    public AltComponent() {}

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
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

    public Component getAltComponent() {
        return altComponent;
    }

    public void setAltComponent(Component altComponent) {
        this.altComponent = altComponent;
    }

    public Double getPriceVariation() {
        return priceVariation;
    }

    public void setPriceVariation(Double priceVariation) {
        this.priceVariation = priceVariation;
    }
}
