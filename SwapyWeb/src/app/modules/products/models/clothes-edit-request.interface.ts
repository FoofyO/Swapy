import { ProductEditRequest } from "./product-edit-request.interface";

export interface ClothesEditRequest extends ProductEditRequest {
    IsNew: boolean;
    ClothesSeasonId: string;
    ClothesSizeId: string;
    ClothesBrandViewId: string;
}