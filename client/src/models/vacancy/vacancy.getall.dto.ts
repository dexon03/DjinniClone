import { LocationDto } from "../common/location.dto.ts";

export interface VacancyGet {
    id: string;
    title: string;
    positionTitle: string;
    description: string;
    salary: number;
    experience: string;
    attendanceMode: string;
    createdAt: Date;
    companyName: string;
    locations: LocationDto[];
}