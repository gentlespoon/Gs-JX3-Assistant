import { Component, OnInit, Input } from '@angular/core';
import { Item } from 'src/app/models/Item/item';
import { ColorService } from 'src/app/services/color/color.service';
import { ItemsService } from 'src/app/services/item/items/items.service';

@Component({
  selector: 'app-item-info',
  templateUrl: './item-info.component.html',
  styleUrls: ['./item-info.component.scss'],
})
export class ItemInfoComponent implements OnInit {
  constructor(
    private itemsService: ItemsService,
    private colorService: ColorService
  ) {}

  ngOnInit(): void {}

  close() {
    this._item = null;
  }

  _item: Item = null;
  @Input() set itemId(itemId: number) {
    this._item = this.itemsService.getItemById(itemId);
  }

  public get item(): Item {
    return this._item;
  }

  // itemInfoService passthrough
  // public get item(): Item {
  //   return this.itemInfoService.item;
  // }
  // colorService passthrough
  public get colors(): object {
    return this.colorService.colors;
  }

  public craft(itemId: number, skill: string[]) {
    this.close();
    console.log('Craft ', itemId, skill);
  }
}
