import { 江湖贡献值 } from './江湖贡献值';

describe('Currency/江湖贡献值', () => {
  it('should create an instance (x > 0, to integer)', () => {
    var instance: 江湖贡献值;

    instance = new 江湖贡献值(1.1);
    expect(instance).toBeTruthy();
    expect(instance.amount == 1);
    expect(instance.toString()).toEqual('1 江湖贡献值');

    instance = new 江湖贡献值(1);
    expect(instance).toBeTruthy();
    expect(instance.amount == 1);
    expect(instance.toString()).toEqual('1 江湖贡献值');
  });

  it('should NOT create an instance (x <= 0)', () => {
    expect(() => {
      new 江湖贡献值(0);
    }).toThrow(new Error('江湖贡献值<=0'));
    expect(() => {
      new 江湖贡献值(-1);
    }).toThrow(new Error('江湖贡献值<=0'));
  });
});
