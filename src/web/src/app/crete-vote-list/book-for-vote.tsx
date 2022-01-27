import { FC, useState } from "react";
import { Card } from "react-bootstrap";
import { BookForVote } from "./types";

type BookProps = {
    book: BookForVote;
}
export const BookForVoteDetail: FC<BookProps> = ({ book: bookRaw }) => {
    const [book, setBook] = useState<BookForVote>(bookRaw);

    function selectBook() {
        book.Selected = !!!book.Selected;
        bookRaw.Selected = book.Selected;
        setBook({ ...book });
    }

    return (
        <Card className={"book mt-3" + (book.Selected !== true ? '' : 'border border-3 border-primary rounded ')} style={{ width: '18rem' }} onClick={() => selectBook()}>
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

}