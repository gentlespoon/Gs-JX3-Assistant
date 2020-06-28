import { 金钱 } from './金钱';

describe('Currency/金钱', () => {
  it('should create an instance (x > 0)', () => {
    let instance: 金钱;

    // 1单位

    instance = new 金钱(0.0001);
    expect(instance).toBeTruthy();
    expect(instance.amount).toEqual(0.0001);
    expect(instance.toString()).toEqual('1 铜');

    instance = new 金钱(0.01);
    expect(instance).toBeTruthy();
    expect(instance.amount).toEqual(0.01);
    expect(instance.toString()).toEqual('1 银');

    instance = new 金钱(1);
    expect(instance).toBeTruthy();
    expect(instance.amount).toEqual(1);
    expect(instance.toString()).toEqual('1 金');

    instance = new 金钱(10000);
    expect(instance).toBeTruthy();
    expect(instance.amount).toEqual(10000);
    expect(instance.toString()).toEqual('1 砖');

    // 2单位

    instance = new 金钱(0.0101);
    expect(instance).toBeTruthy();
    expect(instance.amount).toEqual(0.0101);
    expect(instance.toString()).toEqual('1 银 1 铜');

    instance = new 金钱(1.0001);
    expect(instance).toBeTruthy();
    expect(instance.amount).toEqual(1.0001);
    expect(instance.toString()).toEqual('1 金 1 铜');

    instance = new 金钱(10000.0001);
    expect(instance).toBeTruthy();
    expect(instance.amount).toEqual(10000.0001);
    expect(instance.toString()).toEqual('1 砖 1 铜');

    instance = new 金钱(1.01);
    expect(instance).toBeTruthy();
    expect(instance.amount).toEqual(1.01);
    expect(instance.toString()).toEqual('1 金 1 银');

    instance = new 金钱(10000.01);
    expect(instance).toBeTruthy();
    expect(instance.amount).toEqual(10000.01);
    expect(instance.toString()).toEqual('1 砖 1 银');

    instance = new 金钱(10001);
    expect(instance).toBeTruthy();
    expect(instance.amount).toEqual(10001);
    expect(instance.toString()).toEqual('1 砖 1 金');

    // 3单位

    instance = new 金钱(1.0101);
    expect(instance).toBeTruthy();
    expect(instance.amount).toEqual(1.0101);
    expect(instance.toString()).toEqual('1 金 1 银 1 铜');

    instance = new 金钱(10000.0101);
    expect(instance).toBeTruthy();
    expect(instance.amount).toEqual(10000.0101);
    expect(instance.toString()).toEqual('1 砖 1 银 1 铜');

    instance = new 金钱(10001.0001);
    expect(instance).toBeTruthy();
    expect(instance.amount).toEqual(10001.0001);
    expect(instance.toString()).toEqual('1 砖 1 金 1 铜');

    instance = new 金钱(10001.01);
    expect(instance).toBeTruthy();
    expect(instance.amount).toEqual(10001.01);
    expect(instance.toString()).toEqual('1 砖 1 金 1 银');

    // 4单位

    instance = new 金钱(10001.0101);
    expect(instance).toBeTruthy();
    expect(instance.amount).toEqual(10001.0101);
    expect(instance.toString()).toEqual('1 砖 1 金 1 银 1 铜');
  });

  it('should NOT create an instance (x <= 0)', () => {
    expect(() => {
      new 金钱(-1);
    }).toThrow(new Error('金钱<=0'));
  });
});
