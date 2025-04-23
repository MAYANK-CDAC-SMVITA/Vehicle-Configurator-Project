
package com.example.repository;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import com.example.model.Segment;

@Repository
public interface SegmentRepository extends JpaRepository<Segment, Integer> {
}