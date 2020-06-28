import { Injectable } from '@angular/core';
import { ItemInfoFloat } from 'src/app/models/item/item-info-float/item-info-float';
import { Item } from 'src/app/models/item/item/Item';

@Injectable({
  providedIn: 'root',
})
export class ItemInfoFloatService {
  _floats: ItemInfoFloat[] = [];

  constructor() {}

  public get floats(): ItemInfoFloat[] {
    return this._floats;
  }

  public clear(): void {
    this._floats = [];
  }

  public add(item: Item, x: number, y: number): void {
    this.remove(item);
    console.log('Adding item to floating list ' + item.id);
    this._floats.push(new ItemInfoFloat(item, x, y));
    console.log();
  }

  public remove(item: Item): void {
    for (let i = this._floats.length - 1; i >= 0; i--) {
      if (this._floats[i].item.id == item.id) {
        console.log('Removing item from floating list ' + item.id);
        this._floats.splice(i, 1);
      }
    }
  }
}
