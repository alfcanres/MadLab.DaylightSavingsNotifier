import { Component, input, Input } from '@angular/core';

@Component({
  selector: 'app-validation-summary',
  imports: [],
  templateUrl: './validation-summary.html'
})
export class ValidationSummary {
  @Input() messages: string[] = [];
  @Input() title: string = 'Validation Summary';
  @Input() type: 'error' | 'warning' | 'info' = 'error';
  @Input() footer: string = '';


  getClasss(): string {
    switch (this.type) {
      case 'error':
        return 'alert alert-danger';
      case 'warning':
        return 'alert alert-warning';
      case 'info':
        return 'alert alert-info';
      default:
        return '';
    }
  }
}
