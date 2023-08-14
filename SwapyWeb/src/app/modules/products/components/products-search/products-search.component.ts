import { AfterViewInit, ChangeDetectorRef, Component, HostListener, OnInit } from '@angular/core';
import { Product } from '../../models/product.model';
import { SharedApiService } from 'src/app/modules/main/services/shared-api.service';
import { environment } from 'src/environments/environment';
import { PageResponse } from 'src/app/core/models/page-response.interface';
import { CategoryNode } from 'src/app/core/models/category-node.interface';
import { SpinnerService } from 'src/app/shared/spinner/spinner.service';
import { ProductApiService } from '../../services/product-api.service';
import { Specification } from 'src/app/core/models/specification';
import { forkJoin } from 'rxjs';
import { CategoryType } from 'src/app/core/enums/category-type.enum';
import { CurrencyResponse } from 'src/app/core/models/currency-response.interface';
import { CheckboxItem } from '../../models/checkbox-item.class';
import { SubcategoryType } from 'src/app/core/enums/subcategory-type.enum';
import { AutoResponse } from '../../models/auto-response.interface';
import { RealEstateResponse } from '../../models/real-estate-response.interface';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-products-search',
  templateUrl: './products-search.component.html',
  styleUrls: ['./products-search.component.scss']
})
export class ProductsSearchComponent implements OnInit, AfterViewInit {
  suitableProducts!: Product[];
  allPages: number | null = 0;
  currentPage: number = 0;
  suitableProductsCount: number | null = 0;
  pageSize: number = 10;
  isLoadingProducts: boolean = true;
  isNotFoundProducts: boolean = false;

  isRangeSliderMouseDown: boolean = false;

  selectedFilter: string = '1';
  sortByPrice: boolean = false;
  reverseSort: boolean = true;

  categories!: CategoryNode[];
  selectedCategoryId!: string;
  subcategoriesHierarchy: CategoryNode[][] = [];
  selectedSubcategoriesId: string[] = [];
  currentSubcategoryNesting: number = -1;

  currencies!: CurrencyResponse[];
  selectedCurrencyId!: string;
  selectedCurrencySymbol!: string;

  minPrice!: number;
  maxPrice!: number;
  priceSliderOptions: any = {
    floor: null, 
    ceil: null, 
    step: 1
  };

  cities!: Specification<string>[];
  selectedCityId!: string;

  breeds!: CheckboxItem<Specification<string>>[];

  selectedIsNewFilter: number = 0;

  minMiliage!: number;
  maxMiliage!: number;
  miliageSliderOptions: any = {
    floor: null, 
    ceil: null, 
    step: 1
  };

  minEngineCapacity!: number;
  maxEngineCapacity!: number;
  engineCapacitySliderOptions: any = {
    floor: null, 
    ceil: null, 
    step: 1
  };

  olderReleaseYear!: number;
  newerReleaseYear!: number;
  releaseYearSliderOptions: any= {
    floor: null, 
    ceil: null, 
    step: 1
  };

  fuelTypes!: CheckboxItem<Specification<string>>[];

  colors!: CheckboxItem<Specification<string>>[];

  transmissionTypes!: CheckboxItem<Specification<string>>[];

  brands!: CheckboxItem<Specification<string>>[];

  clotheIsShoe: boolean = false;

  selectedClothesTypeFilter: number = 0;

  clothesSizes!: CheckboxItem<Specification<string>>[];

  genders!: Specification<string>[];
  selectedGenderId!: string;

  clothesSeasons!: CheckboxItem<Specification<string>>[];

  clothesViews!: Specification<string>[];
  selectedClothesViewId!: string;

  memories!: CheckboxItem<Specification<string>>[];

  models!: Specification<string>[];
  selectedModelId!: string;

  minArea!: number;
  maxArea!: number;
  areaSliderOptions: any = {
    floor: null, 
    ceil: null, 
    step: 1
  };

  minRooms!: number;
  maxRooms!: number;
  roomsSliderOptions: any = {
    floor: null, 
    ceil: null, 
    step: 1
  };

  selectedIsRentFilter: number = 0;

  tvTypes!: CheckboxItem<Specification<string>>[];

  selectedIsSmartFilter: number = 0;

  screenResolutions!: CheckboxItem<Specification<string>>[];

  screenDiagonals!: CheckboxItem<Specification<number>>[];

  selectedTitle!: string;

  private _selectedCategoryType!: CategoryType | undefined;

  get selectedCategoryType(): CategoryType | undefined { return this._selectedCategoryType; }

