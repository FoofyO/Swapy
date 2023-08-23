import { ProductEditRequest } from "./product-edit-request.interface";

export interface ItemEditRequest extends ProductEditRequest {
    IsNew: boolean;
    ItemTypeId: string;
}