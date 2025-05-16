import React, { useEffect, useRef, useState } from 'react';
import '../styles/Visits.css';
import doctorImg from '../assets/doctor.png';
import manImg from '../assets/man.png';
import { AppointmentSlotDTO } from '../types/AppointmentSlotDTO';
import Map from '../components/Map';
import Modal from '../components/Modal';

const Visits: React.FC = () => {
    const [selectedSpecialists, setSelectedSpecialists] = useState<string[]>([]);
  //  const [selectedDate, setSelectedDate] = useState<string>('');
    const visitsListRef = useRef<HTMLDivElement>(null);
    const [showMap, setShowMap] = useState(false);
    const [mapAddress, setMapAddress] = useState('');

    const openMap = (address: string) => {
        setMapAddress(address);
        setShowMap(true);
    };
    const closeMap = () => setShowMap(false);

    const allSpecialists = [
        'Internista', 'Kardiolog', 'Dermatolog',
        'Okulista', 'Ginekolog', 'Stomatolog',
        'Urolog', 'Psycholog', 'Pediatra'
    ];

    const searchInputRef = useRef<HTMLInputElement>(null);
    const dateInputRef = useRef<HTMLInputElement>(null);
    const selectedSpecialistsRef = useRef<Set<string>>(new Set());

    const toggleSpecialist = (spec: string) => {
        const current = selectedSpecialistsRef.current;
        if (current.has(spec)) {
            current.delete(spec);
        } else {
            current.add(spec);
        }
    };

    const [visits, setVisits] = useState<AppointmentSlotDTO[]>([]);
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        if (visits.length > 0) {
            visitsListRef.current?.scrollIntoView({ behavior: 'smooth', block: 'start' });
        }
    }, [visits]);
    const handleLocationClick = async (medicalFacilityId: number) => {
        try {
            const response = await fetch(`https://localhost:7061/api/medicalFacilities/simple-address/${medicalFacilityId}`);
            if (!response.ok) {
                console.error("Nie udało się pobrać adresu");
                return;
            }

            const address = await response.text();
            openMap(address);
        } catch (error) {
            console.error("Błąd podczas pobierania adresu", error);
        }
    };
    const handleSearch = async () => {
        setLoading(true);
        setError('');

        const searchCriteria = searchInputRef.current?.value || '';
        const dateCriteria = dateInputRef.current?.value || '';
        const hasSpecialists = selectedSpecialistsRef.current.size > 0;

        if (!searchCriteria && !dateCriteria && !hasSpecialists) {
            setError('Proszę wypełnić przynajmniej jeden filtr: specjalizację, nazwę placówki lub datę.');
            setLoading(false);
            return; 
        }

        try {
            const criteria = {
                facilityNameOrSpecialization: searchCriteria || null,
                date: dateCriteria ? new Date(dateCriteria).toISOString() : null,
                specializationsClicked: Array.from(selectedSpecialistsRef.current),
            };

            const response = await fetch(`https://localhost:7061/api/appointment/search`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(criteria),
            });

            if (!response.ok) {
                const err = await response.text();
                setError(err);
                setVisits([]);
            } else {
                const data = await response.json();
                setVisits(data);
            }
        } catch {
            setError('Błąd połączenia z serwerem.');
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="visits-container">
            <div className="content">
                <div className="form-section">
                    <h2>Znajdź lekarza i umów wizytę</h2>
                    {error && <span className="text-danger small mb-2">{error}</span>}
                    <input
                        type="text"
                        placeholder="Wpisz specjalizację lub nazwę placówki"
                        className="search-input"
                        ref={searchInputRef}
                    />

                    <h3 className="date-label">Termin</h3>
                    <input
                        type="date"
                        className="date-input"
                        ref={dateInputRef}
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

                    <button className="show-doctors" onClick={handleSearch}>Pokaż dostępnych lekarzy</button>
                </div>

                <div className="image-section">
                    <img src={doctorImg} alt="Lekarz" />
                </div>
            </div>

            <div className="visits-list" ref={visitsListRef}>
                {loading && <p>Ładowanie wizyt...</p>}
                {error && <p className="text-danger">{error}</p>}
                {!loading && visits.length === 0 && <p>Brak wyników.</p>}
                {visits.map((visit, index) => (
                    <div className="visit-box" key={index}>
                        <p className="visit-date">
                            {new Date(visit.startTime).toLocaleString('pl-PL')}
                        </p>
                        <button className="book-button">Umów</button>
                        <p className="doctor-name">{visit.doctorFullName}</p>
                        <p className="specialty">{visit.specialization}</p>
                        <div className="clinic-location-row">
                            <p className="clinic">{visit.medicalFacilityName}</p>
                            <p className="location"               
                                onClick={() => handleLocationClick(visit.medicalFacilityId)}
                            >Lokalizacja</p>
                        </div>
                    </div>

                ))}

            </div>

            <div className="man-image">
                <img src={manImg} alt="Pacjent" />
            </div>

            {showMap && (
                <Modal onClose={closeMap} modalTitle="Lokalizacja">
                    <Map address={mapAddress} />
                </Modal>
            )}
        </div>
    );
};

export default Visits;
