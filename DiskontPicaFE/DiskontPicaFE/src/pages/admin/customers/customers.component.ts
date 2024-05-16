import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableModule, MatTableDataSource  } from '@angular/material/table';
import { MatDialogModule,MatDialog, MatDialogConfig} from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { Product } from '../../../models/product';
import {  MatFormFieldModule } from '@angular/material/form-field';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { ProductService } from '../../../services/product.service';
import { ProductDialogComponent } from '../dialogs/product-dialog/product-dialog.component';
import { MatInputModule } from '@angular/material/input';
import { Order } from '../../../models/order';
import { OrdersService } from '../../../services/orders.service';
import { OrderDialogComponent } from '../dialogs/order-dialog/order-dialog.component';
import { CustomerService } from '../../../services/customer.service';
import { FormsModule } from '@angular/forms';
import { Customer } from '../../../models/customer';
import { OrderItemsComponent } from "../order-items/order-items.component";
import { OrdersComponent } from "../orders/orders.component";

@Component({
    selector: 'app-customers',
    standalone: true,
    templateUrl: './customers.component.html',
    styleUrl: './customers.component.css',
    imports: [CommonModule, FormsModule, MatTableModule, MatPaginator, MatSort, MatDialogModule, MatFormFieldModule, MatToolbarModule, MatIconModule, MatInputModule, ProductDialogComponent, OrderItemsComponent, OrdersComponent]
})
export class CustomersComponent {

  subscription!: Subscription;
  displayedColumns = ['name', 'email','adress'];
  dataSource!: MatTableDataSource<Customer>;
  @ViewChild(MatPaginator, {static: false}) paginator!: MatPaginator;
  @ViewChild(MatSort, {static: false}) sort!: MatSort;

  public selectedCustomer!:Customer;

  constructor(private customerService:CustomerService,private dialog:MatDialog){}

  ngOnInit(): void { 
    this.loadData(); 
  
  }
  ngOnChanges(): void { this.loadData(); }


  public loadData(){
    this.subscription = this.customerService.GetAllCustomers().subscribe(
      data => {
        this.dataSource = new MatTableDataSource(data);

        this.dataSource.sortingDataAccessor =(row:Customer,columnName:string):string => {
          var columnValue = row[columnName as keyof Customer] as unknown as string;
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

  selectRow(row: any) {
    this.selectedCustomer = row;
  }

}
