import { Component, OnInit } from '@angular/core';
import { SkillService } from '../../services/skills/skill.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-lifeskill',
  templateUrl: './lifeskill.component.html',
  styleUrls: ['./lifeskill.component.scss'],
})
export class LifeskillComponent implements OnInit {
  constructor(private activatedRoute: ActivatedRoute) {
    this.params = this.activatedRoute.params.subscribe((params) => {
      this.mode = params['mode'];
    });
  }

  ngOnInit(): void {}

  private params: any;

  public mode: string;
}
