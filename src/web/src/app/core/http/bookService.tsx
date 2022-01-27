
import { Book } from '../../shared/types/types';
import { Requests } from './serviceCore'

export interface ImportBookRequest {
	url: string;
	user: string;
}

export const BooksService = {
	get: (): Promise<Book[]> => Requests.get('books'),
	getUnreaded: (): Promise<Book[]> => Requests.get(`books/unreaded`),
	import: (o: ImportBookRequest): Promise<void> => Requests.post('books', o),
	readed: (id: string): Promise<void> => Requests.put(`books/${id}/readed`, {}),
	unreaded: (id: string): Promise<void> => Requests.put(`books/${id}/unreaded`, {}),
};
