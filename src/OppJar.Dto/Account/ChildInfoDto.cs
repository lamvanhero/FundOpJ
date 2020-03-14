namespace OppJar.Dto
{
    public class ChildInfoDto : BaseUserInfoDto
    {
        public string School { get; set; }
    }

    public class ChildInfoDtoValitor : BaseUserInfoDtoValidator<ChildInfoDto> { }
}
