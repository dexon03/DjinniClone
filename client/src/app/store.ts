import { configureStore } from '@reduxjs/toolkit'
import { vacancyApi } from './features/vacancy/vacancy.api'
import { setupListeners } from '@reduxjs/toolkit/dist/query/react'
import { profileApi } from './features/profile/profile.api'
import { skillApi } from './features/skills/skill.api'

export const store = configureStore({
  reducer: {
    [vacancyApi.reducerPath]: vacancyApi.reducer,
    [profileApi.reducerPath]: profileApi.reducer,
  },
  middleware(getDefaultMiddleware) {
    return getDefaultMiddleware().concat(vacancyApi.middleware, profileApi.middleware)
  },
})

setupListeners(store.dispatch)

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch