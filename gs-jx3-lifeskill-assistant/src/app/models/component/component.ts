import { Item } from '../Item/item';

export class Component {
  itemId: Item;
  amount: number;
  constructor(partialComponent: Partial<Component>) {
    if (
      partialComponent &&
      partialComponent.itemId &&
      partialComponent.amount
    ) {
      Object.assign(this, partialComponent);
    } else {
      throw new Error('Missing critical component information');
    }
  }
}
