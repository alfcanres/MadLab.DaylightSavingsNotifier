export interface ObservedTimeZone {
  id: number;
  countryId: number;
  color: string;
  displayName: string;
  comments: string;
  timeZoneId: string;
  dstStarts: Date | null;
  dstEnds: Date | null;
  lastChanged: Date | null;
  timeZoneObservesDST: boolean;
  nextTransitionDate: Date | null;
  isActive: boolean;
  notifyDaysBefore: number;
}