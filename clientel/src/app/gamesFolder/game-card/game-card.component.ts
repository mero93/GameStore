import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Game } from 'src/app/_models/Game';
import { OrderService } from 'src/app/_services/order.service';

@Component({
  selector: 'app-game-card',
  templateUrl: './game-card.component.html',
  styleUrls: ['./game-card.component.css']
})
export class GameCardComponent implements OnInit {
  @Input() game: Game; 
  @Input() isAdmin: boolean = false;
  
  @Output() handleEditGame = new EventEmitter<number>()

  @Output() handleDeleteGame = new EventEmitter<number>()

  constructor(private router: Router, public orderService: OrderService) { }

  ngOnInit(): void {
    console.log("isAdmin", this.isAdmin)
  }

  navigate(id: number) {
    this.router.navigate(["/games/"]), {
      queryParams: { id: id}
    }
  }

  onEditClick() {
    console.log("edit game fired");
    this.handleEditGame.emit(this.game.id);
  }

  onDeleteClick() {
    console.log("delete game fired")
    this.handleDeleteGame.emit(this.game.id);
  }
}
