import { RecruiterProfile } from '../../models/profile/recruiter.profile.model';
import { createSlice } from "@reduxjs/toolkit";

export const RecruiterProfileSlice = createSlice({
    name: 'profile',
    initialState: {
        profile: undefined as RecruiterProfile | undefined,
    },
    reducers: {
        setProfile: (state, action) => {
            state.profile = action.payload;
        }
    }
});

export const { setProfile } = RecruiterProfileSlice.actions;

export default RecruiterProfileSlice.reducer;