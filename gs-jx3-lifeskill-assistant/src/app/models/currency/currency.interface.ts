export interface Currency {
  Amount: number;
  Unit: string;
  toString(): string;
  toAdvancedString(): string;
}
