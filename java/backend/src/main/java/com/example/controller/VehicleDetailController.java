package com.example.controller;

import java.util.List;

import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.example.model.VehicleDetail;
import com.example.service.VehicleDetailService;

@RestController
@RequestMapping("/api/vehicle-components")
@CrossOrigin(origins = "*") // Allow frontend requests
public class VehicleDetailController {

    private final VehicleDetailService vehicleDetailService;

    public VehicleDetailController(VehicleDetailService vehicleDetailService) {
        this.vehicleDetailService = vehicleDetailService;
    }

    @GetMapping("/{modelId}")
    public List<VehicleDetail> getComponentsByModel(@PathVariable Long modelId) {
        return vehicleDetailService.getComponentsByModelId(modelId);
    }
}
