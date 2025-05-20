import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle.min.js';
import 'react-big-calendar/lib/css/react-big-calendar.css';


import './styles/index.css';


import Navbar from './components/Navbar';
import Home from './pages/Home';
import Login from './pages/Login';
import Planned from './pages/Planned';
import Register from './pages/Register';
import Visits from './pages/Visits';
import PatientProfile from './pages/PatientProfile'; 
import ProtectedRoute from './components/ProtectedRoute';
import { AuthProvider } from './components/AuthContext';

ReactDOM.createRoot(document.getElementById('root')!).render(
    <React.StrictMode>
        <AuthProvider>
            <BrowserRouter>
                <Navbar />
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/login" element={<Login />} />
                    <Route path="/register" element={<Register />} />
                    <Route path="/planned" element={<Planned />} />
                    <Route path="/visits" element={<ProtectedRoute><Visits /></ProtectedRoute>} />
                    <Route path="/profile" element={<ProtectedRoute><PatientProfile /></ProtectedRoute>} /> 
                </Routes>
            </BrowserRouter>
        </AuthProvider>
    </React.StrictMode>
);
