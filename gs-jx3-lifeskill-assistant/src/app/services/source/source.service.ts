import { Injectable } from '@angular/core';
import { Skill } from 'src/app/models/skill/skill';
import { SkillService } from '../skill/skill.service';
import { Source } from 'src/app/models/source/source';

@Injectable({
  providedIn: 'root',
})
export class SourceService {
  verbose = true;

  sources: Source[] = [];

  constructor(private skillService: SkillService) {
    this.sources = [
      new Source('sn', '神农'),
      new Source('cj', '采金'),
      new Source('pd', '庖丁'),
    ];
    for (let skill of this.skillService.skills) {
      this.sources.push(new Source(skill.abbr, skill.name));
    }
    if (this.verbose) console.log('Sources:', this.sources);
  }

  public getSourceByAbbr(abbr: string): Source {
    if (this.verbose) console.log('Searching for source abbr: ', abbr);
    let matchingSource = this.sources.filter((x) => x.abbr === abbr);
    if (matchingSource.length == 1) {
      return matchingSource[0];
    } else {
      throw new Error('Source not found');
    }
  }
}
