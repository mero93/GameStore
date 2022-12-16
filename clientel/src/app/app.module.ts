import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule, HTTP_INTERCEPTORS}  from '@angular/common/http';
import { TooltipModule } from 'ngx-bootstrap/tooltip';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RegisterComponent } from './register/register.component';
import { GamesComponent } from './gamesFolder/games/games.component';
import { GameComponent } from './gamesFolder/game-detail/game.component';
import { GameStoreService } from './_services/gamestore.service';
import { AccountService } from './_services/account.service';
import { TextInputComponent } from './_forms/text-input/text-input.component';
import { DateInputComponent } from './_forms/date-input/date-input.component';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { NotFoundComponent } from './errorsFolder/not-found/not-found.component';
import { TestErrorsComponent } from './errorsFolder/test-errors/test-errors.component';
import { ServerErrorComponent } from './errorsFolder/server-error/server-error.component';
import { DiscussionsListComponent } from './discussionsFolder/discussions-list/discussions-list.component';
import { DiscussionDetailComponent } from './discussionsFolder/discussion-detail/discussion-detail.component';
import { CommentListComponent } from './commentsFolder/comment-list/comment-list.component';
import { CommentComponent } from './commentsFolder/comment/comment.component';
import { CommentFormComponent } from './commentsFolder/comment-form/comment-form.component';
import { ToastrModule } from 'ngx-toastr';
import { BasketComponent } from './basket/basket.component';
import { AuthGuard } from './_guards/auth.guard';
import { RatingModule, RatingConfig} from 'ngx-bootstrap/rating';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { GameCardComponent } from './gamesFolder/game-card/game-card.component';

import { BsDatepickerModule, BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { BsDropdownModule,BsDropdownConfig } from 'ngx-bootstrap/dropdown';
import { ModalModule, BsModalService } from 'ngx-bootstrap/modal';
import { TabsModule, TabsetConfig } from 'ngx-bootstrap/tabs';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { TimeagoModule } from 'ngx-timeago';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import {MatExpansionModule} from '@angular/material/expansion';
import { NumberInputComponent } from './_forms/number-input/number-input.component';
import {MAT_FORM_FIELD_DEFAULT_OPTIONS} from '@angular/material/form-field';
import {MatBadgeModule} from '@angular/material/badge';
import { RegisterFormComponent } from './_forms/register-form/register-form.component';
import { AdditionalInfoFormComponent } from './_forms/additional-info-form/additional-info-form.component';
import {MatStepperModule} from '@angular/material/stepper';
import { AlertModule } from 'ngx-bootstrap/alert';
import { GameFormComponent } from './_forms/game-form/game-form.component';
import { PhotoEditorComponent } from './_forms/photo-editor/photo-editor.component';
import {FileUploadModule } from 'ng2-file-upload';
import { ProfileComponent } from './profile/profile.component';
import {MatTooltipModule} from '@angular/material/tooltip';






@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    RegisterComponent,
    GamesComponent,
    GameComponent,
    TextInputComponent,
    DateInputComponent,
    TestErrorsComponent,
    NotFoundComponent,
    ServerErrorComponent,
    BasketComponent,
    GameCardComponent,
    DiscussionsListComponent,
    DiscussionDetailComponent,
    CommentComponent,
    CommentFormComponent,
    CommentListComponent,
    NumberInputComponent,
    RegisterFormComponent,
    AdditionalInfoFormComponent,
    GameFormComponent,
    PhotoEditorComponent,
    ProfileComponent,
  ],
  
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    TooltipModule.forRoot(),
    FormsModule,
    BsDatepickerModule.forRoot(),
    BsDropdownModule,
    ModalModule,
    ReactiveFormsModule,
    PaginationModule.forRoot(),
    RatingModule,
    TabsModule.forRoot(),
     ToastrModule.forRoot({
       positionClass: 'toastr-bottom-right'
     }),
     TimeagoModule.forRoot(),
     MatSelectModule,
     MatButtonModule,
     MatFormFieldModule,
     MatToolbarModule,
     MatIconModule,
     MatSlideToggleModule,
     MatExpansionModule,
     MatBadgeModule,
     MatStepperModule,
     AlertModule,
     FileUploadModule,
     MatTooltipModule,
  ],
  providers: [BsDatepickerConfig, GameStoreService, AccountService, AuthGuard, RatingConfig,
    BsDropdownConfig, BsModalService, TabsetConfig,
  {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
  {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
  { provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: { appearance: 'fill' } },

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
