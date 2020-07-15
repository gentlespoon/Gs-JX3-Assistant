import { Currency } from '../currency.interface';

export class 侠义值 implements Currency {
  name = '侠义值';
  amount: number;

  constructor(x: number) {
    if (x <= 0) {
      throw new Error('侠义值<=0');
    }
    this.amount = Math.floor(x);
  }

  toString(): string {
    return this.amount + ' 侠义值';
  }
}
