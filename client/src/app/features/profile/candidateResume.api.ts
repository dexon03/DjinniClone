import { createApi } from "@reduxjs/toolkit/dist/query/react";
import { ApiServicesRoutes } from "../../../api/api.services.routes";
import { axiosBaseQuery } from "../../../api/axios.baseQuery";
import { environment } from "../../../environment/environment";

export const candidateResumeApi = createApi({
    reducerPath: 'candidateResumeApi',
    baseQuery: axiosBaseQuery({ baseUrl: environment.apiUrl + ApiServicesRoutes.profile }),
    tagTypes: ['PdfResume'],
    endpoints: (builder) => ({
        downloadResume: builder.query<Blob, string>({
            query: (id: string) => ({
                url: `/profile/downloadResume/${id}`,
                method: 'get',
                responseType: 'blob'
            }),
            providesTags: ['PdfResume']
        }),
        uploadResume: builder.mutation<void, FormData>({
            query: (data: FormData) => ({
                url: `/profile/uploadResume`,
                method: 'put',
                data: data
            }),
            invalidatesTags: ['PdfResume']
        }),
    })
});

export const {
    useLazyDownloadResumeQuery,
    useUploadResumeMutation
} = candidateResumeApi;

