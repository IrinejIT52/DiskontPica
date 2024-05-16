import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableModule, MatTableDataSource  } from '@angular/material/table';
import { MatDialogModule,MatDialog, MatDialogConfig} from '@angular/material/dialog';
import { Subscription, timeInterval } from 'rxjs';
import { Product } from '../../../models/product';
import {  MatFormFieldModule } from '@angular/material/form-field';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { ProductService } from '../../../services/product.service';
import { ProductDialogComponent } from '../dialogs/product-dialog/product-dialog.component';
import { MatInputModule } from '@angular/material/input';
import { CountryService } from '../../../services/country.service';
import { CategoryService } from '../../../services/category.service';
import { AdminService } from '../../../services/admin.service';
import { Category } from '../../../models/category';
import { Country } from '../../../models/county';
import { Administrator } from '../../../models/administrator';




@Component({
    selector: 'app-products',
    standalone: true,
    templateUrl: './products.component.html',
    styleUrl: './products.component.css',
    imports: [CommonModule, MatTableModule, MatPaginator, MatSort, MatDialogModule, MatFormFieldModule, MatToolbarModule, MatIconModule, MatInputModule, ProductDialogComponent]
})
export class ProductsComponent  {
  subscription!: Subscription;
  displayedColumns = ['name', 'description', 'price', 'stock','countryId','categoryId','adminId', 'actions'];
  dataSource!: MatTableDataSource<Product>;
  selected!: Product;
  @ViewChild(MatPaginator, {static: false}) paginator!: MatPaginator;
  @ViewChild(MatSort, {static: false}) sort!: MatSort;

  public categoryList: Category[] =[];
  public countryList: Country[]=[];
  public adminList: Administrator[]=[];

  constructor(private productService:ProductService,private dialog:MatDialog,private countryService:CountryService,private categoryService:CategoryService,private adminService:AdminService){}

  ngOnInit(): void { 
    this.loadData(); 
    this.countryService.GetAllCountries().subscribe((data)=>{
      this.countryList=data;
    })
    this.categoryService.GetAllCategories().subscribe((data)=>{
      this.categoryList=data;
    })
    this.adminService.GetAllAdministrators().subscribe((data)=>{
      this.adminList=data;
    })
  }
  ngOnChanges(): void { this.loadData(); }


  public loadData(){
    this.subscription = this.productService.GetAllProducts().subscribe(
      data => {
        this.dataSource = new MatTableDataSource(data);

        this.dataSource.sortingDataAccessor =(row:Product,columnName:string):string => {
          var columnValue = row[columnName as keyof Product] as unknown as string;
          return columnValue;
        }

        this.dataSource.sort = this.sort;

        this.dataSource.paginator=this.paginator;
      }
    )
  }

  openDialog(flag:number,product?:Product): void{
    
    const dialogRef = this.dialog.open(ProductDialogComponent, {data:(product ? product : new Product())})
    dialogRef.componentInstance.flagArtDialog = flag;

   
    if(flag===2){
      console.log(this.selected);
      dialogRef.componentInstance.data.categoryId = this.selected.categoryId;
      dialogRef.componentInstance.data.countryId = this.selected.countryId;
      dialogRef.componentInstance.data.adminId = this.selected.adminId;
    }

    
    dialogRef.afterClosed().subscribe(res =>{
      if(res === 1){
        this.loadData();
      }

    })
  }  

  applyFilter(filterValue: any) {
    filterValue = filterValue.target.value
    filterValue = filterValue.trim();
    filterValue = filterValue.toLocaleLowerCase();
    this.dataSource.filter = filterValue; 
  }

  selectRow(product: Product) {
    this.selected = product;
  }
}


