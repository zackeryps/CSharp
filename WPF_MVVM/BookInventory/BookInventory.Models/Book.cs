namespace BookInventory.Models
{
    public class Book
    {
        public int InventoryId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public BookGenre Genre { get; set; }
        public BookFormat Format { get; set; }
        public decimal Price { get; set; }
        public string Isbn { get; set; }
        public string ImageUri { get; set; }
        public override string ToString()
        {
            return $"ID:{InventoryId}, {Title}, {Author}, {Genre}, {Format}, ${Price}, ISBN:{Isbn}";
        }
    }
}
