import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Game } from 'src/app/_models/Game';
import { GameStoreService } from 'src/app/_services/gamestore.service';
import { OrderService } from 'src/app/_services/order.service';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {
  game: Game = {
    id: 1,
    name: "",
    ratingNumber: 0,
    price: 0,
    photoUrl: undefined,
    publicId: undefined
}

  constructor(private gamesService: GameStoreService, private route: ActivatedRoute, public orderService: OrderService) { }

  ngOnInit(): void {
    this.loadGame();
  }

  loadGame() {
    this.gamesService.returnGameById(parseInt(this.route.snapshot.paramMap.get('id'))).subscribe(
      data => this.game = data
    )
  }
}
