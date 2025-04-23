import React from 'react';
import './Header.css';

const Header = ({ email }) => {
  return (
    <div className="header">
      <h1>Vehicle Configurator</h1>
      {email && (
        <div className="welcome-email">
          <p>Welcome: {email}</p>
        </div>
      )}
    </div>
  );
};

export default Header;
