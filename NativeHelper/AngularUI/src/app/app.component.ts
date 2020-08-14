import { Component } from '@angular/core';
import { CheckUpdateService } from './services/check-update/check-update.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  constructor(private checkUpdateService: CheckUpdateService) {
    this.checkUpdateService.checkUpdate();
  }
}
