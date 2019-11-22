using System;
using System.Collections.Generic;
using checkin;
using Checkin.Data.Retrieving;
using Xamarin.Forms;

namespace Checkin
{
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage()
		{
			InitializeComponent();

            if(Settings.IsRegistered)
            {
                uuidEnry.IsEnabled = false;
                uuidEnry.Text = Settings.UUID;
                registeredLabel.Text = "Device has been Registered";
                registeredLabel.TextColor = Color.Green;
            }


            registerButton.Clicked+= async delegate {

                try
                {
                    if (!string.IsNullOrEmpty(uuidEnry.Text))
                    {
                        var res = await APIGetService.RegisterDevice(uuidEnry.Text, Constants._version);

                        if(res.Notes.ToLower().Contains("device already registered"))
                        {
                            await Application.Current.MainPage.DisplayAlert("Atention!", "Device has been already registered", "OK");
                        }

                        else if (res.Notes.ToLower().Contains("no such device in database"))
                        {
                            await Application.Current.MainPage.DisplayAlert("Atention!", "Device not found", "OK");
                        }

                        else if (res.Notes.ToLower().Contains("error"))
                        {
                            await Application.Current.MainPage.DisplayAlert("Atention!", "Device registration error", "OK");
                        }

                        else if (res.IsResgistered)
                        {
                            Settings.IsRegistered = true;
                            Settings.UUID = res.DeviceID;
                            uuidEnry.IsEnabled = false;
                            registeredLabel.Text = "Device has been Registered";
                            registeredLabel.TextColor = Color.Green;
                            await Application.Current.MainPage.DisplayAlert("Success", "Device has been registered", "OK");
                            await Navigation.PopAsync();
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Failed", "Couldn't register the device", "OK");
                        }
                    }

                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Warning!", "Please enter device UUID", "OK");
                    }
                }
                catch (Exception ex)
                {

                }
                
            };
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
