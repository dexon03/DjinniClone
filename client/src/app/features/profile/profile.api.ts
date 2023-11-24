import { createApi } from "@reduxjs/toolkit/dist/query/react";
import { axiosBaseQuery } from "../../../api/axios.baseQuery";
import { environment } from "../../../environment/environment";
import { CandidateProfile } from "../../../models/profile/candidate.profile.model";
import { Role } from "../../../models/common/role.enum";
import { RecruiterProfile } from "../../../models/profile/recruiter.profile.model";

export const profileApi = createApi({
    reducerPath: 'profileApi',
    baseQuery: axiosBaseQuery({ baseUrl: environment.apiUrl }),
    endpoints: (builder) => ({
        getCandidateProfile: builder.query<CandidateProfile, string>({ query: (id: string) => ({ url: `/api/profile/${Role.Candidate}/${id}`, method: 'get' }) }),
        getRecruiterProfile: builder.query<RecruiterProfile, string>({ query: (id: string) => ({ url: `/api/profile/${Role.Recruiter}/${id}`, method: 'get' }) }),
        updateCandidateProfile: builder.mutation<CandidateProfile, CandidateProfile>({ query: (profile: CandidateProfile) => ({ url: `/api/profile/updateCandidate`, method: 'put', data: profile }) }),
        updateRecruiterProfile: builder.mutation<RecruiterProfile, RecruiterProfile>({ query: (profile: RecruiterProfile) => ({ url: `/api/profile/updateRecruiter`, method: 'put', data: profile }) }),
    }),
});

export const { useGetCandidateProfileQuery, useGetRecruiterProfileQuery, useUpdateCandidateProfileMutation, useUpdateRecruiterProfileMutation } = profileApi;

export const { useQuerySubscription } = profileApi.endpoints.getCandidateProfile;  