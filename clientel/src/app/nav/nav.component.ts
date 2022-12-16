import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { LoginModel } from '../_models/loginModel';
import { AccountService } from '../_services/account.service';
import { OrderService } from '../_services/order.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: LoginModel = {
    usernameoremail: null, password: null
  }
  modalRef: BsModalRef;
  toggle: boolean;

  constructor(private modalService: BsModalService, public accountService: AccountService,
    private router: Router, private toastr: ToastrService, public orderService:OrderService) { }


  ngOnInit(): void {
    this.orderService.initiateOrder()
  }

  login() {
    this.accountService.login(this.model).subscribe(
      {
        next: response => {
          console.log(response);
          this.modalService.hide();
        },
        error: error => {
          console.error(error);
          this.toastr.error(error.error);
      }
    });
  }

  logout() {
    this.accountService.logout();
    if (this.router.url === '/basket'
      || this.router.url === '/orders '
      || this.router.url === '/edit-profile')
    this.router.navigateByUrl('/games');
  }

  register() {
    this.router.navigate(['/register']);
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
 }
}