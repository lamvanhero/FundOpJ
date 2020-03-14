namespace OppJar.Dto
{
    public class CurrentUserProfileDto
    {
        public CurrentUserProfileDto() { }

        public CurrentUserProfileDto(string roleName, string email,
            string userId, string customerId, string displayName, string avatar, string firstName, string lastName)
        {
            Email = email;
            RoleName = roleName;
            UserId = userId;
            DisplayName = displayName;
            Avatar = avatar;
            CustomerId = customerId;
            FirstName = firstName;
            LastName = lastName;
        }

        public string RoleName { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public string DisplayName { get; set; }
        public string Avatar { get; set; }
        public string CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
