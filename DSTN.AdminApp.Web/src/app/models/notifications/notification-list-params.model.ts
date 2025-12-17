export interface NotificationListParams {
  observedTimeZoneId?: number | null;
  wasRead?: boolean | null;
  recordsPerPage: number;
  currentPage: number;
}