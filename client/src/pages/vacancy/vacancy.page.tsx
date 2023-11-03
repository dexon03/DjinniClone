import { VacancyTile } from "../../components/vacancy.tile.tsx";
import { useGetVacanciesQuery } from "../../app/features/vacancy/vacancy.api.ts";

export function VacancyPage() {
    const { data, isError, isLoading, error } = useGetVacanciesQuery();


    if (isLoading) {
        return <p>Loading...</p>;
    }

    if (isError) {
        return <p>Error: {error}</p>;
    }


    return (
        <>
            {data && data.map(vacancy => {
                return <VacancyTile key={vacancy.id} vacancy={vacancy} />
            })}
        </>
    )
}