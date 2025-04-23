package com.example.service;
import java.util.List;
import com.example.model.Model;

public interface ModelService 
{
	List<Model> getAllModelsByManuIdAndSegId(long segId,long manuId);
	
	List<Model> getAllModels();
	
	Model getModelsById(long id);
}
