import { Component, OnInit } from '@angular/core';
import { AutomatorService } from '../../services/automator/automator.service';
import {
  faPlay,
  faStop,
  faQuestionCircle,
} from '@fortawesome/free-solid-svg-icons';
import { ConnState } from '../../models/automator/conn-state.enum';

import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-automator',
  templateUrl: './automator.component.html',
  styleUrls: ['./automator.component.scss'],
})
export class AutomatorComponent implements OnInit {
  constructor(private activatedRoute: ActivatedRoute) {
    this.params = this.activatedRoute.params.subscribe((params) => {
      this.mode = params['mode'];
    });
  }

  ngOnInit(): void {}

  private params: any;
  public mode: string;
}
