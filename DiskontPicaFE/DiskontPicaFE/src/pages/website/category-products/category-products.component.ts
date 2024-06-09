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
import { MatInputModule } from '@angular/material/input';
import { CountryService } from '../../../services/country.service';
import { CategoryService } from '../../../services/category.service';
import { AdminService } from '../../../services/admin.service';
import { Category } from '../../../models/category';
import { Country } from '../../../models/county';
import { Administrator } from '../../../models/administrator';
import { CustomerCartComponent } from '../customer-cart/customer-cart.component';
import { CartService } from '../../../services/cart.service';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-category-products',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatPaginator, MatSort, MatFormFieldModule, MatToolbarModule, MatIconModule, MatInputModule,CustomerCartComponent,MatButtonModule],
  templateUrl: './category-products.component.html',
  styleUrl: './category-products.component.css'
})
export class CategoryProductsComponent {
  subscription!: Subscription;
  displayedColumns = ['name', 'description', 'price', 'stock', 'actions'];
  dataSource!: MatTableDataSource<Product>;
  @ViewChild(MatPaginator, {static: false}) paginator!: MatPaginator;
  @ViewChild(MatSort, {static: false}) sort!: MatSort;

  
  

  constructor(private productService:ProductService,private cartService:CartService){}

  ngOnInit(): void { 
    this.loadData(); }

  ngOnChanges(): void { 
    this.loadData(); }

  

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

  applyFilter(filterValue: any) {
    filterValue = filterValue.target.value
    filterValue = filterValue.trim();
    filterValue = filterValue.toLocaleLowerCase();
    this.dataSource.filter = filterValue; 
  }

  addToCart(product:Product){
    this.cartService.addToCart(product);
  }

 

}
