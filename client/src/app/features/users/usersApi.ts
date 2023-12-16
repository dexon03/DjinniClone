import { createApi } from "@reduxjs/toolkit/query";
import { ApiServicesRoutes } from "../../../api/api.services.routes";
import { axiosBaseQuery } from "../../../api/axios.baseQuery";
import { environment } from "../../../environment/environment";

export const usersApi = createApi({
    reducerPath: 'usersApi',
    baseQuery: axiosBaseQuery({ baseUrl: environment.apiUrl + ApiServicesRoutes.identity }),
    endpoints: (builder) => ({
        getUsers: builder.query<UserGetAll[], void>({
            query: () => ({
                url: '/user',
                method: 'get'
            })
        }),
        getUser: builder.query<UserGet, string>({
            query: (id: string) => ({
                url: `/users/${id}`,
                method: 'get'
            })
        }),
        createUser: builder.mutation<UserGet, UserCreate>({
            query: (body: UserCreate) => ({
                url: '/user',
                method: 'post',
                data: body
            })
        }),
        updateUser: builder.mutation<UserGet, UserUpdate>({
            query: (body: UserUpdate) => ({
                url: `/user`,
                method: 'put',
                data: body
            })
        }),
        deleteUser: builder.mutation<void, string>({
            query: (id: string) => ({
                url: `/user /${id}`,
                method: 'delete'
            })
        })
    })
})