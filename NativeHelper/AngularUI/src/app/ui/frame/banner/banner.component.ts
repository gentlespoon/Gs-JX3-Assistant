import { Component, OnInit } from '@angular/core';
import {
  faCaretDown,
  faCheckCircle,
  faTimesCircle,
} from '@fortawesome/free-solid-svg-icons';
import { Router } from '@angular/router';
import { AutomatorService } from 'src/app/services/automator/automator.service';
import { Version } from '../../../version';

@Component({
  selector: 'app-banner',
  templateUrl: './banner.component.html',
  styleUrls: ['./banner.component.scss'],
})
export class BannerComponent implements OnInit {
  constructor(
    public router: Router,
    public automatorService: AutomatorService
  ) {}

  ngOnInit(): void {}

  public Object = Object;

  // fontAwesome
  public faCaretDown = faCaretDown;
  public faCheckCircle = faCheckCircle;
  public faTimesCircle = faTimesCircle;

  public Version = Version;

  public get activatedRoute(): string {
    return this.router.url;
  }
}
