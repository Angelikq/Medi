import React from 'react';
import { Link } from 'react-router-dom';
import doctorImage from '../assets/homepage.png';
import '../styles/App.css';
import Calendar from '../components/Calendar';

const Home: React.FC = () => (
    <div className="container py-5">
        <div className="row align-items-center flex-column-reverse flex-md-row">
            <div className="col-md-6 mb-4 text-section px-3 px-lg-5">
                <h1 className="fw-medium">
                    Umów wizytę do specjalisty na NFZ w Legnicy – szybko i bez kolejek
                </h1>
                <p className="lead">
                    Wybierz specjalistę, termin i zarezerwuj wizytę online bez wychodzenia z domu.
                </p>
                <div className="d-flex flex-wrap gap-3 justify-content-center justify-content-md-start">
                    <Link to="/register">
                        <button type="button" className="btn lg rounded-pill mb-2 me-md-3 mb-2">
                            ZAREZERWUJ WIZYTĘ
                        </button>
                    </Link>
                    <Link to="/login">
                        <button type ="button" className="btn lg btn-outline-primary rounded-pill mb-2">
                            KONTAKT
                        </button>
                    </Link>
                </div>
            </div>
            <div className="col-md-6 text-center">
                <img src={doctorImage} alt="Lekarze" className="img-fluid custom-img" />
            </div>
        </div>
        <Calendar />
    </div>
);

export default Home;
