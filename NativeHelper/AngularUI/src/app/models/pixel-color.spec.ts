import { PixelColor } from './pixel-color';
import { Coordinates } from './coordinates';
import { Color } from './color';

describe('PixelColor', () => {
  it('should create an instance', () => {
    expect(
      new PixelColor(new Coordinates(0, 0), new Color(0, 0, 0))
    ).toBeTruthy();
  });
});
