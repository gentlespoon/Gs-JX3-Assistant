import { Source } from '../source/source';

export class Item {
  id: number;
  isBounded: boolean = false;
  name: string = '';
  color: number;
  sources: Source[];

  constructor(partialItem: object) {
    if (
      partialItem &&
      !isNaN(partialItem['ID']) &&
      partialItem['Name'] &&
      partialItem['Color']
    ) {
      this.id = parseInt(partialItem['ID'] + '');
      this.isBounded = partialItem['IsBounded'] == true ? true : false;
      this.name = partialItem['Name'];
      this.color = partialItem['Color'];
      let sources = partialItem['Source'];
      if (sources) {
        for (let source of sources) {
          this.sources.push(new Source(source));
        }
      }
    } else {
      throw new Error('Missing critical item information');
    }
  }
}
