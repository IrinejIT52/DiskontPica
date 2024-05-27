import { CommonModule } from '@angular/common';
import { Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
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
import { MatInputModule } from '@angular/material/input';
import { Order } from '../../../models/order';
import { OrdersService } from '../../../services/orders.service';
import { CustomerService } from '../../../services/customer.service';
import { FormsModule } from '@angular/forms';
import { Customer } from '../../../models/customer';
import { MatSnackBar } from '@angular/material/snack-bar';
import { OrderItemsComponent } from '../../admin/order-items/order-items.component';

@Component({
  selector: 'app-customer-orders',
  standalone: true,
  imports: [CommonModule, FormsModule, MatTableModule, MatPaginator, MatSort, MatFormFieldModule, MatToolbarModule, MatIconModule, MatInputModule,OrderItemsComponent],
  templateUrl: './customer-orders.component.html',
  styleUrl: './customer-orders.component.css'
})
export class CustomerOrdersComponent implements OnInit,OnDestroy {

  subscription!: Subscription;
  displayedColumns = ['orderId','finalPrice','orderDate','orderStatus','orderType'];
  dataSource!: MatTableDataSource<Order>;
  @ViewChild(MatPaginator, {static: false}) paginator!: MatPaginator;
  @ViewChild(MatSort, {static: false}) sort!: MatSort;
  highlighted?: boolean;
  hovered?: boolean;

  customerId!:number;

  public selectedOrder!:Order;

  
  public orderStatus: string[] =['PENDING','CONFIRMED','CANCELLED'];
  public orderType: string[] =['REGULAR','BIRTHDAY','ANNIVERSERY','PARTY'];

  constructor(private orderService:OrdersService,private customerService:CustomerService,public snackBar: MatSnackBar){}

  parseJwt(token:any) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(window.atob(base64).split('').map(function(c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
  }

  ngOnInit(): void { 
    this.loadData(); 
  }

  ngOnChanges(): void { 
    this.loadData(); 
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }


  public loadData(){
    const{name}= this.parseJwt(localStorage.getItem('token'))
    this.customerService.GetCustomerByName(name).subscribe((data)=>{
      this.subscription = this.orderService.GetOrderByCustomer(data[0].customerId).subscribe((data)=>{
          data.sort((a:any, b:any) => b.orderId - a.orderId);

          this.dataSource = new MatTableDataSource(data);

          this.dataSource.sortingDataAccessor =(row:Order,columnName:string):string => {
            var columnValue = row[columnName as keyof Order] as unknown as string;
            return columnValue;
          }

          this.dataSource.sort = this.sort;
          this.dataSource.paginator=this.paginator;
          
        }
      )
    })

    
    
  }



  applyFilter(filterValue: any) {
    filterValue = filterValue.target.value
    filterValue = filterValue.trim();
    filterValue = filterValue.toLocaleLowerCase();
    this.dataSource.filter = filterValue; 
  }
}
