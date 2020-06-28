import { Currency } from '../currency.interface';

export class 江湖贡献值 implements Currency {
  name = '江湖贡献值';
  amount: number;

  constructor(x: number) {
    if (x <= 0) {
      throw new Error('江湖贡献值<=0');
    }
    this.amount = Math.floor(x);
  }

  toString(): string {
    return this.amount + ' 江湖贡献值';
  }
}
