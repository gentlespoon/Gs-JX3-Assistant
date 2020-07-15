import { Component, OnInit } from '@angular/core';
import { Item } from 'src/app/models/item/item/Item';
import { ItemListService } from 'src/app/services/items/item-list/item-list.service';

import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-item-info-detail',
  templateUrl: './item-info-detail.component.html',
  styleUrls: ['./item-info-detail.component.scss'],
})
export class ItemInfoDetailComponent implements OnInit {
  public item: Item = null;

  constructor(
    private itemsService: ItemListService,
    private route: ActivatedRoute
  ) {}

  private set itemId(itemId: string) {
    console.log(itemId);
    let numItemId = parseInt(itemId);
    this.item = this.itemsService.getItemById(numItemId);
  }

  ngOnInit(): void {
    this.itemId = this.route.snapshot.paramMap.get('itemId');
  }
}
