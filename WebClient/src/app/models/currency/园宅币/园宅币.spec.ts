import { 园宅币 } from './园宅币';

describe('Currency/园宅币', () => {
  it('Should create an instance (x > 0, to integer)', () => {
    var instance: 园宅币;

    instance = new 园宅币(1.1);
    expect(instance).toBeTruthy();
    expect(instance.amount == 1);
    expect(instance.toString()).toEqual('1 园宅币');

    instance = new 园宅币(1);
    expect(instance).toBeTruthy();
    expect(instance.amount == 1);
    expect(instance.toString()).toEqual('1 园宅币');
  });

  it('should NOT create an instance (x <= 0)', () => {
    expect(() => {
      new 园宅币(0);
    }).toThrow(new Error('园宅币<=0'));
    expect(() => {
      new 园宅币(-1);
    }).toThrow(new Error('园宅币<=0'));
  });
});
