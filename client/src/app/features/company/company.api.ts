import { createApi } from "@reduxjs/toolkit/dist/query/react";
import { Company } from "../../../models/common/company.models";
import { axiosBaseQuery } from "../../../api/axios.baseQuery";
import { environment } from "../../../environment/environment";
import { ApiServicesRoutes } from "../../../api/api.services.routes";
import { CompanyCreate } from "../../../models/common/company.create";


export const companyApi = createApi({
    reducerPath: 'companyApi',
    baseQuery: axiosBaseQuery({ baseUrl: environment.apiUrl }),
    tagTypes: ['Company'],
    keepUnusedDataFor: 5,
    endpoints: (builder) => ({
        getProfileCompanies: builder.query<Company[], void>({
            query: () => ({
                url: ApiServicesRoutes.profile + `/company`,
                method: 'get'
            }),
            providesTags: ['Company']
        }),
        updateCompany: builder.mutation<Company, CompanyCreate>({
            query: (company: CompanyCreate) => ({
                url: ApiServicesRoutes.profile + `/company`,
                method: 'put',
                data: company
            }),
            invalidatesTags: ['Company']
        }),
        createCompany: builder.mutation<Company, Company>({
            query: (company: Company) => ({
                url: ApiServicesRoutes.profile + `/company`,
                method: 'post',
                data: company
            }),
            invalidatesTags: ['Company']
        }),
    }),
})


export const { useLazyGetProfileCompaniesQuery, useUpdateCompanyMutation, useCreateCompanyMutation } = companyApi;