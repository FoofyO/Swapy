import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ProductList } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductApiService {

  private readonly apiUrl: string = environment.productsApiUrl;

  constructor(private httpClient: HttpClient) { }

  getLastesProducts(count: number): Promise<ProductList | undefined> {
    return this.httpClient.get<ProductList>(`${this.apiUrl}/Products?Page=1&PageSize=${count}`).toPromise();
  }

}
