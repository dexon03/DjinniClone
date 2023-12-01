import { useGetCandidatesProfileQuery } from "../../app/features/candidate/candidate.api"
import { CandidateTale } from "../../components/candidate.tile";

export function CandidateList() {

    const { data: candidates, isLoading, isError, error } = useGetCandidatesProfileQuery()

    if (isLoading) {
        return <div>Loading...</div>
    }

    if (isError) {
        return <p>Error: {JSON.stringify(error.data)}</p>;
    }

    return (
        <>
            {candidates && candidates.map(candidate => {
                return <CandidateTale key={candidate.id} profile={candidate} />
            })}
        </>
    )
}