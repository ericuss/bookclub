
import axios, { AxiosResponse } from 'axios';
const config = {
	// baseURL: 'http://host.docker.internal:8080/api/',
	baseURL: 'http://localhost:8080/api/',
	// baseURL: '/api/',
	timeout: 15000,
	headers: {
		'Content-Type': 'application/json',
		'Authorization': `Bearer ${localStorage.getItem("jwt")}`
		// 'X-Requested-With': 'XMLHttpRequest',
	},

	// withCredentials: true
};

const selfWindow: Window = window;

export const Instance = axios.create(config);

Instance.interceptors.response.use((response) => {
	return response;
}, (error) => { // Anything except 2XX goes to here
	const status = error.response?.status || 500;
	if (status === 401) {
		selfWindow.location = window.location.protocol + "//" + window.location.host + "/public/sign-in";
	} else {
		return Promise.reject(error); // Delegate error to calling side
	}
});

export const responseBody = (response: AxiosResponse) => response.data;

export const Requests = {
	get: (url: string) => Instance.get(url).then(responseBody),
	post: (url: string, o: any) => Instance.post(url, JSON.stringify(o)).then(responseBody),
	put: (url: string, o: any) => Instance.put(url, JSON.stringify(o)).then(responseBody),
	delete: (url: string, o: any) => Instance.delete(config.baseURL + url).then(responseBody),
};


