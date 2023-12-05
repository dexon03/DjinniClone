import { createApi } from '@reduxjs/toolkit/query/react';
import { environment } from '../../../environment/environment';
import { VacancyGetAll } from '../../../models/vacancy/vacancy.getall.dto';
import { axiosBaseQuery } from '../../../api/axios.baseQuery';
import { ApiServicesRoutes } from '../../../api/api.services.routes';
import { VacancyGet } from '../../../models/vacancy/vacany.get.dto';

export const vacancyApi = createApi({
    reducerPath: 'vacancyApi',
    baseQuery: axiosBaseQuery({ baseUrl: environment.apiUrl + ApiServicesRoutes.vacancy }),
    endpoints: (builder) => ({
        getVacancies: builder.query<VacancyGetAll[], void>({ query: () => ({ url: '/vacancy', method: 'get' }) }),
        getVacancy: builder.query<VacancyGet, string>({ query: (id: string) => ({ url: `/vacancy/${id}`, method: 'get' }) }),
    }),
});

export const { useGetVacanciesQuery, useGetVacancyQuery } = vacancyApi;
