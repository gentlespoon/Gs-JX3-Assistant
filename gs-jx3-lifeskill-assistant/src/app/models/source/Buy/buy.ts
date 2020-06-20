import { Currency } from '../../currency/currency.interface';

export class Buy {
  from: string = '';
  price: Currency[] = [];
  constructor(from: string, price: Currency[]) {
    this.from = from;
    this.price = price;
  }
}
