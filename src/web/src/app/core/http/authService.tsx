
import { Requests } from './serviceCore'

export interface SignInRequest {
	email: string;
	password: string;
}

export interface RegisterRequest extends SignInRequest {
	password_confirm: string;
}


export const AuthService = {
	signIn: (o: SignInRequest) => Requests.post('login', o),
	register: (o: RegisterRequest) => Requests.post('register', o),
};
