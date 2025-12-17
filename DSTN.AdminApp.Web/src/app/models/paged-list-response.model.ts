export interface PagedListResponse<T> {
  list: T[];
  recordCount: number;
  currentPage: number;
  pageCount: number;
}