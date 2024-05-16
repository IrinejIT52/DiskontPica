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
import { ProductDialogComponent } from '../dialogs/product-dialog/product-dialog.component';
import { MatInputModule } from '@angular/material/input';
import { Order } from '../../../models/order';
import { OrdersService } from '../../../services/orders.service';
import { OrderDialogComponent } from '../dialogs/order-dialog/order-dialog.component';
import { CustomerService } from '../../../services/customer.service';
import { FormsModule } from '@angular/forms';
import { Customer } from '../../../models/customer';
import { OrderItemsComponent } from "../order-items/order-items.component";
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
    selector: 'app-orders',
    standalone: true,
    templateUrl: './orders.component.html',
    styleUrl: './orders.component.css',
    imports: [CommonModule, FormsModule, MatTableModule, MatPaginator, MatSort, MatDialogModule, MatFormFieldModule, MatToolbarModule, MatIconModule, MatInputModule, ProductDialogComponent, OrderItemsComponent]
})
export class OrdersComponent implements OnInit,OnDestroy {
  subscription!: Subscription;
  displayedColumns = ['customerId', 'finalPrice','orderDate','orderStatus','orderType', 'actions'];
  dataSource!: MatTableDataSource<Order>;
  @ViewChild(MatPaginator, {static: false}) paginator!: MatPaginator;
  @ViewChild(MatSort, {static: false}) sort!: MatSort;
  public customerList: Customer[] =[];
  highlighted?: boolean;
  hovered?: boolean;

  public selectedOrder!:Order;

  @Input() customer!:Customer;
  
  public orderStatus: string[] =['PENDING','CONFIRMED','CANCELLED'];
  public orderType: string[] =['REGULAR','BIRTHDAY','ANNIVERSERY','PARTY'];
  
  constructor(private orderService:OrdersService,private dialog:MatDialog,private customerService:CustomerService,public snackBar: MatSnackBar){}

  ngOnInit(): void { 
    this.loadData(); 
    this.customerService.GetAllCustomers().subscribe((data)=>{
      this.customerList=data;
    })

    this.loadDataCustomer();

    
  }
  ngOnChanges(): void { 
    if(this.customer.customerId){
       this.loadDataCustomer();
    }
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

 
  
  

  public loadData(){
    this.subscription = this.orderService.GetAllOrders().subscribe(
      data => {
        this.dataSource = new MatTableDataSource(data);

        this.dataSource.sortingDataAccessor =(row:Order,columnName:string):string => {
          var columnValue = row[columnName as keyof Order] as unknown as string;
          return columnValue;
        }

        this.dataSource.sort = this.sort;
        this.dataSource.paginator=this.paginator;
      }
    )
  }

  public loadDataCustomer(){
    this.subscription = this.orderService.GetOrderByCustomer(this.customer.customerId).subscribe(
      {next:(data)=>this.dataSource = data,
        error: (error) => {
          this.snackBar.open('Kupac nema stavke', 'Zatvori', {
            duration: 2500
          }); this.dataSource = new MatTableDataSource<Order>
        },
        complete: () => console.info('complete')
      }
    )
  }

  openDialog(flag:number,order?:Order): void{
   
    const dialogRef = this.dialog.open(OrderDialogComponent, {data:(order ? order : new Product())})
    dialogRef.componentInstance.flagArtDialog = flag;
    
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

  selectRow(row: any) {
    this.selectedOrder = row;
    
  }

  
  
 
 
 
}

