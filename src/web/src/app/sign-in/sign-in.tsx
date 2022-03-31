
import { FC } from "react";
import { Button, Form } from "react-bootstrap";
import { useAuth } from "./useAuth.hook";
// import './index.css';

export const SignIn: FC = () => {
    const { register, signIn } = useAuth();

    async function signInAction(e: any) {
        e.preventDefault()
        try {
            await signIn(
                e.currentTarget.elements["sign-in-email"].value,
                e.currentTarget.elements["sign-in-password"].value
            )
        } catch (error) {
            console.log(error);
        }
    }

    async function registerAction(e: any) {
        e.preventDefault()
        try {
            await register(
                e.currentTarget.elements["register-email"].value,
                e.currentTarget.elements["register-password"].value,
                e.currentTarget.elements["register-password-confirmation"].value
            )
        } catch (error) {
            console.log(error);
        }
    }

    return (
        <main className=" text-center">
            <Form onSubmit={signInAction} className="sign-in p-5">
                <h1 className="h3 mb-3 font-weight-normal">Sign in</h1>
                <Form.Group className="mb-3" controlId="sign-in-email">
                    <Form.Label>Email</Form.Label>
                    <Form.Control type="email" placeholder="sign-in email" required autoFocus />
                </Form.Group>
                <Form.Group className="mb-3" controlId="sign-in-password">
                    <Form.Label>Password</Form.Label>
                    <Form.Control type="password" placeholder="Enter password" required />
                </Form.Group>

                <Button variant="primary" type="submit">Sign in</Button>
            </Form>

            
            <Form onSubmit={registerAction} className="register p-5">
                <h1 className="h3 mb-3 font-weight-normal">Register</h1>
                <Form.Group className="mb-3" controlId="register-email">
                    <Form.Label>Email</Form.Label>
                    <Form.Control type="email" placeholder="Enter email" required autoFocus />
                </Form.Group>
                <Form.Group className="mb-3" controlId="register-password">
                    <Form.Label>Password</Form.Label>
                    <Form.Control type="password" placeholder="Enter password" required />
                </Form.Group>
                <Form.Group className="mb-3" controlId="register-password-confirmation">
                    <Form.Label>Confirm password</Form.Label>
                    <Form.Control type="password" placeholder="Enter password confirmation" required />
                </Form.Group>

                <Button variant="primary" type="submit">Register</Button>
            </Form>
        </main>


    );
}