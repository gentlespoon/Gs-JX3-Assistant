import { 威望 } from './威望';

describe('Currency/威望', () => {
  it('should create an instance (x > 0, to integer)', () => {
    var instance: 威望;

    instance = new 威望(1.1);
    expect(instance).toBeTruthy();
    expect(instance.amount == 1);
    expect(instance.toString()).toEqual('1 威望');

    instance = new 威望(1);
    expect(instance).toBeTruthy();
    expect(instance.amount == 1);
    expect(instance.toString()).toEqual('1 威望');
  });

  it('should NOT create an instance (x <= 0)', () => {
    expect(() => {
      new 威望(0);
    }).toThrow(new Error('威望<=0'));
    expect(() => {
      new 威望(-1);
    }).toThrow(new Error('威望<=0'));
  });
});
