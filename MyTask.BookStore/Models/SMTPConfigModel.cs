namespace MyTask.BookStore.Models
{
    public class SMTPConfigModel 
    {
        public string SenderAddress { get; set; }
        public string SenderDisplayAddress { get; set; }
        public string UserName { get; set; }
        public string password { get; set; }
        public string MyProperty { get; set; }
        public string host { get; set; }
        public int port { get; set; }
        public bool EnableSSL { get; set; }
        public bool UseDefaultCredientials { get; set; }
        public bool IsBodyHTML { get; set; }
    }
}
