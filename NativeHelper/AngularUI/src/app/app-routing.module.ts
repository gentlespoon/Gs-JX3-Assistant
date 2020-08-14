import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './ui/frame/home/home.component';
import { AutomatorDYComponent } from './ui/automator/automator-dy/automator-dy.component';
import { LanLinkComponent } from './ui/frame/lan-link/lan-link.component';

const routes: Routes = [
  {
    path: 'dy',
    component: AutomatorDYComponent,
  },
  {
    path: 'LAN-Link',
    component: LanLinkComponent,
  },
  { path: '', component: HomeComponent },
  { path: '**', redirectTo: '/', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
