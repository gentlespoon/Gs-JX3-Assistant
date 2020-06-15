import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ItemInfoComponent } from './ui/item/item-info/item-info.component';
import { ItemListComponent } from './ui/item/item-list/item-list.component';
import { IntroComponent } from './ui/intro/intro.component';

const routes: Routes = [
  { path: 'itemInfo', component: ItemInfoComponent },
  { path: 'itemList', component: ItemListComponent },
  { path: '**', component: IntroComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
