import { ProductAddRequest } from "./product-add-request.interface";

export interface AnimalAddRequest extends ProductAddRequest {
    AnimalBreedId: string;
}