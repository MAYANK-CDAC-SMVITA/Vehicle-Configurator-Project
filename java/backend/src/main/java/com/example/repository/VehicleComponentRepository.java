package com.example.repository;

import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import com.example.model.VehicleDetail;

@Repository
public interface VehicleComponentRepository extends JpaRepository<VehicleDetail, Long> {
	 List<VehicleDetail> findByModelId(Long modelId);
}
