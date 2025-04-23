package com.example.service;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.example.model.AltComponent;
import com.example.model.VehicleDetail;
import com.example.repository.AltComponentRepository;
import com.example.repository.VehicleComponentRepository;

@Service
public class ComponentDetailService {

    @Autowired
    private VehicleComponentRepository vehicleComponentRepository;

    @Autowired
    private AltComponentRepository altComponentRepository;

    public Map<String, Object> getComponentDetailsByModelId(Long modelId) {
        List<VehicleDetail> vehicleDetails = vehicleComponentRepository.findByModelId(modelId);
        List<AltComponent> altComponents = altComponentRepository.findByModelId(modelId);

        Map<String, Object> response = new HashMap<>();
        response.put("vehicleComponents", vehicleDetails);
        response.put("alternateComponents", altComponents);
        return response;
    }
}
