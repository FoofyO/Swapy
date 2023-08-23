import { AfterViewInit, Component, Input, OnInit } from '@angular/core';
import 'slick-carousel/slick/slick.css';
import 'slick-carousel/slick/slick-theme.css';
import 'node_modules/slick-carousel/slick/slick.min.js';
import 'node_modules/slick-carousel';
import { Router } from '@angular/router';
import { HttpStatusCode } from 'axios';
import { SharedApiService } from 'src/app/modules/main/services/shared-api.service';
import { Product } from 'src/app/modules/products/models/product.model';
import { AuthFacadeService } from 'src/app/modules/auth/services/auth-facade.service';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss']
})
export class ProductCardComponent implements OnInit {

  @Input() product!: Product;
  isMyProduct!: boolean;

  constructor(private sharedApiService : SharedApiService, private router: Router, private authFacadeService: AuthFacadeService){}

  ngOnInit(): void {
    this.isMyProduct = this.authFacadeService.getUserId() === this.product.userId;
  }

  changeFavorite(event: any, product : Product) : void {
    event.preventDefault();
    (product.isFavorite ? this.sharedApiService.removeFavorites(product.id) : this.sharedApiService.addToFavorites(product.id))
    .subscribe(
      (result) => {product.isFavorite = !product.isFavorite},
      (error) => {
        if(error.response.status === HttpStatusCode.Unauthorized){
          this.router.navigate(['/auth/login']);
        }
      }
    );
  }

  productImageLoadError(event: any) {
    event.srcElement.src = "https://mtek3d.com/wp-content/uploads/2018/01/image-placeholder-500x500.jpg";
  }

  moveToProduct(): void{
    this.router.navigateByUrl(`products/${this.product.id}`);
  }
}
