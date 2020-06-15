import { Component, OnInit, Input } from '@angular/core';
import { ItemsService } from 'src/app/services/items/items.service';
import { ColorService } from 'src/app/services/color/color.service';
import { ItemInfoService } from 'src/app/services/itemInfo/item-info.service';
import { Item } from 'src/app/models/Item/item';

@Component({
  selector: 'app-item-thumbnail',
  templateUrl: './item-thumbnail.component.html',
  styleUrls: ['./item-thumbnail.component.scss'],
})
export class ItemThumbnailComponent implements OnInit {
  constructor(
    private itemsService: ItemsService,
    private colorService: ColorService,
    private itemInfoService: ItemInfoService
  ) {}

  ngOnInit(): void {}

  _item: Item = null;
  public get item(): Item {
    return this._item;
  }
  @Input() set itemId(itemId: number) {
    this._item = this.itemsService.getItemById(itemId);
  }

  // colorService passthrough
  public get colors(): string[] {
    return this.colorService.colors;
  }

  showItemInfo(itemId: number) {
    this.itemInfoService.itemId = itemId;
  }
}
