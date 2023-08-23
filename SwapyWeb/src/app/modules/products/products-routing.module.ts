import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FavoriteProductsComponent } from './components/favorite-products/favorite-products.component';
import { ProductDetailComponent } from './components/product-detail/product-detail.component';
import { ProductsSearchComponent } from './components/products-search/products-search.component';
import { AddComponent } from './components/add/add.component';
import { EditComponent } from './components/edit/edit.component';

const routes: Routes = [
  {path: 'search', component: ProductsSearchComponent },
  {path: 'favorites', component: FavoriteProductsComponent },
  {path: 'edit/:id', component: EditComponent },
  {path: 'add', component: AddComponent },
  {path: ':id', component: ProductDetailComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductRoutingModule { }