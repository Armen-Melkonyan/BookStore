namespace MyTask.BookStore.Data
{
    public class BookGallery
    {
        public int ID { get; set; }
        public int BookID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }

        //Create relationship betwin gallery andd book tables
        public Books Book { get; set; }
    }
}
