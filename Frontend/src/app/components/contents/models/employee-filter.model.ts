export class EmployeeFilter {
    isAscending: boolean | null = null;
    gender: string | null = null;
    category: string | null = null;
    ethnicity: string | null = null;
    department: string | null = null;
    year: number | null = null;
    month: number | null = null;

    // constructor(init?: {
    //     isAscending?: boolean;
    //     gender?: string;
    //     category?: string;
    //     ethnicity?: string;
    //     department?: string;
    //     year?: number;
    //     month?: number;
    // }) {
    //     // No need for Object.assign
    //     if (init) {
    //         this.isAscending = init.isAscending ?? null;
    //         this.gender = init.gender ?? null;
    //         this.category = init.category ?? null;
    //         this.ethnicity = init.ethnicity ?? null;
    //         this.department = init.department ?? null;
    //         this.year = init.year ?? null;
    //         this.month = init.month ?? null;
    //     }
    // }
}
