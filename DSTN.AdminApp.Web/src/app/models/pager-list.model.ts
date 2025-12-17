export class PagerList{
    currentPage: number = 0;
    recordsPerPage: number = 0;
    pageCount: number =  0;
    searchKeyword: string = '';

    constructor(){
        this.currentPage = 0;
        this.recordsPerPage = 0;
        this.pageCount = 0;
        this.searchKeyword = '';
    } 
}