import { FC } from "react";
import { useVoteList } from "./useVoteList.hook";
import { BookForVoteDetail } from "./book-for-vote";

import './index.css';
import { BookForVote } from "./types";

export const VoteListDetail: FC = () => {
    const { state: voteList } = useVoteList();

    const booksForVote = voteList?.Books.map((x) => ({
        ...x,
        Selected: false
    })) as BookForVote[];

    return (
        <div className="vote-vote-list">
            <h1> Vote List {voteList?.Title}</h1>
            <div className="vote-vote-list-books">
                {booksForVote?.map((b, i) => <BookForVoteDetail book={b} />)}
            </div>
        </div>
    );

}