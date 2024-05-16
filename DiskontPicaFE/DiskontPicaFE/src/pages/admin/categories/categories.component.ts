import { CommonModule } from '@angular/common';
import { Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableModule, MatTableDataSource  } from '@angular/material/table';
import { MatDialogModule,MatDialog, MatDialogConfig} from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import {  MatFormFieldModule } from '@angular/material/form-field';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { OrderItem } from '../../../models/orderItem';
import { CategoryService } from '../../../services/category.service';
import { Category } from '../../../models/category';
import { CategoryDialogComponent } from '../dialogs/category-dialog/category-dialog.component';

@Component({
  selector: 'app-categories',
  standalone: true,
  imports: [CommonModule,FormsModule, MatTableModule, MatPaginator, MatSort, MatDialogModule, MatFormFieldModule, MatToolbarModule, MatIconModule, MatInputModule],
  templateUrl: './categories.component.html',
  styleUrl: './categories.component.css'
})
export class CategoriesComponent {
  subscription!: Subscription;
  displayedColumns = ['name', 'description','actions'];
  dataSource!: MatTableDataSource<Category>;
  @ViewChild(MatPaginator, {static: false}) paginator!: MatPaginator;
  @ViewChild(MatSort, {static: false}) sort!: MatSort;

  constructor(private categoryService:CategoryService,private dialog:MatDialog){}

  ngOnInit(): void { 
    this.loadData(); 
  }
  loadData() {
    this.subscription = this.categoryService.GetAllCategories().subscribe(
      data => {
        this.dataSource = new MatTableDataSource(data);

        this.dataSource.sortingDataAccessor =(row:Category,columnName:string):string => {
          var columnValue = row[columnName as keyof Category] as unknown as string;
          return columnValue;
        }

        this.dataSource.sort = this.sort;

        this.dataSource.paginator=this.paginator;
      }
    )
  }

  openDialog(flag:number,product?:Category): void{
    
    const dialogRef = this.dialog.open(CategoryDialogComponent, {data:(product ? product : new Category())})
    dialogRef.componentInstance.flagArtDialog = flag;
    
    dialogRef.afterClosed().subscribe(res =>{
      if(res === 1){
        this.loadData();
      }

    })
  }  

}
