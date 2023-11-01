import { RestClient } from "../../api/rest.client.ts";
import { ApiServicesRoutes } from "../../api/api.services.routes.ts";
import { useEffect, useState } from "react";
import { VacancyGetAll } from "../../models/vacany/vacancy.getall.dto.ts";
import { VacancyTile } from "../../components/vacancy.tile.tsx";

export function VacancyPage() {
    const url = ApiServicesRoutes.vacancy;
    const [vacancies, setVacancies] = useState<VacancyGetAll[]>([]);

    useEffect(() => {
        const restClient: RestClient = new RestClient();
        function fetchVacancies() {
            restClient.get<VacancyGetAll[]>(url).then((response) => { console.log('res:' + response); setVacancies(response); console.log(vacancies) });

            console.log(vacancies)
        }
        fetchVacancies();
        // console.log(vacancies)
    }, [url, setVacancies]);
    return (
        <>
            {vacancies && vacancies.map(vacancy => {
                <VacancyTile vacancy={vacancy} />
            })}
        </>
    )
}