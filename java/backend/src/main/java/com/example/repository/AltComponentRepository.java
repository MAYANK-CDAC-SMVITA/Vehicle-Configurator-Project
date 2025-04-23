package com.example.repository;

import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import com.example.model.AltComponent;

@Repository
public interface AltComponentRepository extends JpaRepository<AltComponent, Long> {
	  List<AltComponent> findByModelId(Long modelId);
}
