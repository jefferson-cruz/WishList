namespace WishList.Services.Models.Common
{
    public class PaginationModel
    {
        private const int MAX_PAGE_SIZE = 50;
        private const int DEFAULT_PAGE = 1;

        private int pageSize = MAX_PAGE_SIZE;
        private int page = DEFAULT_PAGE;
        
        public int Page
        {
            get => this.page;
            set => this.page = (value < DEFAULT_PAGE) ? DEFAULT_PAGE : value;
        }
        public int PageSize
        {
            get => this.pageSize;
            set => this.pageSize = (value < 1 || value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value;
        }

        public int Offset => pageSize * (page - 1);
    }


}
