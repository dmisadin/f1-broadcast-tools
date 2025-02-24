import { Component, ViewChild } from '@angular/core';
import { GridColumn } from '../../../thirdparty/ng-datatable/modals';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Nationality } from '../../../shared/models/Enumerations';
import { KeyNumberPair } from '../../../shared/models/common';
import { RestService } from '../../../core/services/rest.service';
import { Player } from '../../../shared/models/Player';
import { PlayerEndpoints } from '../../../shared/constants/apiUrls';
import { GridRequest } from '../../../shared/models/grid';

@Component({
    standalone: false,
    selector: 'app-players',
    templateUrl: './players.component.html',
    styleUrl: './players.component.css'
})
export class PlayersComponent {
    cols: Array<GridColumn> = [
        { field: 'id', title: 'ID', isUnique: true },
        { field: 'firstName', title: 'First Name' },
        { field: 'lastName', title: 'Last Name' },
        { field: 'email', title: 'Email' },
        { field: 'age', title: 'Age', type: 'number' },
        { field: 'dob', title: 'Birthdate', type: 'date' },
        { field: 'address.city', title: 'City' },
        { field: 'isActive', title: 'Active', type: 'bool' },
    ];
    rows: Array<any> = [];
    total_rows: number = 0;
    loading = false;
    params = {
        current_page: 1,
        pagesize: 5,
        sort_column: 'id',
        sort_direction: 'desc',
        column_filters: [],
        search: '',
    };
    @ViewChild('datatable') datatable: any;
    controller: any;
    timer: any;

    playerEndpoints = PlayerEndpoints;
    form: FormGroup;
    nationalities: KeyNumberPair[];
    gridRequest: GridRequest;

    constructor(private fb: FormBuilder,
                private restService: RestService)
    {
        this.form = this.fb.group({
            name: ['', [Validators.required, Validators.minLength(3)]],
            nationality: [0, Validators.required]
        });

        this.nationalities = Object.entries(Nationality)
            .filter(([key, value]) => typeof value === 'number') // Ensure it's a number
            .map(([key, value]) => ({ key, value: value as number }));

            console.log(this.nationalities)
    }

    onSubmit() {
        if (this.form.invalid) 
            return;

        const newPlayer = { name: this.form.value.name, nationality: parseInt(this.form.value.nationality)};

        this.restService.post<Player>(PlayerEndpoints.add, newPlayer).subscribe(result => console.log(result));
    }


    filterUsers() {
        clearTimeout(this.timer);
        this.timer = setTimeout(() => {
            this.getUsers();
        }, 300);
    }

    async getUsers() {
        // cancel request if previous request still pending before next request fire
        if (this.controller) {
            this.controller.abort();
        }
        this.controller = new AbortController();
        const signal = this.controller.signal;

        try {
            this.loading = true;

            const response = await fetch('https://vue3-datatable-document.vercel.app/api/user', {
                method: 'POST',
                body: JSON.stringify(this.params),
                signal: signal, // Assign the signal to the fetch request
            });

            const data = await response.json();

            this.rows = data?.data;
            this.total_rows = data?.meta?.total;
        } catch { }

        this.loading = false;
    }

    changeServer(data: any) {
        this.params.current_page = data.current_page;
        this.params.pagesize = data.pagesize;
        this.params.sort_column = data.sort_column;
        this.params.sort_direction = data.sort_direction;
        this.params.column_filters = data.column_filters;
        this.params.search = data.search;
        
        /* my way
        this.gridRequest = {
            page: data.current_page,
            pageSize: data.pagesize,
            filters: data.column_filters,
            sortBy: data.sort_column,
            sortDirection: data.sort_direction,
            search: data.search
        }
        */

        if (data.change_type === 'filter' || data.change_type === 'search') {
            this.filterUsers();
        } else {
            this.getUsers();
        }
    }
}
