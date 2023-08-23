export interface ProductAddRequest {
    Title: string;
    Description: string;
    Price: number;
    CurrencyId: string;
    CategoryId: string;
    SubcategoryId: string;
    CityId: string;
    Files: FormData;
}
  