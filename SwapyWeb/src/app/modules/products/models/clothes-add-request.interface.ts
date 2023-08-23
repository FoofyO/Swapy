import { ProductAddRequest } from "./product-add-request.interface";

export interface ClothesAddRequest extends ProductAddRequest {
    IsNew: boolean;
    ClothesSeasonId: string;
    ClothesSizeId: string;
    ClothesBrandViewId: string;
}