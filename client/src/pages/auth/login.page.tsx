import { Button, TextField, Container, Typography } from '@mui/material';
import {Link, NavLink} from 'react-router-dom';

function LoginPage() {
    return (
        <Container maxWidth="sm">
            <Typography variant="h4" align="center" gutterBottom>
                Login
            </Typography>
            <form>
                <TextField
                    label="Email"
                    fullWidth
                    variant="outlined"
                    margin="normal"
                />
                <TextField
                    label="Password"
                    type="password"
                    fullWidth
                    variant="outlined"
                    margin="normal"
                />
                <Button
                    variant="contained"
                    color="primary"
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