import { Component, Input, OnInit, output } from '@angular/core';
import { RestService } from '../../../../core/services/rest.service';
import { ChangeForServer, GridColumn } from '../../../../thirdparty/ng-datatable/modals';
import { GridRequest, GridResponse, GridStructure } from '../../../models/grid';
import { Endpoints } from '../../../models/common';
import { DataTableModule } from '../../../../thirdparty/ng-datatable/ng-datatable.module';

@Component({
    selector: 'grid',
    templateUrl: './grid.component.html',
    styleUrl: './grid.component.css',
    imports: [DataTableModule]
})
export class GridComponent<TDto> implements OnInit {
    @Input() endpoints: Endpoints;
    rowClick = output<TDto>();
    columns: GridColumn[];
    rows: TDto[];
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

    onRowClick(item: TDto) {
        this.rowClick.emit(item);
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
        this.restService.post<GridResponse<TDto>>(this.endpoints.getGridData, this.gridRequest)
                        .subscribe(result => {
                            this.rows = result.records;
                            this.totalRows = result.totalRecords;
                        });
    }
}
