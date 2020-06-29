import { Component, OnInit } from '@angular/core';
import { AutomatorService } from '../../../services/automator/automator.service';
import {
  faPlay,
  faStop,
  faQuestionCircle,
  faExclamationCircle,
  faEye,
} from '@fortawesome/free-solid-svg-icons';
import {
  faTimesCircle as farTimesCircle,
  faCheckCircle as farCheckCircle,
  faEyeSlash as farEyeSlash,
} from '@fortawesome/free-regular-svg-icons';
import { ConnState } from '../../../models/nativeHelper/connection/conn-state.enum';

@Component({
  selector: 'app-automator-status',
  templateUrl: './automator-status.component.html',
  styleUrls: ['./automator-status.component.scss'],
})
export class AutomatorStatusComponent implements OnInit {
  constructor(private automatorService: AutomatorService) {}

  ngOnInit(): void {}
  faPlay = faPlay;
  faStop = faStop;
  faQuestionCircle = faQuestionCircle;
  farTimesCircle = farTimesCircle;
  farCheckCircle = farCheckCircle;
  faExclamationCircle = faExclamationCircle;
  faEye = faEye;
  farEyeSlash = farEyeSlash;

  ConnState = ConnState;

  public get visibility(): boolean {
    return this.automatorService.visibility;
  }
  public set visibility(v: boolean) {
    this.automatorService.visibility = v;
  }

  public get connState(): ConnState {
    if (!this.automatorService.isConnected) {
      return ConnState.none;
    } else {
      if (this.automatorService.heartBeatFailure == 0) {
        return ConnState.connected;
      } else {
        return ConnState.disconnected;
      }
    }
  }

  public get retryCounter(): number {
    return this.automatorService.heartBeatFailure;
  }

  launch() {
    this.automatorService.launch();
  }

  shutdown() {
    this.automatorService.shutdown();
  }
}
