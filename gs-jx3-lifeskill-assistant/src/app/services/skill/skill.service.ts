import { Injectable } from '@angular/core';
import { Skill } from 'src/app/models/skill/skill';

@Injectable({
  providedIn: 'root',
})
export class SkillService {
  verbose = true;
  public skills: Skill[] = [
    new Skill('fr', '缝纫'),
    new Skill('pr', '烹饪'),
    new Skill('ys', '医术'),
    new Skill('zj', '梓匠'),
    new Skill('zz', '铸造'),
  ];
  constructor() {
    console.log('Skills:', this.skills);
  }

  public getSkillByAbbr(abbr: string): Skill {
    if (this.verbose) console.log('Searching for skill abbr: ', abbr);
    let matchingSkill = this.skills.filter((x) => x.abbr === abbr);
    if (matchingSkill.length == 1) {
      return matchingSkill[0];
    } else {
      throw new Error('Skill not found');
    }
  }
}
