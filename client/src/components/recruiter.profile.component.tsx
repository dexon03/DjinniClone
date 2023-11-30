import { TextField, Button, Container, Typography, Avatar, Checkbox, FormControlLabel, InputLabel, MenuItem, OutlinedInput, Select } from '@mui/material';
import { useGetCompaniesQuery, useGetRecruiterProfileQuery, useUpdateRecruiterProfileMutation } from '../app/features/profile/profile.api';
import { useEffect, useState } from 'react';
import { RecruiterProfile } from '../models/profile/recruiter.profile.model';

const RecruiterProfileComponent = ({ id }: { id: string }) => {
  const { data: profile, isError, isLoading, error } = useGetRecruiterProfileQuery(id);
  const { data: companies } = useGetCompaniesQuery();
  const [updateCandidateProfile, { data: updatedProfile, error: updateError }] = useUpdateRecruiterProfileMutation();

  const [name, setName] = useState('');
  const [surname, setSurname] = useState('');
  const [email, setEmail] = useState('');
  const [phoneNumber, setPhoneNumber] = useState('');
  const [dateOfBirth, setDateOfBirth] = useState(new Date());
  const [description, setDescription] = useState('');
  const [linkedInUrl, setLinkedInUrl] = useState('');
  const [positionTitle, setPositionTitle] = useState('');
  const [isActive, setIsActive] = useState(false);
  const [selectedCompany, setSelectedCompany] = useState<string>('');
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
      setSelectedCompany(profile.company ? profile.company.id : '');
      setCompanyName(profile?.company ? profile.company.name : '');
      setCompanyDescription(profile?.company ? profile.company.description : '');
    }
  }, [profile])



  if (isLoading) {
    return <p>Loading...</p>;
  }

  if (isError) {
    return <p>Error: {JSON.stringify(error.data)}</p>;
  }

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await updateCandidateProfile({
        id: profile.id,
        name,
        surname,
        email,
        phoneNumber,
        dateBirth: dateOfBirth,
        description,
        linkedInUrl,
        positionTitle,
        isActive,
        company: {
          id: profile?.company?.id,
          name: companyName,
          description: companyDescription
        }
      } as RecruiterProfile)

    } catch (error) {
      console.error("Error updating profile:", updateError);
    }

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
          <InputLabel>Company</InputLabel>
          <Select
            fullWidth
            value={selectedCompany}
            onChange={(e) => setSelectedCompany(e.target.value)}
            input={<OutlinedInput label="Locations" />}
          >
            {companies && companies.map((company) => (
              <MenuItem key={company.id} value={company.id}>
                {company.name}
              </MenuItem>
            ))}
          </Select>
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
          <FormControlLabel
            control={<Checkbox color="primary" />}
            label="Active"
            value={isActive}
            onChange={(e) => setIsActive(!isActive)}
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