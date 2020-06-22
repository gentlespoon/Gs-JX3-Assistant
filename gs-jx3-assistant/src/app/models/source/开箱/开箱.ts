import { Source } from '../source.interface';

export class 开箱 implements Source {
  name: string;
  explode() {
    throw new Error('Method not implemented.');
  }
}
