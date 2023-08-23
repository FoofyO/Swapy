import { ProductEditRequest } from "./product-edit-request.interface";

export interface RealEstateEditRequest extends ProductEditRequest {
    Area: number;
    Rooms: number;
    IsRent: boolean;
    RealEstateTypeId: string;
}