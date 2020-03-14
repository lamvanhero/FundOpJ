using OppJar.Common.Enum;
using System;

namespace OppJar.Domain
{
    public class MediaFile : IBaseEntity
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string FileName { get; set; }
        public string NiceFileName { get; set; }
        public string Extension { get; set; }
        public FileType FileType { get; set; }
        public UsingForType UsingForType { get; set; }
        public string Url { get; set; }
        public string DiskPath { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual User User { get; set; }
    }
}
