import { Coordinates } from './coordinates';
import { Color } from './color';

export class PixelColor {
  coordinates: Coordinates;
  color: Color;

  constructor(coordinates: Coordinates, color: Color) {
    this.coordinates = coordinates;
    this.color = color;
  }
}
