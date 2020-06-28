import { Component, OnInit, Input } from '@angular/core';
import { Item } from 'src/app/models/item/item/Item';
import { ItemInfoFloatService } from 'src/app/services/items/item-info-float/item-info-float.service';

@Component({
  selector: 'app-item-thumbnail',
  templateUrl: './item-thumbnail.component.html',
  styleUrls: ['./item-thumbnail.component.scss'],
})
export class ItemThumbnailComponent implements OnInit {
  @Input() item: Item;

  constructor(private itemInfoFloatService: ItemInfoFloatService) {}

  ngOnInit(): void {}

  click(e: MouseEvent, item: Item) {
    // console.log('Floating info for item ' + item.id);
    this.itemInfoFloatService.add(item, e.pageX + 15, e.pageY + 15);
  }

  // mouseLeave(e: MouseEvent, item: Item) {
  //   this.itemInfoFloatService.remove(item);
  // }
}
