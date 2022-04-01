
import { VoteList } from '../../shared/types/types';
import { Requests } from './serviceCore'

export interface CreateVoteList {
	title: string;
	numberOfVotes: number;
	books: string[];
}

export interface VoteVoteList {
	id: string;
	books: string[];
}

export const VoteListsService = {
	get: (): Promise<VoteList[]> => Requests.get('vote-lists'),
	getById: (id: string): Promise<VoteList> => Requests.get(`vote-lists/${id}`),
	create: (o: CreateVoteList) => Requests.post('vote-lists', o),
	vote: (o: VoteVoteList) => Requests.post(`vote-lists/${o.id}/vote`, o),
};
