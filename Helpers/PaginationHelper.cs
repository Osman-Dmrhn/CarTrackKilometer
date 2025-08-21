namespace CarKilometerTrack.Helpers
{
    public class PaginationHelper <T>
    {
        public IEnumerable<T> data { get; set; }
        public int previous_page {  get; set; }
        public int current_page { get; set;}

        public int next_page { get; set; }

        public int total_pages { get; set; }

        public int total_items { get; set; }

        public int take {  get; set; }
    }
}
