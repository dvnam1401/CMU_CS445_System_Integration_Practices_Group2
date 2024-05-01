export class EmployeeFilter {
    isAscending!: boolean | null;
    gender!: string | null;
    category!: string | null;
    ethnicity!: string | null;
    department!: string | null;
    year!: number | null;
    month!: number | null;

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
