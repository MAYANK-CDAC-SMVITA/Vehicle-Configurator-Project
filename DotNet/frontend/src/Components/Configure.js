import React, { useState, useEffect, useMemo } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import "./Configure.css";

const Configure = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const { vehicleDetails = {}, components = [], quantity = 1 } = location.state || {};

  const nonConfigurableComponents = useMemo(
    () => components?.filter((comp) => comp?.isConfigurable?.trim() === "N") || [],
    [components]
  );

  const [filteredComponents, setFilteredComponents] = useState(nonConfigurableComponents);
  const [altComponentsMap, setAltComponentsMap] = useState({});
  const [openSection, setOpenSection] = useState(""); // To track the open section (Standard, Interior, or Exterior)
  const [standardComponents, setStandardComponents] = useState([]);
  const [interiorComponents, setInteriorComponents] = useState([]);
  const [exteriorComponents, setExteriorComponents] = useState([]);
  const [selectedAlternates, setSelectedAlternates] = useState({});
  const [totalPrice, setTotalPrice] = useState(vehicleDetails.price * quantity);

  useEffect(() => {
    setFilteredComponents(nonConfigurableComponents);
  }, [nonConfigurableComponents]);

  const { modName = "Unknown Model", imagePath = "", manufacturer = {}, segment = {} } = vehicleDetails;
  const manufacturerName = manufacturer?.name || "Unknown Manufacturer";
  const segmentName = segment?.name || "Unknown Segment";
  const imageUrl = imagePath && imagePath.startsWith("/") ? process.env.PUBLIC_URL + imagePath : imagePath;

  useEffect(() => {
    // Total price logic
    let updatedPrice = vehicleDetails.price * quantity;
    Object.keys(selectedAlternates).forEach((key) => {
      updatedPrice += selectedAlternates[key].priceVariation || 0;
    });
    setTotalPrice(updatedPrice);
  }, [selectedAlternates, quantity, vehicleDetails.price]);

  const handleConfirm = () => {
    const modelId = vehicleDetails.id;
    navigate("/invoice", {
      state: {
        selectedAlternates,
        vehicleDetails,
        totalPrice,
        nonConfigurableComponents: filteredComponents,
        standardComponents,
        interiorComponents,
        exteriorComponents,
        modelId, // Add modelId here
      },
    });
  };

  const handleModify = () => navigate("/welcome");

  const fetchComponents = async (compType) => {
    try {
      const modelId = vehicleDetails.id;
      if (!modelId) {
        console.error("Model ID is undefined.");
        return;
      }

      const response = await fetch(`https://localhost:7185/api/components/${modelId}`);
      if (!response.ok) throw new Error("Failed to fetch component details");

      const data = await response.json();

      const filteredComponents = data.vehicleComponents?.filter(
        (comp) => comp.compType === compType && comp.isConfigurable?.trim() === "Y"
      ) || [];

      if (compType === "S") {
        setStandardComponents(filteredComponents);
        setOpenSection(openSection === "S" ? "" : "S"); // Toggle only if the section isn't already open
      } else if (compType === "I") {
        setInteriorComponents(filteredComponents);
        setOpenSection(openSection === "I" ? "" : "I");
      } else if (compType === "E") {
        setExteriorComponents(filteredComponents);
        setOpenSection(openSection === "E" ? "" : "E");
      }

      const altMap = {};
      filteredComponents.forEach((comp) => {
        if (comp.component?.id) {
          altMap[comp.component.id] = data.alternateComponents?.filter(
            (alt) => alt.component?.id === comp.component.id
          ) || [];
        }
      });

      setAltComponentsMap(altMap);
    } catch (error) {
      console.error("Error fetching component details:", error);
    }
  };

  const handleAlternateSelect = (compId, altId, priceVariation, componentName, altComponentName) => {
    setSelectedAlternates((prev) => ({
      ...prev,
      [compId]: { altId, priceVariation, componentName, altComponentName },
    }));
  };

  const renderComponentSelection = (componentsList) =>
    componentsList.map((comp) => {
      const selectedAlt = selectedAlternates[comp.component.id];
      if (altComponentsMap[comp.component.id]?.length === 0) {
        return (
          <div key={comp.component.id} className="configurable-component">
            <label>{comp.component.componentName} (No Alternates)</label>
          </div>
        );
      }
      return (
        <div key={comp.component.id} className="configurable-component">
          <label>
            {comp.component.componentName}{" "}
            {selectedAlt ? `(Selected: ${selectedAlt.altComponentName})` : "(No Alternate Selected)"}
          </label>
          <select
            defaultValue="" // Set the default value to an empty string
            onChange={(e) => {
              const selectedAlt = altComponentsMap[comp.component.id]?.find(
                (alt) => alt.altComponent?.id === parseInt(e.target.value)
              );
              handleAlternateSelect(
                comp.component.id,
                selectedAlt?.altComponent.id || comp.component.id,
                selectedAlt?.priceVariation || 0,
                comp.component.componentName,
                selectedAlt?.altComponent?.componentName || comp.component.componentName
              );
            }}
          >
            {/* Placeholder option */}
            <option value="" disabled hidden>
              Select an alternate component
            </option>
            {/* Default component option */}
            <option value={comp.component.id}>{comp.component.componentName} (Default)</option>
            {/* Alternate component options */}
            {altComponentsMap[comp.component.id]?.map((alt) => (
              <option key={alt.altComponent.id} value={alt.altComponent.id}>
                {alt.altComponent.componentName} (Rs.{alt.priceVariation?.toFixed(2)})
              </option>
            ))}
          </select>
        </div>
      );
    });

  return (
    <div className="configure">
      <h2>Vehicle Configuration</h2>

      <div className="vehicle-info">
        <div className="vehicle-image">
          {imageUrl ? (
            <img src={imageUrl} alt={`${manufacturerName} ${modName}`} className="vehicle-img" />
          ) : (
            <p>No Image Available</p>
          )}
        </div>
        <div className="vehicle-info-text">
          <h3>
            {manufacturerName} - {modName}
          </h3>
          <p>
            <strong>Segment:</strong> {segmentName}
          </p>
          <p>
            <strong>Variant:</strong> {modName}
          </p>
          <p>
            <strong>Quantity:</strong> {quantity}
          </p>
          <p>
            <strong>Total Price:</strong> Rs.{totalPrice.toFixed(2)}
          </p>
        </div>
      </div>

      <h3>Non-Configurable Components</h3>
      <table>
        <thead>
          <tr>
            <th>Component Name</th>
          </tr>
        </thead>
        <tbody>
          {filteredComponents.length === 0 ? (
            <tr>
              <td colSpan="1">No components available.</td>
            </tr>
          ) : (
            filteredComponents.map((comp, index) => (
              <tr key={index}>
                <td>{comp.component?.componentName || "Unknown Component"}</td>
              </tr>
            ))
          )}
        </tbody>
      </table>

      {openSection === "S" ? (
        standardComponents.length === 0 ? (
          <div className="standard-section">
            <h3>No Standard Configurable Components</h3>
          </div>
        ) : (
          <div className="standard-section">
            <h3>Standard Configurable Components</h3>
            {renderComponentSelection(standardComponents)}
          </div>
        )
      ) : null}

      {openSection === "I" ? (
        interiorComponents.length === 0 ? (
          <div className="interior-section">
            <h3>No Interior Configurable Components</h3>
          </div>
        ) : (
          <div className="interior-section">
            <h3>Interior Configurable Components</h3>
            {renderComponentSelection(interiorComponents)}
          </div>
        )
      ) : null}

      {openSection === "E" ? (
        exteriorComponents.length === 0 ? (
          <div className="exterior-section">
            <h3>No Exterior Configurable Components</h3>
          </div>
        ) : (
          <div className="exterior-section">
            <h3>Exterior Configurable Components</h3>
            {renderComponentSelection(exteriorComponents)}
          </div>
        )
      ) : null}

      <div className="filter-buttons">
        <button onClick={() => fetchComponents("S")} className="button standard-btn">
          Standard
        </button>
        <button onClick={() => fetchComponents("I")} className="button interior-btn">
          Interior
        </button>
        <button onClick={() => fetchComponents("E")} className="button exterior-btn">
          Exterior
        </button>
      </div>

      <div className="buttons">
        <button onClick={handleConfirm} className="button confirm-btn">
          Confirm
        </button>
        <button onClick={handleModify} className="button modify-btn">
          Modify
        </button>
      </div>
    </div>
  );
};

export default Configure;