import { 侠义值 } from './侠义值';

describe('Currency/侠义值', () => {
  it('should create an instance (x > 0, to integer)', () => {
    var instance: 侠义值;

    instance = new 侠义值(1.1);
    expect(instance).toBeTruthy();
    expect(instance.amount == 1);
    expect(instance.toString()).toEqual('1 侠义值');

    instance = new 侠义值(1);
    expect(instance).toBeTruthy();
    expect(instance.amount == 1);
    expect(instance.toString()).toEqual('1 侠义值');
  });

  it('should NOT create an instance (x <= 0)', () => {
    expect(() => {
      new 侠义值(0);
    }).toThrow(new Error('侠义值<=0'));
    expect(() => {
      new 侠义值(-1);
    }).toThrow(new Error('侠义值<=0'));
  });
});
