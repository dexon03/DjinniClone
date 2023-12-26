import { createApi } from "@reduxjs/toolkit/dist/query/react";
import { axiosBaseQuery } from "../../../api/axios.baseQuery";
import { ChatDto } from "../../../models/chat/chat.dto";
import { environment } from "../../../environment/environment";
import { ApiServicesRoutes } from "../../../api/api.services.routes";
import { MessageDto } from "../../../models/chat/message.dto";


export const chatApi = createApi({
    reducerPath: 'chatApi',
    baseQuery: axiosBaseQuery({ baseUrl: environment.apiUrl + ApiServicesRoutes.chat }),
    endpoints: (builder) => ({
        getChatList: builder.query<ChatDto[], string>({
            query: (userId: string) => ({
                url: `/chat/list/` + userId,
                method: 'get'
            })
        }),
        getChatMessages: builder.query<MessageDto[], string>({
            query: (chatId: string) => ({
                url: `/chat/messages/` + chatId,
                method: 'get'
            })
        }),
    })
})


export const { useGetChatListQuery, useGetChatMessagesQuery } = chatApi;