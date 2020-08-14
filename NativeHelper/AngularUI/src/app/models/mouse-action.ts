import { Coordinates } from './coordinates';

export class MouseAction {
  coordinates: Coordinates;
  MB: number;

  constructor(coordinates: Coordinates, MB: number) {
    this.coordinates = coordinates;
    this.MB = MB;
  }
}
