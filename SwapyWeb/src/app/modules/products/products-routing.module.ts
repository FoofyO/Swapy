import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FavoriteProductsComponent } from './components/favorite-products/favorite-products.component';
import { ProductDetailComponent } from './components/product-detail/product-detail.component';
import { ProductsSearchComponent } from './components/products-search/products-search.component';

const routes: Routes = [
  {path: 'search', component: ProductsSearchComponent },
  {path: 'favorites', component: FavoriteProductsComponent },
  {path: ':id', component: ProductDetailComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductRoutingModule { }