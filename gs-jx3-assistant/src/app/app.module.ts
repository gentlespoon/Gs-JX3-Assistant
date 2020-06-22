import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
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
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    DragDropModule,
    FontAwesomeModule,
    ServiceWorkerModule.register('ngsw-worker.js', {
      enabled: environment.production,
    }),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
