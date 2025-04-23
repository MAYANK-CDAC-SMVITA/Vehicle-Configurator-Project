package com.example.model;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;

@Entity
@Table(name = "components")
public class Component {
    
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "comp_id")
    private Long id;

    @Column(name = "comp_name", nullable = false)
    private String componentName;

    public Component() {}

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getComponentName() {
        return componentName;
    }

    public void setComponentName(String componentName) {
        this.componentName = componentName;
    }
}
