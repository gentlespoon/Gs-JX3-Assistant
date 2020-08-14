import { Component, OnInit, Input } from '@angular/core';
import { SkillService } from '../../../services/skills/skill.service';

@Component({
  selector: 'app-lifeskill-tab',
  templateUrl: './lifeskill-tab.component.html',
  styleUrls: ['./lifeskill-tab.component.scss'],
})
export class LifeskillTabComponent implements OnInit {
  constructor(public skillService: SkillService) {}

  ngOnInit(): void {}
  @Input() mode: string = '';

  public get skills(): object {
    return this.skillService.skills;
  }

  public Object = Object;
}
