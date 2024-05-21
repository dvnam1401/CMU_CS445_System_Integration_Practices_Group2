export interface UserRequest {
    userId: number;
    fullName: string;
    userName: string;
    email: string;
    phoneNumber: string;
    isActive: boolean;
    gender: string;
    department: string;
    userNameGroups: string[];
}
