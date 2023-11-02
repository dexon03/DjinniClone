import { createApi } from '@reduxjs/toolkit/query/react';
import { environment } from '../../../environment/environment';
import { VacancyGetAll } from '../../../models/vacany/vacancy.getall.dto';
import { axiosBaseQuery } from '../../../api/axios.baseQuery';

export const vacancyApi = createApi({
    baseQuery: axiosBaseQuery({ baseUrl: environment.apiUrl }),
    endpoints: (builder) => ({
        getVacancies: builder.query<VacancyGetAll[], void>({ query: () => ({ url: '/api/vacancy', method: 'get' }) }),
    }),
});

export const { useGetVacanciesQuery } = vacancyApi;
