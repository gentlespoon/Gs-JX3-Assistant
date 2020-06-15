import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SkillService } from './services/skill/skill.service';
import { Skill } from './models/skill/skill';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  constructor(private router: Router, private skillService: SkillService) {}
  _currentRoute = 'intro';
  public get currentRoute(): string {
    return this._currentRoute;
  }
  public set currentRoute(v: string) {
    this._currentRoute = v;
    this.router.navigate([v]);
  }
  public get skills(): Skill[] {
    return this.skillService.skills;
  }
}
