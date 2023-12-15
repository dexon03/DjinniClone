import { AttendanceMode } from "../common/attendance.enum";
import { LocationDto } from "../common/location.dto";
import { SkillDto } from "../common/skill.dto";
import { Experience } from "./experience.enum";

export interface VacancyUpdateModel {
    id: string;
    categoryId: string;
    title: string;
    positionTitle: string;
    description: string;
    salary: number;
    experience: Experience;
    attendanceMode: AttendanceMode;
    locations: LocationDto[];
    skills: SkillDto[];
}