export class Color {
  R: number;
  G: number;
  B: number;

  constructor(R: number, G: number, B: number) {
    this.R = R;
    this.G = G;
    this.B = B;
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
