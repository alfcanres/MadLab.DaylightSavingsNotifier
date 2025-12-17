import { Component, EventEmitter, Input, input, Output } from '@angular/core';
import { PagerList } from '../../models/pager-list.model';

@Component({
  selector: 'app-list-pager',
  imports: [],
  templateUrl: './list-pager.component.html',
})
export class ListPager {
  @Input() pager: PagerList = new PagerList();
  @Output() pageChanged = new EventEmitter<number>();

  get pages(): number[] {
    const pages: number[] = [];

    for (let i = 1; i <= this.pager.pageCount; i++) {
      pages.push(i);
    }
    
    return pages;
  }

  goToNextPage(): void{
    this.pager.currentPage++;
    this.pageChanged.emit(this.pager.currentPage);
  }

  goToPreviousPage(): void{
    this.pager.currentPage--;
    this.pageChanged.emit(this.pager.currentPage);
  }

  goToPage(page: number): void{ 
    this.pager.currentPage = page;
    this.pageChanged.emit(this.pager.currentPage);
  }
}
