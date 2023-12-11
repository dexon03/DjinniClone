import { Typography } from "@mui/material";
import { useGetRecruiterVacanciesQuery } from "../../app/features/vacancy/vacancy.api";
import { VacancyTile } from "../../components/vacancy.tile";
import { useAppSelector } from "../../hooks/redux.hooks";

export function RecruiterVacanciesList() {
    const recruiterProfileId: string = useAppSelector(state => state.recruiterProfile.profile?.id)
    const { data, isError, isLoading, error, refetch } = useGetRecruiterVacanciesQuery(recruiterProfileId);

    if (isLoading) {
        return <p>Loading...</p>
    }

    if (isError) {
        return <p>Error: {JSON.stringify(error)}</p>
    }

    return (
        <>
            <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', }} className="m-2">
                <Typography variant="h5">My vacancies</Typography>
            </div>
            {data && data.map((vacancy) => <VacancyTile key={vacancy.id} vacancy={vacancy} isRecruiterList={true} refetch={() => refetch()} />)}
        </>
    )
}