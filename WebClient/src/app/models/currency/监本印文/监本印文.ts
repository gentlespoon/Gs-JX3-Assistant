import { Currency } from '../currency.interface';

export class 监本印文 implements Currency {
  name = '监本印文';
  amount: number;

  constructor(x: number) {
    if (x <= 0) {
      throw new Error('监本印文<=0');
    }
    this.amount = Math.floor(x);
  }

  toString(): string {
    return this.amount + ' 监本印文';
  }
}
