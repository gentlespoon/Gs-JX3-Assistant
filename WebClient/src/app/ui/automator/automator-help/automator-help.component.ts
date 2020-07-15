import { Component, OnInit } from '@angular/core';
import { faDownload } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-automator-help',
  templateUrl: './automator-help.component.html',
  styleUrls: ['./automator-help.component.scss'],
})
export class AutomatorHelpComponent implements OnInit {
  constructor() {}

  ngOnInit(): void {}

  faDownload = faDownload;
}
