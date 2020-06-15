export class Item {
  ID: number;
  IsBounded: boolean = false;
  Name: string = '';
  Color: number;
  // JX3 使用了IconID，个人认为使用ItemID命名图标文件即可
  // IconID: number;
  Source: string;
  Components: number[];

  constructor(partialItem: Partial<Item>) {
    if (
      partialItem &&
      !isNaN(partialItem['ID']) &&
      partialItem['Name'] &&
      !isNaN(partialItem['Color'])
      // && partialItem['IconID']
    ) {
      Object.assign(this, partialItem);
    } else {
      throw new Error('Missing critical Item information');
    }
  }
}
