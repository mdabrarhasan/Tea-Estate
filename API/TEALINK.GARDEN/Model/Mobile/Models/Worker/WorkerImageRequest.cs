namespace TEALINK.GARDEN.Model.Mobile.Models.Worker
{
    public class WorkerImageRequest
    {
        public string name { get; set; }
        public int bookId { get; set; }
        public byte[] faceJpg { get; set; }
        public byte[] templates { get; set; }
    }
}
