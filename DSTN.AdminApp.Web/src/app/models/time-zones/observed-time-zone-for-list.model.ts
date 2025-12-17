export interface ObservedTimeZoneForList {
  id: number;
  color: string;
  displayName: string;
  systemTimeZoneId: string;
  nextTransitionDate: Date | null;
  comments: string;
  dstStarts: Date | null;
  dstEnds: Date | null;
  lastChanged: Date | null;
  timeZoneObservesDST: boolean;
  isActive: boolean;
  notificationSchedule: string;
}