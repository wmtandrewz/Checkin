using System;
using System.Collections.Generic;
using checkin;
using Xamarin.Forms;

namespace Checkin
{
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage()
		{
			InitializeComponent();

			this.BindData();
		}

		public void BindData()
		{
			if (Settings.SettingsSAPURL != "" && Settings.SettingsSAPCookie != "")
			{
				SettingsEntrySapUrl.Text = Settings.SettingsSAPURL;
				SettingsEntrySapCookie.Text = Settings.SettingsSAPCookie;
			}
		}

		//Save
		void SaveButtonClickedEvt(object sender, EventArgs e)
		{
			if (EntryChecker(SettingsEntrySapUrl) && EntryChecker(SettingsEntrySapCookie))
			{
				Settings.SettingsSAPURL = SettingsEntrySapUrl.Text;
				Settings.SettingsSAPCookie = SettingsEntrySapCookie.Text;
				Navigation.PopModalAsync(true);
			}
			else {
				DisplayAlert(Constants._headerMessage, "Please Enter Settings!", Constants._buttonClose);
			}
			MessagingCenter.Send<SettingsPage, String>(this, "settingsSaved", "");
		}

		public bool EntryChecker(Entry entry)
		{
			try
			{
				if (entry == null || entry.Text == "" || entry.Text.Contains(" "))
				{
					return false;
				}
				else {
					return true;
				}
			}
			catch (Exception)
			{
				return false;
			}
		}

		//Close
		void CloseButtonClickedEvt(object sender, EventArgs e)
		{
			MessagingCenter.Send<SettingsPage, String>(this, "settingsSaved", "");
			Navigation.PopModalAsync(true);
		}
	}

}
