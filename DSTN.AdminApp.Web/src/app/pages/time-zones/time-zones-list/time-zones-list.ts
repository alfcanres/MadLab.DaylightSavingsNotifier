import { Component } from '@angular/core';
import { AppHeader } from '../../../shared/app-header/app-header';
import { TimeZonesEdit } from '../time-zones-edit/time-zones-edit';
import { ListPager } from '../../../shared/list-pager/list-pager.component';
import { ObservedTimeZone } from '../../../models/time-zones/observed-time-zone.model';
import { PagerList } from '../../../models/pager-list.model';
import { ResultStatus, ServiceResult } from '../../../models/service-result.model';
import { PagedListResponse } from '../../../models/paged-list-response.model';
import { ValidationSummary } from '../../../shared/validation-summary/validation-summary';
import { NoResults } from '../../../shared/no-results/no-results';
import { FormsModule } from '@angular/forms';
import { AddTimeZoneToObserve } from '../../../models/time-zones/add-time-zone-to-observe.model';
import { EditTimeZoneToObserve } from '../../../models/time-zones/edit-time-zone-to-observe.model';

@Component({
  selector: 'app-time-zones-list',
  imports: [AppHeader, TimeZonesEdit, ListPager, ValidationSummary, NoResults, FormsModule],
  templateUrl: './time-zones-list.html',
})
export class TimeZonesList {
  private readonly recordsPerPage: number = 10;
  list: ObservedTimeZone[] = [];
  pager: PagerList = new PagerList();
  errorMessages: string[] = [];
  serviceResult: ServiceResult<PagedListResponse<ObservedTimeZone>> | null = null;
  searchKeyWord: string = '';
  filterBy: string = 'DisplayName';

  constructor() {}

  ngOnInit() {
    console.log('TimeZonesList initialized');
    this.loadData();
  }

  loadData() {
    console.log('Loading data...');
    this.simulateApiCall();

    if (this.serviceResult && this.serviceResult.status !== ResultStatus.Success) {
      this.errorMessages = this.serviceResult.messages;
    } else {
      if (this.serviceResult?.data?.list) {
        this.list = this.serviceResult.data.list;
      }
      this.pager.currentPage = this.serviceResult?.data?.currentPage || 1;
      this.pager.pageCount = this.serviceResult?.data?.pageCount || 1;
      this.pager.recordsPerPage = this.recordsPerPage;
    }

    console.log('Loading data complete');
  }

  simulateApiCall(): void {
    this.list = [
      {
        id: 1,
        countryId: 100,
        color: 'red',
        displayName: 'Zone 1',
        comments: 'Comment 1',
        timeZoneId: 'TZ1',
        dstStarts: null,
        dstEnds: null,
        lastChanged: null,
        timeZoneObservesDST: false,
        nextTransitionDate: null,
        isActive: true,
        notifyDaysBefore: 5,
      },
      {
        id: 2,
        countryId: 101,
        color: 'blue',
        displayName: 'Zone 2',
        comments: 'Comment 2',
        timeZoneId: 'TZ2',
        dstStarts: null,
        dstEnds: null,
        lastChanged: null,
        timeZoneObservesDST: true,
        nextTransitionDate: null,
        isActive: false,
        notifyDaysBefore: 10,
      },
      {
        id: 3,
        countryId: 102,
        color: 'green',
        displayName: 'Zone 3',
        comments: 'Comment 3',
        timeZoneId: 'TZ3',
        dstStarts: null,
        dstEnds: null,
        lastChanged: null,
        timeZoneObservesDST: true,
        nextTransitionDate: null,
        isActive: true,
        notifyDaysBefore: 7,
      },
      {
        id: 5,
        countryId: 104,
        color: 'purple',
        displayName: 'Zone 5',
        comments: 'Comment 5',
        timeZoneId: 'TZ5',
        dstStarts: null,
        dstEnds: null,
        lastChanged: null,
        timeZoneObservesDST: true,
        nextTransitionDate: null,
        isActive: true,
        notifyDaysBefore: 14,
      },
    ];

    let pagedListResponse = {
      list: [],
      recordCount: 0,
      currentPage: 1,
      pageCount: 1,
      recordsPerPage: 10,
    };

    this.serviceResult = {
      status: ResultStatus.Success,
      data: pagedListResponse,
      messages: [],
    };
  }

  onSave($event: AddTimeZoneToObserve | EditTimeZoneToObserve): void 
  {

    

    this.loadData();
  }

  onPageChanged($event: number) {
    throw new Error('Method not implemented.');
  }
}
