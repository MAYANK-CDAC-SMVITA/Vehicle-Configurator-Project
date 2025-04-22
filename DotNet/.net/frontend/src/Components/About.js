import React, { useState, useEffect } from 'react';

const AboutPage = () => {
  const [aboutUs, setAboutUs] = useState('');
  const [whoWeAre, setWhoWeAre] = useState('');
  const [whoContent, setWhoContent] = useState('');
  const [techStack, setTechStack] = useState('');
  const [techContent, setTechContent] = useState('');
  const [error, setError] = useState(null);
  const [language, setLanguage] = useState('en-US'); // Default language

  const fetchData = async (url, setter) => {
    try {
      const response = await fetch(url, {
        method: 'GET',
        headers: {
          'Accept-Language': language,
          'Cache-Control': 'no-cache',
        },
      });

      if (!response.ok) {
        throw new Error('Failed to fetch data');
      }

      const data = await response.text();
      setter(data);
    } catch (err) {
      setError(err.message);
    }
  };

  useEffect(() => {
    fetchData('http://localhost:8083/aboutus', setAboutUs);
    fetchData('http://localhost:8083/who', setWhoWeAre);
    fetchData('http://localhost:8083/whocontent', setWhoContent);
    fetchData('http://localhost:8083/techstack', setTechStack);
    fetchData('http://localhost:8083/techcontent', setTechContent);
  }, [language]); // Refetch data when language changes

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'flex-end' }}>
        <select 
          value={language} 
          onChange={(e) => setLanguage(e.target.value)} 
          style={{ padding: '2px 5px', fontSize: '10px', height: '20px', width: '80px' }}
        >
          <option value="en-US">Eng</option>
          <option value="es-ES">Esp</option>
          <option value="hi-IN">हिंदी</option>
        </select>
      </div>
      
      {error && <div style={{ color: 'red' }}>{`Error: ${error}`}</div>}

      <h2>{aboutUs}</h2>
      <h3>{whoWeAre}</h3>
      <p>{whoContent}</p>

      <h3>{techStack}</h3>
      <p>{techContent}</p>
    </div>
  );
};

export default AboutPage;