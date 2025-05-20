import React, { useEffect, useRef, useState } from 'react';
import '../styles/Visits.css';
import doctorImg from '../assets/doctor.png';
import manImg from '../assets/man.png';
import { AppointmentSlotDTO } from '../types/AppointmentSlotDTO';
import Map from '../components/Map';
import Modal from '../components/Modal';
const apiUrl = import.meta.env.VITE_API_URL;


const Visits: React.FC = () => {
    const [specialization, setSpecialization] = useState<string>();
    const visitsListRef = useRef<HTMLDivElement>(null);
    const [showMap, setShowMap] = useState(false);
    const [mapAddress, setMapAddress] = useState('');
    const searchInputRef = useRef<HTMLInputElement>(null);
    const dateInputRef = useRef<HTMLInputElement>(null);
    const [visits, setVisits] = useState<AppointmentSlotDTO[]>([]);
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(false);
    const [suggestions, setSuggestions] = useState<string[]>([]);
    const [query, setQuery] = useState('');
    const [activeSuggestionIndex, setActiveSuggestionIndex] = useState(0);
    const [showSuggestions, setShowSuggestions] = useState(false);

    useEffect(() => {
        if (visits.length > 0) {
            visitsListRef.current?.scrollIntoView({ behavior: 'smooth', block: 'start' });
        }
    }, [visits]);

    const allSpecialists = [
        'Internista', 'Kardiolog', 'Dermatolog',
        'Okulista', 'Ginekolog', 'Stomatolog',
        'Urolog', 'Psycholog', 'Pediatra'
    ];
    useEffect(() => {
        if (specialization) handleSearch();
    }, [specialization])


    const handleSearch = async () => {
        setLoading(true);
        setError('');

        const searchCriteria = searchInputRef.current?.value || '';
        const dateCriteria = dateInputRef.current?.value || '';

        if (!searchCriteria && !dateCriteria && !specialization) {
            setError('Proszę wypełnić przynajmniej jeden filtr: specjalizację, nazwę placówki lub datę.');
            setLoading(false);
            return; 
        }

        try {
            const criteria = specialization ?
                {
                    specialization: specialization
                } : {
                doctorNameOrSpecialization: searchCriteria || null,
                date: dateCriteria ? new Date(dateCriteria).toISOString() : null,
            };
            
            const response = await fetch(`${apiUrl}/api/appointment/search`, {
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
            setSpecialization("")
        }
    };
    const fetchSuggestions = async (input: string) => {
        if (input.length < 3) {
            setSuggestions([]);
            return;
        }

        try {
            const res = await fetch(`${apiUrl}/api/appointment/suggestions?query=${encodeURIComponent(input)}`);
            if (res.ok) {
                const data = await res.json();
                setSuggestions(data);
            }
        } catch (e) {
            console.error("Błąd pobierania sugestii", e);
        }
    };
    const handleLocationClick = async (medicalFacilityId: number) => {
        try {
            const response = await fetch(`${apiUrl}/api/medicalFacilities/simple-address/${medicalFacilityId}`);
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

    const openMap = (address: string) => {
        setMapAddress(address);
        setShowMap(true);
    };
    const closeMap = () => setShowMap(false);


    return (
        <div className="visits-container">
            <div className="content">
                <div className="form-section">
                    <h2>Znajdź lekarza i umów wizytę</h2>
                    {error && <span className="text-danger small mb-2">{error}</span>}
                    <div className="position-relative">
                        <input
                            type="text"
                            placeholder="Wpisz specjalizację lub nazwisko lekarza"
                            className="search-input"
                            value={query}
                            onChange={(e) => {
                                const val = e.target.value;
                                setQuery(val);
                                fetchSuggestions(val);
                                setActiveSuggestionIndex(0);
                                setShowSuggestions(true);
                            }}
                            onKeyDown={(e) => {
                                if (e.key === 'ArrowDown') {
                                    setActiveSuggestionIndex((prev) =>
                                        prev < suggestions.length - 1 ? prev + 1 : prev
                                    );
                                } else if (e.key === 'ArrowUp') {
                                    setActiveSuggestionIndex((prev) =>
                                        prev > 0 ? prev - 1 : prev
                                    );
                                } else if (e.key === 'Enter') {
                                    if (activeSuggestionIndex >= 0) {
                                        const selected = suggestions[activeSuggestionIndex];
                                        setQuery(selected);
                                        searchInputRef.current!.value = selected;
                                        setSuggestions([]);
                                        setShowSuggestions(false);
                                    }
                                }
                            }}
                            onBlur={() => setTimeout(() => {
                                setShowSuggestions(false);
                            }, 200)}
                            ref={searchInputRef}
                        />
                        {showSuggestions && suggestions.length > 0 && (
                            <ul className="list-group position-absolute w-100 z-3" style={{ maxHeight: '200px', overflowY: 'auto' }}>
                                {suggestions.map((item, index) => (
                                    <li
                                        key={index}
                                        className={`list-group-item list-group-item-action}`}
                                        style={{
                                            backgroundColor: index === activeSuggestionIndex ? '#D7F0F3' : 'white',
                                        }}
                                        role="button"
                                        onClick={() => {
                                            setQuery(item);
                                            searchInputRef.current!.value = item;
                                            setSuggestions([]);
                                            setShowSuggestions(false);
                                        }}
                                    >
                                        {item}
                                    </li>
                                ))}
                            </ul>
                        )}
                    </div>
                    <h3 className="date-label mt-3">Termin</h3>
                    <input
                        type="date"
                        className="date-input"
                        ref={dateInputRef}
                    />
                    <button className="d-block mb-5 show-doctors" onClick={handleSearch}>Pokaż dostępnych lekarzy</button>
                    <div className="specializations-grid">
                        {allSpecialists.map((spec) => (
                            <button
                                key={spec}
                                className="spec-button"
                                onClick={() => setSpecialization(spec)}
                            >
                                {spec}
                            </button>
                        ))}
                    </div>
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
