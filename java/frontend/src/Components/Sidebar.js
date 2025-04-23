import React from 'react';
import { Link } from 'react-router-dom';
import { FaHome, FaInfoCircle, FaUserPlus, FaEnvelope } from 'react-icons/fa';
import './Sidebar.css';

const Sidebar = () => {
  return (
    <div className="sidebar">
      <ul className="sidebar-list">
        <li><Link to="/"><FaHome className="sidebar-icon" /></Link></li>
        <li><Link to="/about"><FaInfoCircle className="sidebar-icon" /></Link></li>
        <li><Link to="/registration"><FaUserPlus className="sidebar-icon" /></Link></li>
        <li><Link to="/contact"><FaEnvelope className="sidebar-icon" /></Link></li>
      </ul>
    </div>
  );
};

export default Sidebar;