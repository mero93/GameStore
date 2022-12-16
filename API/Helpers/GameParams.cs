namespace API.Helpers
{
    public class GameParams : PaginationParams
    {
        public string SortBy { get; set; } = "Newly Added";

        public bool ReverseOrder { get; set; }

        public string PublishersFilter { get; set; }

        public string GenresFilter { get; set; }

        public int MinDownloads { get; set; }

        public int MinRating { get; set; }

        public DateTime MinReleaseDateFilter { get; set; }  = new DateTime(2000);

        public DateTime MaxReleaseDateFilter { get; set; } = DateTime.Now.AddYears(1);

        public DateTime MinDateAddedFilter { get; set; } = new DateTime(2022);

        public DateTime MaxDateAddedFilter { get; set; } = DateTime.Now;

        public string SearchString { get; set; }
    }
}
