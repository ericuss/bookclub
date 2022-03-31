import { BookFromVoteList } from '../shared/types/types'

export interface BookForVote extends BookFromVoteList {
    Selected: boolean;
}
