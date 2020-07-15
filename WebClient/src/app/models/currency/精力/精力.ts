import { Currency } from '../currency.interface';

export class 精力 implements Currency {
  name = '精力';
  amount: number;

  constructor(x: number) {
    if (x <= 0) {
      throw new Error('精力<=0');
    }
    this.amount = Math.floor(x);
  }

  toString(): string {
    return this.amount + ' 精力';
  }
}
