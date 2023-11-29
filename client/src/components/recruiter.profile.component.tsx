import { TextField, Button, Container, Typography, Avatar, Checkbox, FormControlLabel } from '@mui/material';
import { useGetRecruiterProfileQuery } from '../app/features/profile/profile.api';
import { useEffect, useState } from 'react';

const RecruiterProfileComponent = ({ id }: { id: string }) => {
  const { data: profile, isError, isLoading, error } = useGetRecruiterProfileQuery(id);

  const [name, setName] = useState('');
  const [surname, setSurname] = useState('');
  const [email, setEmail] = useState('');
  const [phoneNumber, setPhoneNumber] = useState('');
  const [dateOfBirth, setDateOfBirth] = useState(new Date());
  const [description, setDescription] = useState('');
  const [linkedInUrl, setLinkedInUrl] = useState('');
  const [positionTitle, setPositionTitle] = useState('');
  const [isActive, setIsActive] = useState(false);
  const [companyName, setCompanyName] = useState('');
  const [companyDescription, setCompanyDescription] = useState('');

  useEffect(() => {
    if (profile) {
      setName(profile.name || '');
      setSurname(profile.surname || '');
      setEmail(profile.email || '');
      setPhoneNumber(profile.phoneNumber || '');
      setDateOfBirth(profile.dateBirth!);
      setDescription(profile.description || '');
      setLinkedInUrl(profile.linkedInUrl || '');
      setPositionTitle(profile.positionTitle || '');
      setIsActive(profile.isActive);
      setCompanyName(profile.company.name || '');
      setCompanyDescription(profile.company.description || '');
    }
  }, [profile])



  if (isLoading) {
    return <p>Loading...</p>;
  }

  if (isError) {
    return <p>Error: {JSON.stringify(error.data)}</p>;
  }

  const handleSubmit = (e) => {
    e.preventDefault();

  };

  return (
    <Container component="main" maxWidth="sm">
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
            value={name}
            onChange={(e) => setName(e.target.value)}
          />
          <TextField
            label="Surname"
            margin="normal"
            fullWidth
            value={surname}
            onChange={(e) => setSurname(e.target.value)}
          />
          <TextField
            label="Email"
            margin="normal"
            fullWidth
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
          <TextField
            label="Phone Number"
            margin="normal"
            fullWidth
            value={phoneNumber}
            onChange={(e) => setPhoneNumber(e.target.value)}
          />
          <TextField
            label="Date of Birth"
            type="date"
            margin="normal"
            fullWidth
            InputLabelProps={{
              shrink: true,
            }}
            value={dateOfBirth}
            onChange={(e) => setDateOfBirth(e.target.value)}
          />
          <TextField
            label="Description"
            multiline
            rows={4}
            margin="normal"
            fullWidth
            value={description}
            onChange={(e) => setDescription(e.target.value)}
          />
          <TextField
            label="LinkedIn URL"
            margin="normal"
            fullWidth
            value={linkedInUrl}
            onChange={(e) => setLinkedInUrl(e.target.value)}
          />
          <TextField
            label="Position Title"
            margin="normal"
            fullWidth
            value={positionTitle}
            onChange={(e) => setPositionTitle(e.target.value)}
          />
          <FormControlLabel
            control={<Checkbox color="primary" />}
            label="Active"
            value={isActive}
            onChange={(e) => setIsActive(!isActive)}
          />
          <TextField
            label="Company Name"
            margin="normal"
            fullWidth
            value={companyName}
            onChange={(e) => setCompanyName(e.target.value)}
          />
          <TextField
            label="Description"
            multiline
            rows={4}
            margin="normal"
            fullWidth
            value={companyDescription}
            onChange={(e) => setCompanyDescription(e.target.value)}
          />
          <Button type="submit" fullWidth variant="contained" color="primary">
            Save
          </Button>
        </form>
      </div>
    </Container>
  );
};

export default RecruiterProfileComponent;
