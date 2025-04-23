import React, { useState, useEffect } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import './Welcome.css';

const Welcome = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const email = location.state?.email;

  // State for dropdown options
  const [segments, setSegments] = useState([]);
  const [manufacturers, setManufacturers] = useState([]);
  const [variants, setVariants] = useState([]);

  // Loading state for fetching data
  const [loadingVariants, setLoadingVariants] = useState(false);

  // Selected values
  const [segment, setSegment] = useState('');
  const [manufacturer, setManufacturer] = useState('');
  const [variant, setVariant] = useState('');
  const [quantity, setQuantity] = useState('8'); // Default to 8 (minimum units)

  // Fetch all segments on component mount
  useEffect(() => {
    const fetchSegments = async () => {
      try {
        const response = await fetch('http://localhost:8083/api/segments/');
        if (!response.ok) throw new Error('Failed to fetch segments');
        const data = await response.json();
        setSegments(data);
        console.log(data)
      } catch (error) {
        console.error('Error fetching segments:', error);
      }
    };
    fetchSegments();
  }, []);

  // Fetch manufacturers when segment changes
  useEffect(() => {
    const fetchManufacturers = async () => {
      if (!segment) {
        setManufacturers([]);
        return;
      }
      try {
        const response = await fetch(`http://localhost:8083/api/manufacturers/${segment}`);
        if (!response.ok) throw new Error('Failed to fetch manufacturers');
        const data = await response.json();
        setManufacturers(data);
      } catch (error) {
        console.error('Error fetching manufacturers:', error);
      }
    };
    fetchManufacturers();
  }, [segment]);

  // Fetch variants when segment and manufacturer are selected
  useEffect(() => {
    const fetchVariants = async () => {
      if (!segment || !manufacturer) {
        setVariants([]);
        return;
      }

      setLoadingVariants(true); // Show loading spinner

      try {
        const response = await fetch(`http://localhost:8083/api/models/${segment}/${manufacturer}`);

        if (!response.ok) throw new Error('Failed to fetch variants');
        const data = await response.json();
        setVariants(data);
      } catch (error) {
        console.error('Error fetching variants:', error);
      } finally {
        setLoadingVariants(false); // Hide loading spinner
      }
    };
    fetchVariants();
  }, [segment, manufacturer]);

  // Handle form submission
  const handleSubmit = (e) => {
    e.preventDefault();
    navigate('/vehicle-details', {
      state: { segment, manufacturer, variant, quantity },
    });
  };

  return (
    <div className="welcome">
      <h2>Welcome to Vehicle Configurator</h2>
      {email && <p>Welcome, {email}!</p>}

      <form onSubmit={handleSubmit} className="welcome-form">
        {/* Segment Dropdown */}
        <div className="form-group">
          <label htmlFor="segment">Segment:</label>
          <select
            id="segment"
            value={segment}
            onChange={(e) => setSegment(e.target.value)}
          >
            <option value="">Select Segment</option>
            {segments.length === 0 ? <option disabled>Loading...</option> :
              segments.map((seg) => (
                <option key={seg.id} value={seg.id}>
                  {seg.name}
                </option>
              ))}
          </select>
        </div>

        {/* Manufacturer Dropdown */}
        <div className="form-group">
          <label htmlFor="manufacturer">Manufacturer:</label>
          <select
            id="manufacturer"
            value={manufacturer}
            onChange={(e) => setManufacturer(e.target.value)}
            disabled={!segment}
          >
            <option value="">Select Manufacturer</option>
            {manufacturers.length === 0 ? <option disabled>Loading...</option> :
              manufacturers.map((manu) => (
                <option key={manu.id} value={manu.id}>
                  {manu.name}
                </option>
              ))}
          </select>
        </div>

        {/* Variant Dropdown */}
        <div className="form-group">
          <label htmlFor="variant">Variant:</label>
          <select
            id="variant"
            value={variant}
            onChange={(e) => setVariant(e.target.value)}
            disabled={!segment || !manufacturer || loadingVariants}
          >
            <option value="">Select Variant</option>
            {loadingVariants ? (
              <option disabled>Loading...</option>
            ) : variants.length === 0 ? (
              <option disabled>No variants available</option>
            ) : (
              variants.map((varnt) => (
                <option key={varnt.id} value={varnt.id}>
                  {varnt.modName} {/* Use modName here */}
                </option>
              ))
            )}
          </select>
        </div>

        {/* Quantity Dropdown (Min 8 Units) */}
        <div className="form-group">
          <label htmlFor="quantity">Quantity:</label>
          <select
            id="quantity"
            value={quantity}
            onChange={(e) => setQuantity(e.target.value)}
          >
            {[...Array(10).keys()].map(i => (
              <option key={i + 8} value={i + 8}>
                {i + 8}
              </option>
            ))}
          </select>
        </div>

        <button type="submit" disabled={!variant}>Submit</button>
      </form>
    </div>
  );
};

export default Welcome;
