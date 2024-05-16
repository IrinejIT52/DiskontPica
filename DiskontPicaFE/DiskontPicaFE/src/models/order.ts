import { OrderItem } from "./orderItem";

export class Order {
    orderId!:number;
    customerId!:number;
    orderItems!:Array<OrderItem>
    finalPrice!:number;
    orderDate!:Date;
    orderStatus!:OrderStatus;
    orderType!:OrderType;
    addiitionalInfo!:string;
}

export enum OrderStatus
{
	PENDING,
	CONFIRMED,
    CANCELLED
}

export enum OrderType
{
	REGULAR,
	BIRTHDAY,
	ANNIVERSERY,
	PARTY
}