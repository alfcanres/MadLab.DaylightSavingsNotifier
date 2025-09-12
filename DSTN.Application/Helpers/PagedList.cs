namespace DSTN.Application.Helpers
{
    public class PagedList<T> where T : class
    {

        private IEnumerable<T> _list;
        private int _recordCount;
        private int _currentPage;
        private int _pageCount;
        private int _recordsPerPage;

        public PagedList(IEnumerable<T> list, int recordCount, IPagerParams pagerDTO)
        {
            this.List = list;
            this.RecordCount = recordCount;
            this.CurrentPage = pagerDTO.CurrentPage;
            this.RecordsPerPage = pagerDTO.RecordsPerPage;
            double pageCountDoub = Math.Ceiling(Convert.ToDouble((recordCount / Convert.ToDouble(pagerDTO.RecordsPerPage))));
            this.PageCount = Convert.ToInt32(pageCountDoub);
        }

        public IEnumerable<T> List { get => _list; set => _list = value; }
        public int RecordCount { get => _recordCount; set => _recordCount = value; }
        public int CurrentPage { get => _currentPage; set => _currentPage = value; }
        public int PageCount { get => _pageCount; set => _pageCount = value; }
        public int RecordsPerPage { get => _recordsPerPage; set => _recordsPerPage = value; }

    }
}
