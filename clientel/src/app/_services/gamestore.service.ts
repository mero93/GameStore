import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, map, of } from "rxjs";
import { Genre, Game, Publisher } from "../_models/Game";
import { GameParams } from "../_models/GameParams";
import { PaginatedResult } from "../_models/pagination";
import { getPaginationHeaders } from "./paginationHelper";
import { addDays } from "./typeHelpers";

@Injectable()
export class GameStoreService {
    games: Game[] = [];
    paginatedResult: PaginatedResult<Game[]> = new PaginatedResult<Game[]>();
    gamesCache = new Map();
    gameParams = new GameParams();
    filterForm
    options: string[] = [
        "Newly Added",
        "Newest Releases",
        "Most Popular",
        "Best Rated"
    ]

    minReleaseDate: Date = new Date(2000, 1, 1);
    maxReleaseDate: Date = new Date(new Date().getFullYear() + 1, new Date().getMonth(), new Date().getDate());
    minDateAdded: Date = new Date(2022, 9, 1);
    maxDateAdded: Date = addDays(new Date(), 1);
    

    constructor(private http: HttpClient) {

    }

    loadGames(gameParams: GameParams) {
        var response = this.gamesCache.get(Object.values(gameParams))
        if (response) {
            return of(response);
        }

        let params = getPaginationHeaders(gameParams.pageNumber, gameParams.pageSize);

        params = params.append('minDownloads', gameParams.minDownloads.toString());
        params = params.append('minRating', gameParams.minRating.toString());
        params = params.append('minReleaseDateFilter', gameParams.minReleaseDateFilter.toLocaleDateString());
        params = params.append('maxReleaseDateFilter', gameParams.maxReleaseDateFilter.toLocaleDateString());
        params = params.append('minDateAddedFilter', gameParams.minDateAddedFilter.toLocaleDateString());
        params = params.append('maxDateAddedFilter', gameParams.maxDateAddedFilter.toLocaleDateString());
        params = params.append('sortBy', gameParams.sortBy);
        params = params.append('reverseOrder', gameParams.reverseOrder);
        if (gameParams.publishersFilter.length === 1)
        {
            params = params.append('publishersFilter', gameParams.publishersFilter[0]);
        }

        else if (gameParams.publishersFilter.length > 1)
        {
            params = params.append('publishersFilter', gameParams.publishersFilter.join('-'));
        }
        
        if (gameParams.genresFilter.length === 1)
        {
            params = params.append('publishersFilter', gameParams.genresFilter[0]);
        }

        else if (gameParams.genresFilter.length > 1)
        {
            params = params.append('publishersFilter', gameParams.genresFilter.join('-'));
        }


        return this.http.get<Game[]>("https://localhost:5001/api/games", {observe: 'response', params})
            .pipe(map(response => {
                this.paginatedResult.result = response.body;
                if (response.headers.get('Pagination') !== null) {
                    this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
                }
                return this.paginatedResult;
        }));
    }

    setGameParams(params: GameParams) {
        this.gameParams = params;
    }

    resetGameParams() {
        this.gameParams = new GameParams();
        return this.gameParams;
    }

    returnGameById(gameId: number): Observable<Game> {
        return this.http.get<Game>("https://localhost:5001/api/games/" + gameId)
            .pipe(map(data => {
                return data;
        }));
    }

    getPublishers(): Observable<Publisher[]> {
        return this.http.get<Publisher[]>("https://localhost:5001/api/games/get-publishers")
    }

    getGenres(): Observable<Genre[]> {
        return this.http.get<Genre[]>("https://localhost:5001/api/games/get-genres")
    };

    addGame(model: Game) {
        return this.http.post("https://localhost:5001/api/games/create-game", model)
    }

    editGame(model: Game) {
        return this.http.put("https://localhost:5001/api/games/update-game", model)
    }

    removeGame(id: number) {
        return this.http.delete("https://localhost:5001/api/games/remove-game/" + id)
    }
}