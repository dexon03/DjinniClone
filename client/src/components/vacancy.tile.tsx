import { VacancyGetAll } from "../models/vacancy/vacancy.getall.dto.ts";
import { Card, CardContent } from "@mui/material";

export function VacancyTile({ vacancy }: { vacancy: VacancyGetAll }) {
    return (
        <Card className="m-2">
            <CardContent>
                <h2 className="fw-bold">{vacancy.title}</h2>
                <h4>{vacancy.companyName}</h4>
                <p>{vacancy.attendanceMode}</p>
                <p>{vacancy.experience}</p>
                {
                    vacancy.locations.map(location => {
                        return <p>{location.city}, {location.country}</p>
                    })
                }
                <p className="text-success">{vacancy.salary}$</p>
                <p>{vacancy.description}</p>
            </CardContent>
        </Card>
    )
}