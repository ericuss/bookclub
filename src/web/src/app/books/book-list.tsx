import { FC, useState } from "react";
import { useBooks } from "./useBooks.hook";
import { BooksService, ImportBookRequest } from '../core/http/bookService'
import { Book, Token } from "../shared/types/types";
import './index.css';
import { Button, Card, Form } from "react-bootstrap";

type BookProps = {
    book: Book
}

export const BookList: FC = () => {
    const { state: books, loadBooks } = useBooks();
    const [isImporting, setImporting] = useState(false);

    async function importBook(e: any) {
        e.preventDefault()
        setImporting(true);
        try {
            const request: ImportBookRequest = {
                url: e.currentTarget.elements["book-url"].value,
            }

            if (!request.url) {
                console.log("Request has empty values");
            } else {
                await BooksService.import(request);
                await loadBooks();
            }

        } catch (error) {
            console.log(error);
        }

        setImporting(false);
    }

    async function markAsReaded(id: string) {
        try {
            await BooksService.readed(id);
            await loadBooks();

        } catch (error) {
            console.log(error);
        }
    }

    async function markAsUnreaded(id: string) {
        try {
            await BooksService.unreaded(id);
            await loadBooks();

        } catch (error) {
            console.log(error);
        }
    }

    const getUser = (): Token => {
        try {
            const token = localStorage.getItem("jwt");
            if (token !== null) {
                return JSON.parse(atob(token?.split('.')[1]));
            }

        } catch (e) {
        }

        return { exp: -1, iss: '' };
    };

    const user = getUser();

    const Book: FC<BookProps> = ({ book }) => {
        return (
            <Card className="book mt-3" style={{ width: '18rem' }}>
                {book.Readed !== null && book.Readed?.indexOf(user.iss) !== -1
                    ? <button className="btn btn-warning" onClick={() => markAsUnreaded(book.Id)}>Mark as unreaded</button>
                    : <button className="btn btn-success" onClick={() => markAsReaded(book.Id)}>Mark as readed</button>
                }
                <Card.Img variant="top" src={book.ImageUrl} alt="Book cover" />
                <Card.Body>
                    <Card.Title>{book.Title}</Card.Title>
                    <Card.Subtitle >{book.Series}</Card.Subtitle>
                    <Card.Subtitle className="mt-2" >{book.Authors}</Card.Subtitle>
                    <Card.Text className="mt-2 d-flex flex-column position-relative">
                        <dfn title={book.Sinopsis}> <span className="text-ellipsis--3">{book.Sinopsis}</span> </dfn>
                        <span><i>Added by {book.Username}</i></span>
                        <p className="text-end">{book.Rating}</p>
                    </Card.Text>
                    <a href={book.Url} className="btn btn-secondary" rel="noreferrer" target="_blank">Go to book source</a>
                </Card.Body>
            </Card>
        );
    };

    return (
        <div className="books">
            <Form onSubmit={importBook} className="p-5">
                <Form.Group className="mb-3" controlId="book-url">
                    <Form.Label>Book link</Form.Label>
                    <Form.Control type="text" placeholder="Enter url of book" />
                </Form.Group>

                {/* <button class="btn btn-primary" type="button" disabled>
</button> */}
                <Button variant="primary" type="submit">
                    {isImporting ?
                        <>
                            <span className="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                            Importing ...
                        </>
                        : 'Import'}
                </Button>
            </Form>
            <div className="book-list">
                {books.map((b) => <Book book={b} />)}
            </div>
        </div>
    );

}