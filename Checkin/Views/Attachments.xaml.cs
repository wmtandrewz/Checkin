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
using System.IO;
using Checkin.Models.ModelClasses.Payloads;
using System.Diagnostics;

namespace Checkin
{
	public partial class Attachments : ContentPage
	{
		//Data Source

		userLogout userLogout = new userLogout();
        MediaFile _passportImage = null;
        MediaFile _guestImage = null;

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
            
			var photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions() {
             
                Name = "guestImage.jpg",
                PhotoSize = PhotoSize.Medium

            });

            if (photo != null)
			{
                _guestImage = photo;
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

            var photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions() {

               Name = "PPImage.jpg",
                PhotoSize = PhotoSize.Medium
            });

            if (photo != null)
            {
                _passportImage = photo;

				passportImage.Source = ImageSource.FromStream(() => 
                { 
                    return photo.GetStream();
               });
            }
		}              
                    
		//Page Loading

		void pageLoading()
		{
			Device.BeginInvokeOnMainThread(() =>
						{
							indicator.IsVisible = true;
							indicator.IsRunning = true;
							headerImage.IsVisible = false;
                            attachementBaseLayout.IsVisible = false;
                            DisplayAttachments();
						});
		}

        async void DisplayAttachments()
        {
            try
            {
                var responce = await new CheckInManager().GetAttachments("JPG");
                var output = JObject.Parse(responce);

                List<ReservationAttachment> AttachmentList = new List<ReservationAttachment>();

                if (Enumerable.Any(output["d"]["results"]))
                {
                    var result = Convert.ToString(output["d"]["results"]);

                    AttachmentList = JsonConvert.DeserializeObject<List<ReservationAttachment>>(result);
                }

                foreach (var item in AttachmentList)
                {
                    if (item.AttachmentName.Contains("Passport"))
                    {
                        byte[] data = Convert.FromBase64String(item.AttachmentString);
                        passportImage.Source = ImageSource.FromStream(() => new MemoryStream(data));
                    }
                    else if (item.AttachmentName.Contains("Guest"))
                    {
                        byte[] data = Convert.FromBase64String(item.AttachmentString);
                        guestImage.Source = ImageSource.FromStream(() => new MemoryStream(data));
                    }
                }

                indicator.IsVisible = false;
                indicator.IsRunning = false;
                headerImage.IsVisible = true;
                attachementBaseLayout.IsVisible = true;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                indicator.IsVisible = true;
                indicator.IsRunning = true;
                headerImage.IsVisible = true;
                attachementBaseLayout.IsVisible = false;
            }

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

		void HomeClicked(object sender, EventArgs e)
        {
            new HomeNavigater().GoHome();
        }

        async void SaveAttachmentButton_Clicked(object sender, System.EventArgs e)
        {
            indicator.IsVisible = true;
            indicator.IsRunning = true;
            headerImage.IsVisible = false;
            attachementBaseLayout.IsVisible = false;

            string ppResponce = null;
            string guestResponce = null;

            if (_passportImage != null)
            {
                AttachmentsPayload ppPayload = new AttachmentsPayload();
                ppPayload.IXhotelId = Constants._hotel_code;
                ppPayload.IXreservaId = Constants._reservation_id;
                ppPayload.IXtype = "JPG";
                ppPayload.IXfileName = "Passport Copy";

                var inputStream = _passportImage.GetStream();

                ////Getting Stream as a Memorystream
                var pptMemoryStream = inputStream as MemoryStream;

                if (pptMemoryStream == null)
                {
                    pptMemoryStream = new MemoryStream();
                    inputStream.CopyTo(pptMemoryStream);
                }

                //Adding memorystream into a byte array
                var byteArray = pptMemoryStream.ToArray();

                //Converting byte array into Base64 string
                ppPayload.IXattach = Convert.ToBase64String(byteArray);

                ppResponce = await new PostServiceManager().SetAttachments(ppPayload);


            }

            if (_guestImage != null)
            {
                AttachmentsPayload guestPayload = new AttachmentsPayload();
                guestPayload.IXhotelId = Constants._hotel_code;
                guestPayload.IXreservaId = Constants._reservation_id;
                guestPayload.IXtype = "JPG";
                guestPayload.IXfileName = "Guest Image";

                var inputStream = _guestImage.GetStream();

                ////Getting Stream as a Memorystream
                var guestMemoryStream = inputStream as MemoryStream;

                if (guestMemoryStream == null)
                {
                    guestMemoryStream = new MemoryStream();
                    inputStream.CopyTo(guestMemoryStream);
                }

                //Adding memorystream into a byte array
                var byteArray = guestMemoryStream.ToArray();

                //Converting byte array into Base64 string
                guestPayload.IXattach = Convert.ToBase64String(byteArray);

                guestResponce = await new PostServiceManager().SetAttachments(guestPayload);
            }

            if(ppResponce == "success" || guestResponce=="success")
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Attachements have been successfully uploaded to the system", "OK");
                indicator.IsVisible = false;
                indicator.IsRunning = false;
                headerImage.IsVisible = true;
                attachementBaseLayout.IsVisible = true;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Failed", "Failed to upload attachments.", "OK");
                indicator.IsVisible = false;
                indicator.IsRunning = false;
                headerImage.IsVisible = true;
                attachementBaseLayout.IsVisible = true;
            }
        }
    }
}

