namespace API.Helpers
{
    public class DiscussionParams : PaginationParams
    {
        protected new int _pageSize = 10;

        public override int? PageSize
        {
            get => _pageSize;

            set
            {
                if ((int)value <= 0) _pageSize = 10;
                else if ((int)value <= MaxPageSize) _pageSize = (int)value;
                else _pageSize = MaxPageSize;
            }
        }
    }
}
