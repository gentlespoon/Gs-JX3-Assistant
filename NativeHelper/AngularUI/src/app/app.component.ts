import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Version } from './version';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  constructor(private httpClient: HttpClient) {
    this.checkUpdate();
  }

  checkUpdate() {
    console.log('Checking update');
    this.httpClient
      .get('https://api.github.com/repos/gentlespoon/Gs-JX3-Assistant/releases')
      .subscribe((response) => {
        let releases: object[] = response as object[];
        if (releases.length > 0) {
          let newestVersion = releases[0]['tag_name'];
          if (newestVersion.startsWith('v')) {
            newestVersion = newestVersion.substring(1);
          }
          console.log(newestVersion, Version.current);
          if (newestVersion > Version.current) {
            Version.hasUpdate = true;
          }
        }
      });
  }
}
