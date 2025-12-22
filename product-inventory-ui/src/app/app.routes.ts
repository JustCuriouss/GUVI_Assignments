import { Routes } from '@angular/router';
import { authGuard } from './guard/AuthGuard';
import { Login } from './components/login/login';
import { Register } from './components/register/register';

export const routes: Routes = [
  { 
    path: 'login', 
    component: Login
  },
  { 
    path: 'register', 
    component: Register
  },
  { 
    path: 'products', 
    loadComponent: () => import('./components/product-list/product-list')
      .then(m => m.ProductList),
    canActivate: [authGuard] 
  },
  { 
    path: '', 
    redirectTo: 'products', 
    pathMatch: 'full' 
  },
  { 
    path: '**', 
    redirectTo: 'products' 
  }
];