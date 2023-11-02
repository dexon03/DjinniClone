import { VacancyTile } from "../../components/vacancy.tile.tsx";
import { useGetVacanciesQuery } from "../../app/features/vacancy/vacancy.api.ts";
import { useAppDispatch, useAppSelector } from "../../hooks/redux.hooks.ts";
import { useEffect } from "react";
import { setVacancies } from "../../app/features/vacancy/vacancy.slice.ts";


export function VacancyPage() {
    const { data, isError, isLoading, error } = useGetVacanciesQuery();
    const vacancies = useAppSelector((state) => state.vacancies.value);
    const dispatch = useAppDispatch();
    
    useEffect(() => {
        dispatch(setVacancies(data))
        console.log(data)
        console.log(vacancies)
    },[])

    if (isLoading) {
        return <p>Loading...</p>;
    }

    if (isError) {
        return <p>Error: {error}</p>;
    }


    return (
        <>
            {vacancies && vacancies.map(vacancy => {
                <VacancyTile vacancy={vacancy} />
            })}
        </>
    )
}