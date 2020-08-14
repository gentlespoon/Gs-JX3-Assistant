export class Color {
  R: number;
  G: number;
  B: number;

  constructor(R: number, G: number, B: number) {
    if (R >= 0 && R <= 255) this.R = R;
    else {
      throw new Error('Invalid color');
    }
    if (G >= 0 && G <= 255) this.G = G;
    else {
      throw new Error('Invalid color');
    }
    if (B >= 0 && B <= 255) this.B = B;
    else {
      throw new Error('Invalid color');
    }
  }

  public isSame(c: Color): boolean {
    let allowance = 16;
    if (this.R < c.R - allowance || this.R > c.R + allowance) {
      return false;
    }
    if (this.G < c.G - allowance || this.G > c.G + allowance) {
      return false;
    }
    if (this.R < c.B - allowance || this.R > c.B + allowance) {
      return false;
    }
    return true;
  }
}
