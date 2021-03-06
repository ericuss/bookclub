import { FC } from "react";
import { Button, Form } from "react-bootstrap";
import { useBooks } from "./useBooks.hook";
import { BookForVoteDetail } from "./book-for-vote";

import './index.css';

export const CreateVoteList: FC = () => {
    const { state: books, createVoteList } = useBooks();

    async function createList(e: any) {
        e.preventDefault()
        await createVoteList(
            e.currentTarget.elements["vote-list-name"].value,
            e.currentTarget.elements["vote-list-number-of-votes"].value,
            books.filter(x => x.Selected).map(x => x.Id)
        )
        console.log(books.filter(x => x.Selected))
    }

    return (
        <div className="create-vote-list">
            Create Vote List

            <Form onSubmit={createList} className="p-5">
                <Form.Group className="mb-3" controlId="vote-list-name">
                    <Form.Label>List name</Form.Label>
                    <Form.Control type="text" placeholder="Enter vote list name" />
                </Form.Group>
                <Form.Group className="mb-3" controlId="vote-list-number-of-votes">
                    <Form.Label>Number of votes</Form.Label>
                    <Form.Control type="number" placeholder="Enter the number of voters for users" />
                </Form.Group>
                <Button variant="primary" type="submit">
                    Create list
                </Button>
            </Form>
            <div className="book-list">
                {books.map((b, i) => <BookForVoteDetail key={i} book={b} />)}
            </div>
        </div>
    );

}