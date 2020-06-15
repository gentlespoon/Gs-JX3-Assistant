import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ColorService {
  verbose = true;
  colors: string[] = ['gray', 'white', 'green', 'blue', 'purple', 'orange'];

  constructor() {
    if (this.verbose) console.log('Colors:', this.colors);
  }
}
