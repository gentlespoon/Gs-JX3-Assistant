import { ItemInfoComponent } from './item-info.component';
import { Item } from 'src/app/models/Item/item';
import { ColorService } from 'src/app/services/color/color.service';

describe('ItemInfoComponent', () => {
  let component: ItemInfoComponent;

  beforeEach(() => {
    let colorService = new ColorService();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
