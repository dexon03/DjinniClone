import { Button, TextField, Container, Typography } from '@mui/material';
import {Link} from 'react-router-dom';
import {useState} from "react";
import {RestClient} from "../../api/rest.client.ts";
import {JwtResponse} from "../../models/auth/jwt.respone.ts";
import {ApiServicesRoutes} from "../../api/api.services.routes.ts";
import {LoginModel} from "../../models/auth/login.model.ts";

function LoginPage({setToken} : {setToken: (token: JwtResponse) => void}) {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const restClient = new RestClient();
    const onSubmit = async (event) => {
        event.preventDefault();
        const token = await restClient.post<JwtResponse>(ApiServicesRoutes.auth + '/login', {
            email: email,
            password: password
        } as LoginModel);
        console.log(token)
        setToken(token);
    }
    return (
        <Container maxWidth="sm">
            <Typography variant="h4" align="center" gutterBottom>
                Login
            </Typography>
            <form onSubmit={onSubmit}>
                <TextField
                    label="Email"
                    fullWidth
                    onChange={(event) => setEmail(event.target.value)}
                    variant="outlined"
                    margin="normal"
                />
                <TextField
                    label="Password"
                    type="password"
                    onChange={(event) => setPassword(event.target.value)}
                    fullWidth
                    variant="outlined"
                    margin="normal"
                />
                <Button
                    variant="contained"
                    color="primary"
                    type="submit"
                    fullWidth
                    size="large"
                    style={{ marginTop: 16 }}
                >
                    Sign In
                </Button>
            </form>
            <Button
                variant="text"
                color="primary"
                style={{ marginTop: 8 }}
            >
                Forgot Password
            </Button>
            <Button
                variant="text"
                color="primary"
                style={{ marginTop: 8 }}
                component={Link} to="/register"
            >
                Sign Up
            </Button>
        </Container>
    );
}

export default LoginPage;