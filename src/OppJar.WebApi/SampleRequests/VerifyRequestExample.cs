using OppJar.Core.Services.Model;

namespace OppJar.WebApi.SampleRequests
{
    public class VerifyRequestExample
    {
        public VerifyRequest GetExamples()
        {
            return new VerifyRequest
            {
                AcceptTruliooTermsAndConditions = true,
                CleansedAddress = false,
                ConfigurationName = "Identity Verification",
                CountryCode = "AU",
                ConsentForDataSources = new string[] { "Visa Verification" },
                DataFields = new DataFields
                {
                    PersonInfo = new PersonInfo
                    {
                        FirstGivenName = "J",
                        FirstSurName = "Smith",
                        MiddleName = "Henry",
                        DayOfBirth = 5,
                        MonthOfBirth = 3,
                        YearOfBirth = 1983,
                        MinimumAge = 0
                    },
                    Location = new Location
                    {
                        BuildingNumber = "10",
                        PostalCode = "3108",
                        StateProvinceCode = "VIC",
                        StreetName = "Lawford",
                        StreetType = "St",
                        Suburb = "Doncaster",
                        UnitNumber = "3"
                    },
                    Communication = new Communication
                    {
                        EmailAddress = "testpersonAU%40gdctest.com",
                        Telephone = "03 9896 8785"
                    },
                    Passport = new Passport
                    {
                        Number = "N1236548"
                    }
                }
            };
        }
    }
}
