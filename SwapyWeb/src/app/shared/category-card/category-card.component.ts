import { Component, Input, OnInit } from '@angular/core';
import { Specification } from 'src/app/core/models/specification';

@Component({
  selector: 'app-category-card',
  templateUrl: './category-card.component.html',
  styleUrls: ['./category-card.component.scss']
})
export class CategoryCardComponent implements OnInit {

  @Input() category!: Specification<string>;

  constructor() { }

  ngOnInit(): void {
  }

}
