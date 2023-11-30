import { TextField, Button, Container, Typography, Avatar, Checkbox, FormControlLabel, MenuItem, Select, InputLabel, OutlinedInput } from '@mui/material';
import { useGetCandidateProfileQuery, useGetProfileLocationQuery, useGetProfileSkillsQuery, useQuerySubscription, useUpdateCandidateProfileMutation } from '../app/features/profile/profile.api';
import { useEffect, useState } from 'react';
import { Experience } from '../models/profile/experience.enum';
import { CandidateProfile } from '../models/profile/candidate.profile.model';

const CandidateProfileComponent = ({ id }: { id: string }) => {
  const { data: profile, isError, isLoading, error } = useGetCandidateProfileQuery(id);
  const { data: skills } = useGetProfileSkillsQuery();
  const { data: locations } = useGetProfileLocationQuery();
  const { refetch } = useQuerySubscription(id);
  const [updateCandidateProfile, { data: updatedProfile, error: updateError }] = useUpdateCandidateProfileMutation();

  const [name, setName] = useState('');
  const [surname, setSurname] = useState('');
  const [email, setEmail] = useState('');
  const [phoneNumber, setPhoneNumber] = useState('');
  const [dateOfBirth, setDateOfBirth] = useState(new Date());
  const [description, setDescription] = useState('');
  const [gitHubUrl, setGitHubUrl] = useState('');
  const [linkedInUrl, setLinkedInUrl] = useState('');
  const [positionTitle, setPositionTitle] = useState('');
  const [isActive, setIsActive] = useState(false);
  const [desiredSalary, setDesiredSalary] = useState(0);
  const [workExperience, setWorkExperience] = useState(Experience.NoExperience);
  const [selectedLocations, setSelectedLocations] = useState<string[]>([]);
  const [selectedSkills, setSelectedSkills] = useState<string[]>([]);


  useEffect(() => {
    if (profile) {
      setName(profile.name || '');
      setSurname(profile.surname || '');
      setEmail(profile.email || '');
      setPhoneNumber(profile.phoneNumber || '');
      setDateOfBirth(profile.dateBirth || new Date());
      setDescription(profile.description || '');
      setGitHubUrl(profile.gitHubUrl || '');
      setLinkedInUrl(profile.linkedInUrl || '');
      setPositionTitle(profile.positionTitle || '');
      setIsActive(profile.isActive);
      setDesiredSalary(profile.desiredSalary || 0);
      setWorkExperience(profile.workExperience || Experience.NoExperience);
      setSelectedLocations(profile.locations ? profile.locations.map(location => location.id) : []);
      setSelectedSkills(profile.skills ? profile.skills.map(skill => skill.id) : []);
    }
  }, [profile]);

  if (isLoading) {
    return <p>Loading...</p>;
  }

  if (isError) {
    return <p>Error: {JSON.stringify(error.data)}</p>;
  }

  const handleSubmit = async (e) => {
    console.log(workExperience);
    e.preventDefault();
    try {
      await updateCandidateProfile({
        id: profile?.id,
        name: name,
        surname: surname,
        email: email,
        phoneNumber: phoneNumber,
        dateBirth: dateOfBirth,
        description: description,
        gitHubUrl: gitHubUrl,
        linkedInUrl: linkedInUrl,
        positionTitle: positionTitle,
        isActive: isActive,
        desiredSalary: desiredSalary,
        workExperience: workExperience,
        imageUrl: undefined,
        skills: skills?.filter(skill => selectedSkills.includes(skill.id)),
        locations: locations?.filter(location => selectedLocations.includes(location.id))
      } as CandidateProfile);

      if (updatedProfile) {
        refetch();
      }
    } catch (error) {
      console.error("Error updating profile:", updateError);
    }

  };

  return (
    profile &&
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
            label="Desired Salary"
            margin="normal"
            type='number'
            fullWidth
            value={desiredSalary}
            onChange={(e) => setDesiredSalary(Number(e.target.value))}
          />
          <TextField
            select
            label="Work Experience"
            margin="normal"
            fullWidth
            defaultValue={Experience.NoExperience}
            value={workExperience}
            onChange={(e) => {
              setWorkExperience(Number(e.target.value))
            }}
          >
            {Object.values(Experience).filter((v) => isNaN(Number(v))).map((value) => (
              <MenuItem key={value} value={Experience[value]}>
                {value}
              </MenuItem>
            ))}
          </TextField>
          <TextField
            label="Description"
            multiline
            rows={4}
            margin="normal"
            fullWidth
            value={description}
            onChange={(e) => setDescription(e.target.value)}
          />
          <InputLabel>Locations</InputLabel>
          <Select
            multiple
            fullWidth
            value={selectedLocations}
            onChange={(e) => setSelectedLocations(e.target.value)}
            input={<OutlinedInput label="Locations" />}
          >
            {locations && locations.map((location) => (
              <MenuItem key={location.id} value={location.id}>
                {location.city}, {location.country}
              </MenuItem>
            ))}
          </Select>
          <InputLabel>Skills</InputLabel>
          <Select
            multiple
            fullWidth
            value={selectedSkills}
            onChange={(e) => setSelectedSkills(e.target.value)}
            input={<OutlinedInput label="Skills" />}
          >
            {skills && skills.map((skill) => (
              <MenuItem key={skill.id} value={skill.id}>
                {skill.name}
              </MenuItem>
            ))}
          </Select>
          <TextField
            label="GitHub URL"
            margin="normal"
            fullWidth
            value={gitHubUrl}
            onChange={(e) => setGitHubUrl(e.target.value)}
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
            onChange={() => setIsActive(!isActive)}
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
