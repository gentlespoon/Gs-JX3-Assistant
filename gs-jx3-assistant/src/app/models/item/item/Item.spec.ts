import { Item } from './Item';
import { Color } from '../color.enum';

describe('Item', () => {
  it('should create an instance - {id: 0, name: 测试道具, color: Color.Blue}', () => {
    let instance = new Item({ id: 0, name: '测试道具', color: Color.Blue });
    expect(instance).toBeTruthy();
  });
});
