using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DashBoard.API.Models.DTO
{
    public class PayRateDto
    {
        public int IdPayRates { get; set; }
        public string PayRateName { get; set; }        
        public decimal Value { get; set; }        
        public decimal TaxPercentage { get; set; }        
        public int PayType { get; set; }        
        public decimal PayAmount { get; set; }        
       // public decimal PtLevelC { get; set; }
    }
}
