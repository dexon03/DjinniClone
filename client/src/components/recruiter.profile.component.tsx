import { TextField, Button, Container, Typography, Avatar, Checkbox, FormControlLabel } from '@mui/material';
import { useGetRecruiterProfileQuery } from '../app/features/profile/profile.api';

const RecruiterProfileComponent = ({ id }: { id: string }) => {
  const { data, isError, isLoading, error } = useGetRecruiterProfileQuery(id);

  if (isLoading) {
    return <p>Loading...</p>;
  }

  if (isError) {
    return <p>Error: {error}</p>;
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
            value={data && data.name}
            onChange={(e) => { data.name = e.target.value }}
          />
          <TextField
            label="Surname"
            margin="normal"
            fullWidth
            value={data && data.surname}
            onChange={(e) => { data.surname = e.target.value }}
          />
          <TextField
            label="Email"
            margin="normal"
            fullWidth
            value={data && data.email}
            onChange={(e) => { data.email = e.target.value }}
          />
          <TextField
            label="Phone Number"
            margin="normal"
            fullWidth
            value={data && data.phoneNumber}
            onChange={(e) => { data.phoneNumber = e.target.value }}
          />
          <TextField
            label="Date of Birth"
            type="date"
            margin="normal"
            fullWidth
            InputLabelProps={{
              shrink: true
            }}
            value={data && data.dateOfBirth}
            // onChange={(e) => { data.dateOfBirth = e.target.value }}
          />
          <TextField
            label="Description"
            multiline
            rows={4}
            margin="normal"
            fullWidth
            value={data && data.description}
            onChange={(e) => { data.description = e.target.value }}
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
            value={data && data.gitHubUrl}
            onChange={(e) => { data.gitHubUrl = e.target.value }}
          />
          <TextField
            label="LinkedIn URL"
            margin="normal"
            fullWidth
            value={data && data.linkedInUrl}
            onChange={(e) => { data.linkedInUrl = e.target.value }}
          />
          <TextField
            label="Position Title"
            margin="normal"
            fullWidth
            value={data && data.positionTitle}
            onChange={(e) => { data.positionTitle = e.target.value }}
          />
          <FormControlLabel
            control={<Checkbox color="primary" />}
            label="Active"
            value={data && data.isActive}
            onChange={(e) => { data.isActive = e.target.value }}
          />
          <TextField
            label="Company Name"
            margin="normal"
            fullWidth
            value={data && data.company.name}
            onChange={(e) => { data.company.name = e.target.value }}
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
