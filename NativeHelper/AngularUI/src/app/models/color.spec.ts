import { Color } from './color';

describe('Color', () => {
  it('should create an instance', () => {
    expect(new Color(0, 0, 0)).toBeTruthy();
  });

  it('should not create an instance when color is out of range', () => {
    expect(() => {
      new Color(256, 256, 256);
    }).toThrowError('Invalid color');
  });
});
