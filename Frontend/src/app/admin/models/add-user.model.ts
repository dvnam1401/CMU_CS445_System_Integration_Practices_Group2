export interface AddUser {
    firstName: string;
    lastName: string;
    userName: string;
    email: string;
    password: string;
    phoneNumber: string;   
    department: string;
    isActive: boolean;
    groupIds: number[];
}