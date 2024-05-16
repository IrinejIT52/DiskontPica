import { Component, Inject, Input, NgModule, input } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSelectModule } from '@angular/material/select';
import { Product } from '../../../../models/product';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef} from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { OrdersService } from '../../../../services/orders.service';
import { Order } from '../../../../models/order';

@Component({
  selector: 'app-order-dialog',
  standalone: true,
  imports: [MatFormFieldModule,MatCheckboxModule,MatSelectModule,MatSnackBarModule,FormsModule,MatDialogModule,CommonModule,MatInputModule],
  templateUrl: './order-dialog.component.html',
  styleUrl: './order-dialog.component.css'
})
export class OrderDialogComponent {
  public flagArtDialog!:number;
  public orderStatus: string[] =['PENDING','CONFIRMED','CANCELLED'];

  constructor(public snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<OrderDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Order,
    public orderService:OrdersService) { }

  ngOnInit() {
    
    
  }

  compareTo(a: any, b: any) {
    return a.id == b.id;
  }


  public update(): void {
    this.orderService.UpdateOrder(this.data).subscribe(() => {
      this.snackBar.open('Uspesno izmenjen statusa: ' + this.data.orderStatus, 'OK', {
        duration: 2500
      })
    }),
      (error: Error) => {
        console.log(error.name + ' ' + error.message)
        this.snackBar.open('Doslo je do greske prilikom izmene statusa. ', 'Zatvori', {
          duration: 2500
        })
      };

  }

  public delete(): void {
    this.orderService.DeleteOrder(this.data.orderId).subscribe(() => {
      this.snackBar.open('Uspesno obrisan narudzbe: ' + this.data.orderId, 'OK', {
        duration: 2500
      })
    }),
      (error: Error) => {
        console.log(error.name + ' ' + error.message)
        this.snackBar.open('Doslo je do greske prilikom brisanja narudzbe. ', 'Zatvori', {
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
