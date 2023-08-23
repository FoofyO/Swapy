import { ProductEditRequest } from "./product-edit-request.interface";

export interface AutoEditRequest extends ProductEditRequest {
    Miliage: number;
    EngineCapacity: number;
    ReleaseYear: Date;
    IsNew: boolean;
    FuelTypeId: string;
    AutoColorId: string;
    TransmissionTypeId: string;
    AutoModelId: string;
}