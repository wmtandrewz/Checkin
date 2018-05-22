using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace Checkin
{
	public class serviceDataValidation
	{
		public static string validation(string value)
		{
			if (value != null)
			{
				if (value != "")
				{
					value = value;

				}
				else
				{
					value = Constants._notAvailable;

				}
			}

			return value;
		}

		//Textfield color

		public static Color validationColor(string value)
		{
			Color textColor;

			if (value != Constants._notAvailable)
			{
				textColor = Color.Black;
			}
			else
			{
				textColor = Color.Red;
			}

			return textColor;
		}

		public static string paxValidation(string value)
		{

			if (value == "0")
			{
				value = "";

			}
			else
			{
				value = value;

			}

			return value;
		}
		public static string guestEditGenderValidation(string value)
		{

			if (value == "M")
			{
				value = "1";

			}
			else
			{
				value = "2";

			}

			return value;
		}
		public static string roomPreferencesValidation(string value)
		{

			if (value == "")
			{
				value = "Preferences N/A";

			}
			else
			{
				value = value;

			}

			return value;
		}

		public static string XstdoLimpia { get; internal set; }

		public static string roomImageValidation(string value)
		{

			if (value == Constants._inspectedRoom || value == "IN")
			{
				value = "inspected.png";

			}
			else if (value == Constants._dirtyRoom || value == "DT")

			{
				value = "DirtyRoom.png";
			}

			else if (value == Constants._cleanedRoom || value == "CL")

			{
				value = "CleanRoom.png";
			}
			else
			{
				value = "Unknown.png";

			}

			return value;
		}
		public static string roomImageValidationReservationList(string value)
		{

			if (value == Constants._inspectedRoom || value == "INSPECTED")
			{
				value = "inspected.png";

			}
			else if (value == Constants._dirtyRoom || value == "DIRTYT")

			{
				value = "DirtyRoom.png";
			}

			else if (value == Constants._cleanedRoom || value == "CLEAN")

			{
				value = "CleanRoom.png";
			}
			else
			{
				value = "Unknown.png";

			}

			return value;
		}

		public static string roomNumberValidation(string value)
		{

			if (value == "")
			{
				value = "N/A";

			}
			else
			{
				value = value;

			}

			return value;
		}

		public static string hotelVisitValidation(string value)
		{

			if (value == "" || value == null)
			{
				value = "0";

			}
			else
			{
				value = value;

			}

			return value;
		}
		public static string hotelrRevenueValidation(string value)
		{

			if (value == "" || value == null)
			{
				value = "0.00";

			}
			else
			{
				value = value;

			}

			return value;
		}
		public static DateTime dateOfBirthValidation(string value)
		{
			value = value.Split('(', ')')[0];
			DateTime date = DateTime.Today;
			if (value == "")
			{
				date = DateTime.Today;
			}
			else
			{
				date = DateTime.Parse(value);
			}
			return date;
		}

		public static DateTime dateOfExpiryValidation(string value)
        {
            value = value.Split('(', ')')[0];
            DateTime date = DateTime.Today;
            if (value == "")
            {
                date = DateTime.Today;
            }
            else
            {
                date = DateTime.Parse(value);
            }
            return date;
        }


		public static string performaValidation(int value, string val)
		{
			if (value > 0)
			{
				val = val;
			}
			else
			{
				val = "Not Available";
			}
			return val;
		}


		public static string titleName(string val)
		{
			string name = "";
			var nameToSalutation = CountryDictionary.listofSalutation();
			if (val != "" || val != "")
			{
				//Item Value in dictionar
				name = nameToSalutation.FirstOrDefault(x => x.Value == val).Key;

			}
			return name;
		}

		public static string titleCountry(string val)
		{
			string name = "";
			var nameToCountry = CountryDictionary.listOfCountrie();
			if (val != "" || val != "")
			{
				//Item Value in dictionar
				name = nameToCountry.FirstOrDefault(x => x.Value == val).Key;

			}
			return name;
		}

		public static string titleNationality(string val)
		{
			string name = "";
			var nameToNationality = CountryDictionary.listOfNationalitie();
			if (val != "" || val != "")
			{
				//Item Value in dictiona
				name = nameToNationality.FirstOrDefault(x => x.Value == val).Key;

			}
			return name;
		}

		public static string decimalTruncating(string val)
		{
			string value = (Math.Truncate(Double.Parse(val, CultureInfo.InvariantCulture) * 100) / 100).ToString();
			return value;

		}

	}
}

