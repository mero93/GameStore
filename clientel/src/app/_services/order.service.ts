import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Game } from '../_models/Game';
import { Order, OrderItem } from '../_models/order';
import { decodeUserId } from './tokenHelpers';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  order: Order

  token: any

  constructor(private http: HttpClient) { }

  addToOrder(game: Game) {
    if ((this.order) && this.order.id === undefined) {
      console.log("entering initiateOrder")
      this.initiateOrder()
    }

    console.log("order", this.order);
    
    let item: OrderItem;

    item = this.order.orderItems.find(o => 
      o.gameId === game.id);

    if (item)
    {
      item.quantity++;
    }
    else
    {
      const orderItem = new OrderItem();
      orderItem.orderId = this.order.id
      orderItem.game = game.name;
      orderItem.gameId = game.id;
      orderItem.gamePhotoUrl = game.photoUrl;
      orderItem.price = game.price;
      orderItem.quantity = 1;
  
      this.order.orderItems.push(orderItem);
    }

    localStorage.setItem('order', JSON.stringify(this.order))
  }

  initiateOrder() {
    console.log("order initiated")
    let existingOrder = JSON.parse(localStorage.getItem('order'));
    if (existingOrder !== null) {
      this.order = existingOrder;
      console.log("order got from localstorage", this.order.id)
    }
    else {
      this.order = new Order();
      this.order.id = 0;
      console.log("blank order created", this.order.id)
    }
  }

  calculateSubTotal() {
    this.order.subtotal = this.order.orderItems.reduce(
      (tot, val) => 
    {
      return tot + (val.price * val.quantity);
    }, 0);
  }

  prepareOrder() {
    this.order.appUserId = decodeUserId();
    this.order.orderItems = this.order.orderItems.filter(x =>
      x.price > 0 && x.quantity > 0) 
    this.order.orderDate = new Date();
    this.order.orderItems.sort(x => x.gameId);
  }

  createOrder() {
    this.prepareOrder();
    return this.http.post('https://localhost:5001/api/Orders/create-order', this.order);
  }

  resetOrder() {
    this.order = new Order()
    localStorage.removeItem('order')
  }

  increaseQuantity(item: OrderItem) {
    item.quantity++
    this.order.orderItems.filter(x => x.gameId === item.gameId)[0] = item
    this.calculateSubTotal()
    localStorage.setItem('order', JSON.stringify(this.order))
  }

  decreaseQuantity(item: OrderItem) {
    item.quantity--
    this.order.orderItems.filter(x => x.gameId === item.gameId)[0] = item
    this.calculateSubTotal()
    localStorage.setItem('order', JSON.stringify(this.order))
  }

  removeItem(id: number) {
    this.order.orderItems = this.order.orderItems.filter(x =>
      x.gameId !== id)
    if (this.order.orderItems.length > 0) {
      this.calculateSubTotal()
      localStorage.setItem('order', JSON.stringify(this.order))
    }
    else {
      localStorage.removeItem('order')
    }
  }

  canBeSubmitted() {
    return (this.order.orderItems.length > 0
      && this.order.orderItems.some(x => x.quantity > 0 && x.price >= 0))
  }
}
