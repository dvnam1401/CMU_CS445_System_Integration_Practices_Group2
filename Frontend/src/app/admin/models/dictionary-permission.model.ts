import { GroupPermission } from "./group-permission.model";

export interface GroupPermissions {
    [key: string]: GroupPermission[]; // Dictionary of group names to arrays of permissions
}