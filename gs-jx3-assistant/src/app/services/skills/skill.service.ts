import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SkillService {
  _skills = { ys: '医术', zz: '铸造', fr: '缝纫', pr: '烹饪', zj: '梓匠' };

  constructor() {}

  public get skills(): object {
    return this._skills;
  }
}
