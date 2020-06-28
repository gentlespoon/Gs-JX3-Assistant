import { Injectable } from '@angular/core';
import { Item } from 'src/app/models/item/item/Item';
import { Color } from 'src/app/models/item/color.enum';

@Injectable({
  providedIn: 'root',
})
export class ItemListService {
  private _allItems: Item[] = [];

  public get allItems(): Item[] {
    return this._allItems;
  }

  constructor() {
    this._allItems.push(
      new Item({
        id: 35566,
        name: '木料',
        color: Color.Green,
        description: '木料，做模板的材料 = -',
        bond: true,
      })
    );
    this._allItems.push(
      new Item({
        id: 35567,
        name: '芦苇杆',
        color: Color.Green,
        description: '芦苇杆可以在丐帮采到_(:з」∠)_',
        bond: true,
      })
    );
    this._allItems.push(
      new Item({
        id: 35568,
        name: '竹子',
        color: Color.Green,
        description: '只是测试而已啊为什么要写',
      })
    );
    this._allItems.push(
      new Item({
        id: 35703,
        name: '绳索',
        color: Color.Green,
        description: '不写了',
      })
    );
    this._allItems.push(
      new Item({
        id: 35705,
        name: '楔子',
        color: Color.Green,
        description: '_(:з」∠)_',
      })
    );
    this._allItems.push(
      new Item({
        id: 35707,
        name: '清漆',
        color: Color.Green,
        description: '╭(╯^╰)╮',
      })
    );
    this._allItems.push(
      new Item({ id: 35712, name: '木板', color: Color.Blue, description: '' })
    );
    this._allItems.push(
      new Item({ id: 35713, name: '卯榫', color: Color.Blue, description: '' })
    );
  }

  public getItemById(itemId: number): Item {
    let filteredItems = this._allItems.filter((item) => item.id == itemId);
    if (filteredItems.length == 1) {
      return filteredItems[0];
    } else {
      throw new Error('Item not found');
    }
  }
}
