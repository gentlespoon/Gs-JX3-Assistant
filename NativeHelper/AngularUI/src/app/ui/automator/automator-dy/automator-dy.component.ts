import { Component, OnInit } from '@angular/core';
import {
  faExclamationTriangle,
  faPlay,
  faStop,
} from '@fortawesome/free-solid-svg-icons';
import { faCheckCircle as farCheckCircle } from '@fortawesome/free-regular-svg-icons';
import { MouseAction } from '../../../models/mouse-action';
import { PixelColor } from '../../../models/pixel-color';
import { AutomatorService } from '../../../services/automator/automator.service';
import { Color } from '../../../models/color';

@Component({
  selector: 'app-automator-dy',
  templateUrl: './automator-dy.component.html',
  styleUrls: ['./automator-dy.component.scss'],
})
export class AutomatorDYComponent implements OnInit {
  constructor(private automatorService: AutomatorService) {}

  ngOnInit(): void {}

  faExclamationTriangle = faExclamationTriangle;
  farCheckCircle = farCheckCircle;
  faPlay = faPlay;
  faStop = faStop;

  anglingAction: MouseAction;
  settingAngling: boolean = false;

  harvestAction: MouseAction;
  settingHarvest: boolean = false;

  baitIndicator: PixelColor;
  settingBait: boolean = false;

  private makeProgressbar(pct: number): string {
    let progressbarColor = 'white';
    return `background: linear-gradient(to right, ${progressbarColor} 0%, ${progressbarColor} ${pct}%, transparent ${pct}%)`;
  }

  public get baitIndicatorStyle(): string {
    return `background-color: rgb(${this.baitIndicator.color.R}, ${this.baitIndicator.color.G}, ${this.baitIndicator.color.B}); width: 30px; height: 30px; border: 1px solid black; display: inline-block;`;
  }

  // 钓鱼

  angleTimeout: number = 25;
  _angleCountdown: number = 0;
  public get angleProgressbarStyle(): string {
    let pct = Math.ceil((this._angleCountdown / this.angleTimeout) * 100);
    return this.makeProgressbar(pct);
  }
  _angleCheckColorInterval: any;
  _angleCountdownInterval: any;
  public get angleCountdown() {
    return this._angleCountdown;
  }
  private _angleCheckColorIntervalAction() {
    if (!this.isFishing) {
      this.stop();
    }
    this.automatorService.getPixelColor(
      this.baitIndicator.coordinates.X,
      this.baitIndicator.coordinates.Y,
      (pixelColor) => {
        if (this.baitIndicator.color.isSame(pixelColor.color)) {
          this.harvest();
        }
      }
    ),
      (error) => {
        console.error(error);
      };
  }
  private _angleCountDownIntervalAction() {
    if (!this.isFishing) {
      this.stop();
    }
    this._angleCountdown++;
    if (this._angleCountdown >= this.angleTimeout) {
      this.clearAllInterval();
      this.angle();
    }
  }
  private startAngleCountdown() {
    this.clearAllInterval();
    this._angleCountdown = 0;
    this._angleCountdownInterval = setInterval(() => {
      this._angleCountDownIntervalAction();
    }, 1000);
    this._angleCheckColorInterval = setInterval(() => {
      this._angleCheckColorIntervalAction();
    }, 200);
  }

  // 收获

  harvestTimeout: number = 8;
  _harvestCountdown: number = 0;
  public get harvestProgressbarStyle(): string {
    let pct = Math.ceil((this._harvestCountdown / this.harvestTimeout) * 100);
    return this.makeProgressbar(pct);
  }
  _harvestCountdownInterval: any;
  public get harvestCountdown() {
    return this._harvestCountdown;
  }
  private _harvestCountdownIntervalAction() {
    if (!this.isFishing) {
      this.stop();
    }
    this._harvestCountdown++;
    if (this._harvestCountdown >= this.harvestTimeout) {
      this.angle();
    }
  }
  private startHarvestCountdown() {
    this.clearAllInterval();
    this._harvestCountdown = 0;
    this._harvestCountdownInterval = setInterval(() => {
      this._harvestCountdownIntervalAction();
    }, 1000);
  }

  private clearAllInterval() {
    clearInterval(this._harvestCountdownInterval);
    clearInterval(this._angleCheckColorInterval);
    clearInterval(this._angleCountdownInterval);
  }

  // 钓鱼状态

  isFishing: boolean = false;

  public get canStart(): boolean {
    return (
      !this.isFishing &&
      !!this.harvestAction &&
      !!this.anglingAction &&
      !!this.baitIndicator
    );
  }
  public get canStop(): boolean {
    return this.isFishing;
  }

  // 设置行为

  public setAnglingAction() {
    this.settingAngling = true;
    this.automatorService.getCursorCoordinates(
      (mouseAction) => {
        this.anglingAction = mouseAction;
        this.settingAngling = false;
      },
      (err) => {
        this.settingAngling = false;
      }
    );
  }

  public setHarvestAction() {
    this.settingHarvest = true;
    this.automatorService.getCursorCoordinates(
      (mouseAction) => {
        this.harvestAction = mouseAction;
        this.settingHarvest = false;
      },
      (error) => {
        this.settingHarvest = false;
      }
    );
  }

  public setBaitIndicator() {
    this.settingBait = true;
    this.automatorService.getCursorCoordinates(
      (mouseAction) => {
        this.automatorService.getPixelColor(
          mouseAction.coordinates.X,
          mouseAction.coordinates.Y,
          (pixelColor) => {
            this.baitIndicator = pixelColor;
            this.settingBait = false;
          },
          (error) => {
            this.settingBait = false;
          }
        );
      },
      (error) => {
        this.settingBait = false;
      }
    );
  }

  // 控制

  public start() {
    this.isFishing = true;
    setTimeout(() => this.angle(), 2000);
  }

  public stop() {
    this.isFishing = false;
    this.clearAllInterval();
  }

  // 钓鱼
  private angle() {
    this.clearAllInterval();
    this.automatorService.mouseClick(
      this.anglingAction.coordinates.X,
      this.anglingAction.coordinates.Y,
      this.anglingAction.MB,
      1
    );
    this.startAngleCountdown();
    this._harvestCountdown = 0;
  }

  // 收杆
  private harvest() {
    this.clearAllInterval();
    this._angleCountdown = this.angleTimeout;
    this.automatorService.mouseClick(
      this.harvestAction.coordinates.X,
      this.harvestAction.coordinates.Y,
      this.harvestAction.MB,
      1
    );
    this.startHarvestCountdown();
  }
}
