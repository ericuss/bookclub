
import { Book } from './types';
import { Requests } from '../core/http/serviceCore'

export interface ImportBookRequest {
	url: string;
	user: string;
}

export const BooksService = {
	get: (): Promise<Book[]> => Requests.get('books'),
	import: (o: ImportBookRequest): Promise<void> => Requests.post('books', o),
	readed: (id: string): Promise<void> => Requests.put(`books/${id}/readed`, {}),
	unreaded: (id: string): Promise<void> => Requests.put(`books/${id}/unreaded`, {}),
};
