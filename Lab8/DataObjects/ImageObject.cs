namespace Lab8.DataObjects
{
    public class ImageObject
    {
        public int ID { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
