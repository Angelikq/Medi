import React from 'react';
import { Container, Row, Col, Button } from 'react-bootstrap';
import doctorImage from '../assets/homepage.png';
import '../styles/App.css';
import { Link } from 'react-router-dom';

const Home: React.FC = () => (
    <Container className="py-5">
        <Row className="align-items-center flex-column-reverse flex-md-row">
            <Col md={6} className="mb-4 text-section px-3 px-lg-5">
                <h1 className="fw-medium">
                    Umów wizytę do specjalisty na NFZ w Legnicy – szybko i bez kolejek
                </h1>
                <p className="lead">
                    Wybierz specjalistę, termin i zarezerwuj wizytę online bez wychodzenia z domu.
                </p>
                <div className="d-flex flex-wrap gap-3 justify-content-center justify-content-md-start">
                    <Link to="/register">
                        <Button size="lg" className="rounded-pill mb-2 me-md-3">
                            ZAREZERWUJ WIZYTĘ
                        </Button>
                    </Link>
                    <Link to="/login">
                        <Button variant="outline-primary" size="lg" className="rounded-pill mb-2">
                            KONTAKT
                        </Button>
                    </Link>
                </div>
            </Col>
            <Col md={6} className="text-center">
                <img src={doctorImage} alt="Lekarze" className="img-fluid custom-img" />
            </Col>
        </Row>
    </Container>
);

export default Home;
