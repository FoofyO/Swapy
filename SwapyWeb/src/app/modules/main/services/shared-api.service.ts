import { Injectable } from '@angular/core';
import axios, { AxiosError, AxiosResponse } from 'axios';
import { EMPTY, Observable, catchError, from, map, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PageResponse } from 'src/app/core/models/page-response.interface';
import { Product } from 'src/app/modules/products/models/product.model';
import { Specification } from 'src/app/core/models/specification';
import { Shop } from '../../shops/models/shop.model';
import { CategoryNode } from 'src/app/core/models/category-node.interface';
import { CurrencyResponse } from 'src/app/core/models/currency-response.interface';

@Injectable({
  providedIn: 'root'
})
export class SharedApiService {
  private readonly animalsApiUrl : string = environment.animalsApiUrl; 
    private readonly productsApiUrl : string = environment.productsApiUrl; 
    private readonly categoriesApiUrl : string = environment.categoriesApiUrl; 
    private readonly shopsApiUrl : string =environment.shopsApiUrl;

    getBreeds(animalTypeId : string) : Observable<Specification<string>[]> {
      return from(axios.get(`${this.animalsApiUrl}/Breeds/${animalTypeId}`)).pipe(
        map((response: AxiosResponse<any>) => {
          const breeds: Specification<string>[] = response.data.$values;
          return breeds;
        }),
        catchError((error: AxiosError) => {
          return EMPTY;
        })
      );
    }

    GetCities(): Observable<Specification<string>[]>{
      return from(axios.get(`${this.productsApiUrl}/Cities`)).pipe(
        map((response: AxiosResponse<any>) => {
          const cities: Specification<string>[] = response.data.$values;
          return cities;
        }),
        catchError((error: AxiosError) => {
          return EMPTY;
        })
      );
    }

    getCurrencies() : Observable<CurrencyResponse[]> {
      return from(axios.get(`${this.productsApiUrl}/Currencies`)).pipe(
        map((response: AxiosResponse<any>) => {
          const currencies: CurrencyResponse[] = response.data.$values;
          return currencies;
        }),
        catchError((error: AxiosError) => {
          return EMPTY;
        })
      );
    }

    getCategories(): Observable<CategoryNode[]> {
        return from(axios.get(`${this.categoriesApiUrl}`)).pipe(
          map((response: AxiosResponse<any>) => {
            const categories: CategoryNode[] = response.data.$values;
            return categories;
          }),
          catchError((error: AxiosError) => {
            return EMPTY;
          })
        );
    }

    GetSiblingsByCategoryAsync(categoryId : string): Observable<CategoryNode[]> {
      return from(axios.get(`${this.categoriesApiUrl}/Subcategories/Siblings/${categoryId}`)).pipe(
        map((response: AxiosResponse<any>) => {
          const subcategories: CategoryNode[] = response.data.$values;
          return subcategories;
        }),
        catchError((error: AxiosError) => {
          return EMPTY;
        })
      );
    }

    GetSubcategoriesByCategoryAsync(categoryId : string): Observable<CategoryNode[]> {
      return from(axios.get(`${this.categoriesApiUrl}/Subcategories/Category/${categoryId}`)).pipe(
        map((response: AxiosResponse<any>) => {
          const subcategories: CategoryNode[] = response.data.$values;
          return subcategories;
        }),
        catchError((error: AxiosError) => {
          return EMPTY;
        })
      );
    }

    GetSubcategoriesBySubcategoryAsync(subcategoryId : string): Observable<CategoryNode[]> {
      return from(axios.get(`${this.categoriesApiUrl}/Subcategories/Subcategory/${subcategoryId}`)).pipe(
        map((response: AxiosResponse<any>) => {
          const subcategories: CategoryNode[] = response.data.$values;
          return subcategories;
        }),
        catchError((error: AxiosError) => {
          return EMPTY;
        })
      );
    }

    getShops(page: number, pageSize: number, sortByViews : boolean, reverseSort : boolean): Observable<PageResponse<Shop>> {
      return from(
        axios.get(`${this.shopsApiUrl}?Page=${page}&PageSize=${pageSize}&SortByViews=${sortByViews}&ReverseSort=${reverseSort}`)
      ).pipe(
        map(response => ({
          items: response.data.items.$values,
          count: response.data.count,
          allPages: response.data.allPages,
          minPrice: response.data.minPrice,
          maxPrice: response.data.maxPrice
        })),
        catchError(error => {
          throw error;
        })
      );
    }

    getProducts(page: number, pageSize: number, sortByPrice : boolean, reverseSort : boolean, userId : string | null = null): Observable<PageResponse<Product>> {
        let url = `${this.productsApiUrl}?Page=${page}&PageSize=${pageSize}&SortByPrice=${sortByPrice}&ReverseSort=${reverseSort}`
        url += userId != null ? `&OtherUserId=${userId}` : '';

        return from(
          axios.get(url)
        ).pipe(
          map(response => ({
            items: response.data.items.$values.map((item: any) => ({
              ...item,
              images: item.images.$values
            })),
            count: response.data.count,
            allPages: response.data.allPages,
            minPrice: response.data.minPrice,
            maxPrice: response.data.maxPrice
          })),
          catchError(error => {
            throw error;
          })
        );
    }

    addToFavorites(productId: string){
        return from(
            axios.post(`${this.productsApiUrl}/FavoriteProducts/${productId}`)
          ).pipe(
            map(response => ({
                response: response,
            })),
            catchError(error => {
                throw error;
            })
          );
    }

    removeFavorites(productId: string){
      return from(
          axios.delete(`${this.productsApiUrl}/FavoriteProducts/${productId}`)
        ).pipe(
          map(response => ({
              response: response,
          })),
          catchError(error => {
              throw error;
          })
        );
    }

    getFavoriteProducts(page: number, pageSize: number, sortByPrice : boolean, reverseSort : boolean): Observable<PageResponse<Product>> {
      let url = `${this.productsApiUrl}/FavoriteProducts?Page=${page}&PageSize=${pageSize}&SortByPrice=${sortByPrice}&ReverseSort=${reverseSort}`
      return from(
        axios.get(url)
      ).pipe(
        map(response => ({
          items: response.data.items.$values.map((item: any) => ({
            ...item,
            images: item.images.$values
          })),
          count: response.data.count,
          allPages: response.data.allPages,
          minPrice: response.data.minPrice,
          maxPrice: response.data.maxPrice
        })),
        catchError(error => {
          throw error;
        })
      );
    }
}