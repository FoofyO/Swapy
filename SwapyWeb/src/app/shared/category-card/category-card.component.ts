import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Specification } from 'src/app/core/models/specification';

@Component({
  selector: 'app-category-card',
  templateUrl: './category-card.component.html',
  styleUrls: ['./category-card.component.scss']
})
export class CategoryCardComponent implements OnInit {

  @Input() category!: Specification<string>;

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  moveToCategory(): void{
    this.router.navigateByUrl(`products/search?category=${this.category.id}`);
  }
}
