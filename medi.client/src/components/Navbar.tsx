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
        window.location.href = '/'
    };

    return (
        <>
            <nav className="navbar navbar-expand-lg bg-white px-2 shadow-sm">
                <div className="container-fluid">
                    <Link className="navbar-brand d-flex align-items-center" to="/">
                        <img src={logo} alt="Logo" height={70} className="me-3" />
                        <span className="fw-semibold fs-4 text-dark ms-2">Medica</span>
                    </Link>

                    <button
                        className="navbar-toggler d-lg-none"
                        type="button"
                        data-bs-toggle="offcanvas"
                        data-bs-target="#offcanvasNavbar"
                        aria-controls="offcanvasNavbar"
                    >
                        <span className="navbar-toggler-icon"></span>
                    </button>

                    <div className="collapse navbar-collapse justify-content-end" id="navbarContent">
                        <ul className="navbar-nav gap-3">
                            {!isAuthenticated ? (
                                <>
                                    <li className="nav-item">
                                        <Link className="nav-link text-dark fw-medium" to="/register">Zarejestruj się</Link>
                                    </li>
                                    <li className="nav-item">
                                        <Link className="nav-link text-dark fw-medium" to="/login">Zaloguj się</Link>
                                    </li>
                                </>
                            ) : (
                                <>
                                    <li className="nav-item">
                                        <Link className="nav-link text-dark fw-medium" to="/profile">Profil pacjenta</Link>
                                    </li>
                                    <li className="nav-item">
                                        <Link className="nav-link text-dark fw-medium" to="/visits">Moje wizyty</Link>
                                    </li>
                                    <li className="nav-item">
                                        <Link className="nav-link text-dark fw-medium" to="/contact">Kontakt</Link>
                                    </li>
                                    <li className="nav-item">
                                        <Link onClick={logout} className="nav-link text-dark fw-medium" to="/">Wyloguj się</Link>
                                    </li>
                                </>
                            )}
                        </ul>
                    </div>
                </div>
            </nav>

            <div className="offcanvas offcanvas-end d-lg-none" tabIndex={-1} id="offcanvasNavbar" aria-labelledby="offcanvasNavbarLabel">
                <div className="offcanvas-header">
                    <h5 className="offcanvas-title" id="offcanvasNavbarLabel">Menu</h5>
                    <button type="button" className="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>
                </div>
                <div className="offcanvas-body">
                    <ul className="navbar-nav">
                        {!isAuthenticated ? (
                            <>
                                <li className="nav-item">
                                    <Link className="nav-link" to="/register" data-bs-dismiss="offcanvas" onClick={() => window.location.href = '/register'}>Zarejestruj się</Link>
                                </li>
                                <li className="nav-item">
                                    <Link className="nav-link" to="/login" data-bs-dismiss="offcanvas" onClick={() => window.location.href = '/login'}>Zaloguj się</Link>
                                </li>
                            </>
                        ) : (
                            <>
                                <li className="nav-item">
                                        <Link className="nav-link" to="/profile" data-bs-dismiss="offcanvas" onClick={() => window.location.href = '/profile'}>Profil pacjenta</Link>
                                </li>
                                <li className="nav-item">
                                        <Link className="nav-link" to="/visits" data-bs-dismiss="offcanvas" onClick={() => window.location.href = '/visits'}>Moje wizyty</Link>
                                </li>
                                    <li className="nav-item">
                                        <Link className="nav-link" to="/contact" data-bs-dismiss="offcanvas" onClick={() => window.location.href = '/contact'}>Kontakt</Link>
                                </li>
                                <li className="nav-item">
                                        <Link className="nav-link" to="/" onClick={logout} data-bs-dismiss="offcanvas">Wyloguj się</Link>
                                </li>
                            </>
                        )}
                    </ul>
                </div>
            </div>
        </>
    );
};

export default Navbar;
