namespace BookStore.DataAccessLayer.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
