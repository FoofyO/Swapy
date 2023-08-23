import { ProductAddRequest } from "./product-add-request.interface";

export interface ElectronicsAddRequest extends ProductAddRequest {
    IsNew: boolean;
    MemoryModelId: string;
    ModelColorId: string;
}