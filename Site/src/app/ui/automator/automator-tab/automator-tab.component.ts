import { Component, OnInit, Input } from '@angular/core';
import { faFish, faDownload } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-automator-tab',
  templateUrl: './automator-tab.component.html',
  styleUrls: ['./automator-tab.component.scss'],
})
export class AutomatorTabComponent implements OnInit {
  constructor() {}

  ngOnInit(): void {}

  @Input() mode: string = '';

  faFish = faFish;
  faDownload = faDownload;
}
