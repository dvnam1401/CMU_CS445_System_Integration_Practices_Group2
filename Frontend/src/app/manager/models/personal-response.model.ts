import { BenefitPlanResponse } from './benefit-request.model';

export class PersonalResponse {
    personalId: number;
    currentFirstName: string;
    currentLastName: string;
    currentMiddleName: string;
    birthDate: Date;
    currentCity: string;
    socialSecurityNumber: string;
    driversLicense: string;
    currentAddress1: string;
    currentZip: number;
    currentMaritalStatus: string;
    shareholderStatus: number;
    currentCountry: string;
    currentAddress2: string;
    currentGender: string;
    currentPhoneNumber: string;
    currentPersonalEmail: string
    ethnicity: string;
    benefitPlanName: string;    
}