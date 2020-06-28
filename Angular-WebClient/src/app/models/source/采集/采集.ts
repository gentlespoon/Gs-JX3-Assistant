import { Source } from '../source.interface';

export class 采集 implements Source {
  name: string;
  explode() {
    throw new Error('Method not implemented.');
  }
}
