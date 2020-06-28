import { Component, OnInit } from '@angular/core';
import { AutomatorService } from 'src/app/services/automator/automator.service';
import { faDownload, faPlay } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-automator',
  templateUrl: './automator.component.html',
  styleUrls: ['./automator.component.scss'],
})
export class AutomatorComponent implements OnInit {
  faDownload = faDownload;
  faPlay = faPlay;

  constructor(public automatorService: AutomatorService) {}

  ngOnInit(): void {}

  launch() {
    this.automatorService.launch();
  }

  hideWindow(hide: boolean) {
    this.automatorService.hideWindow(hide);
  }

  download() {
    this.automatorService.download();
  }

  getCursorCoordinates() {
    this.automatorService.getCursorCoordinates();
  }

  mouseClickAt(x: number, y: number) {
    this.automatorService.mouseClickAt(x, y);
  }
}
