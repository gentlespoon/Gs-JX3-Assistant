import { Currency } from '../currency.interface';

export class 名剑币 implements Currency {
  name: '名剑币';
  amount: number;

  constructor(x: number) {
    if (x <= 0) {
      throw new Error('名剑币<=0');
    }
    this.amount = Math.floor(x);
  }

  toString(): string {
    return this.amount + ' 名剑币';
  }
}
