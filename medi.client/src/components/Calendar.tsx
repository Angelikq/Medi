import React, { useState } from 'react';
import { FaCaretLeft, FaCaretRight, FaTimes } from 'react-icons/fa';
import { useIsMobile } from '../hooks/useIsMobile'; 

const getWeekDates = (startDate: Date) => {
    const dates = [];
    const current = new Date(startDate);
    const dayOfWeek = current.getDay();
    const diffToMonday = dayOfWeek === 0 ? -6 : 1 - dayOfWeek;
    current.setDate(current.getDate() + diffToMonday);
    for (let i = 0; i < 7; i++) {
        const day = new Date(current);
        day.setDate(current.getDate() + i);
        dates.push(day);
    }
    return dates;
};

const Calendar: React.FC = () => {
    const [currentWeekStart, setCurrentWeekStart] = useState(new Date());
    const [selectedTime, setSelectedTime] = useState<string | null>(null);
    const [expanded, setExpanded] = useState(false);
    const [mobileOpen, setMobileOpen] = useState(false);
    const isMobile = useIsMobile();

    const availableTimes = [
        '08:00', '08:30', '09:00', '09:30', '10:00', '10:30', '11:00', '11:30',
        '12:00', '12:30', '13:00', '13:30', '14:00'
    ];

    const reservedTimes = [
        { date: new Date(2025, 4, 8, 10, 0), time: '10:00' },
        { date: new Date(2025, 4, 9, 14, 30), time: '14:30' },
    ];

    const handleSelectTime = (time: string, date: Date) => {
        setSelectedTime(`${time} ${date.toLocaleDateString()}`);
        alert(`Wybrano termin: ${time} ${date.toLocaleDateString()}`);
    };

    const handleNextWeek = () => {
        const nextWeekStart = new Date(currentWeekStart);
        nextWeekStart.setDate(currentWeekStart.getDate() + 7);
        setCurrentWeekStart(nextWeekStart);
    };

    const handlePrevWeek = () => {
        const prevWeekStart = new Date(currentWeekStart);
        prevWeekStart.setDate(currentWeekStart.getDate() - 7);
        setCurrentWeekStart(prevWeekStart);
    };

    const isReserved = (date: Date, time: string) => {
        return reservedTimes.some((reservation) =>
            reservation.date.toLocaleDateString() === date.toLocaleDateString() && reservation.time === time
        );
    };

    const weekDates = getWeekDates(currentWeekStart);
    const visibleDates = isMobile ? weekDates.slice(0, 3) : weekDates;

    const calendarContent = (
        <>
            <div className="d-flex justify-content-between mb-4">
                <button className={`btn btn-primary rounded-4 ps-2 pe-3 ${isMobile ? " custom-small-font py-2 " : "fs-6 py-1"}`} onClick={handlePrevWeek}>
                    <FaCaretLeft className="me-1" />
                    Poprzedni
                </button>
                <h5 className="text-center mb-0" style={{ fontSize: '1rem' }}>
                    {weekDates[0].toLocaleDateString('pl-PL', { day: 'numeric', month: 'numeric' })} -{' '}
                    {weekDates[6].toLocaleDateString('pl-PL', { day: 'numeric', month: 'numeric', year: 'numeric' })}
                </h5>
                <button className={`btn btn-primary rounded-4 ps-3 pe-2 ${isMobile ? "custom-small-font py-2 " : "fs-6 py-1"}`} onClick={handleNextWeek}>
                    Następny
                    <FaCaretRight className="ms-1" />
                </button>
            </div>

            <div className="position-relative overflow-hidden shadow-sm">
                <div className={`table-scroll-wrapper ${expanded ? 'expanded' : ''}`}>
                    <div className="table-responsive">
                        <table className="table table-sm">
                            <thead>
                                <tr>
                                    {visibleDates.map((date, index) => (
                                        <th key={index} className="text-center">
                                            <div>{date.toLocaleDateString('pl-PL', { weekday: 'short' }).toUpperCase()}</div>
                                            <div>{date.toLocaleDateString('pl-PL', { day: '2-digit', month: '2-digit' })}</div>
                                        </th>
                                    ))}
                                </tr>
                            </thead>
                            <tbody>
                                {availableTimes.map((time, timeIndex) => (
                                    <tr key={timeIndex}>
                                        {visibleDates.map((date, dateIndex) => {
                                            const isTimeReserved = isReserved(date, time);
                                            return (
                                                <td key={dateIndex} className="text-center">
                                                    <button
                                                        className={`btn ${isTimeReserved ? 'btn-outline-secondary text-muted' : 'btn-outline-primary'} ${isMobile ? "px-4 py-2" : "px-2 py-1"}`}
                                                        onClick={() => !isTimeReserved && handleSelectTime(time, date)}
                                                        disabled={isTimeReserved}
                                                        style={{
                                                            textDecoration: isTimeReserved ? 'line-through' : 'none',
                                                            fontSize: '0.9rem'
                                                        }}
                                                    >
                                                        {time}
                                                    </button>
                                                </td>
                                            );
                                        })}
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                </div>

                {!expanded && (
                    <div
                        className="gradient-overlay position-absolute bottom-0 start-0 w-100"
                        style={{ height: '80px', background: 'linear-gradient(to bottom, transparent, #f8f9fa)' }}
                    />
                )}
            </div>

            <div className="text-center mt-3">
                <button
                    className="btn btn-link text-dark text-decoration-none fw-semibold"
                    onClick={() => setExpanded(!expanded)}
                    style={{ fontSize: '0.95rem' }}
                >
                    {expanded ? 'Ukryj ↑' : 'Pokaż więcej ↓'}
                </button>
            </div>
        </>
    );

    return (
        <>
            {isMobile ? (
                <>
                    {!mobileOpen && (
                        <div className="text-center mt-3">
                            <button
                                className="btn btn-primary btn-sm"
                                onClick={() => setMobileOpen(true)}
                            >
                                Otwórz kalendarz
                            </button>
                        </div>
                    )}
                    {mobileOpen && (
                        <div className="position-fixed top-0 start-0 w-100 h-100 bg-white p-3 overflow-auto" style={{ zIndex: 1050 }}>
                            <div className="text-end">
                                <button className="btn btn-sm" onClick={() => setMobileOpen(false)}>
                                    <FaTimes />
                                </button>
                            </div>
                            {calendarContent}
                        </div>
                    )}
                </>
            ) : (
                <div className="calendar container overflow-hidden border rounded p-4" style={{ maxWidth: '520px' }}>
                    {calendarContent}
                </div>
            )}
        </>
    );
};

export default Calendar;
