
import { Book } from '../shared/types/types';
import { useEffect, useState } from 'react';
import { BooksService } from '../core/http/bookService'


export interface UseBooksType {
    state: Book[];
    loadBooks(): Promise<void>;
}

export function useBooks(): UseBooksType {
    const [state, setState] = useState<Book[]>([]);

    useEffect(() => {
        loadBooks();
    }, []);

    async function loadBooks() {
        try {
            const response = await BooksService.get();

            setState(response || []);

        } catch (error) {
            console.log(error);
        }
    }

    return { state, loadBooks };
}
