import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-product-dialogue',
  imports: [ReactiveFormsModule, MatDialogModule, MatFormFieldModule, 
    MatInputModule, MatButtonModule, MatCheckboxModule, MatDatepickerModule,
    MatNativeDateModule],
  templateUrl: './product-dialogue.html',
  styleUrl: './product-dialogue.css',
  providers: [DatePipe]
})
export class ProductDialog {
  private readonly fb = inject(FormBuilder);
  private readonly datePipe = inject(DatePipe);
  private readonly dialogRef = inject(MatDialogRef<ProductDialog>);
  readonly data = inject<any>(MAT_DIALOG_DATA);

  isEditMode = !!this.data.id;
  
  productForm = this.fb.group({
    name: [this.data.name || '', Validators.required],
    category: [this.data.category || '', Validators.required],
    description: [this.data.description || ''],
    price: [this.data.price || 0, [Validators.required, Validators.min(0)]],
    quantity: [this.data.quantity || 0, [Validators.required, Validators.min(0)]],
    isActive: [this.data.isActive ?? true],
    createdDate: [this.data.createdDate || new Date().toISOString(), Validators.required]
  });

  onSave() {
    if (this.productForm.valid) {
      const rawData = this.productForm.value;
      const formattedDate = this.datePipe.transform(rawData.createdDate, 'yyyy-MM-dd');

      const finalProduct = {
        ...rawData,
        createdDate: formattedDate
      };

      this.dialogRef.close(finalProduct);
    }
  }
}