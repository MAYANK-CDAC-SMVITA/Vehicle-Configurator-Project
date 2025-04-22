import React, { useState, useEffect } from "react";
import { BrowserRouter as Router, Routes, Route, Navigate } from "react-router-dom";
import HorizontalNavbar from "./Components/HorizontalNavbar";
import Sidebar from "./Components/Sidebar";
import Home from "./Components/Home";
import About from "./Components/About";
import Registration from "./Components/Registration";
import Contact from "./Components/Contact";
import Login from "./Components/Login";
import Welcome from "./Components/Welcome";
import Configure from "./Components/Configure";
import VehicleDetails from "./Components/VehicleDetails";
import Invoice from "./Components/Invoice";
import "./App.css";

function App() {
    const [email, setEmail] = useState(sessionStorage.getItem("email") || '');  // User email state from sessionStorage
    const [isLoggedIn, setIsLoggedIn] = useState(!!sessionStorage.getItem("email"));  // Check if user is logged in

    useEffect(() => {
      const checkSession = () => {
          const storedEmail = sessionStorage.getItem("email");
          setIsLoggedIn(!!storedEmail);  // ✅ Update login state
          setEmail(storedEmail || "");  // ✅ Update email state
      };
  
      window.addEventListener("storage", checkSession); // ✅ Detect session updates across tabs
      checkSession();  // ✅ Check session immediately on component mount
  
      return () => window.removeEventListener("storage", checkSession);
  }, []);
  

    const handleLogout = () => {
        const confirmLogout = window.confirm("Are you sure you want to logout?");
        if (confirmLogout) {
            sessionStorage.clear();  // Clear sessionStorage
            setIsLoggedIn(false);  // Update login status
            setEmail('');  // Clear email
            window.location.href = "/login"; // Ensures full refresh after logout
        }
    };

    return (
        <Router>
            <div className="App">
                <HorizontalNavbar isLoggedIn={isLoggedIn} onLogout={handleLogout} />
                <div className="main-container">
                    <Sidebar />
                    <div className="content">
                        <Routes>
                            <Route path="/" element={<Home />} />
                            <Route path="/about" element={<About />} />
                            <Route path="/registration" element={<Registration />} />
                            <Route path="/contact" element={<Contact />} />
                            <Route path="/login" element={<Login setEmail={setEmail} />} />

                            {isLoggedIn ? (
                                <>
                                    <Route path="/welcome" element={<Welcome />} />
                                    <Route path="/configure" element={<Configure />} />
                                    <Route path="/vehicle-details" element={<VehicleDetails />} />
                                    <Route path="/invoice" element={<Invoice />} />
                                </>
                            ) : (
                                <Route path="*" element={<Navigate to="/login" />} />
                            )}
                        </Routes>
                    </div>
                </div>
            </div>
        </Router>
    );
}

export default App;