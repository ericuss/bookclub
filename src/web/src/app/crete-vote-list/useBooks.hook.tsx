
import { BookForVote } from './types';
import { useEffect, useState } from 'react';
import { BooksService } from '../core/http/bookService'


export interface UseBooksType {
    state: BookForVote[];
    setState: React.Dispatch<React.SetStateAction<BookForVote[]>>;
    loadUnreadedBooks(): Promise<void>;
}

export function useBooks(): UseBooksType {
    const [state, setState] = useState<BookForVote[]>([]);

    useEffect(() => {
        loadUnreadedBooks();
    }, []);

    async function loadUnreadedBooks() {
        try {
            const response = await BooksService.getUnreaded();
            const books = response as BookForVote[] || [];
            books.forEach(x => x.Selected = false)
            setState(books);

        } catch (error) {
            console.log(error);
        }
    }

    return { state, setState, loadUnreadedBooks };
}
