import { createApi } from "@reduxjs/toolkit/dist/query/react";
import { axiosBaseQuery } from "../../../api/axios.baseQuery";
import { environment } from "../../../environment/environment";
import { ApiServicesRoutes } from "../../../api/api.services.routes";
import { CandidateProfile } from "../../../models/profile/candidate.profile.model";
import { CandidateFilter } from "../../../models/common/candidates.filter";

export const candidateApi = createApi({
    reducerPath: 'candidateApi',
    baseQuery: axiosBaseQuery({ baseUrl: environment.apiUrl + ApiServicesRoutes.profile }),
    keepUnusedDataFor: 5,
    endpoints: (builder) => ({
        getCandidatesProfile: builder.query<CandidateProfile[], CandidateFilter>({
            query: (filter: CandidateFilter) => ({
                url: `/profile/getCandidatesProfile?` +
                    `searchTerm=${filter.searchTerm}&page=${filter.page}&pageSize=${filter.pageSize}&experience=${!filter.experience && filter.experience !== 0 ? '' : filter.experience}&attendanceMode=${!filter.attendanceMode && filter.attendanceMode != 0 ? '' : filter.attendanceMode}&skill=${filter.skill}&location=${filter.location}`,
                method: 'get'
            })
        }),
    })
})


export const { useGetCandidatesProfileQuery } = candidateApi;