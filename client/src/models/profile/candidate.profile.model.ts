import { Experience } from "./experience.enum";

export interface CandidateProfile {
    id : string;
    name? : string;
    surname? : string;
    email? : string;
    phoneNumber? : string;
    dateOfBirth? : Date;
    description: string;
    imageUrl? : string;
    gitHubUrl? : string;
    linkedInUrl? : string;
    positionTitle? : string;
    workExperience : Experience;
    isActive : boolean;
}