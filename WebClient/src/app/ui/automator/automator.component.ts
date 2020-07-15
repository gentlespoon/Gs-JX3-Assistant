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
  constructor(
    private activatedRoute: ActivatedRoute,
    private automatorService: AutomatorService
  ) {
    this.activatedRoute.params.subscribe((urlVariables) => {
      this.mode = urlVariables['mode'];
    });
    this.activatedRoute.queryParams.subscribe((queryParams) => {
      // console.log(params);
      if (queryParams['port']) {
        var port = parseInt(queryParams['port']);
        console.log('Connect mode: port = ' + port);
        this.automatorService.connect(port);
      }
    });
  }

  ngOnInit(): void {}

  public mode: string;
}
