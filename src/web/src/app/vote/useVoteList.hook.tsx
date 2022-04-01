
import { useEffect, useState } from 'react';
import { VoteListsService } from '../core/http/voteListsService'
import { VoteList } from '../shared/types/types';


export interface UseVoteListType {
    state: VoteList[] | null;
    setState: React.Dispatch<React.SetStateAction<VoteList[] | null>>;
    loadVoteList(): Promise<void>;
}

export function useVoteList(): UseVoteListType {
    const [state, setState] = useState<VoteList[] | null>(null);

    useEffect(() => {
        loadVoteList();
    }, []);

    async function loadVoteList() {
        try {
            const response = await VoteListsService.get();
            setState(response);

        } catch (error) {
            console.log(error);
        }
    }

    return { state, setState, loadVoteList };
}
