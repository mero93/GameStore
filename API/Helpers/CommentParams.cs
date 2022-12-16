namespace API.Helpers
{
    public class CommentParams : PaginationParams
    {
        protected new int _pageSize = 15;

        public override int? PageSize
        {
            get => _pageSize;

            set
            {
                if ((int)value <= 0) _pageSize = 15;
                else if ((int)value <= MaxPageSize) _pageSize = (int)value;
                else _pageSize = MaxPageSize;
            }
        }
    }
}
