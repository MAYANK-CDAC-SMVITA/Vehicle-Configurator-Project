package com.example.service;
import java.util.List;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
import org.springframework.stereotype.Service;
import com.example.model.Model;
import com.example.repository.ModelRepository;

@Component
public  class ModelServiceImpl implements ModelService {

    
    ModelRepository modelRepository;

    @Override
    public List<Model> getAllModelsByManuIdAndSegId( long segId,long manuID) 
    {
        return modelRepository.findByManufacturerIdAndSegmentId1( segId,manuID);
    }
    
    @Override
    public List<Model> getAllModels() 
    {
       return modelRepository.findAll();
    	/*List<Model> mylist=new ArrayList<Model>();
    	Model m1=new Model();
    	mylist.add(m1);
    	return mylist;*/
    }
    
    @Override
    public Model getModelsById(long id) {
        return modelRepository.findById(id).get();
    }
    
}

