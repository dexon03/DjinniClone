import { Button, TextField, Container, Typography } from '@mui/material';
import { NavLink, useNavigate } from 'react-router-dom';
import { FormEvent, useState } from "react";
import { RestClient } from "../../api/rest.client.ts";
import { TokenResponse } from "../../models/auth/jwt.respone.ts";
import { ApiServicesRoutes } from "../../api/api.services.routes.ts";
import { LoginModel } from "../../models/auth/login.model.ts";
import { Role } from '../../models/common/role.enum.ts';
import useToken from '../../hooks/useToken.ts';
import { useAppDispatch } from '../../hooks/redux.hooks.ts';
import { setProfile } from '../../app/slices/recruiter.profile.slice.ts';

function LoginPage() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();
    const { token, setToken } = useToken();
    const dispatch = useAppDispatch();

    const restClient = new RestClient();
    const onSubmit = async (event: FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const tokenResponse = await restClient.post<TokenResponse>(ApiServicesRoutes.identity + '/auth/login', {
            email: email,
            password: password
        } as LoginModel);
        setToken(tokenResponse);
        if (tokenResponse.role === Role[Role.Candidate]) {
            navigate('/vacancy');
        } else {
            dispatch(setProfile(await restClient.get(ApiServicesRoutes.profile + `/profile/${Role.Recruiter}/${tokenResponse.userId}`)));
            navigate('/candidate');
        }
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
                component={NavLink} to="/register"
            >
                Sign Up
            </Button>
        </Container>
    );
}

export default LoginPage;