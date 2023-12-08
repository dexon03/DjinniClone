import { combineReducers, configureStore } from '@reduxjs/toolkit'
import { vacancyApi } from './features/vacancy/vacancy.api'
import { setupListeners } from '@reduxjs/toolkit/dist/query/react'
import { profileApi } from './features/profile/profile.api'
import { companyApi } from './features/company/company.api'
import { candidateApi } from './features/candidate/candidate.api'
import recruiterProfileReducer from './slices/recruiter.profile.slice'
import storage from 'redux-persist/lib/storage'
import { persistReducer, persistStore } from 'redux-persist'



const persistConfig = {
  key: 'root',
  storage,
};

const rootReducer = combineReducers({
  recruiterProfile: recruiterProfileReducer,
  [profileApi.reducerPath]: profileApi.reducer,
  [companyApi.reducerPath]: companyApi.reducer,
  [candidateApi.reducerPath]: candidateApi.reducer,
});

const persistedReducer = persistReducer(persistConfig, rootReducer);


const midleware = [vacancyApi.middleware,
profileApi.middleware,
companyApi.middleware,
candidateApi.middleware]


export const store = configureStore({
  reducer: persistedReducer,
  applyMiddleware(...midleware)
})


setupListeners(store.dispatch)

export const persistor = persistStore(store);
// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch