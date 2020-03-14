namespace Models.Sakila
{
    public partial class NicerButSlowerFilmList
    {
        public ushort? Fid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal? Price { get; set; }
        public ushort? Length { get; set; }
        public string Rating { get; set; }
        public string Actors { get; set; }
    }
}
