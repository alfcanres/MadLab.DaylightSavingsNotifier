import { Component } from '@angular/core';
import { AppHeader } from "../../../shared/app-header/app-header";
import { NotificationsView } from "../notifications-view/notifications-view";
import { ListPager } from "../../../shared/list-pager/list-pager.component";

@Component({
  selector: 'app-notifications-list',
  imports: [AppHeader, NotificationsView, ListPager],
  templateUrl: './notifications-list.html'
})
export class NotificationsList {

}
