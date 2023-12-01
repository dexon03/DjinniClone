import { Card, CardContent, Typography, Chip, Box, Button } from "@mui/material";
import { CandidateProfile } from "../models/profile/candidate.profile.model";
import { Experience } from "../models/vacancy/experience.enum";
import { useNavigate } from "react-router-dom";

export function CandidateTale({ profile }: { profile: CandidateProfile }) {
    const countries = [...new Set(profile?.locations?.map(location => location.country))].join(', ');
    const navigate = useNavigate();

    const handleViewClick = () => {
        navigate('/candidate/' + profile.id);
    }

    return (
        profile &&
        <Card className="m-2">
            <CardContent>
                <Typography variant="h5">{profile.positionTitle}</Typography>
                <Typography variant="body2">{countries}, {Experience[profile.workExperience]}</Typography>
                <Typography variant="h6" align="right" style={{ color: 'green' }}>{profile.desiredSalary} USD</Typography>
                <Typography variant="body1">{profile.description.slice(0, 100)}{profile.description.length > 100 ? '...' : null}</Typography>
                <Box m={2} />
                {profile.skills && profile.skills.map((skill) => (
                    <Chip label={skill.name} variant="outlined" />
                ))}
                <Button variant="contained" color="primary" onClick={handleViewClick} style={{ float: 'right' }}>
                    View
                </Button>
            </CardContent>
        </Card>
    );
}