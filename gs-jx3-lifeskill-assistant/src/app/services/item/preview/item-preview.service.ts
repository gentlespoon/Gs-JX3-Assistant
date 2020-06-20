import { Injectable } from '@angular/core';
import { ItemsService } from '../items/items.service';

@Injectable({
  providedIn: 'root',
})
export class ItemPreviewService {
  constructor(private itemsService: ItemsService) {}

  list: number[] = [];
  show(itemId: number): void {
    if (this.itemsService.getItemById(itemId)) {
      this.list.push(itemId);
    }
  }
}
