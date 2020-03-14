namespace OppJar.Dto
{
    public class EventDto : IBaseDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string CreatedBy { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UrlPhotos { get; set; }
        public string NiceUrl { get; set; }
        public string Status { get; set; }
    }
}
