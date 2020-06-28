import { ItemInfoFloat } from './item-info-float';
import { Item } from '../item/Item';

describe('ItemInfoFloat', () => {
  it('should create an instance', () => {
    expect(
      new ItemInfoFloat(new Item(0, '测试道具1', 0, []), 0, 0)
    ).toBeTruthy();
  });
});
