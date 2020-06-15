import { Component, OnInit } from '@angular/core';
import { ItemsService } from 'src/app/services/items.service';
import { Item } from 'src/app/models/Item/item';

@Component({
  selector: 'app-item-list',
  templateUrl: './item-list.component.html',
  styleUrls: ['./item-list.component.scss'],
})
export class ItemListComponent implements OnInit {
  color = {
    gray: false,
    white: true,
    green: true,
    blue: true,
    purple: true,
    orange: true,
  };

  public get items(): Item[] {
    return this.itemsService.items;
  }

  // pagination
  page = 1;
  pageSize = 50;

  constructor(public itemsService: ItemsService) {
    setInterval(() => {
      console.log(this.items);
    }, 1000);
  }

  ngOnInit(): void {}
}
