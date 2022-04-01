
import { FC, useEffect, useState } from "react";
import { Button, Form } from "react-bootstrap";
import { useAuth } from "./useAuth.hook";
import { ReactComponent as Logo } from '../../assets/images/books.svg';
// import './index.css';

export const SignIn: FC = () => {
    const { register, signIn } = useAuth();
    const [isRegisterVisible, setIsRegisterVisible] = useState<boolean>(false);

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

            setIsRegisterVisible(false)
        } catch (error) {
            console.log(error);
        }
    }

useEffect(() => { console.log(isRegisterVisible)}, [isRegisterVisible])
    const signInForm = () => {
        return <Form onSubmit={signInAction} className="sign-in ">
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
            <p className="text-center">Not a member? <span data-toggle="tab" className="link-primary"  onClick={() => setIsRegisterVisible(true)}>Register</span></p>
        </Form>;

    }

    const registerForm = () => {
        return <Form onSubmit={registerAction} className="register">
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
            <p className="text-center">Already a member? <span data-toggle="tab" className="link-primary"  onClick={() => setIsRegisterVisible(false)}>Sign in</span></p>
        </Form>;
    }



    return (
        <main className=" text-center">
            <div className="row justify-content-center">
                <div className="col-md-12 col-lg-10">
                    <div className="wrap d-md-flex align-items-center">

                        <Logo className="w-50"></Logo>
                        {/* <div className="img" style={{ backgroundImage: "url(images/xbg-1.jpg.pagespeed.ic.3OAd9jZTMD.webp)" }}>
                        </div> */}
                        <div className="login-wrap p-4 p-md-5 w-50 ">
                            {!isRegisterVisible ? signInForm() : registerForm()}
                        </div>
                    </div>
                </div>
            </div>
        </main>
    );
}