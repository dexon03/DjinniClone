import React, { useState } from 'react';
import { Button, TextField, Container, Typography, Radio, RadioGroup, FormControlLabel } from '@mui/material';

function RegisterPage() {
    const [selectedRole, setSelectedRole] = useState('candidate');

    const handleRoleChange = (event) => {
        setSelectedRole(event.target.value);
    };

    return (
        <Container maxWidth="sm">
            <Typography variant="h4" align="center" gutterBottom>
                Register
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