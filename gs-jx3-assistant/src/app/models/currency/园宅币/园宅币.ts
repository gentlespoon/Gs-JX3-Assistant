import { Currency } from '../currency.interface';

export class 园宅币 implements Currency {
  name = '园宅币';
  amount: number;

  constructor(x: number) {
    if (x <= 0) {
      throw new Error('园宅币<=0');
    }
    this.amount = Math.floor(x);
  }

  toString(): string {
    return this.amount + ' 园宅币';
  }
}
