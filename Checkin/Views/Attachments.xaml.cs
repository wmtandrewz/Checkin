using System;
using System.Collections.Generic;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Globalization;
using System.Linq;
using Checkin.Models.ModelClasses;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Checkin
{
	public partial class Attachments : ContentPage
	{
		//Data Source

		userLogout userLogout = new userLogout();



		public Attachments()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

			ReservationIDText.Text = Constants._reservation_id;
			ClientNameText.Text = Constants._clientName;

            //Show Loading Message
            pageLoading();


		}

        async void GuestTapGestureRecognizerTapped(object sender, EventArgs e)
		{
			
			await CrossMedia.Current.Initialize();

			if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }
            
			var photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions() { });

            if (photo != null)
			{
				guestImage.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
			}
                

		}

        async void PassportTapGestureRecognizerTapped(object sender, System.EventArgs e)
		{
			await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions() { });

            if (photo != null)
            {
				passportImage.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
            }
		}              
                    
		//Page Loading

		void pageLoading()
		{
			Device.BeginInvokeOnMainThread(() =>
						{
							//attachmentsIndicator.IsVisible = true;
							//attachmentsIndicator.IsRunning = true;
							headerImage.IsVisible = true;
						});
		}

        void ClearPPClicked(object sender, System.EventArgs e)
		{
			passportImage.Source = "camera.png";
		}

        void ClearGuestClicked(object sender, System.EventArgs e)
		{
			guestImage.Source = "camera.png";
		}

		void stopLoading()

		{
			//attachmentsIndicator.IsVisible = false;
			//attachmentsIndicator.IsRunning = false;
			headerImage.IsVisible = true;
		}

		protected override bool OnBackButtonPressed()
		{
			return true;
		}

		//Logout button click

		async void LogoutButtonClickedEvt(object sender, EventArgs e)
		{
			userLogout.logout();
			//Navigate to Login P

		}
	}
}

