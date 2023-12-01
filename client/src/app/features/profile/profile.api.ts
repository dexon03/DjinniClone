import { createApi } from "@reduxjs/toolkit/dist/query/react";
import { axiosBaseQuery } from "../../../api/axios.baseQuery";
import { environment } from "../../../environment/environment";
import { CandidateProfile } from "../../../models/profile/candidate.profile.model";
import { Role } from "../../../models/common/role.enum";
import { RecruiterProfile } from "../../../models/profile/recruiter.profile.model";
import { ApiServicesRoutes } from "../../../api/api.services.routes";
import { SkillGetAllDto } from "../../../models/common/SkillGetAllDto.model";
import { LocationDto } from "../../../models/common/location.dto";
export const profileApi = createApi({
    reducerPath: 'profileApi',
    baseQuery: axiosBaseQuery({ baseUrl: environment.apiUrl + ApiServicesRoutes.profile }),
    endpoints: (builder) => ({
        getCandidateProfile: builder.query<CandidateProfile, string>({ query: (id: string) => ({ url: `/profile/${Role.Candidate}/${id}`, method: 'get' }) }),
        getRecruiterProfile: builder.query<RecruiterProfile, string>({ query: (id: string) => ({ url: `/profile/${Role.Recruiter}/${id}`, method: 'get' }) }),
        updateCandidateProfile: builder.mutation<CandidateProfile, CandidateProfile>({ query: (profile: CandidateProfile) => ({ url: `/profile/updateCandidate`, method: 'put', data: profile }) }),
        updateRecruiterProfile: builder.mutation<RecruiterProfile, RecruiterProfile>({ query: (profile: RecruiterProfile) => ({ url: `/profile/updateRecruiter`, method: 'put', data: profile }) }),
        getProfileLocation: builder.query<LocationDto[], void>({ query: () => ({ url: '/location', method: 'get' }) }),
        getProfileSkills: builder.query<SkillGetAllDto[], void>({ query: () => ({ url: `/skill`, method: 'get' }) }),
    }),
});

export const { useGetCandidateProfileQuery, useGetRecruiterProfileQuery, useUpdateCandidateProfileMutation, useUpdateRecruiterProfileMutation, useLazyGetProfileSkillsQuery, useLazyGetProfileLocationQuery } = profileApi;

export const { useQuerySubscription } = profileApi.endpoints.getCandidateProfile;  