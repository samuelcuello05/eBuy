import Styles from './Login.module.css';
import { NavLink, resolvePath } from "react-router-dom";
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import { ReactComponent as BackButton } from '../../images/goback.svg'

export default function Login() {
    return(
        <section className={Styles["register"]}>
          
            <Form  className={Styles["register-form"]}>
                <NavLink to={resolvePath(-1).pathname} className={Styles["back-link"]}>
                    <BackButton className={Styles["back-button"]} />
                </NavLink>
                 
                <h2 className={Styles["register-h2"]}>Login</h2>
                
                <Form.Group className={`${Styles["input-container"]} ${"mb-3"}`} controlId="formBasicEmail">
                    <Form.Label>Email address</Form.Label>
                    <Form.Control type="email" placeholder="Enter email" required/>
                </Form.Group>

                <Form.Group className={`${Styles["input-container"]} ${"mb-3"}`} controlId="formBasicPassword">
                    <Form.Label>Password</Form.Label>
                    <Form.Control type="password" placeholder="Password" required/>
                </Form.Group>

                <Button variant="primary" type="submit" id={Styles["submit-button"]}>
                    Login
                </Button>
                
                <Form.Text className={`${Styles["login-text"]} ${"text-muted"}`} id={Styles["login-text"]}>
                ¿Dont have an account?
                    <NavLink to={"/register"}>
                        Register
                    </NavLink>
                </Form.Text>
               
            </Form>
            <div>
                
          
            </div>
        </section>
    );
}