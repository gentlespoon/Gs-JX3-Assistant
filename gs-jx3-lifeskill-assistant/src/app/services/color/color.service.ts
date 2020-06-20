import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ColorService {
  verbose = true;
  colors: object = {
    灰: 'gray',
    白: 'white',
    绿: 'green',
    蓝: 'blue',
    紫: 'purple',
    橙: 'orange',
  };

  constructor() {
    if (this.verbose) console.log('Colors:', this.colors);
  }
}
