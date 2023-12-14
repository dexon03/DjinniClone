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
        },
        resetProfile: (state) => {
            state.profile = undefined;
        }
    }
});

export const { setProfile, resetProfile } = RecruiterProfileSlice.actions;

export default RecruiterProfileSlice.reducer;