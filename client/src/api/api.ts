import { environment } from "../environment/environment";
import axios from 'axios';

const api = axios.create({
    baseURL: environment.apiUrl,
});

api.interceptors.request.use(
    (config) => {
        const storageToken = localStorage.getItem('token');
        const token = storageToken ? JSON.parse(storageToken)?.accessToken : null;
        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    },
    (error) => Promise.reject(error)
);

api.interceptors.response.use(
    (response) => response,
    async (error) => {
        const originalRequest = error.config;

        if (error.response.status === 401 && !originalRequest._retry) {
            originalRequest._retry = true;

            try {
                const storageToken = localStorage.getItem('token');
                const refreshToken = storageToken ? JSON.parse(storageToken)?.refreshToken : null;
                const response = await axios.post(environment.apiUrl + '/api/auth/refresh', { refreshToken });
                const { token } = response.data;

                localStorage.setItem('token', token);

                originalRequest.headers.Authorization = `Bearer ${token}`;
                return axios(originalRequest);
            } catch (error) {
                console.log(error);

            }
        }

        return Promise.reject(error);
    }
);

export default api;