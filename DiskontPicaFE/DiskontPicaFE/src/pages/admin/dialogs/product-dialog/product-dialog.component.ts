import { Component, DoCheck, Inject, Input, NgModule, input } from '@angular/core';
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
  selector: 'app-product-dialog',
  standalone: true,
  imports: [MatFormFieldModule,MatCheckboxModule,MatSelectModule,MatSnackBarModule,FormsModule,MatDialogModule,CommonModule,MatInputModule],
  templateUrl: './product-dialog.component.html',
  styleUrl: './product-dialog.component.css'
})
export class ProductDialogComponent {

  public flagArtDialog!:number;
  

  public categoryList: Category[] =[];
  public countryList: Country[]=[];
  public adminList: Administrator[]=[];


  constructor(public snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<ProductDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Product,
    public productService: ProductService,
    public countryService: CountryService,
    public categoryService: CategoryService,
    public adminService: AdminService) { }


  ngOnInit() {
    
    this.categoryService.GetAllCategories().subscribe(
      (res:any) =>{
        this.categoryList=res;
      }
    )
    this.countryService.GetAllCountries().subscribe(
      (res:any) =>{
        this.countryList=res;
      }
    )
    this.adminService.GetAllAdministrators().subscribe(
      (res:any) =>{
        this.adminList=res;
      }
    )
    
    
  }



  

  compareTo(a: any, b: any) {
    return a.id == b.id;
  }

  public add(): void {
    this.productService.AddProduct(this.data).subscribe(() => {
      this.snackBar.open('Uspesno dodat proizvod: ' + this.data.name, 'OK', {
        duration: 2500
      })
    }),
      (error: Error) => {
        console.log(error.name + ' ' + error.message)
        this.snackBar.open('Doslo je do greske prilikom dodavanja nove proizvoda. ', 'Zatvori', {
          duration: 2500
        })
      };
  }


  public update(): void {
    this.productService.UpdateProduct(this.data).subscribe(() => {
      this.snackBar.open('Uspesno izmenjen proizvoda: ' + this.data.name, 'OK', {
        duration: 2500
      })
    }),
      (error: Error) => {
        console.log(error.name + ' ' + error.message)
        this.snackBar.open('Doslo je do greske prilikom izmene proizvoda. ', 'Zatvori', {
          duration: 2500
        })
      };

  }

  public delete(): void {
    this.productService.DeleteProduct(this.data.productId).subscribe(() => {
      this.snackBar.open('Uspesno obrisan proizvoda: ' + this.data.productId, 'OK', {
        duration: 2500
      })
    }),
      (error: Error) => {
        console.log(error.name + ' ' + error.message)
        this.snackBar.open('Doslo je do greske prilikom brisanja proizvoda. ', 'Zatvori', {
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
