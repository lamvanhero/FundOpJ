using OppJar.Common.Helpers;
using OppJar.Common.Enum;
using System;

namespace OppJar.Dto
{
    public class FeedDto
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string FileUrl { get; set; }
        public string AvatarUrl { get; set; }
        public int Like { get; set; }
        public FeedType FeedType { get; set; } = FeedType.Text;
        public string FeedTypeValue => Enum.GetName(typeof(FeedType), FeedType);
        public string Preview { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string FullName { get; set; }
        public Privacy Privacy { get; set; }
        public string UserSlug { get; set; }
        public string PrivacyValue => System.Enum.GetName(typeof(Privacy), Privacy);
        public string TimeAgo => CreatedAt.GetTimeAgo();
        public PageResultDto<CommentDto> Comments { get; set; } = new PageResultDto<CommentDto>();
    }
}
