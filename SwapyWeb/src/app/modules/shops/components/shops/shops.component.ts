import { AfterViewInit, Component, OnInit } from '@angular/core';
import { PageResponse } from 'src/app/core/models/page-response.interface';
import { PaginationComponent } from 'src/app/shared/pagination/pagination.component';
import { PaginationService } from 'src/app/shared/pagination/pagination.service';
import { Shop } from '../../models/shop.model';
import { Observable } from 'rxjs';
import { SharedApiService } from 'src/app/modules/main/services/shared-api.service';
@Component({
  selector: 'app-shops',
  templateUrl: './shops.component.html',
  styleUrls: ['./shops.component.scss'],
})
export class ShopsComponent implements AfterViewInit, OnInit {
  shops$!: Observable<PageResponse<Shop>>;
  selectedFilter: string = '1';
  
  constructor(private paginationService: PaginationService, private sharedApiService : SharedApiService) {
  }

  ngOnInit(): void {
    this.loadShops();
  }

  ngAfterViewInit(): void {
    this.paginationService.updatePagination(50, 100)
  }

  loadShops(): void{
    switch(this.selectedFilter) {
      case '1':{
        this.shops$ = this.sharedApiService.getShops(1, 10, false, true);
        break;
      }
      case '2':{
        this.shops$ = this.sharedApiService.getShops(1, 10, true, true);
        break;
      }
      case '3':{
        this.shops$ = this.sharedApiService.getShops(1, 10, true, false);
        break;
      }
      case '4':{
        this.shops$ = this.sharedApiService.getShops(1, 10, true, true);
        break;
      }
      default:{
        this.shops$ = this.sharedApiService.getShops(1, 10, true, true);
        break;
      }
    }

  }

  onSelectFilterChange(): void{
    this.loadShops();
  }

}
