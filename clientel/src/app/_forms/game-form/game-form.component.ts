import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, Validators, ValidatorFn, AbstractControl, FormGroup } from '@angular/forms';
import { Subject } from 'rxjs';
import { Game, Genre, Publisher } from 'src/app/_models/Game';
import { ImageUplModelType } from 'src/app/_models/imageUplModelType';
import { Photo } from 'src/app/_models/photo';

@Component({
  selector: 'app-game-form',
  templateUrl: './game-form.component.html',
  styleUrls: ['./game-form.component.css']
})
export class GameFormComponent {
  form: FormGroup;
  validationErrors: string[] = [];
  game: Game;

  imageUplModelType = ImageUplModelType.Game

  eventsSubject: Subject<void> = new Subject<void>();

  @Input() model: Game;
  @Input() genres: Genre[];
  @Input() publishers: Publisher[];

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {

    console.log(this.genres);
    console.log(this.publishers);
    console.log(this.model)
    this.initializeForm();
  }

  @Output()
  handleSubmit = new EventEmitter<{game: Game, bool: boolean}>();

  @Output()
  handleCancel = new EventEmitter<void>();

  emitEventToChild() {
    this.eventsSubject.next();
  }

  initializeForm() {
    this.form = this.fb.group({
      name: [this.model? this.model.name : "",
        [Validators.required, Validators.maxLength(40), Validators.minLength(3)]],
      releaseDate: [this.model? this.model.releaseDate : new Date(),
        [Validators.required]],
      description: [this.model? this.model.description : "",
        [Validators.required, Validators.maxLength(600), Validators.minLength(9),]],
      categoriesId: [this.model? this.model.categoriesId :  [] as number[],
        [Validators.required, Validators.minLength(1)]],
      publisherId: [this.model? this.model.publisherId : 0, 
        [Validators.required, Validators.min(1)]],
      price: [this.model? this.model.price : 0,
        [Validators.required, Validators.min(0)]],
      
      //Not Visible
      id: [this.model? this.model.id : 0],
      dateAdded: [this.model? this.model.dateAdded : new Date()],
      rating: [this.model? this.model.rating : 0],
      ratingNumber: [this.model? this.model.ratingNumber : 0],
      downloads: [this.model? this.model.downloads : 0],
      categories: [this.model? this.model.categories : [] as string[]],
      publisher: [this.model? this.model.publisher : ""],
    })
  }

  onSubmit() {
    this.game = new Game(this.form.value);
    this.game.categories = this.genres.filter(
      genre => this.form.controls.categoriesId.value?.some(id => id === genre.id)
    ).map(function(g) {
      return g.name
    });
    this.game.publisher = this.publishers.filter(
      publisher => this.form.controls.publisherId.value === publisher.id
    )[0]?.name;
    this.eventsSubject.next();
  }

  onImageUploaded(photo: Photo) {
    let bool = this.model !== undefined;
    if (photo) {
      this.game.photoUrl = photo.photoUrl;
    this.game.publicId = photo.publicId;
    }
    let game = this.game;
    this.handleSubmit.emit({game,  bool});
    this.form.reset();
  }

  onCancel() {
    this.handleCancel.emit();
    this.form.reset();
  }
}
