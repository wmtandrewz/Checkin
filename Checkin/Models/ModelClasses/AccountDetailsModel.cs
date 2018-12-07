using System;
namespace Checkin.Models.ModelClasses
{
    public class AccountDetailsModel
    {
        public string HotelCode { get; set; }
        public string AccountHolder { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string SwiftCode { get; set; }
    }
}
