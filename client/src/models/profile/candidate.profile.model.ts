import { LocationDto } from "../common/location.dto";
import { SkillDto } from "../common/skill.dto";
import { Experience } from "./experience.enum";
import { Profile } from "./profile";

export interface CandidateProfile extends Profile {
    workExperience: Experience;
    desiredSalary: number;
    skills?: SkillDto[];
    locations?: LocationDto[];
}