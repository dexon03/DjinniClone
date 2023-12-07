import { VacancyTile } from "../../components/vacancy.tile.tsx";
import { useGetVacanciesQuery } from "../../app/features/vacancy/vacancy.api.ts";
import { Typography, Button } from "@mui/material";
import { useNavigate } from "react-router-dom";

export function VacancyListPage() {
    const { data, isError, isLoading, error } = useGetVacanciesQuery();
    const navigate = useNavigate();

    if (isLoading) {
        return <p>Loading...</p>;
    }

    if (isError) {
        return <p>Error: {error}</p>;
    }

    const handleCreateVacancy = () => {
        navigate(`/vacancy/create`);
    }
    return (
        <>
            <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', }} className="m-2">
                <Typography variant="h5">Vacancies</Typography>
                <Button variant="contained" onClick={() => handleCreateVacancy()}>
                    Create Vacancy
                </Button>
            </div>
            {data && data.map((vacancy) => <VacancyTile key={vacancy.id} vacancy={vacancy} />)}
        </>
    )
}