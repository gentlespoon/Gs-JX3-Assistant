import { Component, OnInit } from '@angular/core';
import { SkillService } from 'src/app/services/skills/skill.service';
import { faCaretDown } from '@fortawesome/free-solid-svg-icons';
import { Router } from '@angular/router';

@Component({
  selector: 'app-banner',
  templateUrl: './banner.component.html',
  styleUrls: ['./banner.component.scss'],
})
export class BannerComponent implements OnInit {
  constructor(public skillService: SkillService, public router: Router) {}

  ngOnInit(): void {}

  public get skills(): object {
    return this.skillService.skills;
  }

  public Object = Object;

  // fontAwesome
  public faCaretDown = faCaretDown;

  public get activatedRoute(): string {
    return this.router.url;
  }
}
