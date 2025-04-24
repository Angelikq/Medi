import React from 'react';
import { Container, Row, Col } from 'react-bootstrap';
import '../styles/Register.css';
import nurseImage from '../assets/nurse.png';

const Register: React.FC = () => (
    <Container className="register-page justify-content-around align-items-center w-100 row  align-items-center" fluid lg={6} md={8} sm={10}>
                <div className="form-box">
                    <h2>Zapisz się do Medica</h2>
                    <p>Załóż konto i ciesz się wygodą</p>
                    <input type="text" placeholder="Imię i Nazwisko" />
                    <input type="email" placeholder="Adres e mail" />
                    <input type="password" placeholder="Hasło" />
                    <button className="register-button">Stwórz konto</button>
                    <p className="login-link mt-3">
                        Masz już konto? <a href="/login" className="bold-link">Zaloguj się</a>
                    </p>
                </div>
                <img src={nurseImage} alt="Nurse" className="img-fluid register-image d-none d-lg-block row justify-content-center align-items-center" />
    </Container>
);

export default Register;
