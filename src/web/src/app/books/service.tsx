
import { Book } from './types';
import { Requests, Instance } from '../core/http/serviceCore'

export interface ImportBookRequest {
    url: string;
}

export const BooksService = {
	get: (): Promise<Book[]> => Requests.get('books'),
	import: async (o: ImportBookRequest): Promise<any> => await Requests.post('books', o),
};
