using System;

namespace OppJar.Dto
{
    public class MediaFileDto
    {
        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UserId { get; set; }
        public string FileName { get; set; }
        public string NiceFileName { get; set; }
        public string Extension { get; set; }
        public string FileType { get; set; }
        public string Url { get; set; }
        public string DiskPath { get; set; }
    }
}
