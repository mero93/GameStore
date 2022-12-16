export class OrderItem {
    quantity: number;
    price: number;
    orderId: number = 0;
    gameId: number;
    game: string;
    gamePhotoUrl: string;
}

export class Order {
    id: number = 0;
    appUserId: number;
    orderDate: Date;
    subtotal: number;
    orderItems: OrderItem[] = [];
}

