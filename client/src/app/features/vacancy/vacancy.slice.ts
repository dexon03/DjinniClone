import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import { VacancyGetAll } from '../../../models/vacany/vacancy.getall.dto'
import { RootState } from '../../store';

// Define a type for the slice state
interface VacancyState {
  value: VacancyGetAll[];
}

// Define the initial state using that type
const initialState: VacancyState = {
  value: [] as VacancyGetAll[],
}

export const vacanciesSlice = createSlice({
  name: 'vacancies',
  initialState,
  reducers: {
    setVacancies: (state, action: PayloadAction<VacancyGetAll[]>) => {
      state.value = action.payload
    }
  },
})

export const { setVacancies } = vacanciesSlice.actions

// Other code such as selectors can use the imported `RootState` type
export const selectVacancies = (state: RootState) => state.vacancies.value

export default vacanciesSlice.reducer