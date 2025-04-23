package com.example.controller;

import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.example.service.ComponentDetailService;



@RestController
@RequestMapping("/api/components")
@CrossOrigin(origins = "*")
public class ComponentDetailController {

    @Autowired
    private ComponentDetailService vehicleComponentService;

    @GetMapping("/{modelId}")
    public Map<String, Object> getComponentDetails(@PathVariable Long modelId) {
        return vehicleComponentService.getComponentDetailsByModelId(modelId);
    }
}
