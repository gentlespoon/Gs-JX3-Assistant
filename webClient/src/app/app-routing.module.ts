import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ItemListComponent } from './ui/items/item-list/item-list.component';
import { ItemInfoDetailComponent } from './ui/items/item-info-detail/item-info-detail.component';
import { PageNotFoundComponent } from './ui/error/page-not-found/page-not-found.component';
import { HomeComponent } from './ui/frame/home/home.component';
import { AutomatorComponent } from './ui/automator/automator.component';

const routes: Routes = [
  { path: 'automator', component: AutomatorComponent },
  { path: 'itemList', component: ItemListComponent },
  { path: 'itemInfo/:itemId', component: ItemInfoDetailComponent },
  { path: '', component: HomeComponent },
  { path: '**', component: PageNotFoundComponent }, // Wildcard
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
