export interface Notification {
  id: number;
  timeZoneId: number;
  timeZoneColor: string;
  timeZoneDisplayName: string;
  dstTransition: Date;
  notifyDate: Date;
  message: string;
  createdAt: Date;
  wasRead: boolean;
  readAt: Date | null;
}