import { ProductAddRequest } from "./product-add-request.interface";

export interface TvAddRequest extends ProductAddRequest {
    IsNew: boolean;
    IsSmart: boolean;
    TvTypeId: string;
    TvBrandId: string;
    ScreenResolutionId: string;
    ScreenDiagonalId: string;
}