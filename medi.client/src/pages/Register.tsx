import React from 'react';
import '../styles/Register.css';
import nurseImage from '../assets/nurse.png';

const Register: React.FC = () => {
    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
    };
    return (
    <div className="register-page justify-content-around align-items-center w-100 row col-lg-6 col-md-8 col-sm-10">
        <div className="form-box">
            <h2>Zapisz się do Medica</h2>
            <p>Załóż konto i ciesz się wygodą</p>
            <form onSubmit={handleSubmit} className="w-100">

            <input
                type="text"
                placeholder="Imię i Nazwisko"
            />
            <input
                type="email"
                placeholder="Adres e mail"
            />
            <input
                type="password"
                placeholder="Hasło"
            />
                <button className="register-button">Stwórz konto</button>
            </form>

            <p className="login-link mt-3">
                Masz już konto? <a href="/login" className="bold-link">Zaloguj się</a>
            </p>
        </div>
        <img src={nurseImage} alt="Nurse" className="img-fluid register-image d-none d-lg-block row justify-content-center align-items-center" />
    </div>
);}

export default Register;
