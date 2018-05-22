using System;

namespace Checkin
{
	public class guestDetails
	{
		public int guestNumber { get; set; }

		public string identificationMethod { get; set; }

		public string passportIdNumber { get; set; }

		public string salutation { get; set; }

		public string guestName { get; set; }

		public string guestFirstName { get; set; }

		public string guestLastName { get; set; }

		public string gender { get; set; }

		public string email { get; set; }

		public string contactNumber { get; set; }

		public string houseNumber { get; set; }

		public string street { get; set; }

		public string city { get; set; }

		public string country { get; set; }

		public string countryKeyValue { get; set; }

		public string nationality { get; set; }

		public string dateOfBirth { get; set; }

		public string dateOfExpiry { get; set; }

		public string language { get; set; }

		public string guestCode { get; set; }

		public string noOfVisits { get; set; }

		public string noOfVisitsHotel { get; set; }

		public string revenueTotal { get; set; }

		public string revenueRoom { get; set; }

		public string reveneuFB { get; set; }

		public string revenueOther { get; set; }



		public guestDetails(int GuestNumber, string IdentificationMethod, string Passportnumber,string Salutation,
							 string GuestName, string Guestfirstname, string Guestseccondname, string Gender,
							 string Email, string ContactNumber, string Housenumber, string Street, string City,
							 string Country, string CountryKeyValue, string Nationality, string DateOfBirth, string DateOfExpiry, string Language, string GuestCode, string NoOfVisits, string NoVisitsHotel,
						   string RevenuTotal, string RevenuRoom, string RevenueFB, string RevenueOther)
		{
			guestNumber = GuestNumber;

			identificationMethod = IdentificationMethod;

			passportIdNumber = Passportnumber;

			salutation = Salutation;

			guestName = GuestName;

			guestFirstName = Guestfirstname;

			guestLastName = Guestseccondname;

			gender = Gender;

			email = Email;

			contactNumber = ContactNumber;

			houseNumber = Housenumber;

			street = Street;

			city = City;

			country = Country;

			countryKeyValue = CountryKeyValue;

			nationality = Nationality;

			dateOfBirth = DateOfBirth;

			dateOfExpiry = DateOfExpiry;

			language = Language;

			guestCode = GuestCode;

			noOfVisits = NoOfVisits;

			noOfVisitsHotel = NoVisitsHotel;

			revenueTotal = RevenuTotal;

			revenueRoom = RevenuRoom;

			reveneuFB = RevenueFB;

			revenueOther = RevenueOther;
		}
	}

}

