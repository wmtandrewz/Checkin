using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Checkin
{
	public class FieldValidation
	{
		public static string guestSaveDetailsValidation(Picker identificationMethod, string identificationMethodNumber,
														string guestFirstName, string guestLastName,
														DatePicker birthday, string contactNumber,
														string guestEmail, string city, string street)
		{
			string emailExpresion = @"^\s*[\w\-\+_']+(\.[\w\-\+_']+)*\@[A-Za-z0-9]([\w\.-]*[A-Za-z0-9])?\.[A-Za-z][A-Za-z\.]*[A-Za-z]$";
			if (identificationMethod.SelectedIndex == -1)
			{
				return "Please Select Identification Method";
			}
			else if (identificationMethodNumber == "" && identificationMethod.SelectedIndex == 1)
			{
				return "Please Enter Passport Number";
			}
			else if (identificationMethodNumber == "" && identificationMethod.SelectedIndex == 0)
			{
				return "Please Enter National ID Number";
			}
			else if (identificationMethodNumber.Length < 10 && identificationMethod.SelectedIndex == 0)
			{
				return "Please enter the required number of characters for NationalID";
			}
			else if (identificationMethodNumber.Length > 20)
			{
				return "Maximum 20 charactors allowed for Identification Number";
			}
			else if (guestFirstName == "")
			{
				return "Please Enter First Name";
			}
			else if (guestFirstName.Length > 40)
			{
				return "Maximum 40 charactors allowed for First Name";
			}
			else if (guestLastName == "")
			{
				return "Please Enter Last Name";
			}
			else if (guestLastName.Length > 40)
			{
				return "Maximum 40 charactors allowed for Last Name";
			}
			else if (birthday.Date == DateTime.Today)
			{
				return "Please Select Date of Birth";
			}
			else if (contactNumber != "" && (contactNumber).Length < 10)
			{
				return "Contact Number Should Contain More Than 10 Digits";
			}
			else if (guestEmail != "" && Regex.IsMatch(guestEmail, emailExpresion) == false)
			{
				return "Please Enter The Email In Proper Format";
			}
			else if (city != "" && city.Length > 40)
			{
				return "Maximum 40 charactors allowed for city name";
			}
			else if (street != "" && street.Length > 60)
			{
				return "Maximum 60 charactors allowed for street name";
			}
			return "";
		}
	}
}
