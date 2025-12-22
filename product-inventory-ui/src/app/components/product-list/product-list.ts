import { Component, OnInit, viewChild, inject, effect } from '@angular/core';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { Product } from '../../models/Product';
import { AuthService } from '../../services/auth-service';
import { ProductService } from '../../services/product-service';
import { ProductDialog } from '../product-dialogue/product-dialogue';

@Component({
  selector: 'app-product-list',
  imports: [CurrencyPipe, MatTableModule, MatPaginatorModule, MatSortModule,
    MatButtonModule, MatIconModule, MatFormFieldModule, MatInputModule, MatDialogModule, CommonModule],
  templateUrl: './product-list.html',
  styleUrl: './product-list.css',
})
export class ProductList implements OnInit {
  private readonly productService = inject(ProductService);
  private readonly authService = inject(AuthService);
  private readonly dialog = inject(MatDialog);

  // UPDATED COLUMNS: Added description, quantity, isActive, createdDate
  displayedColumns: string[] = [
    'id', 
    'name', 
    'description', 
    'category', 
    'price', 
    'quantity', 
    'isActive', 
    'createdDate', 
    'actions'
  ];
  
  dataSource = new MatTableDataSource<Product>();

  // Angular Signal Queries
  paginator = viewChild.required(MatPaginator);
  sort = viewChild.required(MatSort);

  constructor() {
    effect(() => {
      this.dataSource.paginator = this.paginator();
      this.dataSource.sort = this.sort();
    });
  }

  ngOnInit() {
    this.loadProducts();
  }

  loadProducts() {
    this.productService.getProducts().subscribe({
      next: (data) => this.dataSource.data = data,
      error: (err) => console.error(err)
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
    
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  openDialog(product?: Product) {
    const dialogRef = this.dialog.open(ProductDialog, {
      width: '500px',
      data: product || {} 
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (product) {
          // Update
          this.productService.updateProduct(product.id, { ...product, ...result })
            .subscribe(() => this.loadProducts());
        } else {
          // Create
          this.productService.createProduct(result)
            .subscribe(() => this.loadProducts());
        }
      }
    });
  }

  deleteProduct(id: number) {
    if(confirm('Are you sure you want to delete this product?')) {
      this.productService.deleteProduct(id).subscribe(() => this.loadProducts());
    }
  }

  logout() {
    this.authService.logout();
  }
}