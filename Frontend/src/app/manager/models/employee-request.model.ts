export interface EmployeeResponse {
    idEmployee: number,
    employmentCode: number,
    firstName: string,
    lastName: string,
    ssn: number,
    employmentStatus: string,
    hireDateForWorking: Date,
    numberDaysRequirementOfWorkingPerMonth: number,
    department: string,
    payRate: string,
    payRatesIdPayRates: number
}