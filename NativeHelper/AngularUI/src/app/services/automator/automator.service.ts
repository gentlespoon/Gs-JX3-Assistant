import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MouseAction } from '../../models/mouse-action';
import { PixelColor } from '../../models/pixel-color';
import { Coordinates } from '../../models/coordinates';
import { Color } from '../../models/color';

@Injectable({
  providedIn: 'root',
})
export class AutomatorService {
  constructor(private httpClient: HttpClient) {
    setTimeout(() => {
      this.startHeartBeat();
    }, 500);
  }

  public isConnected = false;

  expectedversion = '20.08.13.1900';

  public heartBeatInterval: any;
  public heartBeatFailure: number = 0;
  public heartBeatFailureAllowance: number = 2;

  public automatorAddresses: string[] = [];

  startHeartBeat() {
    this.heartBeatFailure = 0;
    this.heartBeatInterval = setInterval(() => {
      this.httpClient.get('/api/heartBeat').subscribe(
        (response) => {
          // when a success heartbeat comes back, clear the failure counter;
          this.heartBeatFailure = 0;

          if (!this.isConnected) {
            this.checkVersion();
            this.getAutomatorAddress();
            this.isConnected = true;
          }
        },
        (error) => {
          this.heartBeatFailure++;
          if (this.heartBeatFailure > this.heartBeatFailureAllowance) {
            this.isConnected = false;
            clearInterval(this.heartBeatInterval);
            alert('连接中断，请退出重新打开。');
          } else {
            console.error('连接中断' + this.heartBeatFailure);
          }
        }
      );
    }, 5000);
  }

  getPixelColor(
    x: number,
    y: number,
    callback: (pixelColor: PixelColor) => void,
    errcb: (err: any) => void = null
  ): void {
    this.httpClient.get(`/api/getPixelColor?X=${x}&Y=${y}`).subscribe(
      (response) => {
        callback(
          new PixelColor(
            new Coordinates(x, y),
            new Color(response['R'], response['G'], response['B'])
          )
        );
      },
      (error) => {
        console.error(error);
        if (errcb) {
          errcb(error);
        }
      }
    );
  }

  getCursorCoordinates(
    callback: (mouseAction: MouseAction) => void,
    errcb: (err: any) => void = null
  ): void {
    this.httpClient.get(`/api/getCursorCoordinates`).subscribe(
      (response) => {
        callback(
          new MouseAction(
            new Coordinates(response['X'], response['Y']),
            response['MB']
          )
        );
      },
      (error) => {
        console.error(error);
        if (errcb) {
          errcb(error);
        }
      }
    );
  }

  mouseClick(x: number, y: number, mb: number = 1, dblClick = 0): void {
    this.httpClient
      .get(`/api/mouseClick?X=${x}&Y=${y}&MB=${mb}&dblClick=${dblClick}`)
      .subscribe(
        (response) => {},
        (error) => {
          console.error(error);
        }
      );
  }

  checkVersion(): void {
    this.httpClient.get(`/api/version`, { responseType: 'text' }).subscribe(
      (response) => {
        if (response !== this.expectedversion) {
          window.alert(
            `助手客户端已有新版本，请使用新版本。\n\n您的版本：${response}\n当前版本：${this.expectedversion}`
          );
        }
      },
      (error) => {}
    );
  }

  getAutomatorAddress(): void {
    this.httpClient.get(`/api/getIPAddresses`).subscribe((response) => {
      this.automatorAddresses = response as string[];
    });
  }
}
