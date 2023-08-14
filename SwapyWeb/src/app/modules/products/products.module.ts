import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FavoriteProductsComponent } from './components/favorite-products/favorite-products.component';
import { AddComponent } from './components/add/add.component';
import { ProductRoutingModule } from './products-routing.module';
import { FormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';
import { ProductDetailComponent } from './components/product-detail/product-detail.component';
import { ProductsSearchComponent } from './components/products-search/products-search.component';
import { NgxSliderModule } from '@angular-slider/ngx-slider';


@NgModule({
  declarations: [
    FavoriteProductsComponent,
    AddComponent,
    ProductDetailComponent,
    ProductsSearchComponent
  ],
  imports: [
    CommonModule,
    ProductRoutingModule,
    FormsModule,
    SharedModule,
    NgxSliderModule
  ]
})
export class ProductsModule { }
