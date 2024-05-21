export interface UserResponse {
    userId: number;
    userName: string;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    isActive: boolean;
    department: string;
    groupIds: number[];
}