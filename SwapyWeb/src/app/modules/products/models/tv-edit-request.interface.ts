import { ProductEditRequest } from "./product-edit-request.interface";

export interface TvEditRequest extends ProductEditRequest {
    IsNew: boolean;
    IsSmart: boolean;
    TvTypeId: string;
    TvBrandId: string;
    ScreenResolutionId: string;
    ScreenDiagonalId: string;
}