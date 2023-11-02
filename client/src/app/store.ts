import { configureStore } from '@reduxjs/toolkit'
import { vacancyApi } from './features/vacancy/vacancy.api'
import { setupListeners } from '@reduxjs/toolkit/dist/query/react'
// ...

export const store = configureStore({
  reducer: {
    [vacancyApi.reducerPath]: vacancyApi.reducer,
  },
  middleware(getDefaultMiddleware) {
    return getDefaultMiddleware().concat(vacancyApi.middleware)
  },
})

setupListeners(store.dispatch)

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch