import React, { useState } from 'react';
import witaImage from '../assets/woman.png';
import AlertMessage from '../components/AlertMessage';

interface FormData {
    email: string;
    password: string;
}

const Login: React.FC = () => {
    const [errors, setErrors] = useState({
        email: '',
        password: ''
    });
    const [message, setMessage] = useState<{ type: 'success' | 'error'; text: string } | null>(null);

    const validate = (formData: FormData) => {
        const newErrors = { email: '', password: '' };
        let valid = true;

        if (!/\S+@\S+\.\S+/.test(formData.email)) {
            newErrors.email = 'Nieprawidłowy adres e-mail.';
            valid = false;
        }

        if (!formData.password.trim()) {
            newErrors.password = 'Hasło jest wymagane.';
            valid = false;
        }

        setErrors(newErrors);
        return valid;
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        const form = e.target as HTMLFormElement;

        const formData: FormData = {
            email: (form.elements.namedItem('email') as HTMLInputElement).value,
            password: (form.elements.namedItem('password') as HTMLInputElement).value,
        };
        if (!validate(formData)) return;

        try {
            const response = await fetch('https://localhost:7061/api/users/login', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(formData),
            });

            if (!response.ok) {
                const errorData = await response.json().catch(() => null);
                const msg = errorData?.message || 'Wystąpił błąd. Spróbuj ponownie później.';
                setMessage({ type: 'error', text: msg });
                return;
            }

            setMessage({ type: 'success', text: 'Zalogowano pomyślnie!' });
        } catch {
            setMessage({ type: 'error', text: 'Nie udało się połączyć z serwerem. Sprawdź połączenie z internetem.' });
        }
    };

    return (
        <div className="register-page row justify-content-around align-items-center w-100 row col-lg-6 col-md-8, col-sm-10">
            <AlertMessage message={message} setMessage={setMessage} />
            <div className="form-box">
                <h2 className="form-heading">Zaloguj się na swoje konto</h2>
                <p className="form-subtitle">Miło Cię znowu widzieć!</p>
                <form onSubmit={handleSubmit} className="w-100">
                    <div className="mb-3">
                        <input
                            type="email"
                            className={`form-control ${errors.email ? 'border border-danger bg-danger-subtle' : ''}`}
                            placeholder="Adres e‑mail"
                            name="email"
                        />
                        {errors.email && <span className="text-danger small mb-2">{errors.email}</span>}
                    </div>

                    <div className="mb-3">
                        <input
                            type="password"
                            className={`form-control ${errors.password ? 'border border-danger bg-danger-subtle' : ''}`}
                            placeholder="Hasło"
                            name="password"
                        />
                        {errors.password && <span className="text-danger small mb-2">{errors.password}</span>}
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
    );
}

export default Login;
