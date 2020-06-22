import { Currency } from '../currency.interface';

export class 师徒值 implements Currency {
  name = '师徒值';
  amount: number;

  constructor(x: number) {
    if (x <= 0) {
      throw new Error('师徒值<=0');
    }
    this.amount = Math.floor(x);
  }

  toString(): string {
    return this.amount + ' 师徒值';
  }
}
