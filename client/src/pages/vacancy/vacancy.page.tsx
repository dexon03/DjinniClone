import {RestClient} from "../../api/rest.client.ts";
import {ApiServicesRoutes} from "../../api/api.services.routes.ts";
import {useEffect} from "react";
import {VacancyGetAll} from "../../models/vacany/vacancy.getall.dto.ts";
import {VacancyTile} from "../../components/vacancy.tile.tsx";

export function VacancyPage() {
    const url = ApiServicesRoutes.vacancy;
    const restClient : RestClient = new RestClient()
    let vacancies : VacancyGetAll[] = [];
    useEffect( () => {
        const getVacancies = async () => await restClient.get<VacancyGetAll[]>(url);
        getVacancies().then(r => vacancies = r);
    }, []);
    return(
        <>
            {vacancies.map(vacancy => {
                <VacancyTile vacancy={vacancy} />
            })}
        </>
    )
}