  set selectedCategoryType(value: CategoryType | undefined) {
    this.spinnerService.changeSpinnerState(true);
    switch(value){
      case CategoryType.AnimalsType: {
        this.sharedApiService.getBreeds(this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1]).subscribe((response : Specification<string>[]) => {
          this.breeds = response.map(breed => new CheckboxItem<Specification<string>>(breed));
          this._selectedCategoryType = value;
          this.spinnerService.changeSpinnerState(false);
          return;
        })
        break;
      }
      case CategoryType.AutosType: {
        forkJoin([
          this.productApiService.getFuelTypes(),
          this.productApiService.getColors(),
          this.productApiService.getTransmissionTypes(),
          this.productApiService.getAutoBrands([this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1]])
        ]).subscribe(
          ([fuelTypes, colors, transmissionTypes, brands]: [Specification<string>[], Specification<string>[], Specification<string>[], Specification<string>[]]) => {
            this.fuelTypes = fuelTypes.map(item => new CheckboxItem<Specification<string>>(item));
            this.colors = colors.map(item => new CheckboxItem<Specification<string>>(item));
            this.transmissionTypes = transmissionTypes.map(item => new CheckboxItem<Specification<string>>(item));
            this.brands = brands.map(item => new CheckboxItem<Specification<string>>(item));
            this._selectedCategoryType = value;
            this.spinnerService.changeSpinnerState(false);
            return;
          },
          error => {
            console.error('Error fetching data:', error);
            this._selectedCategoryType = value;
            this.spinnerService.changeSpinnerState(false);
            return;
          }
        );
        break;
      }
      case CategoryType.ClothesType: {
        forkJoin([
          this.productApiService.getClotheBrands([this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1]]),
          this.productApiService.getClothesSizes(this.selectedClothesTypeFilter == -1, this.clotheIsShoe),
          this.productApiService.getGenders(),
          this.productApiService.getClothesSeasons(),
          this.productApiService.getClothesViews(this.selectedGenderId !== undefined ? this.selectedGenderId : null, this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== undefined ? this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] : null)
        ]).subscribe(
          ([brands, clothesSizes, genders, clothesSeasons, clothesViews]: [Specification<string>[], Specification<string>[], Specification<string>[], Specification<string>[], Specification<string>[]]) => {
            this.brands = brands.map(item => new CheckboxItem<Specification<string>>(item));
            this.clothesSizes = clothesSizes.map(item => new CheckboxItem<Specification<string>>(item));
            this.genders = genders;
            this.clothesSeasons = clothesSeasons.map(item => new CheckboxItem<Specification<string>>(item));
            this.clothesViews = clothesViews;
            this._selectedCategoryType = value;
            this.spinnerService.changeSpinnerState(false);
            return;
          },
          error => {
            console.error('Error fetching data:', error);
            this._selectedCategoryType = value;
            this.spinnerService.changeSpinnerState(false);
            return;
          }
        );
        break;
      }
      case CategoryType.ElectronicsType: {
        forkJoin([
          this.productApiService.getElectronicColors(this.selectedModelId !== undefined ? this.selectedModelId : null),
          this.productApiService.getElectronicBrands(this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1]),
          this.productApiService.getElectronicMemories(this.selectedModelId !== undefined ? this.selectedModelId : null),
          this.productApiService.getElectronicModels(this.brands.filter(brand => brand.selected).map(brand => brand.value.id).length > 0 ? this.brands.filter(brand => brand.selected).map(brand => brand.value.id) : null, this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== undefined ? this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] : null)
        ]).subscribe(
          ([colors, brands, memories, models]: [Specification<string>[], Specification<string>[], Specification<string>[], Specification<string>[]]) => {
            this.colors = colors.map(item => new CheckboxItem<Specification<string>>(item));
            this.brands = brands.map(item => new CheckboxItem<Specification<string>>(item));
            this.memories = memories.map(item => new CheckboxItem<Specification<string>>(item));
            this.models = models;
            this._selectedCategoryType = value;
            this.spinnerService.changeSpinnerState(false);
            return;
          },
          error => {
            console.error('Error fetching data:', error);
            this._selectedCategoryType = value;
            this.spinnerService.changeSpinnerState(false);
            return;
          }
        );
        break;
      }
      case CategoryType.ItemsType: {
        this._selectedCategoryType = value;
        this.spinnerService.changeSpinnerState(false);
        return;
        break;
      }
      case CategoryType.RealEstatesType: {
        this._selectedCategoryType = value;
        this.spinnerService.changeSpinnerState(false);
        return;
        break;
      }
      case CategoryType.TVsType: {
        forkJoin([
          this.productApiService.getTVBrands(),
          this.productApiService.getTVTypes(),
          this.productApiService.getTVScreenResolutions(),
          this.productApiService.getTVScreenDiagonals()
        ]).subscribe(
          ([brands, tvTypes, screenResolutions, screenDiagonals]: [Specification<string>[], Specification<string>[], Specification<string>[], Specification<number>[]]) => {
            this.brands = brands.map(item => new CheckboxItem<Specification<string>>(item));
            this.tvTypes = tvTypes.map(item => new CheckboxItem<Specification<string>>(item));
            this.screenResolutions = screenResolutions.map(item => new CheckboxItem<Specification<string>>(item));
            this.screenDiagonals = screenDiagonals.map(item => new CheckboxItem<Specification<number>>(item));
            this._selectedCategoryType = value;
            this.spinnerService.changeSpinnerState(false);
            return;
          },
          error => {
            console.error('Error fetching data:', error);
            this._selectedCategoryType = value;
            this.spinnerService.changeSpinnerState(false);
            return;
          }
        );
        break;
      }
      default: {
        this._selectedCategoryType = undefined;
        this.spinnerService.changeSpinnerState(false);
        return;
        break;
      }
    }
  }

  constructor(private productApiService: ProductApiService, private sharedApiService : SharedApiService, private spinnerService: SpinnerService, private changeDetectorRef : ChangeDetectorRef, private route: ActivatedRoute) {
    this.route.queryParams.subscribe(params => {
      this.selectedTitle = params['title'];
    });
  }

  ngOnInit(): void {
    this.spinnerService.changeSpinnerState(true);
    alert(this.selectedTitle);
    this.minPrice = this.priceSliderOptions.floor;
    this.maxPrice = this.priceSliderOptions.ceil;
    this.minMiliage = this.miliageSliderOptions.floor;
    this.maxMiliage = this.miliageSliderOptions.ceil;
    this.minEngineCapacity = this.engineCapacitySliderOptions.floor;
    this.maxEngineCapacity = this.engineCapacitySliderOptions.ceil;
    this.olderReleaseYear = this.releaseYearSliderOptions.floor;
    this.newerReleaseYear = this.releaseYearSliderOptions.ceil;
    this.minArea = this.areaSliderOptions.floor;
    this.maxArea = this.areaSliderOptions.ceil;
    this.minRooms = this.roomsSliderOptions.floor;
    this.maxRooms = this.roomsSliderOptions.ceil;

    forkJoin([
      this.sharedApiService.getCategories(),
      this.sharedApiService.getCurrencies(),
      this.sharedApiService.GetCities()
    ]).subscribe(
      ([categories, currencies, cities]: [CategoryNode[], CurrencyResponse[], Specification<string>[]]) => {
        this.categories = categories;
        this.currencies = currencies;
        this.selectedCurrencyId = currencies[0].id;
        this.selectedCurrencySymbol = currencies[0].symbol;
        this.cities = cities;
        this.spinnerService.changeSpinnerState(false);
      },
      error => {
        console.error('Error fetching data:', error);
        this.spinnerService.changeSpinnerState(false);
      }
    );
    this.loadSuitableProducts();
  }

  ngAfterViewInit(): void {}

  onSelectSortChange(): void{
    switch(this.selectedFilter) {
      case '1':{
        this.sortByPrice = false;
        this.reverseSort = true;
        break;
      }
      case '2':{
        this.sortByPrice = false;
        this.reverseSort = false;
        break;
      }
      case '3':{
        this.sortByPrice = true;
        this.reverseSort = false;
        break;
      }
      case '4':{
        this.sortByPrice = false;
        this.reverseSort = true;
        break;
      }
      default:{
        this.sortByPrice = false;
        this.reverseSort = true;
        break;
      }
    }
    this.loadSuitableProducts(true);
  }

  loadSuitableProducts(isNewRequest: boolean = false): void {
    this.isLoadingProducts = true;

    this.priceSliderOptions = {
      floor: this.minPrice, 
      ceil: this.maxPrice, 
      step: 1
    };
    this.miliageSliderOptions = {
      floor: this.minMiliage, 
      ceil: this.maxMiliage, 
      step: 1
    };
    this.engineCapacitySliderOptions = {
      floor: this.minEngineCapacity, 
      ceil: this.maxEngineCapacity, 
      step: 1
    };
    this.releaseYearSliderOptions = {
      floor: this.olderReleaseYear, 
      ceil: this.newerReleaseYear, 
      step: 1
    };
    this.areaSliderOptions = {
      floor: this.minArea, 
      ceil: this.maxArea, 
      step: 1
    };
    this.roomsSliderOptions = {
      floor: this.minRooms, 
      ceil: this.maxRooms, 
      step: 1
    };
  
    if(isNewRequest){
      this.suitableProductsCount = null;
      this.allPages = null;
      this.suitableProducts = [];
      this.currentPage = 1;
    }
    else { this.currentPage++; }

    switch(this.selectedCategoryType){
      case CategoryType.AnimalsType: {   
        this.minMiliage = this.miliageSliderOptions.floor;
        this.maxMiliage = this.miliageSliderOptions.ceil;
        this.minEngineCapacity = this.engineCapacitySliderOptions.floor;
        this.maxEngineCapacity = this.engineCapacitySliderOptions.ceil;
        this.olderReleaseYear = this.releaseYearSliderOptions.floor;
        this.newerReleaseYear = this.releaseYearSliderOptions.ceil;
        this.minArea = this.areaSliderOptions.floor;
        this.maxArea = this.areaSliderOptions.ceil;
        this.minRooms = this.roomsSliderOptions.floor;
        this.maxRooms = this.roomsSliderOptions.ceil;

        this.productApiService.getFilteredAnimals(this.currentPage, this.pageSize, this.sortByPrice, this.reverseSort, this.selectedTitle !== undefined ? this.selectedTitle : null, this.selectedCurrencyId !== undefined ? this.selectedCurrencyId : null, this.minPrice, this.maxPrice, this.currentSubcategoryNesting >= 0 && this.selectedCategoryId !== undefined ? this.selectedCategoryId : null, this.currentSubcategoryNesting > 0 && this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== undefined ? this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] : null, this.selectedCityId !== undefined ? this.selectedCityId : null, this.breeds.filter(breed => breed.selected).map(breed => breed.value.id), this.currentSubcategoryNesting > 0 && this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== undefined ? [this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1]] : null).subscribe((response: PageResponse<Product>) => { 
          response.items.forEach(item => {
            if (Array.isArray(item.images)) {
              item.images = item.images.map((image) => `${environment.blobUrl}/product-images/${image}`);
            }
          });
          this.suitableProductsCount = response.count;
          this.allPages = response.allPages;
          this.priceSliderOptions = {
            floor: response.minPrice, 
            ceil: response.maxPrice, 
            step: 1
          };
          if(this.suitableProducts != null) { this.suitableProducts.push(...response.items); }
          else { this.suitableProducts = response.items; }
          this.isLoadingProducts = false;
        },
        (error) => {
          this.isLoadingProducts = false;
          this.isNotFoundProducts = true;
        });
        break;
      }
      case CategoryType.AutosType: {
        this.minArea = this.areaSliderOptions.floor;
        this.maxArea = this.areaSliderOptions.ceil;
        this.minRooms = this.roomsSliderOptions.floor;
        this.maxRooms = this.roomsSliderOptions.ceil;

        this.productApiService.getFilteredAutos(this.currentPage, this.pageSize, this.sortByPrice, this.reverseSort, this.selectedTitle !== undefined ? this.selectedTitle : null, this.selectedCurrencyId !== undefined ? this.selectedCurrencyId : null, this.minPrice, this.maxPrice, this.currentSubcategoryNesting >= 0 && this.selectedCategoryId !== undefined ? this.selectedCategoryId : null, this.currentSubcategoryNesting > 0 && this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== undefined ? this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] : null, this.selectedCityId !== undefined ? this.selectedCityId : null, this.selectedIsNewFilter !== 0 ? this.selectedIsNewFilter === 1 : null, this.miliageSliderOptions.floor != null ? this.minMiliage : null, this.miliageSliderOptions.ceil != null ? this.maxMiliage : null, this.engineCapacitySliderOptions.floor != null ? this.minEngineCapacity : null, this.engineCapacitySliderOptions.floor != null ? this.maxEngineCapacity : null, this.releaseYearSliderOptions.floor != null ? new Date(this.olderReleaseYear, 0, 1) : null, this.miliageSliderOptions.ceil != null ? new Date(this.newerReleaseYear, 12, 0) : null, this.fuelTypes.filter(fuelType => fuelType.selected).map(fuelType => fuelType.value.id), this.colors.filter(color => color.selected).map(color => color.value.id), this.transmissionTypes.filter(transmissionType => transmissionType.selected).map(transmissionType => transmissionType.value.id), this.brands.filter(brand => brand.selected).map(brand => brand.value.id), this.currentSubcategoryNesting > 0 && this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== undefined ? [this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1]] : null).subscribe((response: AutoResponse) => { 
          response.items.forEach(item => {
            if (Array.isArray(item.images)) {
              item.images = item.images.map((image) => `${environment.blobUrl}/product-images/${image}`);
            }
          });
          this.suitableProductsCount = response.count;
          this.allPages = response.allPages;
          this.priceSliderOptions = {
            floor: response.minPrice, 
            ceil: response.maxPrice, 
            step: 1
          };
          this.miliageSliderOptions = {
            floor: response.minMiliage, 
            ceil: response.maxMiliage, 
            step: 1
          };
          this.engineCapacitySliderOptions = {
            floor: response.minEngineCapacity, 
            ceil: response.maxEngineCapacity, 
            step: 1
          };
          this.releaseYearSliderOptions = {
            floor: response.olderReleaseYear, 
            ceil: response.newerReleaseYear, 
            step: 1
          };
          if(this.suitableProducts != null) { this.suitableProducts.push(...response.items); }
          else { this.suitableProducts = response.items; }
          this.isLoadingProducts = false;
        },
        (error) => {
          this.isLoadingProducts = false;
          this.isNotFoundProducts = true;
        });
        break;
      }
      case CategoryType.ClothesType: {
        this.minMiliage = this.miliageSliderOptions.floor;
        this.maxMiliage = this.miliageSliderOptions.ceil;
        this.minEngineCapacity = this.engineCapacitySliderOptions.floor;
        this.maxEngineCapacity = this.engineCapacitySliderOptions.ceil;
        this.olderReleaseYear = this.releaseYearSliderOptions.floor;
        this.newerReleaseYear = this.releaseYearSliderOptions.ceil;
        this.minArea = this.areaSliderOptions.floor;
        this.maxArea = this.areaSliderOptions.ceil;
        this.minRooms = this.roomsSliderOptions.floor;
        this.maxRooms = this.roomsSliderOptions.ceil;

        this.productApiService.getFilteredClothes(this.currentPage, this.pageSize, this.sortByPrice, this.reverseSort, this.selectedTitle !== undefined ? this.selectedTitle : null, this.selectedCurrencyId !== undefined ? this.selectedCurrencyId : null, this.minPrice, this.maxPrice, this.currentSubcategoryNesting >= 0 && this.selectedCategoryId !== undefined ? this.selectedCategoryId : null, this.currentSubcategoryNesting > 0 && this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== undefined ? this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] : null, this.selectedCityId !== undefined ? this.selectedCityId : null, this.selectedIsNewFilter !== 0 ? this.selectedIsNewFilter === 1 : null, this.clothesSeasons.filter(clothesSeason => clothesSeason.selected).map(clothesSeason => clothesSeason.value.id), this.clothesSizes.filter(clothesSize => clothesSize.selected).map(clothesSize => clothesSize.value.id), this.brands.filter(brand => brand.selected).map(brand => brand.value.id), [this.selectedClothesViewId], this.currentSubcategoryNesting > 0 && this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== undefined ? [this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1]] : null, [this.selectedGenderId]).subscribe((response: PageResponse<Product>) => { 
          response.items.forEach(item => {
            if (Array.isArray(item.images)) {
              item.images = item.images.map((image) => `${environment.blobUrl}/product-images/${image}`);
            }
          });
          this.suitableProductsCount = response.count;
          this.allPages = response.allPages;
          this.priceSliderOptions = {
            floor: response.minPrice, 
            ceil: response.maxPrice, 
            step: 1
          };
          if(this.suitableProducts != null) { this.suitableProducts.push(...response.items); }
          else { this.suitableProducts = response.items; }
          this.isLoadingProducts = false;
        },
        (error) => {
          this.isLoadingProducts = false;
          this.isNotFoundProducts = true;
        });
        break;
      }
      case CategoryType.ElectronicsType: {
        this.minMiliage = this.miliageSliderOptions.floor;
        this.maxMiliage = this.miliageSliderOptions.ceil;
        this.minEngineCapacity = this.engineCapacitySliderOptions.floor;
        this.maxEngineCapacity = this.engineCapacitySliderOptions.ceil;
        this.olderReleaseYear = this.releaseYearSliderOptions.floor;
        this.newerReleaseYear = this.releaseYearSliderOptions.ceil;
        this.minArea = this.areaSliderOptions.floor;
        this.maxArea = this.areaSliderOptions.ceil;
        this.minRooms = this.roomsSliderOptions.floor;
        this.maxRooms = this.roomsSliderOptions.ceil;

        this.productApiService.getFilteredElectronics(this.currentPage, this.pageSize, this.sortByPrice, this.reverseSort, this.selectedTitle !== undefined ? this.selectedTitle : null, this.selectedCurrencyId !== undefined ? this.selectedCurrencyId : null, this.minPrice, this.maxPrice, this.currentSubcategoryNesting >= 0 && this.selectedCategoryId !== undefined ? this.selectedCategoryId : null, this.currentSubcategoryNesting > 0 && this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== undefined ? this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] : null, this.selectedCityId !== undefined ? this.selectedCityId : null, this.selectedIsNewFilter !== 0 ? this.selectedIsNewFilter === 1 : null, this.memories.filter(memory => memory.selected).map(memory => memory.value.id), this.colors.filter(color => color.selected).map(color => color.value.id), [this.selectedModelId], this.brands.filter(brand => brand.selected).map(brand => brand.value.id), this.currentSubcategoryNesting > 0 && this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== undefined ? [this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1]] : null).subscribe((response: PageResponse<Product>) => { 
          response.items.forEach(item => {
            if (Array.isArray(item.images)) {
              item.images = item.images.map((image) => `${environment.blobUrl}/product-images/${image}`);
            }
          });
          this.suitableProductsCount = response.count;
          this.allPages = response.allPages;
          this.priceSliderOptions = {
            floor: response.minPrice, 
            ceil: response.maxPrice, 
            step: 1
          };
          if(this.suitableProducts != null) { this.suitableProducts.push(...response.items); }
          else { this.suitableProducts = response.items; }
          this.isLoadingProducts = false;
        },
        (error) => {
          this.isLoadingProducts = false;
          this.isNotFoundProducts = true;
        });
        break;
      }
      case CategoryType.ItemsType: {
        this.minMiliage = this.miliageSliderOptions.floor;
        this.maxMiliage = this.miliageSliderOptions.ceil;
        this.minEngineCapacity = this.engineCapacitySliderOptions.floor;
        this.maxEngineCapacity = this.engineCapacitySliderOptions.ceil;
        this.olderReleaseYear = this.releaseYearSliderOptions.floor;
        this.newerReleaseYear = this.releaseYearSliderOptions.ceil;
        this.minArea = this.areaSliderOptions.floor;
        this.maxArea = this.areaSliderOptions.ceil;
        this.minRooms = this.roomsSliderOptions.floor;
        this.maxRooms = this.roomsSliderOptions.ceil;

        this.productApiService.getFilteredItems(this.currentPage, this.pageSize, this.sortByPrice, this.reverseSort, this.selectedTitle !== undefined ? this.selectedTitle : null, this.selectedCurrencyId !== undefined ? this.selectedCurrencyId : null, this.minPrice, this.maxPrice, this.currentSubcategoryNesting >= 0 && this.selectedCategoryId !== undefined ? this.selectedCategoryId : null, this.currentSubcategoryNesting > 0 && this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== undefined ? this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] : null, this.selectedCityId !== undefined ? this.selectedCityId : null, this.selectedIsNewFilter !== 0 ? this.selectedIsNewFilter === 1 : null, this.currentSubcategoryNesting > 0 && this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== undefined ? [this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1]] : null).subscribe((response: PageResponse<Product>) => { 
          response.items.forEach(item => {
            if (Array.isArray(item.images)) {
              item.images = item.images.map((image) => `${environment.blobUrl}/product-images/${image}`);
            }
          });
          this.suitableProductsCount = response.count;
          this.allPages = response.allPages;
          this.priceSliderOptions = {
            floor: response.minPrice, 
            ceil: response.maxPrice, 
            step: 1
          };
          if(this.suitableProducts != null) { this.suitableProducts.push(...response.items); }
          else { this.suitableProducts = response.items; }
          this.isLoadingProducts = false;
        },
        (error) => {
          this.isLoadingProducts = false;
          this.isNotFoundProducts = true;
        });
        break;
      }
      case CategoryType.RealEstatesType: {
        this.minMiliage = this.miliageSliderOptions.floor;
        this.maxMiliage = this.miliageSliderOptions.ceil;
        this.minEngineCapacity = this.engineCapacitySliderOptions.floor;
        this.maxEngineCapacity = this.engineCapacitySliderOptions.ceil;
        this.olderReleaseYear = this.releaseYearSliderOptions.floor;
        this.newerReleaseYear = this.releaseYearSliderOptions.ceil;

        this.productApiService.getFilteredRealEstates(this.currentPage, this.pageSize, this.sortByPrice, this.reverseSort, this.selectedTitle !== undefined ? this.selectedTitle : null, this.selectedCurrencyId !== undefined ? this.selectedCurrencyId : null, this.minPrice, this.maxPrice, this.currentSubcategoryNesting >= 0 && this.selectedCategoryId !== undefined ? this.selectedCategoryId : null, this.currentSubcategoryNesting > 0 && this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== undefined ? this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] : null, this.selectedCityId !== undefined ? this.selectedCityId : null, this.selectedIsRentFilter !== 0 ? this.selectedIsRentFilter === 1 : null, this.areaSliderOptions.ceil != null ? this.maxArea : null, this.areaSliderOptions.floor != null ? this.minArea : null, this.roomsSliderOptions.floor != null ? this.minRooms : null, this.roomsSliderOptions.ceil != null ? this.maxRooms : null, this.currentSubcategoryNesting > 0 && this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== undefined ? [this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1]] : null).subscribe((response: RealEstateResponse) => { 
          response.items.forEach(item => {
            if (Array.isArray(item.images)) {
              item.images = item.images.map((image) => `${environment.blobUrl}/product-images/${image}`);
            }
          });
          this.suitableProductsCount = response.count;
          this.allPages = response.allPages;
          this.priceSliderOptions = {
            floor: response.minPrice, 
            ceil: response.maxPrice, 
            step: 1
          };
          this.areaSliderOptions = {
            floor: response.minArea, 
            ceil: response.maxArea, 
            step: 1
          };
          this.roomsSliderOptions = {
            floor: response.minRooms, 
            ceil: response.maxRooms, 
            step: 1
          };
          if(this.suitableProducts != null) { this.suitableProducts.push(...response.items); }
          else { this.suitableProducts = response.items; }
          this.isLoadingProducts = false;
        },
        (error) => {
          this.isLoadingProducts = false;
          this.isNotFoundProducts = true;
        });
        break;
      }
      case CategoryType.TVsType: {
        this.minMiliage = this.miliageSliderOptions.floor;
        this.maxMiliage = this.miliageSliderOptions.ceil;
        this.minEngineCapacity = this.engineCapacitySliderOptions.floor;
        this.maxEngineCapacity = this.engineCapacitySliderOptions.ceil;
        this.olderReleaseYear = this.releaseYearSliderOptions.floor;
        this.newerReleaseYear = this.releaseYearSliderOptions.ceil;
        this.minArea = this.areaSliderOptions.floor;
        this.maxArea = this.areaSliderOptions.ceil;
        this.minRooms = this.roomsSliderOptions.floor;
        this.maxRooms = this.roomsSliderOptions.ceil;

        this.productApiService.getFilteredTVs(this.currentPage, this.pageSize, this.sortByPrice, this.reverseSort, this.selectedTitle !== undefined ? this.selectedTitle : null, this.selectedCurrencyId !== undefined ? this.selectedCurrencyId : null, this.minPrice, this.maxPrice, this.currentSubcategoryNesting >= 0 && this.selectedCategoryId !== undefined ? this.selectedCategoryId : null, this.currentSubcategoryNesting > 0 && this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== undefined ? this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] : null, this.selectedCityId !== undefined ? this.selectedCityId : null, this.selectedIsNewFilter !== 0 ? this.selectedIsNewFilter === 1 : null, this.selectedIsSmartFilter !== 0 ? this.selectedIsSmartFilter === 1 : null, this.tvTypes.filter(tvType => tvType.selected).map(tvType => tvType.value.id), this.brands.filter(brand => brand.selected).map(brand => brand.value.id), this.screenResolutions.filter(screenResolution => screenResolution.selected).map(screenResolution => screenResolution.value.id), this.screenDiagonals.filter(screenDiagonal => screenDiagonal.selected).map(screenDiagonal => screenDiagonal.value.id)).subscribe((response: PageResponse<Product>) => { 
          response.items.forEach(item => {
            if (Array.isArray(item.images)) {
              item.images = item.images.map((image) => `${environment.blobUrl}/product-images/${image}`);
            }
          });
          this.suitableProductsCount = response.count;
          this.allPages = response.allPages;
          this.priceSliderOptions = {
            floor: response.minPrice, 
            ceil: response.maxPrice, 
            step: 1
          };
          if(this.suitableProducts != null) { this.suitableProducts.push(...response.items); }
          else { this.suitableProducts = response.items; }
          this.isLoadingProducts = false;
        },
        (error) => {
          this.isLoadingProducts = false;
          this.isNotFoundProducts = true;
        });
        break;
      }
      default: {
        this.productApiService.getFilteredProducts(this.currentPage, this.pageSize, this.sortByPrice, this.reverseSort, null, this.selectedCurrencyId !== undefined ? this.selectedCurrencyId : null, this.minPrice, this.maxPrice, this.currentSubcategoryNesting >= 0 && this.selectedCategoryId !== undefined ? this.selectedCategoryId : null, this.currentSubcategoryNesting > 0 && this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== undefined ? this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] : null, this.selectedCityId !== undefined ? this.selectedCityId : null).subscribe((response: PageResponse<Product>) => { 
          response.items.forEach(item => {
            if (Array.isArray(item.images)) {
              item.images = item.images.map((image) => `${environment.blobUrl}/product-images/${image}`);
            }
          });
          this.suitableProductsCount = response.count;
          this.allPages = response.allPages;
          this.priceSliderOptions = {
            floor: response.minPrice, 
            ceil: response.maxPrice, 
            step: 1
          };
          if(this.suitableProducts != null) { this.suitableProducts.push(...response.items); }
          else { this.suitableProducts = response.items; }
          this.isLoadingProducts = false;
        },
        (error) => {
          this.isLoadingProducts = false;
          this.isNotFoundProducts = true;
        });
        break;
      }
    }
  }

  trackByProductId(index: number, product: Product): string {
    return product.id;
  }

  generateRange(count: number): number[] { return Array.from({length: count}, (_, i) => i + 1); }

  filterBlockCollapse(event: MouseEvent): void {
    let thisObj = event.currentTarget as HTMLElement;
    const dataWidget = thisObj.getAttribute('dataWidget');
    const targetElement = jQuery(`#${dataWidget}`);

    if (thisObj.classList.contains('filter-title-container-collapse')) {
        thisObj.classList.remove('filter-title-container-collapse');
        targetElement.removeClass("collapse");
    } else {
        thisObj.classList.add('filter-title-container-collapse');
        targetElement.addClass("collapse");
    }
  }

  onSelectCategoryChange(): void {
    this.subcategoriesHierarchy.splice(-this.currentSubcategoryNesting);
    this.selectedSubcategoriesId.splice(-this.currentSubcategoryNesting);
    this.currentSubcategoryNesting = -1;
    if(this.selectedCategoryId == 'undefined'){
      this.selectedCategoryType = undefined;
      this.loadSuitableProducts(true);
      return;
    }
    else{
      this.selectedCategoryType = this.categories.find(c => c.id.toLowerCase() === this.selectedCategoryId.toLowerCase())?.type;
      this.loadSuitableProducts(true);
    }
    this.spinnerService.changeSpinnerState(true);
    this.sharedApiService.GetSubcategoriesByCategoryAsync(this.selectedCategoryId).subscribe(
      (response: CategoryNode[]) => {
        this.subcategoriesHierarchy[++this.currentSubcategoryNesting] = response;
        this.spinnerService.changeSpinnerState(false);
      }
    );
  }

  onSelectSubcategoryChange(index: number): void {
    console.log('1');
    if(-this.currentSubcategoryNesting + index < 0){
      this.subcategoriesHierarchy.splice(-this.currentSubcategoryNesting + index);
      this.selectedSubcategoriesId.splice(-this.currentSubcategoryNesting + index);
      this.currentSubcategoryNesting = index;
    }
    console.log('2');
    if(this.selectedSubcategoriesId[index] == 'undefined'){
      return;
    }
    console.log('3');
    if(this.subcategoriesHierarchy[index].find(item => item.id === this.selectedSubcategoriesId[index])?.isFinal){
      return;
    }
    console.log('4');
    this.spinnerService.changeSpinnerState(true);
    this.sharedApiService.GetSubcategoriesBySubcategoryAsync(this.selectedSubcategoriesId[index]).subscribe(
      (response: CategoryNode[]) => {
        console.log(response);
        this.subcategoriesHierarchy[++this.currentSubcategoryNesting] = response;     
        this.clotheIsShoe = this.subcategoriesHierarchy[index].find(item => item.id === this.selectedSubcategoriesId[index])?.subType === SubcategoryType.Shoe;
        this.spinnerService.changeSpinnerState(false);
      }
    );
  }

  onSelectCurrencyChange(): void {
    alert(this.selectedCurrencyId);
    this.selectedCurrencySymbol = this.currencies.find(currency => currency.id == this.selectedCurrencyId)?.symbol as string;
  }

  handlePositiveNumberInput(event: Event): void {
    const inputElement = event.target as HTMLInputElement;
    let value = inputElement.value.replace(/[^0-9]/g, '');
    value = parseInt(value, 10).toString();
    inputElement.value = value;
    switch(inputElement.id){
      case 'price-input-min': { this.minPrice = Number.isNaN(parseInt(value)) ? this.priceSliderOptions.floor : parseInt(value); break; }
      case 'price-input-max': { this.maxPrice = Number.isNaN(parseInt(value)) ? this.priceSliderOptions.floor : parseInt(value); break; }
      case 'miliage-input-min': { this.minMiliage = Number.isNaN(parseInt(value)) ? this.miliageSliderOptions.floor : parseInt(value); break; }
      case 'miliage-input-max': { this.maxMiliage = Number.isNaN(parseInt(value)) ? this.miliageSliderOptions.floor : parseInt(value); break; }
      case 'engine-capacity-input-min': { this.minEngineCapacity = Number.isNaN(parseInt(value)) ? this.engineCapacitySliderOptions.floor : parseInt(value); break; }
      case 'engine-capacity-input-max': { this.maxEngineCapacity = Number.isNaN(parseInt(value)) ? this.engineCapacitySliderOptions.floor : parseInt(value); break; }
      case 'release-year-input-min': { this.olderReleaseYear = Number.isNaN(parseInt(value)) ? this.releaseYearSliderOptions.floor : parseInt(value); break; }
      case 'release-year-input-max': { this.newerReleaseYear = Number.isNaN(parseInt(value)) ? this.releaseYearSliderOptions.floor : parseInt(value); break; }
      case 'area-input-min': { this.minArea = Number.isNaN(parseInt(value)) ? this.areaSliderOptions.floor : parseInt(value); break; }
      case 'area-input-max': { this.maxArea = Number.isNaN(parseInt(value)) ? this.areaSliderOptions.floor : parseInt(value); break; }
      case 'rooms-input-min': { this.minRooms = Number.isNaN(parseInt(value)) ? this.roomsSliderOptions.floor : parseInt(value); break; }
      case 'rooms-input-max': { this.maxRooms = Number.isNaN(parseInt(value)) ? this.roomsSliderOptions.floor : parseInt(value); break; }
    }
  }

  isRangeSliderMouseDownEvent(): void {
    this.isRangeSliderMouseDown = true;
  }

  @HostListener('document:mouseup', ['$event'])
  isRangeSliderMouseUpEvent(): void {
    if(this.isRangeSliderMouseDown){
      this.isRangeSliderMouseDown = false;
      this.loadSuitableProducts(true);
    }
  }
}
