import { Experience } from "./experience.enum";
import { Profile } from "./profile";

export interface CandidateProfile extends Profile {
    experience: Experience;
    desiredSalary : number;
    skills : any;
    locations: any;
}