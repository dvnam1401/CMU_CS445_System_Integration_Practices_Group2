export interface AddEmployment {
    personalId: number;
    benefitPlanId: number;
    employmentId: number;
    lastName: string | null;  // Cho phép null nếu trong C# là nullable
    firstName: string | null;  // Cho phép null nếu trong C# là nullable
    payRatesIdPayRates: number;
    phoneNumber: string | null;  // Cho phép null nếu trong C# là nullable
    ssn: number;  // Kiểu number có thể bao gồm decimal trong TypeScript
    shareholderStatus: number | null;  // Thêm nullable phù hợp với short? trong C#
    email: string | null;  // Cho phép null nếu trong C# là nullable
    address: string | null;  // Cho phép null nếu trong C# là nullable
    gender: string | null;  // Cho phép null nếu trong C# là nullable
    department: string | null;  // Cho phép null nếu trong C# là nullable
}
