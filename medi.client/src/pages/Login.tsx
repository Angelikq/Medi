import React from 'react';
import { Container, Row, Col } from 'react-bootstrap';
import '../styles/Login.css';
import witaImage from '../assets/woman.png';
import LoginForm from '../components/LoginForm';

const Login: React.FC = () => (
    <Container className="login-page" fluid>
        <Row className="justify-content-center align-items-center min-vh-100">
            <Col lg={6} md={8} sm={10}>
                <div className="form-box">
                    <LoginForm />
                </div>
            </Col>
            <Col lg={6} className="d-none d-lg-block">
                <img src={witaImage} alt="Wita" className="img-fluid login-image" />
            </Col>
        </Row>
    </Container>
);

export default Login;
