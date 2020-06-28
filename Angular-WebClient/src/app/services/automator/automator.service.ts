import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AutomatorService {
  constructor(private httpClient: HttpClient) {}

  expectedversion = '20.06.28.0042';

  public heartBeatFailure: number = 0;
  private heartBeatInterval: any;
  public isConnected: boolean = false;
  public nhPort: number = 65512;

  private makeURL(queryString: string = ''): string {
    return 'http://localhost:' + this.nhPort + '/' + queryString;
  }

  private get RandomPort() {
    return Math.floor(Math.random() * (65530 - 5000) + 5000);
  }

  startHeartBeat() {
    this.heartBeatFailure = 0;
    this.heartBeatInterval = setInterval(() => {
      this.httpClient.get(this.makeURL('heartBeat')).subscribe(
        (response) => {
          // when a success heartbeat comes back, clear the failure counter;
          this.heartBeatFailure = 0;
          this.isConnected = true;
        },
        (error) => {
          this.heartBeatFailure++;
          if (this.heartBeatFailure > 5) {
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
    clearInterval(this.heartBeatInterval);
  }

  launch() {
    this.stopHeartBeat();
    window.location.href = `com.gentlespoon.jx3.nh://port=${this.nhPort}&window=show`;
    this.startHeartBeat();
  }

  hideWindow(hide: boolean) {
    let url = 'window/';
    if (hide) {
      url += 'hide';
    } else {
      url += 'show';
    }
    this.httpClient.get(this.makeURL(url)).subscribe(
      (response) => {
        console.log();
      },
      (error) => {
        this.heartBeatFailure++;
      }
    );
  }

  download() {
    window.location.href = `/assets/binary/gs-jx3-native-helper-${this.expectedversion}.exe`;
  }
}
