import { Component, OnInit, Input } from '@angular/core';
import { ItemsService } from 'src/app/services/item/items/items.service';
import { ColorService } from 'src/app/services/color/color.service';
import { Item } from 'src/app/models/Item/item';
import { ItemPreviewService } from 'src/app/services/item/preview/item-preview.service';

@Component({
  selector: 'app-item-name',
  templateUrl: './item-name.component.html',
  styleUrls: ['./item-name.component.scss'],
})
export class ItemNameComponent implements OnInit {
  constructor(
    private itemsService: ItemsService,
    private colorService: ColorService,
    private itemPreviewService: ItemPreviewService
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
  public get colors(): object {
    return this.colorService.colors;
  }

  showItemInfo(itemId: number) {
    this.itemPreviewService.show(itemId);
  }
}
