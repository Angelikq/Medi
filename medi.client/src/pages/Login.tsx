import React, { useState } from 'react';
import witaImage from '../assets/woman.png';

const Login: React.FC = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        console.log("Logging in with:", email, password);
    };
    return (
    <div className="register-page row justify-content-around align-items-center w-100 row col-lg-6 col-md-8, col-sm-10">
        <div className="form-box">
                <h2 className="form-heading">Zaloguj się na swoje konto</h2>
                <p className="form-subtitle">Miło Cię znowu widzieć!</p>
                <form onSubmit={handleSubmit} className="w-100">
                    <div className="mb-3">
                        <input
                            type="email"
                            className="form-control"
                            placeholder="Adres e‑mail"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                        />
                    </div>

                    <div className="mb-3">
                        <input
                            type="password"
                            className="form-control"
                            placeholder="Hasło"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                        />
                    </div>

                    <button type="submit" className="register-button">
                        Zaloguj się
                    </button>
                </form>

                <div className="text-center mt-3 login-link">
                    <p>
                        Nie masz konta? <a href="/register">Zarejestruj się</a>
                    </p>
            </div>
        </div>
            <img src={witaImage} alt="Wita" className="img-fluid register-image d-none d-lg-block row justify-content-center align-items-center" />
    </div>
);}

export default Login;
