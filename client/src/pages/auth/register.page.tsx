import React, { useState } from 'react';
import { Button, TextField, Container, Typography, Radio, RadioGroup, FormControlLabel } from '@mui/material';
import {RestClient} from "../../api/rest.client.ts";
import {JwtResponse} from "../../models/auth/jwt.respone.ts";
import {ApiServicesRoutes} from "../../api/api.services.routes.ts";
import {RegisterModel} from "../../models/auth/register.model.ts";

function RegisterPage() {
    const [selectedRole, setSelectedRole] = useState('candidate');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [phoneNumber, setPhoneNumber] = useState('');
    const handleRoleChange = (event) => {
        setSelectedRole(event.target.value);
    };


    const restClient : RestClient = new RestClient();

    const onSubmit = async (event) => {
        event.preventDefault();
        const token = await restClient.post<JwtResponse>(ApiServicesRoutes.auth + '/register', {
            email: email,
            password: password,
            firstName: firstName,
            lastName: lastName,
            phoneNumber: phoneNumber,
            role: selectedRole
        } as RegisterModel);

        if(token){

        };
    }

    return (
        <Container maxWidth="sm">
            <Typography variant="h4" align="center" gutterBottom>
                Register
            </Typography>
            <form onSubmit={onSubmit}>
                <TextField
                    label="First name"
                    fullWidth
                    value={firstName}
                    onChange={(event) => setFirstName(event.target.value)}
                    variant="outlined"
                    margin="normal"
                />
                <TextField
                    label="Last name"
                    fullWidth
                    value={lastName}
                    onChange={(event) => setLastName(event.target.value)}
                    variant="outlined"
                    margin="normal"
                />
                <TextField
                    label="Email"
                    fullWidth
                    value={email}
                    onChange={(event) => setEmail(event.target.value)}
                    variant="outlined"
                    margin="normal"
                />
                <TextField
                    label="Phone number"
                    fullWidth
                    value={phoneNumber}
                    onChange={(event) => setPhoneNumber(event.target.value)}
                    variant="outlined"
                    margin="normal"
                />
                <TextField
                    label="Password"
                    type="password"
                    fullWidth
                    value={password}
                    onChange={(event) => setPassword(event.target.value)}
                    variant="outlined"
                    margin="normal"
                />

                <RadioGroup
                    aria-label="role"
                    name="role"
                    value={selectedRole}
                    onChange={handleRoleChange}
                >
                    <FormControlLabel
                        value="recruiter"
                        control={<Radio />}
                        label="I am recruiter"
                    />
                    <FormControlLabel
                        value="candidate"
                        control={<Radio />}
                        label="I am candidate"
                    />
                </RadioGroup>

                <Button
                    variant="contained"
                    color="primary"
                    fullWidth
                    type="submit"
                    size="large"
                    style={{ marginTop: 16 }}
                >
                    Sign Up
                </Button>
            </form>
        </Container>
    );
}

export default RegisterPage;