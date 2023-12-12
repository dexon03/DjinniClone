import { createApi } from '@reduxjs/toolkit/query/react';
import { environment } from '../../../environment/environment';
import { VacancyGetAll } from '../../../models/vacancy/vacancy.getall.dto';
import { axiosBaseQuery } from '../../../api/axios.baseQuery';
import { ApiServicesRoutes } from '../../../api/api.services.routes';
import { VacancyGet } from '../../../models/vacancy/vacany.get.dto';
import { VacancyCreate } from '../../../models/vacancy/vacancy.create.dto';
import { SkillGetAllDto } from '../../../models/common/SkillGetAllDto.model';
import { LocationDto } from '../../../models/common/location.dto';
import { Category } from '../../../models/vacancy/category.model';

export const vacancyApi = createApi({
    reducerPath: 'vacancyApi',
    baseQuery: axiosBaseQuery({ baseUrl: environment.apiUrl + ApiServicesRoutes.vacancy }),
    keepUnusedDataFor: 5,
    endpoints: (builder) => ({
        getVacancies: builder.query<VacancyGetAll[], void>({ query: () => ({ url: '/vacancy', method: 'get' }) }),
        getRecruiterVacancies: builder.query<VacancyGetAll[], string>({ query: (recruiterId: string) => ({ url: '/vacancy/getRecruiterVacancies/' + recruiterId, method: 'get' }) }),
        getVacancy: builder.query<VacancyGet, string>({ query: (id: string) => ({ url: `/vacancy/${id}`, method: 'get' }) }),
        createVacancy: builder.mutation<VacancyGet, VacancyCreate>({ query: (body: VacancyCreate) => ({ url: '/vacancy', method: 'post', data: body }) }),
        getVacancyLocation: builder.query<LocationDto[], void>({ query: () => ({ url: '/location', method: 'get' }) }),
        getVacancySkills: builder.query<SkillGetAllDto[], void>({ query: () => ({ url: `/skill`, method: 'get' }) }),
        getVacancyCategories: builder.query<Category[], void>({ query: () => ({ url: `/category`, method: 'get' }) }),
        activateDisactivateVacancy: builder.mutation<void, string>({ query: (id: string) => ({ url: `/vacancy/${id}/activate-deactivate`, method: 'put' }) }),
    }),
});

export const { useGetVacanciesQuery, useGetRecruiterVacanciesQuery, useGetVacancyQuery, useCreateVacancyMutation, useLazyGetVacancyLocationQuery, useLazyGetVacancySkillsQuery, useLazyGetVacancyCategoriesQuery, useActivateDisactivateVacancyMutation } = vacancyApi;