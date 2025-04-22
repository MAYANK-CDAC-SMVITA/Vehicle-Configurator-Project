import React from 'react';
import Carousel from 'react-bootstrap/Carousel';
import { Link } from 'react-router-dom';
import './Home.css';

const Home = () => {
  return (
    <div className="home">
      <Carousel className="carousel-fullscreen" interval={4000} fade>
        <Carousel.Item>
          <img className="d-block w-100" src="c4.jpg" alt="First slide" />
        </Carousel.Item>
        <Carousel.Item>
          <img className="d-block w-100" src="c2.jpg" alt="Second slide" />
        </Carousel.Item>
        <Carousel.Item>
          <img className="d-block w-100" src="c5.jpg" alt="Third slide" />
        </Carousel.Item>
        <Carousel.Item>
          <img className="d-block w-100" src="c3.jpg" alt="Fourth slide" />
        </Carousel.Item>
        <Carousel.Item>
          <img className="d-block w-100" src="c6.jpg" alt="Fifth slide" />
        </Carousel.Item>
      </Carousel>

      {/* Centered Text & Button */}
      <h1 className="home-title">Welcome to <span>V-Config</span></h1>
      <Link to="/registration" className="register-btn">Register Now</Link>
    </div>
  );
};

export default Home;