import { Routes } from '@angular/router';
import { LoginComponent } from '../pages/website/login/login.component';
import { LayoutComponent } from '../pages/website/layout/layout.component';
import { ProductsComponent } from '../pages/admin/products/products.component';
import { CustomerOrdersComponent } from '../pages/website/customer-orders/customer-orders.component';
import { CustomerCartComponent } from '../pages/website/customer-cart/customer-cart.component';
import { CheckoutComponent } from '../pages/website/checkout/checkout.component';
import { CategoryProductsComponent } from '../pages/website/category-products/category-products.component';
import { DashboardComponent } from '../pages/admin/dashboard/dashboard.component';
import { CategoriesComponent } from '../pages/admin/categories/categories.component';
import { CustomersComponent } from '../pages/admin/customers/customers.component';
import { OrdersComponent } from '../pages/admin/orders/orders.component';
import { RegisterComponent } from '../pages/website/register/register.component';
import { LoginLayoutComponent } from '../pages/website/login-layout/login-layout.component';

export const routes: Routes = [

    {
        path:'',
        redirectTo:'product',
        pathMatch:'full'
    },
    {
        path:'',
        component:LoginLayoutComponent,
        children:[
            {
                path:'register',
                component:RegisterComponent
            },
            {
                path:'login',
                component:LoginComponent
            },
        ]
    },
    {
        path:'',
        component:LayoutComponent,
        children:[
           
            {
                path:'product',
                component:CategoryProductsComponent
            },
            {
                path:'customer-orders',
                component:CustomerOrdersComponent
            },
            {
                path:'customer-cart',
                component:CustomerCartComponent
            },
            {
                path:'checkout',
                component:CheckoutComponent
            },
           
        ]

    },

    {
        path:'',
        component:DashboardComponent,
        children:[
            {
                path:'admin-products',
                component:ProductsComponent
            },
            {
                path:'categories',
                component:CategoriesComponent
            },
            {
                path:'customers',
                component:CustomersComponent
            },
            {
                path:'orders',
                component:OrdersComponent
            }
           
        ]
    },
          
        
    
];
