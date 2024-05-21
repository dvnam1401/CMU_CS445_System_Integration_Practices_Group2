import { Groups } from "./group.model";

export interface UserUpdate {
    userId: number;
    userName: string;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    isActive: boolean;
    department: string;
    groups: Groups[];
}
