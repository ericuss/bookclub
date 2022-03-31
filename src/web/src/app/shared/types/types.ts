export interface Book {
    Id: string;
    Title: string;
    Series: string;
    Authors: string;
    Rating: string;
    Sinopsis: string;
    ImageUrl: string;
    Url: string;
    Pages: string;
    Username: string;
    Readed?: string[];
}

export interface Window {
    location: any;
}

export interface Token {
    exp: number;
    iss: string;
}

export interface VoteList {
    Id: string;
    UserId: string;
    Title: string;
    Books: BookFromVoteList[];
}

export interface BookFromVoteList extends Book {
    VotedBy?: string[];
}
