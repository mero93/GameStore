import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject, map } from 'rxjs';
import { Order, OrderItem } from '../_models/order';
import { OrderService } from '../_services/order.service';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.css']
})
export class BasketComponent implements OnInit {

  constructor(public orderService: OrderService, private router: Router, private modalService: BsModalService,) { }

  modalRef: BsModalRef;
  canBeSubmitted: boolean;

  authorRequired: BehaviorSubject<string> = new BehaviorSubject("")

  @ViewChild('EditGameTemplate') showOrderTemp: TemplateRef<any>;


  ngOnInit(): void {
    this.orderService.initiateOrder()
    this.canBeSubmitted = this.orderService.canBeSubmitted()
  }

  createOrder() {
    this.orderService.createOrder().subscribe()
    this.orderService.resetOrder()
    this.router.navigateByUrl('games');
  }

  addOne(item: OrderItem) {
    this.orderService.increaseQuantity(item)
    this.canBeSubmitted = this.orderService.canBeSubmitted()
  }

  removeOne(item: OrderItem) {
    this.orderService.decreaseQuantity(item)
    this.canBeSubmitted = this.orderService.canBeSubmitted()
  }

  removeItem(id: number) {
    this.orderService.removeItem(id)
    this.canBeSubmitted = this.orderService.canBeSubmitted()
  }

  showOrder() {
      this.modalRef = this.modalService.show(this.showOrderTemp);
  }
}
