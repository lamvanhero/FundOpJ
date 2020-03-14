namespace OppJar.Dto
{
    public class UserDto : IBaseDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Slug { get; set; }
    }
}
