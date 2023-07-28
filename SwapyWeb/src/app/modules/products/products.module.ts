import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FavoriteProductsComponent } from './components/favorite-products/favorite-products.component';
import { AddProductComponent } from './components/add-product/add-product.component';
import { AddComponent } from './components/add/add.component';



@NgModule({
  declarations: [
    FavoriteProductsComponent,
    AddProductComponent,
    AddComponent
  ],
  imports: [
    CommonModule
  ]
})
export class ProductsModule { }
