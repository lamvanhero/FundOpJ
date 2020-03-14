using OppJar.Common.Helpers;
using System;

namespace OppJar.Dto
{
    public class CommentDto
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string FileMimeType { get; set; }
        public string FileUrl { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int Like { get; set; }
        public string ParentId { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string ProfileURL { get; set; }
        public string ReferId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string TimeAgo => CreatedAt.GetTimeAgo();
    }
}
