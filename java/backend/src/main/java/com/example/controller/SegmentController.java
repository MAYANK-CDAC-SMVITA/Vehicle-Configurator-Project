package com.example.controller;

import java.util.List;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.example.model.Model;
import com.example.model.Segment;
import com.example.service.SegmentService;

@CrossOrigin(origins ="*")
@RestController
@RequestMapping("/api/segments")
public class SegmentController {
	
    private final SegmentService segmentService;

    @Autowired
    public SegmentController(SegmentService segmentService) {
        this.segmentService = segmentService;
    }  

    @GetMapping("/")
    public ResponseEntity<List<Segment>> getAllSegments() {
        try {
        	List<Segment> segments = segmentService.getAllSegments();
            return new ResponseEntity<>(segments, HttpStatus.OK);
		} catch (Exception e) {
			return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
		}
		}
  /*
    @GetMapping("/{id}")
    public ResponseEntity<Segment> getSegmentById(@PathVariable int id) {
    	Segment segment = segmentService.getSegmentById(id);
        if (segment != null) {
            return new ResponseEntity<>(segment, HttpStatus.OK);
        } else {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
    }
  */  
}
