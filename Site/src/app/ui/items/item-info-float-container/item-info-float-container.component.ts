import { Component, OnInit } from '@angular/core';
import { ItemInfoFloatService } from 'src/app/services/items/item-info-float/item-info-float.service';

@Component({
  selector: 'app-item-info-float-container',
  templateUrl: './item-info-float-container.component.html',
  styleUrls: ['./item-info-float-container.component.scss'],
})
export class ItemInfoFloatContainerComponent implements OnInit {
  constructor(public itemInfoFloatService: ItemInfoFloatService) {}

  ngOnInit(): void {}
}
