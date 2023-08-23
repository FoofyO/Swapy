import { ProductAddRequest } from "./product-add-request.interface";

export interface AutoAddRequest extends ProductAddRequest {
    Miliage: number;
    EngineCapacity: number;
    ReleaseYear: Date;
    IsNew: boolean;
    FuelTypeId: string;
    AutoColorId: string;
    TransmissionTypeId: string;
    AutoModelId: string;
}