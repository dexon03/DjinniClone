export interface Profile {
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
    isActive : boolean;
}