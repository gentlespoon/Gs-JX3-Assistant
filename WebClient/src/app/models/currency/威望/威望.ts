import { Currency } from '../currency.interface';

export class 威望 implements Currency {
  name = '威望';
  amount: number;

  constructor(x: number) {
    if (x <= 0) {
      throw new Error('威望<=0');
    }
    this.amount = Math.floor(x);
  }

  toString(): string {
    return this.amount + ' 威望';
  }
}
