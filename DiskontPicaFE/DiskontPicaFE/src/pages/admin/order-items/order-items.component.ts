import { CommonModule } from '@angular/common';
import { Component, DoCheck, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
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
import { FormsModule } from '@angular/forms';
import { OrderItemService } from '../../../services/order-item.service';
import { OrderItem } from '../../../models/orderItem';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Customer } from '../../../models/customer';

@Component({
  selector: 'app-order-items',
  standalone: true,
  imports: [CommonModule,FormsModule, MatTableModule, MatPaginator, MatSort, MatDialogModule, MatFormFieldModule, MatToolbarModule, MatIconModule, MatInputModule, ProductDialogComponent],
  templateUrl: './order-items.component.html',
  styleUrl: './order-items.component.css'
})
export class OrderItemsComponent implements OnInit,OnDestroy {
  subscription!: Subscription;
  displayedColumns = ['productId','price', 'quantity','priceQuantity'];
  dataSource!: MatTableDataSource<OrderItem>;
  @ViewChild(MatPaginator, {static: false}) paginator!: MatPaginator;
  @ViewChild(MatSort, {static: false}) sort!: MatSort;
  public productList: Product[] =[];

  @Input() order!:Order;
  @Input() customer!:Customer;
  
  
  constructor(private orderItemService:OrderItemService,private dialog:MatDialog,private productService:ProductService, public snackBar: MatSnackBar){}


  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  ngOnInit(): void { 
    this.loadData(); 
    this.productService.GetAllProducts().subscribe((data)=>{
      this.productList=data;
    })
    
    
  }
  ngOnChanges(): void { 
    if(this.order.orderId){
      this.loadData();
    } 
  }


  public loadData(){
    this.subscription = this.orderItemService.GetOrderItemByOrder(this.order.orderId).subscribe(
      {next:(data)=>this.dataSource = data,
        error: (error) => {
          this.snackBar.open('Porudzbina nema stavke', 'Zatvori', {
            duration: 2500
          }); this.dataSource = new MatTableDataSource<OrderItem>
        },
        complete: () => console.info('complete')
      }
    )
  }
}


