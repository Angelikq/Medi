export interface AppointmentSlotDTO {
    id: number;
    startTime: string;
    doctorFullName: string;
    specialization: string;
    doctorId: number;
    medicalFacilityName: string;
    medicalFacilityId: number;
}
