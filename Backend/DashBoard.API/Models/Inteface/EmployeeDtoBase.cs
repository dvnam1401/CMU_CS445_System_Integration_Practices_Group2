namespace DashBoard.API.Models.Inteface
{
    public interface EmployeeDtoBase
    {      
        public string LastName { get; set; }
        public string FirstName { get; set; }        
        public string PhoneNumber { get; set; }
        public short? ShareholderStatus { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
