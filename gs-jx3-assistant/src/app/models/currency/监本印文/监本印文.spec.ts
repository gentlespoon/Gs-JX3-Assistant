import { 监本印文 } from './监本印文';

describe('Currency/监本印文', () => {
  it('should create an instance (x > 0, to integer)', () => {
    var instance: 监本印文;

    instance = new 监本印文(1.1);
    expect(instance).toBeTruthy();
    expect(instance.amount == 1);
    expect(instance.toString()).toEqual('1 监本印文');

    instance = new 监本印文(1);
    expect(instance).toBeTruthy();
    expect(instance.amount == 1);
    expect(instance.toString()).toEqual('1 监本印文');
  });

  it('should NOT create an instance (x <= 0)', () => {
    expect(() => {
      new 监本印文(0);
    }).toThrow(new Error('监本印文<=0'));
    expect(() => {
      new 监本印文(-1);
    }).toThrow(new Error('监本印文<=0'));
  });
});
