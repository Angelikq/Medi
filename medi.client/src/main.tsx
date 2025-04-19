import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import './styles/index.css';
import './styles/Navbar.css';
import './styles/App.css';
import './styles/Login.css';
import './styles/Register.css';
import './styles/Visits.css';

import Navbar from './components/Navbar';
import Home from './pages/App';
import Login from './pages/Login';
import Register from './pages/Register';
import Visits from './pages/Visits';
import PatientProfile from './pages/PatientProfile'; 

ReactDOM.createRoot(document.getElementById('root')!).render(
    <React.StrictMode>
        <BrowserRouter>
            <Navbar />
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/login" element={<Login />} />
                <Route path="/register" element={<Register />} />
                <Route path="/visits" element={<Visits />} />
                <Route path="/profil-pacjenta" element={<PatientProfile />} /> 
            </Routes>
        </BrowserRouter>
    </React.StrictMode>
);
