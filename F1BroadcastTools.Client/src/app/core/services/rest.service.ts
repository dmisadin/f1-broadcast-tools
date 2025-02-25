import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

@Injectable({
    providedIn: 'root'
})
export class RestService {

    constructor(private httpClient: HttpClient) { }

    get<T>(path: string, queryParams?: HttpParams) {
        return this.httpClient.get<T>(environment.apiUrl + path, { params: queryParams });
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
