import { Component, Input, OnInit } from '@angular/core';
import { Shop } from 'src/app/modules/shops/models/shop.model';

@Component({
  selector: 'app-shop-card',
  templateUrl: './shop-card.component.html',
  styleUrls: ['./shop-card.component.scss']
})
export class ShopCardComponent implements OnInit {

  @Input() shop!: Shop;

  constructor() { }

  ngOnInit(): void {
  }

  shopLogoLoadError(event: any) {
    event.srcElement.src = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQC-I_ZtbTLG-gsgJiY2_V5YP53FdUMs1C28w&usqp=CAU";
  }
}
