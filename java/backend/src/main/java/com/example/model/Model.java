package com.example.model;
import org.hibernate.annotations.OnDelete;
import org.hibernate.annotations.OnDeleteAction;
import com.fasterxml.jackson.annotation.JsonIgnore;
import jakarta.persistence.CascadeType;
import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.FetchType;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.ManyToOne;
import jakarta.persistence.Table;

@Entity
@Table(name = "models")
public class Model {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private long id;

//    @ManyToOne(fetch = FetchType.LAZY, optional = false)
    @ManyToOne(cascade=CascadeType.ALL)
      @JoinColumn(name = "segId", referencedColumnName="id")
      private Segment segment;

//    @ManyToOne(fetch = FetchType.LAZY, optional = false)
     @ManyToOne(cascade=CascadeType.ALL)
      @JoinColumn(name = "manuId", referencedColumnName="id")
//    @OnDelete(action = OnDeleteAction.CASCADE)
     private Manufacturer manufacturer;

    @Column(nullable = false)
    private String modName;

    @Column(nullable = false)
    private int price;

    @Column(nullable = false)
    private String imagePath;

    @Column(nullable = false)
    private int minQty;

    @Column(columnDefinition = "int default 5")
    private int safetyRating;

    public Model() {
        // Default constructor
    }

    // Constructors if needed
//    public Model(long id, Segment segment, Manufacturer manufacturer, String modName, int price, String imagePath,
//			int minQty, int safetyRating) {
//		super();
//		this.id = id;
//		this.segment = segment;
//		this.manufacturer = manufacturer;
//		this.modName = modName;
//		this.price = price;
//		this.imagePath = imagePath;
//		this.minQty = minQty;
//		this.safetyRating = safetyRating;
//	}


    public long getId() {
        return id;
    }

	public void setId(long id) {
        this.id = id;
    }

    public Segment getSegment() {
        return segment;
    }

    public void setSegment(Segment segment) {
        this.segment = segment;
    }

    public Manufacturer getManufacturer() {
        return manufacturer;
    }

    public void setManufacturer(Manufacturer manufacturer) {
        this.manufacturer = manufacturer;
    }

    public String getModName() {
        return modName;
    }

	public void setModName(String modName) {
        this.modName = modName;
    }

    public int getPrice() {
        return price;
    }

    public void setPrice(int price) {
        this.price = price;
    }

    public String getImagePath() {
        return imagePath;
    }

    public void setImagePath(String imagePath) {
        this.imagePath = imagePath;
    }

    public int getMinQty() {
        return minQty;
    }   

    public void setMinQty(int minQty) {
        this.minQty = minQty;
    }
    

    public int getSafetyRating() {
        return safetyRating;
    }

    public void setSafetyRating(int safetyRating) {
        this.safetyRating = safetyRating;
    }

	@Override
	public String toString() {
		return "Model [id=" + id + ", segment=" + segment + ", manufacturer=" + manufacturer + ", modName=" + modName
				+ ", price=" + price + ", imagePath=" + imagePath + ", minQty=" + minQty + ", safetyRating="
				+ safetyRating + "]";
	}

 
    
    
}

