import { Injectable } from '@angular/core';
import { HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PlayerEndpoints } from '../constants/apiUrls';
import { RestService } from '../../core/services/rest.service';
import { LookupDto } from '../models/common';

export interface Player {
    id: number;
    name: string;
    nationality: string;
}

@Injectable({ providedIn: 'root' })
export class PlayerService {
    constructor(private restService: RestService) { }

    searchPlayers(query: string, field: string = 'name'): Observable<LookupDto[]> {
        if (!query) 
            return new Observable<LookupDto[]>;

        const params = new HttpParams()
            .set('query', query)
            .set('field', field)
            .set('limit', '20'); // Top 20 results

        return this.restService.get<LookupDto[]>(PlayerEndpoints.search, params);
    }
}
