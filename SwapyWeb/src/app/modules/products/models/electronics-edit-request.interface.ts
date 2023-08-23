import { ProductEditRequest } from "./product-edit-request.interface";

export interface ElectronicsEditRequest extends ProductEditRequest {
    IsNew: boolean;
    MemoryModelId: string;
    ModelColorId: string;
}