export class Game {
    public constructor(init?: Partial<Game>) {
        Object.assign(this, init)
    }

    id: number | undefined;
    name: string;
    releaseDate?: Date;
    dateAdded?: Date;
    description?: string;
    downloads?: number;
    rating?: number;
    ratingNumber: number;
    price: number;
    categories?: string[];
    categoriesId?: number[];        
    publisher?: string;
    publisherId?: number;
    photoUrl: string | undefined;
    publicId: string | undefined;
}

export class Publisher {
    id: number;
    name: string;
}

export class Genre {
    id: number;
    name: string;
}