import { Energy, Money } from './currency';

describe('Currency', () => {
  // 精力
  it('should create an Energy instance', () => {
    expect(new Energy(1)).toBeTruthy();
  });

  it('should return Energy string', () => {
    let e = new Energy(1);
    let expected = '1 精力';
    expect(e.toString()).toEqual(expected);
  });

  it('should return advanced Energy string', () => {
    let e = new Energy(1);
    let expected = '1 精力';
    expect(e.toAdvancedString()).toEqual(expected);
  });

  // 金钱
  it('should create a Money instance', () => {
    expect(new Money(1)).toBeTruthy();
  });

  it('should return Money string', () => {
    let e: Money;
    let expected: string;

    e = new Money(1);
    expected = '1 金';
    expect(e.toString()).toEqual(expected);
    e = new Money(10000);
    expected = '10000 金';
    expect(e.toString()).toEqual(expected);
    e = new Money(0.0001);
    expected = '0.0001 金';
    expect(e.toString()).toEqual(expected);
  });

  it('should return advanced Money string', () => {
    let e: Money;
    let expected: string;

    // 1单位测试
    e = new Money(10000);
    expected = '1 砖';
    expect(e.toAdvancedString()).toEqual(expected);

    e = new Money(2345);
    expected = '2345 金';
    expect(e.toAdvancedString()).toEqual(expected);

    e = new Money(0.67);
    expected = '67 银';
    expect(e.toAdvancedString()).toEqual(expected);

    e = new Money(0.0089);
    expected = '89 铜';
    expect(e.toAdvancedString()).toEqual(expected);

    // 2单位测试
    e = new Money(12345);
    expected = '1 砖 2345 金';
    expect(e.toAdvancedString()).toEqual(expected);

    e = new Money(10000.67);
    expected = '1 砖 67 银';
    expect(e.toAdvancedString()).toEqual(expected);

    e = new Money(10000.0089);
    expected = '1 砖 89 铜';
    expect(e.toAdvancedString()).toEqual(expected);

    e = new Money(2345.67);
    expected = '2345 金 67 银';
    expect(e.toAdvancedString()).toEqual(expected);

    e = new Money(2345.0089);
    expected = '2345 金 89 铜';
    expect(e.toAdvancedString()).toEqual(expected);

    e = new Money(0.6789);
    expected = '67 银 89 铜';
    expect(e.toAdvancedString()).toEqual(expected);

    // 3单位测试
    e = new Money(12345.67);
    expected = '1 砖 2345 金 67 银';
    expect(e.toAdvancedString()).toEqual(expected);

    e = new Money(10000.6789);
    expected = '1 砖 67 银 89 铜';
    expect(e.toAdvancedString()).toEqual(expected);

    e = new Money(12345.0089);
    expected = '1 砖 2345 金 89 铜';
    expect(e.toAdvancedString()).toEqual(expected);

    e = new Money(2345.6789);
    expected = '2345 金 67 银 89 铜';
    expect(e.toAdvancedString()).toEqual(expected);

    // 4单位测试
    e = new Money(12345.6789);
    expected = '1 砖 2345 金 67 银 89 铜';
    expect(e.toAdvancedString()).toEqual(expected);
  });
});
