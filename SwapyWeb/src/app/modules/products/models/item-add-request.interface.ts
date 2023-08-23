import { ProductAddRequest } from "./product-add-request.interface";

export interface ItemAddRequest extends ProductAddRequest {
    IsNew: boolean;
    ItemTypeId: string;
}