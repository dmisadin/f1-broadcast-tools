import { GridColumn, SortDirection } from "../../thirdparty/ng-datatable/modals";

export interface GridStructure {
    columns: GridColumn[];
}

export interface GridRequest {
    page: number;
    pageSize: number;
    filters: GridFilter[];
    sortBy: string;
    sortDirection: SortDirection;
    search: string;
}

export interface GridFilter {
    filterType: string;
    property: string;
    value: any;
}

export interface GridResponse<TDto> {
    records: TDto[];
    totalRecords: number;
}
