import { Component } from './Component/component';
import { Collect } from './Collect/collect';
import { Currency } from '../currency/currency.interface';

export class Source {
  Name: string = '';
  Components: Component[] = [];
  Collect: Collect[] = [];
  Cost: Currency[] = [];
  Other: string = '';

  constructor(source: object) {
    if (!source) {
      throw new Error('');
    }
    switch (source['source']) {
      case '铸造':
      case '医术':
      case '烹饪':
      case '缝纫':
      case '梓匠':
        for (let component of source['component']) {
          this.Components.push(
            new Component(component['itemId'], component['amount'])
          );
        }
        break;
      case '神农':
      case '采金':
      case '庖丁':
        for (let collect of source['collect']) {
          this.Collect.push(new Collect(collect['map'], collect['energy']));
        }
      case '购买':
        for ()
      default:
        this.Other = source['other'];
    }
  }
}
