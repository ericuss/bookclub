import { FC, useEffect, useState } from "react";
import { useVoteList } from "./useVoteList.hook";
import { BookForVoteDetail } from "./book-for-vote";

import './index.css';
import { BookForVote } from "./types";

export const VoteListDetail: FC = () => {
    const { state: voteList } = useVoteList();
    const [voted, setVoted] = useState<number>(0);
    const [isSubmitValid, setIsSubmitValid] = useState<boolean>(false);
    const [booksForVote, setBooksForVote] = useState<BookForVote[]>([]);

    useEffect(() => {
        setBooksForVote(voteList?.Books.map((x) => ({
            ...x,
            Selected: false
        })) as BookForVote[] || [voteList]);
    }, [voteList])

    const selectBook = (index: number) => {
        booksForVote[index].Selected = !booksForVote[index].Selected;
        setVoted(booksForVote.filter(x => x.Selected).length);
    }

    useEffect(() => {
        if (voteList == null) return;

        setIsSubmitValid(voteList.NumberOfVotes >= voted);
    }, [voteList, voted])

    return (
        <div className="vote-vote-list">
            <h1> Vote List {voteList?.Title}</h1>
            <div className="d-flex justify-content-between p-3">
                <h5> Voted: {voted}/{voteList?.NumberOfVotes}</h5>
                <button className={"btn btn-primary " + (isSubmitValid ? "" : " disabled")}>Vote</button>
            </div>
            <div className="vote-vote-list-books">
                {booksForVote.length !== 0 && booksForVote.map((b, i) => <BookForVoteDetail key={i} index={i} selectBook={selectBook} book={b} />)}
            </div>
        </div>
    );

}