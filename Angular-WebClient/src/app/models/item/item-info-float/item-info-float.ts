import { Item } from '../item/Item';

export class ItemInfoFloat {
  item: Item;
  X: number;
  Y: number;

  constructor(item: Item, X: number, Y: number) {
    this.item = item;
    this.X = X;
    this.Y = Y;
  }
}
