import { Component, OnInit } from '@angular/core';
import { ItemsService } from 'src/app/services/items/items.service';
import { ColorService } from 'src/app/services/color/color.service';
import { Item } from 'src/app/models/Item/item';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ItemInfoComponent } from '../item-info/item-info.component';
import { ItemInfoService } from 'src/app/services/itemInfo/item-info.service';

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
    private colorService: ColorService,
    private itemInfoService: ItemInfoService
  ) {}

  ngOnInit(): void {}

  // itemsService passthrough
  public get items(): Item[] {
    return this.itemsService.items;
  }
  // colorService passthrough
  public get colors(): string[] {
    return this.colorService.colors;
  }

  public get filteredItems(): Item[] {
    return this.itemsService.items.filter((x) => {
      return (
        x.Name.indexOf(this.filteredKeyword) !== -1 &&
        this.filteredColor[this.colors[x.Color]]
      );
    });
  }
}
