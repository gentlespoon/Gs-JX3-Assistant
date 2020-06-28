import { Currency } from '../currency.interface';

export class 金钱 implements Currency {
  name = '金钱';
  amount: number;

  constructor(x: number) {
    if (x <= 0) {
      throw new Error('金钱<=0');
    }
    this.amount = x;
  }

  toString(): string {
    let remainder = this.amount;
    let output = '';
    let x = 0;
    x = Math.floor(remainder / 10000);
    remainder %= 10000;
    if (x > 0) {
      output += ` ${x} 砖`;
    }
    x = Math.floor(remainder / 1);
    remainder %= 1;
    if (x > 0) {
      output += ` ${x} 金`;
    }
    x = Math.floor(remainder / 0.01);
    remainder %= 0.01;
    if (x > 0) {
      output += ` ${x} 银`;
    }
    x = Math.round(remainder / 0.0001);
    if (x > 0) {
      output += ` ${x} 铜`;
    }
    return output.trim();
  }
}
