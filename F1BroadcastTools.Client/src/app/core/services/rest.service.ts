import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

@Injectable({
    providedIn: 'root'
})
export class RestService {

    private httpClient: HttpClient;

    constructor(httpClient: HttpClient) {
        this.httpClient = httpClient;
    }

    get<T>(path: string) {
        return this.httpClient.get<T>(environment.apiUrl + path);
    }

    getFile<T>(path: string) {
        return this.httpClient.get(environment.apiUrl + path, { responseType: 'blob' });
    }

    post<T>(path: string, data?: any) {
        return this.httpClient.post<T>(environment.apiUrl + path, data);
    }

    save(path: string, data?: any) {
        return this.httpClient.post(environment.apiUrl + path, data, { responseType: 'text' });
    }

    delete(path: string) {
        return this.httpClient.delete(environment.apiUrl + path);
    }
}
