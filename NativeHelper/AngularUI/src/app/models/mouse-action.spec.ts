import { MouseAction } from './mouse-action';
import { Coordinates } from './coordinates';

describe('MouseAction', () => {
  it('should create an instance', () => {
    expect(new MouseAction(new Coordinates(0, 0), 1)).toBeTruthy();
  });
});
