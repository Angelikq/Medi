import React, { useState } from "react";

const LoginForm: React.FC = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        console.log("Logging in with:", email, password);
    };

    return (

        <div className="card p-4 shadow" style={{ width: "22rem" }}>
            <h2 className="text-center mb-3">Logowanie</h2>
            <form onSubmit={handleSubmit}>
                <div className="mb-3">
                    <label className="form-label">Email</label>
                    <input
                        type="email"
                        className="form-control"
                        placeholder="Wprowadź email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                    />
                </div>

                <div className="mb-3">
                    <label className="form-label">Hasło</label>
                    <input
                        type="password"
                        className="form-control"
                        placeholder="Wprowadź hasło"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                    />
                </div>

                <button type="submit" className="btn btn-primary w-100">
                    Zaloguj się
                </button>
            </form>
        </div>
    );
};

export default LoginForm;
