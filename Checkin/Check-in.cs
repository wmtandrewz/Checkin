using System;
using checkin;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace Checkin
{
	public class App : Application
	{
		static NavigationPage _NavPage;

		public App()
		{
			
			if (Settings.Username != "" && Settings.Password != "" && Settings.HotelCode != "")
			{
				//Restore Username and Password
				var user = new User();
				user = new User()
				{
					Username = Settings.Username,
					Password = Settings.Password
				};
				Constants._gatewayURL = Settings.SettingsSAPURL;
				Constants._cookie = Settings.SettingsSAPCookie;
				Constants._user = user;
				Constants._hotel_code = Settings.HotelCode;
				//Restore AccessToken and Expires Time

				Constants._access_token = Settings.AccessToken;
				Constants._expires_in = Settings.ExpiresTime;



				MainPage = new NavigationPage(new GetMainPage());
				//MasterDetailPage
			}
			else //Not Logged Inn
			{
				Settings.SettingsSAPURL = "https://alastor.keells.lk:44300";
				Settings.SettingsSAPCookie = "sap-XSRF_GWP_100";
				MainPage = new NavigationPage(new LoginPage());
			}


			MessagingCenter.Subscribe<userLogout, string>(this, "logout", (sender, arg) =>
			{
				MainPage = new NavigationPage(new LoginPage()); ;
			});

			MessagingCenter.Subscribe<HomeNavigater>(this, "home", (sender) =>
            {
				MainPage = new NavigationPage(new GetMainPage());
            });
            



		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}

