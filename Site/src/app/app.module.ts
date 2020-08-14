import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { ItemListComponent } from './ui/items/item-list/item-list.component';
import { SafeHtmlPipe } from './pipes/safe-html/safe-html.pipe';
import { ItemInlineNameComponent } from './ui/items/item-inline-name/item-inline-name.component';
import { ItemInfoDetailComponent } from './ui/items/item-info-detail/item-info-detail.component';
import { PageNotFoundComponent } from './ui/error/page-not-found/page-not-found.component';
import { ItemInfoFloatComponent } from './ui/items/item-info-float/item-info-float.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ItemInfoFloatContainerComponent } from './ui/items/item-info-float-container/item-info-float-container.component';
import { BannerComponent } from './ui/frame/banner/banner.component';
import { HomeComponent } from './ui/frame/home/home.component';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';
import { AutomatorComponent } from './ui/automator/automator.component';
import { LifeskillComponent } from './ui/lifeskill/lifeskill.component';
import { LsprComponent } from './ui/lifeskill/lspr/lspr.component';
import { LsfrComponent } from './ui/lifeskill/lsfr/lsfr.component';
import { LszjComponent } from './ui/lifeskill/lszj/lszj.component';
import { LszzComponent } from './ui/lifeskill/lszz/lszz.component';
import { LsysComponent } from './ui/lifeskill/lsys/lsys.component';
import { AutomatorHelpComponent } from './ui/automator/automator-help/automator-help.component';
import { AutomatorDYComponent } from './ui/automator/automator-dy/automator-dy.component';
import { AutomatorTabComponent } from './ui/automator/automator-tab/automator-tab.component';
import { LifeskillTabComponent } from './ui/lifeskill/lifeskill-tab/lifeskill-tab.component';
import { AutomatorSetupComponent } from './ui/automator/automator-setup/automator-setup.component';
import { AutomatorStatusComponent } from './ui/automator/automator-status/automator-status.component';

@NgModule({
  declarations: [
    AppComponent,
    ItemListComponent,
    SafeHtmlPipe,
    ItemInlineNameComponent,
    ItemInfoDetailComponent,
    PageNotFoundComponent,
    ItemInfoFloatComponent,
    ItemInfoFloatContainerComponent,
    BannerComponent,
    HomeComponent,
    AutomatorComponent,
    LifeskillComponent,
    LsprComponent,
    LsfrComponent,
    LszjComponent,
    LszzComponent,
    LsysComponent,
    AutomatorHelpComponent,
    AutomatorDYComponent,
    AutomatorTabComponent,
    LifeskillTabComponent,
    AutomatorSetupComponent,
    AutomatorStatusComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    DragDropModule,
    FontAwesomeModule,
    FormsModule,
    ServiceWorkerModule.register('ngsw-worker.js', {
      enabled: environment.production,
    }),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
