import { Item } from '../../Item/item';

export class Component {
  itemId: number;
  amount: number;
  constructor(itemId: number, amount: number) {
    this.itemId = itemId;
    this.amount = amount;
  }
}
