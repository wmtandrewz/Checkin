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
using System.Collections.ObjectModel;
using System.ComponentModel;
using Checkin.Data.Posting;
using SkiaSharp;
using Checkin.Interface;

namespace Checkin
{
	public partial class Attachments : ContentPage
	{
		//Data Source

		userLogout userLogout = new userLogout();
        MediaFile _passportImage = null;

		public Attachments()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

			ReservationIDText.Text = Constants._reservation_id;
			ClientNameText.Text = Constants._clientName;

            //Show Loading Message
            pageLoading();
            

        }

        /*
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
                PhotoSize = PhotoSize.Small
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
        */
                    
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
                attachmentListView.IsVisible = false;

                var responce = await new CheckInManager().GetAttachments("JPG");
                var output = JObject.Parse(responce);

                List<ReservationAttachment> AttachmentList = new List<ReservationAttachment>();
                List<AttachmentsModel> ImagesList = new List<AttachmentsModel>();

                ItemList itemList;

                if (Enumerable.Any(output["d"]["results"]))
                {
                    var result = Convert.ToString(output["d"]["results"]);

                    AttachmentList = JsonConvert.DeserializeObject<List<ReservationAttachment>>(result);
                }

                int i = 1;

                foreach (var item in AttachmentList)
                {
                    if (item.AttachmentName.Contains("Passport"))
                    {
                        byte[] data = Convert.FromBase64String(item.AttachmentString);
                        var source = ImageSource.FromStream(() => new MemoryStream(data));

                        ImagesList.Add(new AttachmentsModel {Sequence = i,Source = source });

                        i++;
                    }
                    
                }

                if(ImagesList!=null)
                {
                    itemList = new ItemList(ImagesList);
                    attachmentListView.ItemsSource = itemList.Items;
                    attachmentListView.IsVisible = true;
                }

                indicator.IsVisible = false;
                indicator.IsRunning = false;
                headerImage.IsVisible = true;
                attachementBaseLayout.IsVisible = true;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                indicator.IsVisible = false;
                indicator.IsRunning = false;
                headerImage.IsVisible = true;
                attachementBaseLayout.IsVisible = true;
            }

            MessagingCenter.Unsubscribe<GuestEdit>(this, "Passport");
        }

        /*
        void ClearPPClicked(object sender, System.EventArgs e)
		{
			passportImage.Source = "camera.png";
		}
        */

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

        void ShowAttachmentButton_Clicked(object sender, System.EventArgs e)
        {
            DisplayAttachments();
        }


        public class ItemList : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            public ObservableCollection<AttachmentsModel> _items;

            public ObservableCollection<AttachmentsModel> Items
            {
                get { return _items; }
                set
                {
                    _items = value;
                    OnPropertyChanged("Items");
                }
            }

            protected virtual void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged == null)
                    return;
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

            public ItemList(List<AttachmentsModel> itemList)
            {
                Items = new ObservableCollection<AttachmentsModel>();
                foreach (AttachmentsModel itm in itemList)
                {
                    Items.Add(itm);
                }
            }
        }

    }
}

