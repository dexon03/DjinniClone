import { environment } from "../environment/environment";
import axios from 'axios';
import { ApiServicesRoutes } from "./api.services.routes";

const api = axios.create({
    baseURL: environment.apiUrl,
});


api.interceptors.request.use(
    (config) => {
        const storageToken = localStorage.getItem('token');
        const token = storageToken ? JSON.parse(storageToken)?.accessToken : null;
        if (token) {
            config.headers!.Authorization = `Bearer ${token}`;
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
                axios.post(environment.apiUrl + ApiServicesRoutes.identity + '/auth/refresh', { refreshToken })
                    .then((response) => {
                        const token = response.data;
                        const stringToken = JSON.stringify(token);

                        localStorage.setItem('token', stringToken);

                        originalRequest.headers.Authorization = `Bearer ${token.accessToken}`;
                    });
                return axios(originalRequest);
            } catch (error) {
                console.log(error);
            }
        }

        return Promise.reject(error);
    }
);

export default api;