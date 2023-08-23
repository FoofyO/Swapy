export interface ProductEditRequest {
    ProductId: string;
    Title: string;
    Description: string;
    Price: number | null;
    CurrencyId: string;
    CategoryId: string;
    SubcategoryId: string;
    CityId: string;
    OldPaths: string[];
    NewFiles: FormData;
}
  