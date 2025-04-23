package com.example.repository;

import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

import com.example.model.VehicleDetail;

@Repository
@Transactional
public interface VehicleDetailRepository extends JpaRepository<VehicleDetail, Long> {
    List<VehicleDetail> findByModel_Id(Long modelId); // Fetch components by model ID
}
