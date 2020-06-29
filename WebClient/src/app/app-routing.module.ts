import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ItemListComponent } from './ui/items/item-list/item-list.component';
import { ItemInfoDetailComponent } from './ui/items/item-info-detail/item-info-detail.component';
import { PageNotFoundComponent } from './ui/error/page-not-found/page-not-found.component';
import { HomeComponent } from './ui/frame/home/home.component';
import { AutomatorComponent } from './ui/automator/automator.component';
import { LifeskillComponent } from './ui/lifeskill/lifeskill.component';
import { LsfrComponent } from './ui/lifeskill/lsfr/lsfr.component';
import { LsprComponent } from './ui/lifeskill/lspr/lspr.component';
import { LsysComponent } from './ui/lifeskill/lsys/lsys.component';
import { LszjComponent } from './ui/lifeskill/lszj/lszj.component';
import { LszzComponent } from './ui/lifeskill/lszz/lszz.component';
import { AutomatorHelpComponent } from './ui/automator/automator-help/automator-help.component';
import { AutomatorDYComponent } from './ui/automator/automator-dy/automator-dy.component';

const routes: Routes = [
  {
    path: 'automator',
    component: AutomatorComponent,
  },
  {
    path: 'automator/:mode',
    component: AutomatorComponent,
  },
  { path: 'itemList', component: ItemListComponent },
  { path: 'itemInfo/:itemId', component: ItemInfoDetailComponent },
  {
    path: 'lifeSkill',
    component: LifeskillComponent,
  },
  {
    path: 'lifeSkill/:mode',
    component: LifeskillComponent,
  },
  { path: '', component: HomeComponent },
  { path: '**', component: PageNotFoundComponent }, // Wildcard
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
