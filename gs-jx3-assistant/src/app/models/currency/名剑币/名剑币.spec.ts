import { 名剑币 } from './名剑币';

describe('Currency/名剑币', () => {
  it('should create an instance (x > 0, to integer)', () => {
    var instance: 名剑币;

    instance = new 名剑币(1.1);
    expect(instance).toBeTruthy();
    expect(instance.amount == 1);
    expect(instance.toString()).toEqual('1 名剑币');

    instance = new 名剑币(1);
    expect(instance).toBeTruthy();
    expect(instance.amount == 1);
    expect(instance.toString()).toEqual('1 名剑币');
  });

  it('should NOT create an instance (x <= 0)', () => {
    expect(() => {
      new 名剑币(0);
    }).toThrow(new Error('名剑币<=0'));
    expect(() => {
      new 名剑币(-1);
    }).toThrow(new Error('名剑币<=0'));
  });
});
