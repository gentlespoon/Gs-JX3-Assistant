import { Source } from '../source.interface';

export class 制作 implements Source {
  name: string;
  explode() {
    throw new Error('Method not implemented.');
  }
}
