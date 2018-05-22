using System;

namespace Checkin
{
	public class StatusChange
	{
		
        public string XhotelId { get; private set; }

		public string XreservaId { get; private set; }

		public string XtipoDocId { get; private set; }

		public string XnumeroDoc { get; private set; }

		public string Xorden { get; private set; }

		public string Title { get; private set; }

        public string Name1 { get; private set; }

		public string Name2 { get; private set; }

		public string Parge { get; private set; }

		public string SmtpAddr { get; private set; }

		public string MobileNo { get; private set; }

		public string HouseNum1 { get; private set; }

		public string Street { get; private set; }

		public string City1 { get; private set; }

		public string Country { get; private set; }

		public string County { get; private set; }

		public string Langu { get; private set; }

		public string Gbdat { get; private set; }

		public string XfechaExpiry { get; private set; }

		public StatusChange(string HotelID, string ReservationID, string Identificationmethod, string IdentificationCode, string GuestNumber,string salutation,
							 string Firstname, string Lastname, string Gender
			, string Email, string Contactno, string Houseno
			, string street, string City, string country, string Nationality
			, string Language, string DateOfBirth , string DateOfExpiry)
		{
			XhotelId = HotelID;
			XreservaId = ReservationID;
			XtipoDocId = Identificationmethod;
			XnumeroDoc = IdentificationCode;
			Xorden = GuestNumber;
			Title = salutation;
			Name1 = Firstname;
			Name2 = Lastname;
			Parge = Gender;
			SmtpAddr = Email;
			MobileNo = Contactno;
			HouseNum1 = Houseno;
			Street = street;
			City1 = City;
			Country = country;
			County = Nationality;
			Langu = Language;
			Gbdat = DateOfBirth;
			XfechaExpiry = DateOfExpiry;
		}
        
		//public StatusChange(string HotelID, string ReservationID,string GuestNumber,string salutation,string Firstname, string Lastname,string IdentificationCode, string DateOfExpiry,string Identificationmethod,string country, string Language)
        //{
        //    XhotelId = HotelID;
        //    XreservaId = ReservationID;
        //    XtipoDocId = Identificationmethod;
        //    XnumeroDoc = IdentificationCode;
        //    Xorden = GuestNumber;
        //    Title = salutation;
        //    Name1 = Firstname;
        //    Name2 = Lastname;
        //    Country = country;
        //    Langu = Language;
        //    XfechaExpiry = DateOfExpiry;
        //}

	}
}

