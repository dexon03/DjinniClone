import { createApi } from '@reduxjs/toolkit/query/react';
import { environment } from '../../../environment/environment';
import { VacancyGetAll } from '../../../models/vacancy/vacancy.getall.dto';
import { axiosBaseQuery } from '../../../api/axios.baseQuery';
import { ApiServicesRoutes } from '../../../api/api.services.routes';
import { VacancyGet } from '../../../models/vacancy/vacany.get.dto';
import { VacancyCreate } from '../../../models/vacancy/vacancy.create.dto';
import { SkillDto } from '../../../models/common/skill.dto';
import { LocationDto } from '../../../models/common/location.dto';
import { Category } from '../../../models/vacancy/category.model';
import { VacancyUpdateModel } from '../../../models/vacancy/vacancy.update.dto';

export const vacancyApi = createApi({
    reducerPath: 'vacancyApi',
    tagTypes: ['VacancyAll', 'RecruiterVacancy'],
    baseQuery: axiosBaseQuery({ baseUrl: environment.apiUrl + ApiServicesRoutes.vacancy }),
    keepUnusedDataFor: 5,
    endpoints: (builder) => ({
        getVacancies: builder.query<VacancyGetAll[], void>({
            query: () => ({
                url: '/vacancy',
                method: 'get'
            }),
            providesTags: ['VacancyAll']
        }),
        getRecruiterVacancies: builder.query<VacancyGetAll[], string>({
            query: (recruiterId: string) => ({
                url: '/vacancy/getRecruiterVacancies/' + recruiterId,
                method: 'get'
            }),
            providesTags: ['RecruiterVacancy']
        }),
        getVacancy: builder.query<VacancyGet, string>({ query: (id: string) => ({ url: `/vacancy/${id}`, method: 'get' }) }),
        createVacancy: builder.mutation<VacancyGet, VacancyCreate>({
            query: (body: VacancyCreate) => ({
                url: '/vacancy',
                method: 'post',
                data: body
            }),
            invalidatesTags: ['VacancyAll', 'RecruiterVacancy']
        }),
        getVacancyLocation: builder.query<LocationDto[], void>({ query: () => ({ url: '/location', method: 'get' }) }),
        getVacancySkills: builder.query<SkillDto[], void>({ query: () => ({ url: `/skill`, method: 'get' }) }),
        getVacancyCategories: builder.query<Category[], void>({ query: () => ({ url: `/category`, method: 'get' }) }),
        activateDisactivateVacancy: builder.mutation<void, string>({
            query: (id: string) => ({
                url: `/vacancy/${id}/activate-deactivate`,
                method: 'put'
            }),
            invalidatesTags: ['VacancyAll', 'RecruiterVacancy']
        }),
        updateVacancy: builder.mutation<VacancyGet, VacancyUpdateModel>({
            query: (body: VacancyUpdateModel) => ({
                url: `/vacancy`,
                method: 'put',
                data: body
            }),
            invalidatesTags: ['VacancyAll', 'RecruiterVacancy']
        }),
        deleteVacancy: builder.mutation<void, string>({
            query: (id: string) => ({
                url: `/vacancy/${id}`,
                method: 'delete'
            }),
            invalidatesTags: ['VacancyAll', 'RecruiterVacancy']
        }),
    }),
});

export const {
    useGetVacanciesQuery,
    useGetRecruiterVacanciesQuery,
    useGetVacancyQuery,
    useCreateVacancyMutation,
    useLazyGetVacancyLocationQuery,
    useLazyGetVacancySkillsQuery,
    useLazyGetVacancyCategoriesQuery,
    useActivateDisactivateVacancyMutation,
    useUpdateVacancyMutation,
    useDeleteVacancyMutation,
} = vacancyApi;

export const { useQuerySubscription: useQuerySubscriptionGetAllVacancies } = vacancyApi.endpoints.getVacancies;