import React, { useEffect, useState } from 'react';
import { MapContainer, TileLayer, Marker, Popup } from 'react-leaflet';
import 'leaflet/dist/leaflet.css';

type Props = {
    address: string;
};

const Map: React.FC<Props> = ({ address }) => {
    const [coords, setCoords] = useState<{ lat: number; lon: number } | null>(null);

    useEffect(() => {
        const fetchCoordinates = async () => {
            try {
                const response = await fetch(
                    `https://nominatim.openstreetmap.org/search?q=${encodeURIComponent(
                        address
                    )}&format=json`,
                    {
                        headers: {
                            'User-Agent': 'medivisit-demo/1.0' 
                        }
                    }
                );
                const data = await response.json();
                if (data.length > 0) {
                    setCoords({ lat: parseFloat(data[0].lat), lon: parseFloat(data[0].lon) });
                }
            } catch (err) {
                console.error('Błąd geokodowania:', err);
            }
        };

        fetchCoordinates();
    }, [address]);

    if (!coords) return <p>Ładowanie mapy...</p>;

    return (
        <MapContainer center={[coords.lat, coords.lon]} zoom={15} style={{ height: '300px', width: '300px', marginTop: '10px' }}>
            <TileLayer
                attribution='&copy; OpenStreetMap contributors'
                url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
            />
            <Marker position={[coords.lat, coords.lon]}>
                <Popup>{address}</Popup>
            </Marker>
        </MapContainer>
    );
};

export default Map;
