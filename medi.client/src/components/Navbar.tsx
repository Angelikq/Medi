import React from 'react';
import { Link } from 'react-router-dom';
import logo from '../assets/logo.png';
import '../styles/Navbar.css';
import { useAuth } from './AuthContext';

const Navbar: React.FC = () => { 
    const { isAuthenticated, setIsAuthenticated } = useAuth();

    const logout = () => {
        localStorage.removeItem('token');
        setIsAuthenticated(false);
    };
    return (
        <nav className="navbar navbar-expand-lg bg-white px-2">
            <div className="container-fluid">
                <Link className="navbar-brand d-flex align-items-center" to="/">
                    <img src={logo} alt="Logo" height={80} className="me-3" />
                    <span className="fw-semibold fs-4 text-dark ms-2">Medica</span>
                </Link>
                <button
                    className="navbar-toggler"
                    type="button"
                    data-bs-toggle="collapse"
                    data-bs-target="#navbarContent"
                    aria-controls="navbarContent"
                    aria-expanded="false"
                    aria-label="Toggle navigation"
                >
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse justify-content-end" id="navbarContent">
                    <ul className="navbar-nav gap-3">
                        {!isAuthenticated ? (
                            <>
                                <li className="nav-item">
                                    <Link className="nav-link text-dark fw-medium" to="/register">
                                        Zarejestruj się
                                    </Link>
                                </li>
                                <li className="nav-item">
                                    <Link className="nav-link text-dark fw-medium" to="/login">
                                        Zaloguj się
                                    </Link>
                                </li>
                            </>
                        ) : (
                             <>
                                <li className="nav-item">
                                    <Link className="nav-link text-dark fw-medium" to="/profil-pacjenta">
                                        Profil pacjenta
                                    </Link>
                                </li>
                                <li className="nav-item">
                                    <Link className="nav-link text-dark fw-medium" to="/visits">
                                        Moje wizyty
                                    </Link>
                                    </li>
                                <li className="nav-item">
                                    <Link className="nav-link text-dark fw-medium" to="/kontakt">
                                        Kontakt
                                    </Link>
                                    </li>
                                <li className="nav-item">
                                    <Link onClick={logout} className="nav-link text-dark fw-medium" to="/">
                                        Wyloguj się
                                    </Link>
                                </li>
                            </>
                        )}
                    </ul>
                </div>
            </div>
        </nav>
    );
}

export default Navbar;
