namespace DashBoard.API.Repositories.Implementation
{
    public class PersonalDetailDto
    {
        public decimal PersonalId { get; set; }
        public string? CurrentFirstName { get; set; }
        public string? CurrentLastName { get; set; }        
        public DateOnly? BirthDate { get; set; }
        public string? CurrentCity { get; set; }
        public string? CurrentGender { get; set; }
        public string? CurrentPhoneNumber { get; set; }
        public string? CurrentPersonalEmail { get; set; }

    }
}
