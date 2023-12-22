import { combineReducers, configureStore } from '@reduxjs/toolkit'
import { vacancyApi } from './features/vacancy/vacancy.api'
import { setupListeners } from '@reduxjs/toolkit/dist/query/react'
import { profileApi } from './features/profile/profile.api'
import { companyApi } from './features/company/company.api'
import { candidateApi } from './features/candidate/candidate.api'
import recruiterProfileReducer from './slices/recruiter.profile.slice'
import storage from 'redux-persist/lib/storage'
import { persistReducer, persistStore } from 'redux-persist'
import { usersApi } from './features/users/usersApi'
import { candidateResumeApi } from './features/profile/candidateResume.api'



const persistConfig = {
  key: 'root',
  storage,
  blacklist: ['candidateResumeApi']
};

const appReducer = combineReducers({
  recruiterProfile: recruiterProfileReducer,
  [candidateResumeApi.reducerPath]: candidateResumeApi.reducer,
  [usersApi.reducerPath]: usersApi.reducer,
  [vacancyApi.reducerPath]: vacancyApi.reducer,
  [profileApi.reducerPath]: profileApi.reducer,
  [companyApi.reducerPath]: companyApi.reducer,
  [candidateApi.reducerPath]: candidateApi.reducer,
});

const rootReducer = (state, action) => {
  if (action.type === 'SIGNOUT_REQUEST') {
    storage.removeItem('persist:root')
    return appReducer(undefined, action);
  }
  return appReducer(state, action);
}

const persistedReducer = persistReducer(persistConfig, rootReducer);


export const store = configureStore({
  reducer: persistedReducer,
  middleware(getDefaultMiddleware) {
    return getDefaultMiddleware().concat(
      candidateResumeApi.middleware,
      usersApi.middleware,
      vacancyApi.middleware,
      profileApi.middleware,
      companyApi.middleware,
      candidateApi.middleware
    )
  },
})

setupListeners(store.dispatch)

export const persistor = persistStore(store);

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch