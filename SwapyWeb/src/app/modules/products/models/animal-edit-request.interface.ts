import { ProductEditRequest } from "./product-edit-request.interface";

export interface AnimalEditRequest extends ProductEditRequest {
    AnimalBreedId: string;
}