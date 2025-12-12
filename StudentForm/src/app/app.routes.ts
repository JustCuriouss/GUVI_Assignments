import { Routes } from '@angular/router';
import { Home } from './home/home';
import { AddStudent } from './add-student/add-student';

export const routes: Routes = [
    { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: Home },
  { path: 'add-student', component: AddStudent }
];
