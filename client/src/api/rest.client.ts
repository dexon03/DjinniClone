import {environment} from "../environment/environment.ts";

export class RestClient {
    private readonly apiUrl : string = environment.apiUrl;
    async get<T>(url: string): Promise<T> {
        const response = await fetch(this.apiUrl + url);
        return await response.json();
    }

    async post<T>(url: string, body: any): Promise<T> {
        const response = await fetch(this.apiUrl + url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(body)
        });
        return await response.json();
    }

    async put<T>(url: string, body: any): Promise<T> {
        const response = await fetch(this.apiUrl + url, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(body)
        });
        return await response.json();
    }

    async delete<T>(url: string): Promise<T> {
        const response = await fetch(this.apiUrl + url, {
            method: 'DELETE'
        });
        return await response.json();
    }
}