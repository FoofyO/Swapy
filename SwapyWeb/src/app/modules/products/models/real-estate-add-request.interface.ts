import { ProductAddRequest } from "./product-add-request.interface";

export interface RealEstateAddRequest extends ProductAddRequest {
    Area: number;
    Rooms: number;
    IsRent: boolean;
    RealEstateTypeId: string;
}