import React, { useState } from 'react';
import '../styles/Visits.css';
import doctorImg from '../assets/doctor.png';
import manImg from '../assets/man.png';
import Button from '../components/Button';

const Visits: React.FC = () => {
    const [selectedSpecialists, setSelectedSpecialists] = useState<string[]>([]);
    const [selectedDate, setSelectedDate] = useState<string>('');

    const allSpecialists = [
        'Internista', 'Kardiolog', 'Dermatolog',
        'Okulista', 'Ginekolog', 'Stomatolog',
        'Urolog', 'Psycholog', 'Pediatra'
    ];

    const toggleSpecialist = (spec: string) => {
        setSelectedSpecialists((prev) =>
            prev.includes(spec) ? prev.filter((s) => s !== spec) : [...prev, spec]
        );
    };

    return (
        <div className="visits-container">
            <div className="content">
                <div className="form-section">
                    <h2>Znajdź lekarza i umów wizytę</h2>

                    <input
                        type="text"
                        placeholder="Wpisz specjalizację lub nazwę placówki"
                        className="search-input"
                    />

                    <h3 className="date-label">Termin</h3>
                    <input
                        type="date"
                        className="date-input"
                        value={selectedDate}
                        onChange={(e) => setSelectedDate(e.target.value)}
                    />

                    <div className="specializations-grid">
                        {allSpecialists.map((spec) => (
                            <button
                                key={spec}
                                className={`spec-button ${selectedSpecialists.includes(spec) ? 'selected' : ''}`}
                                onClick={() => toggleSpecialist(spec)}
                            >
                                {spec}
                            </button>
                        ))}
                    </div>

                    <button className="show-doctors">Pokaż dostępnych lekarzy</button>
                </div>

                <div className="image-section">
                    <img src={doctorImg} alt="Lekarz" />
                </div>
            </div>

            <div className="visits-list">
                <div className="visit-box">
                    <p className="visit-date">15 sierpnia 2024, 10:00</p>
                    <button className="book-button">Umów</button>
                    <p className="doctor-name">dr med. Jan Kowalski</p>
                    <p className="specialty">Kardiolog</p>
                    <div className="clinic-location-row">
                        <p className="clinic">Przychodnia "Biegunowa"</p>
                        <p className="location">Lokalizacja</p>
                    </div>
                </div>

                <div className="visit-box">
                    <p className="visit-date">25 września 2024, 10:00</p>
                    <button className="book-button">Umów</button>
                    <p className="doctor-name">dr med. Joanna Niksa</p>
                    <p className="specialty">Internista</p>
                    <div className="clinic-location-row">
                        <p className="clinic">Przychodnia "Tęcza"</p>
                        <p className="location">Lokalizacja</p>
                    </div>
                </div>
            </div>

            <div className="man-image">
                <img src={manImg} alt="Pacjent" />
            </div>
        </div>
    );
};

export default Visits;
