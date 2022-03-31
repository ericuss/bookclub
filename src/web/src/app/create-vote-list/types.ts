import { Book } from '../shared/types/types'

export interface BookForVote extends Book {
    Selected: boolean;
}
