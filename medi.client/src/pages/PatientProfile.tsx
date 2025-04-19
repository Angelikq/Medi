import React, { useState, useRef } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import "../styles/PatientProfile.css";

import defaultAvatar from "../assets/avatar.png";
import cardIcon from "../assets/pacjentcard.png";
import dataIcon from "../assets/personaldata.png";
import historyIcon from "../assets/medicalhistory.png";
import { Pencil } from "lucide-react";

const PatientProfile: React.FC = () => {
    const [avatar, setAvatar] = useState<string>(defaultAvatar);
    const fileInputRef = useRef<HTMLInputElement>(null);

    const handleAvatarChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const file = event.target.files?.[0];
        if (file) {
            const reader = new FileReader();
            reader.onloadend = () => {
                if (reader.result) {
                    setAvatar(reader.result as string);
                }
            };
            reader.readAsDataURL(file);
        }
    };

    const triggerFileSelect = () => {
        fileInputRef.current?.click();
    };

    return (
        <div className="container py-5 patient-profile-wrapper">
            <div className="profile-header mb-5">
                <div className="profile-inner d-flex flex-column flex-md-row align-items-center gap-4">
                    <div className="avatar-wrapper position-relative">
                        <img src={avatar} alt="Avatar" className="avatar-img" />
                        <div className="edit-icon" onClick={triggerFileSelect}>
                            <Pencil size={20} color="#333" />
                        </div>
                        <input
                            type="file"
                            accept="image/*"
                            ref={fileInputRef}
                            onChange={handleAvatarChange}
                            className="d-none"
                        />
                    </div>
                    <div className="text-wrapper">
                        <h2 className="fw-semibold">Dzień dobry, Adamie!</h2>
                        <p className="text-muted">Jak możemy Ci pomóc?</p>
                    </div>
                </div>
            </div>

            <div className="d-flex flex-column gap-4 align-items-center">
                <button className="profile-button d-flex justify-content-between align-items-center">
                    <div className="d-flex align-items-center gap-3">
                        <img src={cardIcon} alt="Karta pacjenta" className="button-icon" />
                        <span>Karta pacjenta</span>
                    </div>
                    <span className="arrow">→</span>
                </button>

                <button className="profile-button d-flex justify-content-between align-items-center">
                    <div className="d-flex align-items-center gap-3">
                        <img src={dataIcon} alt="Dane osobowe" className="button-icon" />
                        <span>Dane osobowe</span>
                    </div>
                    <span className="arrow">→</span>
                </button>

                <button className="profile-button d-flex justify-content-between align-items-center">
                    <div className="d-flex align-items-center gap-3">
                        <img src={historyIcon} alt="Historia medyczna" className="button-icon" />
                        <span>Historia medyczna</span>
                    </div>
                    <span className="arrow">→</span>
                </button>
            </div>
        </div>
    );
};

export default PatientProfile;
