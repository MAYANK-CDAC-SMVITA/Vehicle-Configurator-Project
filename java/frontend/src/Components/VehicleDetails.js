import React, { useState, useEffect } from 'react';
import { useLocation, useNavigate } from 'react-router-dom'; 
import './VehicleDetails.css';

const VehicleDetails = () => {
  const location = useLocation();
  const navigate = useNavigate(); 
  const { variant, quantity } = location.state || {};

  const [vehicleDetails, setVehicleDetails] = useState(null);
  const [components, setComponents] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchVehicleDetails = async () => {
      try {
        const vehicleResponse = await fetch(`http://localhost:8080/api/models/${variant}`);
        if (!vehicleResponse.ok) throw new Error('Failed to fetch vehicle details');
        const vehicleData = await vehicleResponse.json();
        setVehicleDetails(vehicleData);

        const componentsResponse = await fetch(`http://localhost:8083/api/vehicle-components/${variant}`);
        if (!componentsResponse.ok) throw new Error('Failed to fetch component details');
        const componentsData = await componentsResponse.json();
        setComponents(componentsData);
      } catch (error) {
        console.error('Error fetching details:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchVehicleDetails();
  }, [variant]);

  if (loading) return <p>Loading vehicle details...</p>;
  if (!vehicleDetails) return <p>No vehicle details available.</p>;

  const { modName, price, imagePath, manufacturer, segment } = vehicleDetails;
  const manufacturerName = manufacturer?.name || 'Unknown';
  const segmentName = segment?.name || 'Unknown';

  const imageUrl = imagePath && imagePath.startsWith('/') ? process.env.PUBLIC_URL + imagePath : imagePath;
  const totalPrice = price * quantity;

  const groupedComponents = components.reduce((acc, comp) => {
    if (!acc[comp.compType]) {
      acc[comp.compType] = [];
    }
    acc[comp.compType].push(comp);
    return acc;
  }, {});

  const compTypeMapping = {
    'E': 'Exterior Features',
    'S': 'Standard Features',
    'I': 'Interior Features'
  };

  const handleConfigure = () => navigate('/configure', { state: { vehicleDetails, components, quantity } });
  const handleModify = () => navigate('/welcome');
  const handleConfirm = () => alert('Vehicle details confirmed!');

  return (
    <div className="vehicle-details">
      <h1>Vehicle Details</h1>
      
      <div className="details-container">
        {/* Image Section */}
        <div className="vehicle-image-section">
          <img src={imageUrl} alt={`${manufacturerName} ${modName}`} className="vehicle-img" />
        </div>

        {/* General Info Section */}
        <div className="vehicle-info-section">
          <h3>{manufacturerName} - {modName}</h3>
          <p><strong>Segment:</strong> {segmentName}</p>
          <p><strong>Manufacturer:</strong> {manufacturerName}</p>
          <p><strong>Variant:</strong> {modName}</p>
          <p><strong>Quantity:</strong> {quantity}</p>
          <p><strong>Total Price:</strong> Rs.{totalPrice.toFixed(2)}</p>
        </div>

        {/* Component Details Section */}
        <div className="component-details-section">
          <h3>Component Details</h3>
          <table>
            <thead>
              <tr>
                
                <th>Component Name</th>
              </tr>
            </thead>
            <tbody>
              {Object.keys(groupedComponents).length === 0 ? (
                <tr><td colSpan="2">No components available for this model</td></tr>
              ) : (
                Object.keys(groupedComponents).map((type, index) => (
                  <React.Fragment key={index}>
                    <tr><td colSpan="2" className="component-header"><strong>{compTypeMapping[type] || 'Unknown Type'}</strong></td></tr>
                    {groupedComponents[type].map((comp, compIndex) => (
                      <tr key={compIndex}>
                       
                        <td>{comp.component.componentName}</td>
                      </tr>
                    ))}
                  </React.Fragment>
                ))
              )}
            </tbody>
          </table>
        </div>
      </div>

      {/* Buttons */}
      <div className="button-container">
        <button onClick={handleConfirm} className="button confirm-btn">Confirm</button>
        <button onClick={handleConfigure} className="button configure-btn">Configure</button>
        <button onClick={handleModify} className="button modify-btn">Modify</button>
      </div>
    </div>
  );
};

export default VehicleDetails;