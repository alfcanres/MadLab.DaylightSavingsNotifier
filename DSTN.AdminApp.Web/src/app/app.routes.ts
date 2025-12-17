import { Routes } from '@angular/router';
import { Dashboard } from './pages/dashboard/dashboard/dashboard';
import { TimeZonesList } from './pages/time-zones/time-zones-list/time-zones-list';
import { NotificationsList } from './pages/notifications/notifications-list/notifications-list';

export const routes: Routes = [
    {
        path: 'dashboard',
        component: Dashboard
    },
    {
        path: 'time-zones',
        component: TimeZonesList
    },
    {
        path: 'notifications',
        component: NotificationsList
    },    
    {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full'
    }
];
