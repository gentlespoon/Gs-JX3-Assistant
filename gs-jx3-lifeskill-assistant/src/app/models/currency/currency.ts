import { Currency } from './currency.interface';

export class Energy implements Currency {
  Amount: number = 0;
  Unit: string = '精力';
  constructor(amount: number) {
    this.Amount = amount;
  }

  public toString(): string {
    return `${this.Amount} ${this.Unit}`;
  }
  public toAdvancedString(): string {
    return this.toString();
  }
}
