
import { BookForVote } from './types';
import { useEffect, useState } from 'react';
import { BooksService } from '../core/http/bookService'
import { CreateVoteList, VoteListsService } from '../core/http/voteListsService'


export interface UseBooksType {
    state: BookForVote[];
    setState: React.Dispatch<React.SetStateAction<BookForVote[]>>;
    loadUnreadedBooks(): Promise<void>;
    createVoteList(title: string, books: string[]): Promise<void>;
}

export function useBooks(): UseBooksType {
    const [state, setState] = useState<BookForVote[]>([]);

    useEffect(() => {
        loadUnreadedBooks();
    }, []);

    async function loadUnreadedBooks() {
        try {
            const response = await BooksService.get();
            const books = response as BookForVote[] || [];
            books.forEach(x => x.Selected = false)
            setState(books);

        } catch (error) {
            console.log(error);
        }
    }

    async function createVoteList(title: string, books: string[]) {
        try {
            const voteList: CreateVoteList = {
                title,
                books,
            }
            await VoteListsService.create(voteList);
        } catch (error) {
            console.log(error);
        }
    }

    return { state, setState, loadUnreadedBooks, createVoteList };
}
