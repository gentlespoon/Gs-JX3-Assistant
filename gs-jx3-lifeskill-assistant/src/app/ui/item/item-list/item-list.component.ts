import { Component, OnInit } from '@angular/core';
import { ItemsService } from 'src/app/services/item/items/items.service';
import { ColorService } from 'src/app/services/color/color.service';
import { Item } from 'src/app/models/Item/item';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ItemInfoComponent } from '../item-info/item-info.component';

@Component({
  selector: 'app-item-list',
  templateUrl: './item-list.component.html',
  styleUrls: ['./item-list.component.scss'],
})
export class ItemListComponent implements OnInit {
  filteredColor = {
    gray: false,
    white: true,
    green: true,
    blue: true,
    purple: true,
    orange: true,
  };

  // pagination
  page = 1;
  pageSize = 50;
  filteredKeyword = '';

  constructor(
    private itemsService: ItemsService,
    private colorService: ColorService
  ) {}

  ngOnInit(): void {}

  // itemsService passthrough
  public get items(): Item[] {
    return this.itemsService.items;
  }
  // colorService passthrough
  public get colors(): object {
    return this.colorService.colors;
  }

  public get filteredItems(): Item[] {
    return this.itemsService.items.filter((x) => {
      return (
        x.name.indexOf(this.filteredKeyword) !== -1 &&
        this.filteredColor[this.colors[x.color]]
      );
    });
  }
}
