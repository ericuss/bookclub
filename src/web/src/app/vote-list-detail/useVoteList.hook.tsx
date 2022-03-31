
import { useEffect, useState } from 'react';
import { VoteListsService } from '../core/http/voteListsService'
import { VoteList } from '../shared/types/types';


export interface UseVoteListType {
    state: VoteList | null;
    setState: React.Dispatch<React.SetStateAction<VoteList | null>>;
    loadVoteList(id: string): Promise<void>;
}

export function useVoteList(): UseVoteListType {
    const [state, setState] = useState<VoteList | null>(null);

    useEffect(() => {
        const paths = window.location.pathname.split("/");
        loadVoteList(paths[paths.length - 1]);
    }, []);

    async function loadVoteList(id: string) {
        try {
            const response = await VoteListsService.getById(id);
            const voteList = response as VoteList;
            setState(voteList);

        } catch (error) {
            console.log(error);
        }
    }

    return { state, setState, loadVoteList };
}
