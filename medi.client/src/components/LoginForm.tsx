import React, { useState } from "react";

const LoginForm: React.FC = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        console.log("Logging in with:", email, password);
    };

    return (
        <div className="login-card">
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

                <button type="submit" className="login-btn">
                    Zaloguj się
                </button>
            </form>

            <div className="text-center mt-3 login-link">
                <p>
                    Nie masz konta? <a href="/register">Zarejestruj się</a>
                </p>
            </div>
        </div>
    );
};

export default LoginForm;
