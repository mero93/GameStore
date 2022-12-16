namespace API.Helpers
{
    public class PaginationParams
    {
        protected const int MaxPageSize = 50;

        protected int _pageSize = 6;

        private int _pageNumber = 1;

        public int? PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = (int)value > 0 ? (int)value : 1;
        }

        public virtual int? PageSize
        {
            get => _pageSize;

            set
            {
                if ((int)value <= 0) _pageSize = 6;
                else if ((int)value <= MaxPageSize) _pageSize = (int)value;
                else _pageSize = MaxPageSize;
            }
        }
    }
}
