package com.example.service;

import java.util.List;

import org.springframework.stereotype.Service;

import com.example.model.VehicleDetail;
import com.example.repository.VehicleDetailRepository;

@Service
public class VehicleDetailService {

    private final VehicleDetailRepository vehicleDetailRepository;

    public VehicleDetailService(VehicleDetailRepository vehicleDetailRepository) {
        this.vehicleDetailRepository = vehicleDetailRepository;
    }

    public List<VehicleDetail> getComponentsByModelId(Long modelId) {
        return vehicleDetailRepository.findByModel_Id(modelId);
    }
}
