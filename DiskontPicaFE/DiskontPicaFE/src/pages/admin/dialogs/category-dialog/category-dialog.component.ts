import { Component, Inject, Input, NgModule, input } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSelectModule } from '@angular/material/select';
import { Product } from '../../../../models/product';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef} from '@angular/material/dialog';
import { ProductService } from '../../../../services/product.service';
import { CountryService } from '../../../../services/country.service';
import { CategoryService } from '../../../../services/category.service';
import { AdminService } from '../../../../services/admin.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { Category } from '../../../../models/category';
import { Country } from '../../../../models/county';
import { Administrator } from '../../../../models/administrator';

@Component({
  selector: 'app-category-dialog',
  standalone: true,
  imports: [MatFormFieldModule,MatCheckboxModule,MatSelectModule,MatSnackBarModule,FormsModule,MatDialogModule,CommonModule,MatInputModule],
  templateUrl: './category-dialog.component.html',
  styleUrl: './category-dialog.component.css'
})
export class CategoryDialogComponent {

  public flagArtDialog!:number;

  constructor(public snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<CategoryDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Category,
    public categoryService:CategoryService) { }

    compareTo(a: any, b: any) {
      return a.id == b.id;
    }
  
    public add(): void {
      this.categoryService.AddCategory(this.data).subscribe(() => {
        this.snackBar.open('Uspesno dodat kategorija: ' + this.data.name, 'OK', {
          duration: 2500
        })
      }),
        (error: Error) => {
          console.log(error.name + ' ' + error.message)
          this.snackBar.open('Doslo je do greske prilikom dodavanja nove kategorija. ', 'Zatvori', {
            duration: 2500
          })
        };
    }
  
  
    public update(): void {
      this.categoryService.UpdateCategory(this.data).subscribe(() => {
        this.snackBar.open('Uspesno izmenjen kategorija: ' + this.data.name, 'OK', {
          duration: 2500
        })
      }),
        (error: Error) => {
          console.log(error.name + ' ' + error.message)
          this.snackBar.open('Doslo je do greske prilikom izmene kategorija. ', 'Zatvori', {
            duration: 2500
          })
        };
  
    }
  
    public delete(): void {
      this.categoryService.DeleteCategory(this.data.categoryId).subscribe(() => {
        this.snackBar.open('Uspesno obrisan kategorija: ' + this.data.name, 'OK', {
          duration: 2500
        })
      }),
        (error: Error) => {
          console.log(error.name + ' ' + error.message)
          this.snackBar.open('Doslo je do greske prilikom brisanja kategorija. ', 'Zatvori', {
            duration: 2500
          })
        };
    }
  
    public cancel(): void {
      this.dialogRef.close();
      this.snackBar.open('Odustali ste od izmene. ', 'Zatvori', {
        duration: 1000
      })
    }
  

}
