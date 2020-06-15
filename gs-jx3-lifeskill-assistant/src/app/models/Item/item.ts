import { Source } from '../source/source';
import { Component } from '../component/component';

export class Item {
  ID: number;
  IsBounded: boolean = false;
  Name: string = '';
  Color: number;
  Source: string[];
  Components: Component[];

  constructor(partialItem: Partial<Item>) {
    if (
      partialItem &&
      !isNaN(partialItem['ID']) &&
      partialItem['Name'] &&
      !isNaN(partialItem['Color'])
    ) {
      this.ID = parseInt(partialItem.ID + '');
      this.IsBounded = partialItem.IsBounded == true ? true : false;
      this.Name = partialItem.Name;
      this.Color = parseInt(partialItem.Color + '');
      this.Source = partialItem.Source;
      if (partialItem.Components) {
        this.Components = [];
        for (let component of partialItem.Components) {
          this.Components.push(new Component(component));
        }
      }
    } else {
      throw new Error('Missing critical item information');
    }
  }
}
