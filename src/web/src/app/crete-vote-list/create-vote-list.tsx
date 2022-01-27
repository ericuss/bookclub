import { FC } from "react";
import { Button, Form } from "react-bootstrap";
import { useBooks } from "./useBooks.hook";
import { BookForVoteDetail } from "./book-for-vote";

import './index.css';

export const CreateVoteList: FC = () => {
    const { state: books } = useBooks();

    async function createList(e: any) {
        e.preventDefault()
        console.log(books.filter(x => x.Selected).length)
    }

    return (
        <div className="create-vote-list">
            Create Vote List

            <Form onSubmit={createList} className="p-5">
                <Form.Group className="mb-3" controlId="user">
                    <Form.Label>Username</Form.Label>
                    <Form.Control type="text" placeholder="Enter user" />
                </Form.Group>
                <Form.Group className="mb-3" controlId="vote-list-name">
                    <Form.Label>List name</Form.Label>
                    <Form.Control type="text" placeholder="Enter vote list name" />
                </Form.Group>
                <Button variant="primary" type="submit">
                    Create list
                    {/* {isImporting ?
                        <>
                            <span className="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                            Importing ...
                        </>
                        : 'Import'} */}
                </Button>
            </Form>
            <div className="book-list">
                {books.map((b, i) => <BookForVoteDetail book={b} />)}
            </div>
        </div>
    );

}