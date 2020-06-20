import { Collect } from './collect';
import { Energy } from '../../currency/currency';

describe('Collect', () => {
  it('should create an instance', () => {
    expect(new Collect([], new Energy(0))).toBeTruthy();
  });
});
