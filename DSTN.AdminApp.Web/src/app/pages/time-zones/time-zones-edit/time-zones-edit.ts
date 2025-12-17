import { Component, Input } from '@angular/core';
import { ObservedTimeZone } from '../../../models/time-zones/observed-time-zone.model';
import { ServiceResult } from '../../../models/service-result.model';
import { EmptyApiResponse } from '../../../models/empty-api-response.model';

@Component({
  selector: 'app-time-zones-edit',
  imports: [],
  templateUrl: './time-zones-edit.html',
})
export class TimeZonesEdit {
  viewModel: TimeZoneViewModel = new TimeZoneViewModel();
  @Input() id: number = 0;
  serviceResult: ServiceResult<ObservedTimeZone> | ServiceResult<EmptyApiResponse> | null = null;
  systemTimeZones: string[] = ['TZ1', 'TZ2', 'TZ3'];

  onInit(): void {
    this.loadTimeZone();
  }

  loadTimeZone() {
    this.viewModel.systemTimeZones = this.systemTimeZones;

    if (this.id !== 0) {
      //TODO: Load existing time zone details
    }
  }

  onSave(): void {
    if (this.id === 0) {
    } else {
    }
  }
}

class TimeZoneViewModel {
  id: number = 0;
  color: string = '';
  displayName: string = '';
  comments: string = '';
  timeZoneId: string = '';
  isActive: boolean = false;
  notifyDaysBefore: number = 0;
  dstStartsOn: string = '';
  dstEndsOn: string = '';
  lastChanged: string = '';
  systemTimeZones: string[] = [];
}
