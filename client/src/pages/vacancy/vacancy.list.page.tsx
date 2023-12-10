import { VacancyTile } from "../../components/vacancy.tile.tsx";
import { useGetVacanciesQuery } from "../../app/features/vacancy/vacancy.api.ts";
import { Typography, Button } from "@mui/material";
import { useNavigate } from "react-router-dom";
import useToken from "../../hooks/useToken.ts";
import { Role } from "../../models/common/role.enum.ts";

export function VacancyListPage() {
    const { token } = useToken();
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
                {token.role == Role[Role.Recruiter] ? <Button variant="contained" onClick={() => handleCreateVacancy()}>
                    Create Vacancy
                </Button> : null
                }
            </div>
            {data && data.map((vacancy) => <VacancyTile key={vacancy.id} vacancy={vacancy} />)}
        </>
    )
}