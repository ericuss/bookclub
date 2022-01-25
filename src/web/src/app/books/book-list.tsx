import { FC } from "react";
import { useBooks } from "./useBooks.hook";
import { BooksService, ImportBookRequest } from './service'
import { Book } from "./types";
import './index.css';

type BookProps = {
    book: Book
}

export const BookList: FC = () => {
    const { state: books, loadBooks} = useBooks();

    async function importBook(e: any) {
        e.preventDefault()

        try {
            const request: ImportBookRequest = {
                url: e.currentTarget.elements["book-url"].value,
            }

            if (!request.url) {
                console.log("url is empty");
            }

            await BooksService.import(request);
            await loadBooks();

        } catch (error) {
            console.log(error);
        }
    }

    const Book: FC<BookProps> = ({ book }) => {
        return (
            <div className="book">
                <img src={book.ImageUrl} />
                <div className="book__name">{book.Title}</div>
                <div className="book__source">{book.Url}</div>
            </div>
        );
    };

    return (
        <div className="books">
            <form onSubmit={importBook}>
                <div>
                    <label htmlFor="book-url">Book link:</label>
                    <input id="book-url" type="text" />
                </div>
                <button type="submit">Import</button>
            </form>
            {books.map((b) => <Book book={b} />)}
        </div>
    );

}