import { useNavigate } from "react-router-dom";
import { VacancyGetAll } from "../models/vacancy/vacancy.getall.dto.ts";
import { Button, Card, CardContent } from "@mui/material";

export function VacancyTile({ vacancy }: { vacancy: VacancyGetAll }) {
    const navigate = useNavigate();

    const handleViewClick = () => {
        // Redirect to the corresponding vacancy page
        navigate(`/vacancy/${vacancy.id}`);
    };

    return (
        <Card className="m-2">
            <CardContent style={{ display: 'flex', flexDirection: 'column' }}>
                <h2 className="fw-bold">{vacancy.title}</h2>
                <h4>{vacancy.companyName}</h4>
                <p>{vacancy.attendanceMode}</p>
                <p>{vacancy.experience}</p>
                {vacancy.locations.map((location) => (
                    <p key={location.id}>{location.city}, {location.country}</p>
                ))}
                <p className="text-success">{vacancy.salary}$</p>
                <p>{vacancy.description}</p>
                <Button
                    variant="contained"
                    color="primary"
                    style={{ alignSelf: 'flex-end' }}
                    onClick={handleViewClick}
                >
                    View
                </Button>
            </CardContent>
        </Card>
    );
}