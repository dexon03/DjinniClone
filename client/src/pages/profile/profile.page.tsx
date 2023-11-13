// import React from 'react';
// import { TextField, Button, Typography } from '@mui/material';

// export default function ProfilePage() {
//     const [profile, setProfile] = React.useState({
//         name: '',
//         surname: '',
//         email: '',
//         phoneNumber: '',
//         dateBirth: '',
//         description: '',
//         imageUrl: '',
//         gitHubUrl: '',
//         linkedInUrl: '',
//         positionTitle: '',
//         isActive: false,
//     });

//     const handleChange = (event) => {
//         setProfile({
//             ...profile,
//             [event.target.name]: event.target.value,
//         });
//     };

//     return (
//         <div>
//             <Typography variant="h4">Profile Page</Typography>
//             <TextField name="name" label="Name" value={profile.name} onChange={handleChange} />
//             <TextField name="surname" label="Surname" value={profile.surname} onChange={handleChange} />
//             <TextField name="email" label="Email" value={profile.email} onChange={handleChange} />
//             <TextField name="phoneNumber" label="Phone Number" value={profile.phoneNumber} onChange={handleChange} />
//             <TextField name="dateBirth" label="Date of Birth" value={profile.dateBirth} onChange={handleChange} />
//             <TextField name="description" label="Description" value={profile.description} onChange={handleChange} />
//             <TextField name="imageUrl" label="Image URL" value={profile.imageUrl} onChange={handleChange} />
//             <TextField name="gitHubUrl" label="GitHub URL" value={profile.gitHubUrl} onChange={handleChange} />
//             <TextField name="linkedInUrl" label="LinkedIn URL" value={profile.linkedInUrl} onChange={handleChange} />
//             <TextField name="positionTitle" label="Position Title" value={profile.positionTitle} onChange={handleChange} />
//             <Button variant="contained" color="primary" onClick={() => { /* handle save logic here */ }}>
//                 Save
//             </Button>
//         </div>
//     );
// }
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
