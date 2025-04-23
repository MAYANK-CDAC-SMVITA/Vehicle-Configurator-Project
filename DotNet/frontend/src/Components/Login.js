import React, { useState } from 'react';
import { loginUser } from '../services/api';  // Importing the login helper API function
import { useNavigate } from 'react-router-dom'; // Importing useNavigate
import './Login.css';  // Import your CSS for styling
import { useEffect } from "react";


const Login = ({ setEmail }) => {
    const [email, setEmailInput] = useState('');  // User email state
    const [password, setPassword] = useState('');  // User password state
    const [error, setError] = useState('');  // Error state for invalid credentials
    const [loading, setLoading] = useState(false);  // Loading state for button
    const navigate = useNavigate();  // Initialize useNavigate

  const handleLogin = async (e) => {
    e.preventDefault();
    setLoading(true);
    setError("");

    try {
        const response = await loginUser(email, password);

        if (response && response.token) {
            // ✅ Store data in sessionStorage
            sessionStorage.setItem("email", email);
            sessionStorage.setItem("companyInfo", JSON.stringify(response.companyInfo));
            sessionStorage.setItem("token", response.token);

            // ✅ Immediately update App state
            setEmail(email);  // Update state in App.js
            window.dispatchEvent(new Event("storage"));  // Force update across tabs

            // ✅ Navigate immediately after setting state
            navigate("/welcome"); 
        } else {
            setError("Invalid credentials");
        }
    } catch (error) {
        setError(error.message || "Login failed. Please try again.");
    } finally {
        setLoading(false);
    }
};


    return (
        <div className="login-container">
            <div className="form-wrapper">
                <img
                    src="https://i.postimg.cc/WbVD3VTV/authentication.png"
                    alt="Login Illustration"
                    className="form-img"
                />
                <form onSubmit={handleLogin} className="form-content">
                    <h1 className="form-title">Welcome </h1>

                    {error && <p className="error-message">{error}</p>}  {/* Error message */}

                    <div className="form-div">
                        <div className="form-icon">
                            <i className="bx bxs-user-circle"></i>
                        </div>
                        <div className="form-div-input">
                            <label htmlFor="email" className="form-label">Username</label>
                            <input
                                type="email"
                                id="email"
                                className="form-input"
                                value={email}
                                onChange={(e) => setEmailInput(e.target.value)}  // Update email state
                                required
                            />
                        </div>
                    </div>

                    <div className="form-div">
                        <div className="form-icon">
                            <i className="bx bx-lock"></i>
                        </div>
                        <div className="form-div-input">
                            <label htmlFor="password" className="form-label">Password</label>
                            <input
                                type="password"
                                id="password"
                                className="form-input"
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}  // Update password state
                                required
                            />
                        </div>
                    </div>

                    <a href="#" className="form-forgot">Forgot Password?</a>

                    <button type="submit" className="form-button" disabled={loading}>
                        {loading ? 'Logging in...' : 'Login'}
                    </button>

                    <div className="form-social">
                        <span className="form-social-text">Or login with</span>

                        <a href="#" className="form-social-icon"><i className="bx bxl-facebook"></i></a>
                        <a href="#" className="form-social-icon"><i className="bx bxl-google"></i></a>
                        <a href="#" className="form-social-icon"><i className="bx bxl-instagram"></i></a>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default Login;