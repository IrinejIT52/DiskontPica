import { OrderItem } from "./orderItem";

export class Order {
    orderId!: number;
    customerId!: number;
    orderItems!: OrderItem[];
    finalPrice!: number;
    orderDate!: string;
    orderStatus!: string;
    orderType!: string;
    addiitionalInfo!: string;
}