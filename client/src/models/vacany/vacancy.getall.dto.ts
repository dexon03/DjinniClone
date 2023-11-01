import { AttendanceMode } from "./attendance.enum.ts";
import { Experience } from "./experience.enum.ts";
import { LocationDto } from "./location.dto.ts";

export interface VacancyGetAll {
    id: string;
    title: string;
    positionTitle: string;
    description: string;
    salary: number;
    experience: Experience;
    attendance: AttendanceMode;
    createdAt: Date;
    companyName: string;
    locations: LocationDto[];
}