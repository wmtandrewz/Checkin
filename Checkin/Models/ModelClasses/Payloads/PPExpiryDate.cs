using System;
namespace Checkin.Models.ModelClasses.Payloads
{
    public class PPExpiryDate
    {
		public string XhotelId { get; private set; }

        public string XreservaId { get; private set; }

        public string XtipoDocId { get; private set; }

        public string XnumeroDoc { get; private set; }

        public string Xorden { get; private set; }

        public string Title { get; private set; }

        public string Name1 { get; private set; }

        public string Name2 { get; private set; }

        public string Country { get; private set; }
        
        public string Langu { get; private set; }
        
        public string XfechaExpiry { get; private set; }
         
		public PPExpiryDate(string HotelID, string ReservationID,string GuestNumber,string salutation,string Firstname, string Lastname,string IdentificationCode, string DateOfExpiry,string Identificationmethod,string country, string Language)
        {
            XhotelId = HotelID;
            XreservaId = ReservationID;
            XtipoDocId = Identificationmethod;
            XnumeroDoc = IdentificationCode;
            Xorden = GuestNumber;
            Title = salutation;
            Name1 = Firstname;
            Name2 = Lastname;
            Country = country;
            Langu = Language;
            XfechaExpiry = DateOfExpiry;
        }
    }
}
