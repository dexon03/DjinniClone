import { useGetCandidatesProfileQuery } from "../../app/features/candidate/candidate.api"
import { CandidateTile } from "../../components/candidate.tile";

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
            {candidates && candidates?.length > 0
                ? candidates.map(candidate => {
                    return <CandidateTile key={candidate.id} profile={candidate} />
                })
                : <p>No candidates found</p>}
        </>
    )
}