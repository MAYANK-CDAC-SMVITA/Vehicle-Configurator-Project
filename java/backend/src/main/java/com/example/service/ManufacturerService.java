
package com.example.service;


import java.util.List;

import com.example.model.Manufacturer;

public interface ManufacturerService {

	List<Manufacturer> getAllManufacturersById(Long id);
}
