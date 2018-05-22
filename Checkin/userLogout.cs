using System;
using Xamarin.Forms;
using Checkin;

namespace Checkin
{
	public class userLogout
	{
		public userLogout()
		{

		}
		public async void logout()
		{
			//Delete Stored Setting
			Settings.Username = string.Empty;
			Settings.Password = string.Empty;
			Settings.AccessToken = string.Empty;
			Settings.HotelCode = string.Empty;
			Settings.Username = string.Empty;
			//Settings.SettingsSAPURL = string.Empty;
			//Settings.SettingsSAPCookie = string.Empty;

			MessagingCenter.Send<userLogout, string>(this, "logout", "");
		}

	}
}

