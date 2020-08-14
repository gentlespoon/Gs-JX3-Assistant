import { Component, OnInit } from '@angular/core';
import { QRCode } from 'qrcode';
import { AutomatorService } from 'src/app/services/automator/automator.service';

@Component({
  selector: 'app-lan-link',
  templateUrl: './lan-link.component.html',
  styleUrls: ['./lan-link.component.scss'],
})
export class LanLinkComponent implements OnInit {
  constructor(public automatorService: AutomatorService) {}

  ngOnInit(): void {}

  public port: string = window.location.port;

  public generateURL(ipAddress: string): string {
    return `${window.location.protocol}//${ipAddress}:${this.port}/`;
  }
}
