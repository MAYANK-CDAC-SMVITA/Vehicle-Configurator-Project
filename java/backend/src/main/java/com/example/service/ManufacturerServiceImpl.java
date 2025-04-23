
package com.example.service;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.example.model.Manufacturer;
import com.example.repository.ManufacturerRepository;



@Service
public class ManufacturerServiceImpl implements ManufacturerService {

	@Autowired
	ManufacturerRepository manufacturerRepository;

	@Override
	public List<Manufacturer> getAllManufacturersById(Long segId) {
		return manufacturerRepository.findBySegmentId(segId);
	}

}

