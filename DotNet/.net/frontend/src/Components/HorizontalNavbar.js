import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import "./HorizontalNavbar.css";

const HorizontalNavbar = ({ isLoggedIn, onLogout }) => {
  const [userLoggedIn, setUserLoggedIn] = useState(isLoggedIn);

  useEffect(() => {
    setUserLoggedIn(isLoggedIn);
  }, [isLoggedIn]);

  return (
    <div className="navbar">
      <h1 className="navbar-title">Vehicle Configurator</h1>
      <ul className="nav-list">
        <li><Link to="/">Home</Link></li>
        <li><Link to="/about">About</Link></li>
        <li><Link to="/registration">Registration</Link></li>
        <li><Link to="/contact">Contact</Link></li>

        {userLoggedIn ? (
          <>
            <li><Link to="/welcome">Dashboard</Link></li>
            <li>
              <button className="logout-btn" onClick={onLogout}>Logout</button>
            </li>
          </>
        ) : (
          <li><Link to="/login">Login</Link></li>
        )}
      </ul>
    </div>
  );
};

export default HorizontalNavbar;
