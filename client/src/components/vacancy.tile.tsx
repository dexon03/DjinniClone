import {VacancyGetAll} from "../models/vacany/vacancy.getall.dto.ts";
import {Card, CardContent} from "@mui/material";

export function VacancyTile({vacancy} : {vacancy: VacancyGetAll}) {
    return (
        <Card>
            <CardContent>
                <h2 className="fw-bold">{vacancy.title}</h2>
                <h4>{vacancy.companyName}</h4>
                {vacancy.locations.forEach(location =>
                    {
                        <p>{location.country}({location.city})</p>
                    })
                }
                <p className="text-success">{vacancy.salary}$</p>
                <p>{vacancy.description}</p>
            </CardContent>
        </Card>
    )
}