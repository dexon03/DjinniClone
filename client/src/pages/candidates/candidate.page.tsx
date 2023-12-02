import { useParams } from "react-router-dom"
import { useGetCandidateProfileQuery } from "../../app/features/profile/profile.api"
import { Card, CardContent, Typography, Chip } from "@mui/material";

export function CandidatePage() {
    const { id } = useParams();
    const { data: profile, isLoading, isError, error } = useGetCandidateProfileQuery(id);

    if (isLoading) {
        return <div>Loading...</div>;
    }

    if (isError) {
        return <p>Error: {JSON.stringify(error.data)}</p>;
    }

    // Create a string of unique locations separated by commas
    const locationString = [...new Set(profile?.locations.map(location => `${location.city}, ${location.country}`))].join(', ');

    // Map attendance modes to their string representations
    const attendanceModes = ['OnSite', 'Remote', 'Mixed', 'OnSiteOrRemote'];

    return (
        profile &&
        <div style={{ display: 'flex' }}>
            <Card style={{ flex: 1, marginRight: '1rem' }}>
                <CardContent>
                    <Typography variant="h5">{profile?.positionTitle}</Typography>
                    <Typography variant="h6" style={{ marginTop: '1rem' }}>Description</Typography>
                    <Typography variant="body1" style={{ whiteSpace: 'pre-wrap' }}>{profile.description}</Typography>
                    {profile.skills && profile.skills.map((skill) => (
                        <Chip label={skill.name} variant="outlined" style={{ margin: '0.5rem 0' }} />
                    ))}
                </CardContent>
            </Card>
            <Card >
                <CardContent>
                    <Typography variant="h6">Locations</Typography>
                    <Typography variant="body1">{locationString}</Typography>
                    <Typography variant="h6" style={{ marginTop: '1rem' }}>Attendance Mode</Typography>
                    <Typography variant="body1">{attendanceModes[profile.attendance]}</Typography>
                    {profile.desiredSalary ?
                        <>
                            <Typography variant="h6" style={{ marginTop: '1rem' }}>Desired salary</Typography>
                            <Typography variant="body1" style={{ color: 'green' }}>{profile.desiredSalary} USD</Typography>
                        </>
                        : null
                    }
                </CardContent>
            </Card>
        </div >
    );
}