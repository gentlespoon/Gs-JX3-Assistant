import { Item } from './item';

describe('Item', () => {
  it('should create an instance if critical partial item information is provided', () => {
    expect(
      new Item({
        ID: 0,
        Name: 'TestItem',
        Color: 0,
      })
    ).toBeTruthy();
  });

  it('should throw error when one or more critical partial item information is missing', () => {
    var expectedError = new Error('Missing critical item information');
    expect(() => {
      new Item({
        // ID: 0,
        Name: 'TestItem',
        Color: 0,
      });
    }).toThrow(expectedError);

    expect(() => {
      new Item({
        ID: 0,
        // Name: 'TestItem',
        Color: 0,
      });
    }).toThrow(expectedError);

    expect(() => {
      new Item({
        ID: 0,
        Name: 'TestItem',
        // Color: 0,
      });
    }).toThrow(expectedError);

    expect(() => {
      new Item({});
    }).toThrow(expectedError);

    expect(() => {
      new Item(null);
    }).toThrow(expectedError);
  });
});
