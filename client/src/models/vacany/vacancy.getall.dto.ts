import {LocationDto} from "./location.dto.ts";

export interface VacancyGetAll {
    id: string;
    title: string;
    position: string;
    description: string;
    salary: number;
    experience: string;
    attendance: string;
    createdAt : Date;
    companyName: string;
    locations: LocationDto[];
}