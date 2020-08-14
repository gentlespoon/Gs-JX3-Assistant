import { 师徒值 } from './师徒值';

describe('Currency/师徒值', () => {
  it('should create an instance (x > 0, to integer)', () => {
    var instance: 师徒值;

    instance = new 师徒值(1.1);
    expect(instance).toBeTruthy();
    expect(instance.amount == 1);
    expect(instance.toString()).toEqual('1 师徒值');

    instance = new 师徒值(1);
    expect(instance).toBeTruthy();
    expect(instance.amount == 1);
    expect(instance.toString()).toEqual('1 师徒值');
  });

  it('should NOT create an instance (x <= 0)', () => {
    expect(() => {
      new 师徒值(0);
    }).toThrow(new Error('师徒值<=0'));
    expect(() => {
      new 师徒值(-1);
    }).toThrow(new Error('师徒值<=0'));
  });
});
