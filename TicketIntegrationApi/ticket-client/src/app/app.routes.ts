import { Routes } from '@angular/router';
import { Tickets } from './tickets/tickets';

export const routes: Routes = [
    {path:'tickets', component:Tickets},
    { path: '', redirectTo: 'tickets', pathMatch: 'full' }

];
