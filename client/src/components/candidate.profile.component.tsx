import { TextField, Button, Container, Typography, Avatar, Checkbox, FormControlLabel } from '@mui/material';
import { useState } from 'react';

const CandidateProfileComponent = () => {
  const [name, setName] = useState('');
  const [surname, setSurname] = useState('');
  const [email, setEmail] = useState('');
  const [phoneNumber, setPhoneNumber] = useState('');
  const [dateOfBirth, setDateOfBirth] = useState('');
  const [description, setDescription] = useState('');
  const [imageUrl, setImageUrl] = useState('');
  const [githubUrl, setGithubUrl] = useState('');
  const [linkedInUrl, setLinkedInUrl] = useState('');
  const [positionTitle, setPositionTitle] = useState('');
  const [workExperience, setWorkExperience] = useState('');
  const [active, setActive] = useState(false);

  const handleSubmit = (e) => {
    e.preventDefault();
    

  };

  return (
    <Container component="main" maxWidth="xs">
      <div>
        <Avatar> {/* Add user avatar here */}</Avatar>
        <Typography component="h1" variant="h5">
          Profile
        </Typography>
        <form onSubmit={handleSubmit}>
          <TextField
            label="Name"
            margin="normal"
            fullWidth
            onChange={(e) => setName(e.target.value)}
          />
          <TextField
            label="Surname"
            margin="normal"
            fullWidth
            onChange={(e) => setSurname(e.target.value)}
          />
          <TextField
            label="Email"
            margin="normal"
            fullWidth
            onChange={(e) => setEmail(e.target.value)}
          />
          <TextField
            label="Phone Number"
            margin="normal"
            fullWidth
            onChange={(e) => setPhoneNumber(e.target.value)}
          />
          <TextField
            label="Date of Birth"
            type="date"
            margin="normal"
            fullWidth
            onChange={(e) => setDateOfBirth(e.target.value)}
          />
          <TextField
            label="Description"
            multiline
            rows={4}
            margin="normal"
            fullWidth
            onChange={(e) => setDescription(e.target.value)}
          />
          <TextField
            label="Image URL"
            margin="normal"
            fullWidth
          />
          <TextField
            label="GitHub URL"
            margin="normal"
            fullWidth
            onChange={(e) => setGithubUrl(e.target.value)}
          />
          <TextField
            label="LinkedIn URL"
            margin="normal"
            fullWidth
            onChange={(e) => setLinkedInUrl(e.target.value)}
          />
          <TextField
            label="Position Title"
            margin="normal"
            fullWidth
            onChange={(e) => setPositionTitle(e.target.value)}
          />
          <FormControlLabel
            control={<Checkbox color="primary" />}
            label="Active"
            onChange={(e) => setActive(e.target.value)}
          />
          <Button type="submit" fullWidth variant="contained" color="primary">
            Save
          </Button>
        </form>
      </div>
    </Container>
  );
};

export default CandidateProfileComponent;