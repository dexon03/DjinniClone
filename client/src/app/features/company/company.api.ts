import { createApi } from "@reduxjs/toolkit/dist/query/react";
import { Company } from "../../../models/common/company.models";
import { axiosBaseQuery } from "../../../api/axios.baseQuery";
import { environment } from "../../../environment/environment";
import { ApiServicesRoutes } from "../../../api/api.services.routes";


export const companyApi = createApi({
    reducerPath: 'companyApi',
    baseQuery: axiosBaseQuery({ baseUrl: environment.apiUrl }),
    endpoints: (builder) => ({
        getProfileCompanies: builder.query<Company[], void>({ query: () => ({ url: ApiServicesRoutes.profile + `/profile/company`, method: 'get' }) }),
        updateCompany: builder.mutation<Company, Company>({ query: (company: Company) => ({ url: ApiServicesRoutes.profile + `/company`, method: 'put', data: company }) }),
    }),
})


export const { useLazyGetProfileCompaniesQuery, useUpdateCompanyMutation } = companyApi;