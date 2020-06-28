import { 精力 } from './精力';

describe('Currency/精力', () => {
  it('should create an instance (x > 0, to integer)', () => {
    var instance: 精力;

    instance = new 精力(1.1);
    expect(instance).toBeTruthy();
    expect(instance.amount == 1);
    expect(instance.toString()).toEqual('1 精力');

    instance = new 精力(1);
    expect(instance).toBeTruthy();
    expect(instance.amount == 1);
    expect(instance.toString()).toEqual('1 精力');
  });

  it('should NOT create an instance (x <= 0)', () => {
    expect(() => {
      new 精力(0);
    }).toThrow(new Error('精力<=0'));
    expect(() => {
      new 精力(-1);
    }).toThrow(new Error('精力<=0'));
  });
});
