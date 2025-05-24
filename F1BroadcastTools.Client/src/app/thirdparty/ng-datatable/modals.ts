export class GridColumn {
    isUnique?: boolean;
    field: string;
    title?: string;
    value?: any;
    condition?: any;
    type?: ColumnType; // string|date|number|bool
    width?: string | undefined;
    minWidth?: string | undefined;
    maxWidth?: string | undefined;
    hide?: boolean;
    filter?: boolean; // column filter
    search?: boolean; // global search
    sort?: boolean;
    html?: boolean;
    cellRenderer?: any; // [Function, string]
    headerClass?: string;
    cellClass?: string;
}

export class Pager {
    public totalRows: number;
    public currentPage: number;
    public pageSize: number;
    public maxPage: number;
    public startPage: number;
    public endPage: number;
    public stringFormat: string;
    public pages: any;
}

export type ColumnType = "string" | "date" | "number" | "bool";

export type ChangeType = "reset" | "page" | "pagesize" | "sort" | "filter" | "search";

export type SortDirection = "asc" | "desc";

export interface ChangeForServer {
    currentPage: number;
    pageSize: number;
    offset: number;
    sortColumn: string;
    sortDirection: SortDirection;
    search: string;
    columnFilters: GridColumn[];
    changeType: ChangeType;
}