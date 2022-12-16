import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BasketComponent } from './basket/basket.component';
import { GamesComponent } from './gamesFolder/games/games.component';
import { AuthGuard } from './_guards/auth.guard';
import { RegisterComponent } from './register/register.component';
import { NotFoundComponent } from './errorsFolder/not-found/not-found.component';
import { GameComponent } from './gamesFolder/game-detail/game.component';
import { TestErrorsComponent } from './errorsFolder/test-errors/test-errors.component';
import { DiscussionDetailComponent } from './discussionsFolder/discussion-detail/discussion-detail.component';
import { ProfileComponent } from './profile/profile.component';
import { DiscussionsListComponent } from './discussionsFolder/discussions-list/discussions-list.component';

const routes: Routes = [
  {path: '', component: GamesComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'basket', component: BasketComponent },
    ]
  },
  {path: 'games', component: GamesComponent},
  {path: 'discussions', component: DiscussionsListComponent},
  {path: 'discussions/:gameId', component: DiscussionsListComponent},
  {path: 'discussions/details/:id', component: DiscussionDetailComponent},
  {path: 'games/:id', component: GameComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'errors', component: TestErrorsComponent},
  {path: 'not-found', component: NotFoundComponent},
  {path: 'member/profile', component: ProfileComponent},
  {path: '**', component: NotFoundComponent, pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
