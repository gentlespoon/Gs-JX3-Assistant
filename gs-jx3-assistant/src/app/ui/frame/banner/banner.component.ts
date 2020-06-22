import { Component, OnInit } from '@angular/core';
import { SkillService } from 'src/app/services/skills/skill.service';
import { faCaretDown } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-banner',
  templateUrl: './banner.component.html',
  styleUrls: ['./banner.component.scss'],
})
export class BannerComponent implements OnInit {
  constructor(public skillService: SkillService) {}

  ngOnInit(): void {}

  public get skills(): object {
    return this.skillService.skills;
  }

  public Object = Object;

  // fontAwesome
  public faCaretDown = faCaretDown;
}
