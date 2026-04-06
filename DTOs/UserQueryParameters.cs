namespace WorkflowApi.DTOs
{
    public class UserQueryParameters
    {

        public string? Search { get; set; }
        public int Page { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > 50) ? 50 : value;
        }

        public string? SortBy { get; set; }
        public bool SortDesc { get; set; } = false;
    }
}
