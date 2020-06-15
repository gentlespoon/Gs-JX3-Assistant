import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule, NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { ItemListComponent } from './ui/item/item-list/item-list.component';
import { FormsModule } from '@angular/forms';
import { ItemInfoComponent } from './ui/item/item-info/item-info.component';
import { IntroComponent } from './ui/intro/intro.component';
import { ZzComponent } from './ui/skill/zz/zz.component';
import { ZjComponent } from './ui/skill/zj/zj.component';
import { YsComponent } from './ui/skill/ys/ys.component';
import { PrComponent } from './ui/skill/pr/pr.component';
import { FrComponent } from './ui/skill/fr/fr.component';
import { ItemThumbnailComponent } from './ui/item/item-thumbnail/item-thumbnail.component';
import { ItemDetailComponent } from './ui/item/item-detail/item-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    ItemListComponent,
    ItemInfoComponent,
    IntroComponent,
    ZzComponent,
    ZjComponent,
    YsComponent,
    PrComponent,
    FrComponent,
    ItemThumbnailComponent,
    ItemDetailComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgbModule,
    FormsModule,
    NgbPaginationModule,
  ],
  providers: [],
  bootstrap: [AppComponent, ItemInfoComponent],
})
export class AppModule {}
