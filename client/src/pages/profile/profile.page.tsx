import { TextField, Button, Container, Typography, Avatar, Checkbox, FormControlLabel } from '@mui/material';

const ProfilePage = () => {
  const handleSubmit = (e) => {
    e.preventDefault();
    // Add your form submission logic here
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
            // Add onChange and value props for controlled component
          />
          <TextField
            label="Surname"
            margin="normal"
            fullWidth
            // Add onChange and value props for controlled component
          />
          <TextField
            label="Email"
            margin="normal"
            fullWidth
            // Add onChange and value props for controlled component
          />
          <TextField
            label="Phone Number"
            margin="normal"
            fullWidth
            // Add onChange and value props for controlled component
          />
          <TextField
            label="Date of Birth"
            type="date"
            margin="normal"
            fullWidth
            // Add onChange and value props for controlled component
          />
          <TextField
            label="Description"
            multiline
            rows={4}
            margin="normal"
            fullWidth
            // Add onChange and value props for controlled component
          />
          <TextField
            label="Image URL"
            margin="normal"
            fullWidth
            // Add onChange and value props for controlled component
          />
          <TextField
            label="GitHub URL"
            margin="normal"
            fullWidth
            // Add onChange and value props for controlled component
          />
          <TextField
            label="LinkedIn URL"
            margin="normal"
            fullWidth
            // Add onChange and value props for controlled component
          />
          <TextField
            label="Position Title"
            margin="normal"
            fullWidth
            // Add onChange and value props for controlled component
          />
          <FormControlLabel
            control={<Checkbox color="primary" />}
            label="Active"
            // Add onChange and value props for controlled component
          />
          <Button type="submit" fullWidth variant="contained" color="primary">
            Save
          </Button>
        </form>
      </div>
    </Container>
  );
};

export default ProfilePage;
