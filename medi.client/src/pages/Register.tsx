import React, { useState } from 'react';
import '../styles/Register.css';
import nurseImage from '../assets/nurse.png';
import AlertMessage from '../components/AlertMessage';

interface FormData {
    name: string;
    email: string;
    password: string;
    confirmPassword: string;
}

const Register: React.FC = () => {
    const [message, setMessage] = useState<{ type: 'success' | 'error'; text: string } | null>(null);
    const [errors, setErrors] = useState({
        name: '',
        email: '',
        password: '',
        confirmPassword: '',
    });

    const validate = (formData: FormData) => {
        const newErrors = { name: '', email: '', password: '', confirmPassword: '' };
        let valid = true;

        if (!formData.name.trim()) {
            newErrors.name = 'Imię i nazwisko jest wymagane.';
            valid = false;
        }

        if (!/\S+@\S+\.\S+/.test(formData.email)) {
            newErrors.email = 'Nieprawidłowy adres e-mail.';
            valid = false;
        }

        if (formData.password.length < 6) {
            newErrors.password = 'Hasło musi mieć co najmniej 6 znaków.';
            valid = false;
        }

        if (formData.password !== formData.confirmPassword) {
            newErrors.confirmPassword = 'Hasła nie są zgodne.';
            valid = false;
        }

        setErrors(newErrors);
        return valid;
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        const form = e.target as HTMLFormElement;

        const formData: FormData = {
            name: (form.elements.namedItem('name') as HTMLInputElement).value,
            email: (form.elements.namedItem('email') as HTMLInputElement).value,
            password: (form.elements.namedItem('password') as HTMLInputElement).value,
            confirmPassword: (form.elements.namedItem('confirmPassword') as HTMLInputElement).value,
        };

        if (!validate(formData)) return;

        try {
            const response = await fetch('https://localhost:7061/api/users/register', {
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

            setMessage({ type: 'success', text: 'Rejestracja zakończona sukcesem!' });
        } catch {
            setMessage({ type: 'error', text: 'Nie udało się połączyć z serwerem. Sprawdź połączenie z internetem.' });
        }
    };

    return (
        <div className="register-page justify-content-around align-items-center w-100 row col-lg-6 col-md-8 col-sm-10">
            <AlertMessage message={message} setMessage={setMessage} />
            <div className="form-box">
                <h2>Zapisz się do Medica</h2>
                <p>Załóż konto i ciesz się wygodą</p>
                <form onSubmit={handleSubmit} className="w-100">
                    <input
                        type="text"
                        name="name"
                        placeholder="Imię i Nazwisko"
                        className={errors.name ? 'border border-danger bg-danger-subtle' : ''}
                    />
                    {errors.name && <span className="text-danger small mb-2">{errors.name}</span>}

                    <input
                        type="email"
                        name="email"
                        placeholder="Adres e-mail"
                        className={errors.email ? 'border border-danger bg-danger-subtle' : ''}
                    />
                    {errors.email && <span className="text-danger small mb-2">{errors.email}</span>}

                    <input
                        type="password"
                        name="password"
                        placeholder="Hasło"
                        className={errors.password ? 'border border-danger bg-danger-subtle' : ''}
                    />
                    {errors.password && <span className="text-danger small mb-2">{errors.password}</span>}

                    <input
                        type="password"
                        name="confirmPassword"
                        placeholder="Powtórz hasło"
                        className={errors.confirmPassword ? 'border border-danger bg-danger-subtle' : ''}
                    />
                    {errors.confirmPassword && <span className="text-danger small mb-2">{errors.confirmPassword}</span>}

                    <button type="submit" className="register-button">Stwórz konto</button>
                </form>

                <p className="login-link mt-3">
                    Masz już konto? <a href="/login" className="bold-link">Zaloguj się</a>
                </p>
            </div>
            <img src={nurseImage} alt="Nurse" className="img-fluid register-image d-none d-lg-block row justify-content-center align-items-center" />
        </div>
    );
};

export default Register;
