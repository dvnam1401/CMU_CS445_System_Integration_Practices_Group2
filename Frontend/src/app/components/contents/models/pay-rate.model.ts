export interface PayRate {
    idPayRates: number; // Sửa đổi từ payRateId sang idPayRates để khớp với C#
    payRateName: string;
    value: number;
    taxPercentage: number;
    payType: number;
    payAmount: number;
}