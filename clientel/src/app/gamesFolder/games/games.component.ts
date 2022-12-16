import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { GameParams } from 'src/app/_models/GameParams';
import { Pagination } from 'src/app/_models/pagination';
import { AccountService } from 'src/app/_services/account.service';
import { Game, Genre, Publisher, } from '../../_models/Game';
import { GameStoreService } from '../../_services/gamestore.service';
import { ImageUplModelType } from '../../_models/imageUplModelType';

@Component({
  selector: 'app-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.css']
})
export class GamesComponent implements OnInit {
  games: Game[];
  isAdmin: boolean;
  genres: Genre[] = [];
  genreList: string[] = [];
  publishers: Publisher[] = [];
  publisherList: string[] = [];
  pagination: Pagination;
  gameParams = new GameParams();
  filterForm: FormGroup;
  model: GameParams;
  options: string[] =[];
  minReleaseDateAbs: Date;
  maxReleaseDateAbs: Date;
  minDateAddedAbs: Date;
  maxDateAddedAbs: Date;
  minReleaseDate: Date;
  maxReleaseDate: Date;
  minDateAdded: Date;
  maxDateAdded: Date;

  gameTo: Game;
  imageUplModelType = ImageUplModelType.Game
  deletionId: number;

  modalRef: BsModalRef;
  toggle: boolean;

  @ViewChild('EditGameTemplate') editRef: TemplateRef<any>;
  @ViewChild('AddImageTemplate') addImageRef: TemplateRef<any>;
  @ViewChild('DeleteGameTemplate') deleteRef: TemplateRef<any>;

  constructor(private modalService: BsModalService, public gameStoreService: GameStoreService, private accountService: AccountService,
    private fb: FormBuilder) { }

  ngOnInit(): void {
    this.checkForAdmin();
    this.options = this.gameStoreService.options;
    this.minReleaseDateAbs = this.gameStoreService.minReleaseDate;
    this.maxReleaseDateAbs = this.gameStoreService.maxReleaseDate;
    this.minDateAddedAbs = this.gameStoreService.minDateAdded;
    this.maxDateAddedAbs = this.gameStoreService.maxDateAdded;
    this.minReleaseDate = this.minReleaseDateAbs;
    this.maxReleaseDate = this.maxReleaseDateAbs;
    this.minDateAdded = this.minDateAddedAbs;
    this.maxDateAdded = this.maxDateAddedAbs;
    this.initializeForm();
    this.gameStoreService.setGameParams(this.gameParams);
    this.gameStoreService.getGenres().subscribe(res =>
      this.genres = res)
    this.gameStoreService.getPublishers().subscribe(res =>
      this.publishers = res);
    this.loadGames();
    
    console.log(this.gameParams, typeof this.gameParams)
    console.log("is admin", this.isAdmin)
    console.log("genres", this.genres);
    console.log("publishers", this.publishers);
    console.log(this.gameStoreService.options);
  }

  initializeForm() {
    this.filterForm = this.fb.group({
      pageSize: [this.gameParams.pageSize ],
      sortBy: [ this.gameParams.sortBy ],
      reverseOrder: [ this.gameParams.reverseOrder],
      publishersFilter: [ this.gameParams.publishersFilter],
      genresFilter: [ this.gameParams.genresFilter],
      minDownloads: [ this.gameParams.minDownloads,
        [Validators.required, Validators.min(0)]],
      minRating: [ this.gameParams.minRating,
        [Validators.required, Validators.min(0)]],
      minReleaseDateFilter: [ this.gameParams.minReleaseDateFilter,
        [Validators.required]],
      maxReleaseDateFilter: [ this.gameParams.maxReleaseDateFilter,
        [Validators.required]],
      minDateAddedFilter: [ this.gameParams.minDateAddedFilter,
        [Validators.required]],
      maxDateAddedFilter: [ this.gameParams.maxDateAddedFilter,
        [Validators.required]],
      searchString: [ this.gameParams.searchString,
        [Validators.minLength(3)]]
    })
    this.filterForm.controls.minReleaseDateFilter.valueChanges.subscribe(() => {
    this.minReleaseDate = this.filterForm.controls.minReleaseDateFilter.value;
    })
    this.filterForm.controls.maxReleaseDateFilter.valueChanges.subscribe(() => {
    this.maxReleaseDate = this.filterForm.controls.maxReleaseDateFilter.value;
    })
    this.filterForm.controls.minDateAddedFilter.valueChanges.subscribe(() => {
    this.minDateAdded = this.filterForm.controls.minDateAddedFilter.value;
    })
    this.filterForm.controls.maxDateAddedFilter.valueChanges.subscribe(() => {
    this.maxDateAdded = this.filterForm.controls.maxDateAddedFilter.value;
    })
  }

  loadGames() {
    this.gameStoreService.loadGames(this.gameParams).subscribe(
      response => {
        this.games = response.result;
        this.pagination = response.pagination;
      }
    );
  }

  resetFilters() {
    this.gameParams = this.gameStoreService.resetGameParams();
    this.loadGames();
  }

  applyFilters() {
    this.gameParams = new GameParams(this.filterForm.getRawValue());
    console.log(this.gameParams, typeof this.gameParams);
    this.loadGames();
  }

  pageChanged(event: any) {
    this.gameParams.pageNumber = event.page;
    this.loadGames();
  }

  checkForAdmin() {
    let user = JSON.parse(localStorage.getItem('user'))
  
    if (user !== null && user.roles.filter(x => x === "Admin")) {
      this.isAdmin = true
    }
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  addOrEditGame({game, bool}:{game: Game, bool: boolean}) {
    console.log("games component", game)
    if (!bool)
    {
      console.log("adding new game")
      this.gameStoreService.addGame(game).subscribe()
      this.gameParams.pageNumber = 1
      this.modalService.hide();
      window.location.reload();
    }
    else
    {
      console.log("edit game")
      this.gameStoreService.editGame(game).subscribe()
      this.games.filter(x => x.id === game.id)[0] = game;
      this.gameTo = undefined;
      this.modalService.hide();
      window.location.reload();
    }
  }

  editGame(id: number) {
    console.log("games component got id", id)
    this.gameTo = this.games.filter(x => x.id === id)[0]
    this.modalRef = this.modalService.show(this.editRef)
  }

  deleteGameDialog(id: number) {
    this.deletionId = id;
    this.modalRef = this.modalService.show(this.deleteRef)
  }

  deleteGame() {
    console.log("deleting a game from games component")
    this.gameStoreService.removeGame(this.deletionId).subscribe()
    this.deletionId = 0
    window.location.reload()
  }

  close() {
    this.modalService.hide();
  }
}
