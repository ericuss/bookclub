
import axios, { AxiosResponse } from 'axios';
const config = {
	// baseURL: 'http://host.docker.internal:8080/api/',
	baseURL: 'http://localhost:8080/api/',
	timeout: 15000,
	headers: {
		'Content-Type': 'application/json',
		// 'X-Requested-With': 'XMLHttpRequest',
	}
};

export const Instance = axios.create(config);

export const responseBody = (response: AxiosResponse) => response.data;

export const Requests = {
	get: (url: string) => Instance.get(url).then(responseBody),
	post: (url: string, o: any): Promise<void> => axios.post('http://localhost:8080/api/' + url, JSON.stringify(o), { headers: config.headers }),
};


