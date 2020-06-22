import { Source } from '../../source/source.interface';
import { Color } from '../color.enum';

export class Item {
  id: number = 0;
  name: string = '';
  color: Color = 0;
  bond: boolean = false;
  description: string = '';
  source: Source[] = [];

  constructor(partialItem: Partial<Item>) {
    if (isNaN(partialItem.id)) {
      throw new Error('Invalid id');
    }
    if (!partialItem.name) {
      throw new Error('Invalid name');
    }
    Object.assign(this, partialItem);
  }
}
