using OppJar.Common.Enum;
using OppJar.Dto;
using System;

namespace OppJar.Web.Models
{
    public class MemoryViewModel
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string FileUrl { get; set; }
        public string AvatarUrl { get; set; }
        public int Like { get; set; }
        public string Preview { get; set; }
        public FeedType FeedType { get; set; } = FeedType.Text;
        public string FeedTypeValue => Enum.GetName(typeof(FeedType), FeedType);
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string FullName { get; set; }
        public string TimeAgo { get; set; }
        public Privacy Privacy { get; set; }
        public string UserSlug { get; set; }
        public string PrivacyValue => Enum.GetName(typeof(Privacy), Privacy);
        public PageResultDto<CommentViewModel> Comments { get; set; } = new PageResultDto<CommentViewModel>();
    }
}
