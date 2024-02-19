import { createApi } from "@reduxjs/toolkit/dist/query/react";
import { axiosBaseQuery } from "../../../api/axios.baseQuery";
import { ChatDto } from "../../../models/chat/chat.dto";
import { environment } from "../../../environment/environment";
import { ApiServicesRoutes } from "../../../api/api.services.routes";
import { MessageDto } from "../../../models/chat/message.dto";
import { ChatCreateDto } from "../../../models/chat/chat.create.dto";


export const chatApi = createApi({
    reducerPath: 'chatApi',
    baseQuery: axiosBaseQuery({ baseUrl: environment.apiUrl + ApiServicesRoutes.chat }),
    tagTypes: ['ChatList', 'ChatMessages'],
    endpoints: (builder) => ({
        getChatList: builder.query<ChatDto[], string>({
            query: (userId: string) => ({
                url: `/chat/list/` + userId,
                method: 'get'
            }),
            providesTags: ['ChatList']
        }),
        getChatMessages: builder.query<MessageDto[], string>({
            query: (chatId: string) => ({
                url: `/chat/messages/` + chatId,
                method: 'get'
            }),
            providesTags: ['ChatMessages']
        }),
        createChat: builder.mutation<void, ChatCreateDto>({
            query: (chat: ChatDto) => ({
                url: `/chat/create`,
                method: 'post',
                data: chat
            }),
            invalidatesTags: ['ChatList', 'ChatMessages']
        }),
    })
})



export const { useGetChatListQuery, useGetChatMessagesQuery, useCreateChatMutation } = chatApi;