import { Component, ViewChild } from '@angular/core';
import { colDef } from '../../../thirdparty/ng-datatable/modals';

@Component({
    standalone: false,
    selector: 'app-players',
    templateUrl: './players.component.html',
    styleUrl: './players.component.css'
})
export class PlayersComponent {
    cols: Array<colDef> = [
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
        } catch {}

        this.loading = false;
    }

    changeServer(data: any) {
        this.params.current_page = data.current_page;
        this.params.pagesize = data.pagesize;
        this.params.sort_column = data.sort_column;
        this.params.sort_direction = data.sort_direction;
        this.params.column_filters = data.column_filters;
        this.params.search = data.search;

        if (data.change_type === 'filter' || data.change_type === 'search') {
            this.filterUsers();
        } else {
            this.getUsers();
        }
    }
}
