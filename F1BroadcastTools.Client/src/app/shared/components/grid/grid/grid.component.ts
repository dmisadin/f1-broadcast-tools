import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Endpoints } from '../../../models/common';
import { RestService } from '../../../../core/services/rest.service';
import { ChangeForServer, GridColumn } from '../../../../thirdparty/ng-datatable/modals';
import { GridFilter, GridRequest, GridResponse, GridStructure } from '../../../models/grid';
import { Player } from '../../../models/Player';

@Component({
    selector: 'grid',
    standalone: false,
    templateUrl: './grid.component.html',
    styleUrl: './grid.component.css'
})
export class GridComponent implements OnInit {
    @Input() endpoints: Endpoints;
    
    columns: GridColumn[];
    rows: any[];
    totalRows: number;
    gridRequest: GridRequest;

    constructor(private restService: RestService) {}

    ngOnInit(): void {
        this.restService.get<GridStructure>(this.endpoints.getGridStructure)
                        .subscribe(result => this.columns = result.columns);

        this.gridRequest = {
            page: 1,
            pageSize: 25,
            filters: [],
            sortBy: "id",
            sortDirection: "asc",
            search: ""
        }              
        this.updateGridRows();
    }

    changeServer(changeData: ChangeForServer) {
        this.gridRequest = {
            page: changeData.currentPage,
            pageSize: changeData.pageSize,
            filters: changeData.columnFilters.map(filter => {
                return {
                    filterType: filter.condition,
                    value: filter.value,
                    property: filter.field
                }
            }),
            sortBy: changeData.sortColumn,
            sortDirection: changeData.sortDirection,
            search: changeData.search,
        }

        this.updateGridRows();
    }

    updateGridRows() {
        this.restService.post<GridResponse<Player>>(this.endpoints.getGridData, this.gridRequest)
                        .subscribe(result => {
                            this.rows = result.records;
                            this.totalRows = result.totalRecords;
                        });
    }
}
