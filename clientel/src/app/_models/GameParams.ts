import { addDays } from "../_services/typeHelpers";

export class GameParams {
    public constructor(init?: Partial<GameParams>) {
        Object.assign(this, init)
    }

    pageNumber: number = 1;
    pageSize: number = 6;
    sortBy: string = "Newly Added";
    reverseOrder: boolean  = false;
    publishersFilter: string[] = [];
    genresFilter: string[] = [];
    minDownloads: number = 0;
    minRating: number = 0;
    minReleaseDateFilter: Date = new Date(2000, 1, 1);
    maxReleaseDateFilter: Date = new Date(new Date().getFullYear() + 1, new Date().getMonth(), new Date().getDate());
    minDateAddedFilter: Date = new Date(2022, 9, 1);
    maxDateAddedFilter: Date = addDays(new Date(), 1);
    searchString: string;
}