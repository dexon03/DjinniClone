import { createApi } from "@reduxjs/toolkit/dist/query/react";
import { axiosBaseQuery } from "../../../api/axios.baseQuery";
import { environment } from "../../../environment/environment";
import { ApiServicesRoutes } from "../../../api/api.services.routes";
import { CandidateProfile } from "../../../models/profile/candidate.profile.model";

export const candidateApi = createApi({
    reducerPath: 'candidateApi',
    baseQuery: axiosBaseQuery({ baseUrl: environment.apiUrl + ApiServicesRoutes.profile }),
    keepUnusedDataFor: 15,
    endpoints: (builder) => ({
        getCandidatesProfile: builder.query<CandidateProfile[], void>({ query: () => ({ url: `/profile/getCandidatesProfile`, method: 'get' }) }),
    })
})


export const { useGetCandidatesProfileQuery } = candidateApi;