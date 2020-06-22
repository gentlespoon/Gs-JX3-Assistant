import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ItemListService } from 'src/app/services/items/item-list/item-list.service';
import { Item } from 'src/app/models/item/item/Item';

@Component({
  selector: 'app-item-list',
  templateUrl: './item-list.component.html',
  styleUrls: ['./item-list.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class ItemListComponent implements OnInit {
  constructor(public itemsService: ItemListService) {}

  ngOnInit(): void {}

  public get items(): Item[] {
    return this.itemsService.allItems;
  }
}
