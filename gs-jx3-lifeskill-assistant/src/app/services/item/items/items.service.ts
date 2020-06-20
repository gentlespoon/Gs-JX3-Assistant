import { Injectable } from '@angular/core';
import { Item } from '../../../models/Item/item';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ApiResponse } from '../../../models/ApiResponse/api-response';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class ItemsService {
  verbose = true;

  constructor(private httpClient: HttpClient) {
    this.getAllItemsFromServer();
  }

  items: Item[] = [];

  private getAllItemsFromServer() {
    this.httpClient
      .get(environment.baseURL + 'allItems')
      .subscribe((response) => {
        let apiResponse = new ApiResponse(response);
        this.items = [];
        for (let rawItem of apiResponse.data) {
          this.items.push(new Item(rawItem));
        }
        if (this.verbose) console.log('Items:', this.items);
      });
  }

  public getItemById(itemId: number): Item {
    if (this.verbose) console.log('Searching for item ID: ', itemId);
    let matchingItem = this.items.filter((x) => x.id === itemId);
    if (matchingItem.length == 1) {
      return matchingItem[0];
    } else {
      throw new Error('Item not found');
    }
  }
}
