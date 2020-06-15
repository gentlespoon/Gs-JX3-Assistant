import { ItemInfoComponent } from './item-info.component';
import { Item } from 'src/app/models/Item/item';
import { ColorService } from 'src/app/services/color/color.service';
import { ItemInfoService } from 'src/app/services/itemInfo/item-info.service';

class MockItemInfoService extends ItemInfoService {
  _item = new Item({ ID: 1, Name: 'TestItem', Color: 1 });
}

describe('ItemInfoComponent', () => {
  let component: ItemInfoComponent;

  beforeEach(() => {
    let mockItemInfoService = new MockItemInfoService(null);
    let colorService = new ColorService();
    component = new ItemInfoComponent(mockItemInfoService, colorService);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
