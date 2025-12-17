import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-no-results',
  imports: [],
  templateUrl: './no-results.html',
})
export class NoResults {
  @Input() message: string = 'There are no matching entries. Clear filters or add a new time zone to get started.';
  @Input() title: string = 'Nothing Here';

}
