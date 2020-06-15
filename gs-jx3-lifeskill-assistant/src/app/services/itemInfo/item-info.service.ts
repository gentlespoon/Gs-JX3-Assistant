import { Injectable } from '@angular/core';
import { Item } from 'src/app/models/Item/item';
import { ItemsService } from '../items/items.service';

@Injectable({
  providedIn: 'root',
})
export class ItemInfoService {
  constructor(private itemsService: ItemsService) {}

  _item: Item = null;
  public get item(): Item {
    return this._item;
  }

  public set itemId(itemId: number | null) {
    if (itemId === null) {
      this._item = null;
    } else {
      this._item = this.itemsService.getItemById(itemId);
    }
  }
}
