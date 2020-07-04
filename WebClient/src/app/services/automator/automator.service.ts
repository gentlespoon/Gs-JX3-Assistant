import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ConnState } from '../../models/automator/conn-state.enum';
import { MouseAction } from '../../models/automator/mouse-action';
import { PixelColor } from '../../models/automator/pixel-color';
import { Coordinates } from '../../models/automator/coordinates';
import { Color } from '../../models/automator/color';

@Injectable({
  providedIn: 'root',
})
export class AutomatorService {
  constructor(private httpClient: HttpClient) {}

  expectedversion = '20.06.28.1903';

  public heartBeatFailure: number = 0;
  private _heartBeatInterval: any;
  public isConnected: boolean = false;
  public nhPort: number = this.getRandomPort();

  private _visible: boolean = false;
  public get visibility(): boolean {
    return this._visible;
  }
  public set visibility(v: boolean) {
    this._visible = v;
    if (this.isConnected) {
      this._setVisibility(v);
    }
  }

  public get connState(): ConnState {
    if (!this.isConnected) {
      return ConnState.none;
    } else {
      if (this.heartBeatFailure == 0) {
        return ConnState.connected;
      } else {
        return ConnState.disconnected;
      }
    }
  }

  private makeURL(queryString: string = ''): string {
    return 'http://localhost:' + this.nhPort + '/' + queryString;
  }

  private getRandomPort(): number {
    return Math.floor(Math.random() * (65530 - 5000) + 5000);
  }

  startHeartBeat() {
    this.heartBeatFailure = 0;
    this._heartBeatInterval = setInterval(() => {
      this.httpClient.get(this.makeURL('heartBeat')).subscribe(
        (response) => {
          // when a success heartbeat comes back, clear the failure counter;
          this.heartBeatFailure = 0;
          if (!this.isConnected) {
            // check version after first heartbeat
            this.isConnected = true;
            this.checkVersion();
          }
        },
        (error) => {
          this.heartBeatFailure++;
          if (this.heartBeatFailure > 3) {
            // on 5 successive failure, we lose connection to the NH.
            this.stopHeartBeat();
            console.error('lost connection to NH');
          }
        }
      );
    }, 3000);
  }

  stopHeartBeat() {
    this.isConnected = false;
    this.heartBeatFailure = 0;
    clearInterval(this._heartBeatInterval);
  }

  launch() {
    this.stopHeartBeat();
    window.location.href = `com.gentlespoon.jx3.nh://port=${this.nhPort}&visible=${this.visibility}`;
    this.startHeartBeat();
  }

  private _setVisibility(visibility: boolean) {
    let url = 'visible?visible=';
    if (this.visibility) {
      url += 'true';
    } else {
      url += 'false';
    }
    this.httpClient.get(this.makeURL(url)).subscribe(
      (response) => {
        // console.log();
      },
      (error) => {
        this.heartBeatFailure++;
      }
    );
  }

  download() {
    window.location.href = `/assets/binary/gs-jx3-native-helper-${this.expectedversion}.exe`;
  }

  getPixelColor(
    x: number,
    y: number,
    callback: (pixelColor: PixelColor) => void,
    errcb: (err: any) => void = null
  ): void {
    this.httpClient.get(this.makeURL(`getPixelColor?X=${x}&Y=${y}`)).subscribe(
      (response) => {
        // console.log(response);
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
        // this.heartBeatFailure++;
      }
    );
  }

  getCursorCoordinates(
    callback: (mouseAction: MouseAction) => void,
    errcb: (err: any) => void = null
  ): void {
    this.httpClient.get(this.makeURL(`getCursorCoordinates`)).subscribe(
      (response) => {
        // console.log(response);
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
        // this.heartBeatFailure++;
      }
    );
  }

  mouseClick(x: number, y: number, mb: number = 1): void {
    this.httpClient
      .get(this.makeURL(`mouseClickAt?X=${x}&Y=${y}&MB=${mb}`))
      .subscribe(
        (response) => {
          // console.log(response);
        },
        (error) => {
          console.error(error);
          // this.heartBeatFailure++;
        }
      );
  }

  checkVersion(): void {
    this.httpClient
      .get(this.makeURL('version'), { responseType: 'text' })
      .subscribe(
        (response) => {
          if (response !== this.expectedversion) {
            window.alert(
              `助手客户端已有新版本，请使用新版本。\n\n您的版本：${response}\n当前版本：${this.expectedversion}`
            );
            this.download();
            this.shutdown();
          }
        },
        (error) => {
          // this.heartBeatFailure++;
        }
      );
  }

  shutdown() {
    this.stopHeartBeat();
    this.httpClient
      .get(this.makeURL('shutdown'), { responseType: 'text' })
      .subscribe(
        (response) => {
          if (response !== '1') {
            this.heartBeatFailure++;
            this.stopHeartBeat();
          }
        },
        (error) => {
          // this.heartBeatFailure++;
        }
      );
  }
}
