import { Routes } from '@angular/router';
import { Students } from './components/students/students';
import { Login } from './components/login/login';
import { Register } from './components/register/register';

export const routes: Routes = [
    { path: 'login', component: Login },
      {path:'components/students', component:Students},
      { path: 'register', component: Register },
      { path: '', redirectTo: 'login', pathMatch: 'full' }

];
