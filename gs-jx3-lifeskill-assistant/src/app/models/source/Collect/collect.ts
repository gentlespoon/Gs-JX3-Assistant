import { Energy } from '../../currency/currency';

export class Collect {
  map: string[] = [];
  energy: Energy;
  constructor(map: string[], energy: Energy) {
    this.map = map;
    this.energy = energy;
  }
}
