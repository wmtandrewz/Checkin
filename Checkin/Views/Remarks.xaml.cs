using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Newtonsoft.Json;
using System.Globalization;
using System.Linq;
using Checkin.Models.ModelClasses;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Rg.Plugins.Popup.Services;
using Checkin.Views;
using Checkin.Models.ModelClasses.Payloads;
using Rg.Plugins.Popup.Extensions;

namespace Checkin
{
	public partial class Remarks : ContentPage
	{
		//Data Source

		userLogout userLogout = new userLogout();

		CheckInManager checkInManager = new CheckInManager();

		List<RemarksModel> remarksList = new List<RemarksModel>();

		public Remarks()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

			//Show Loading Message

			pageLoading();

			MessagingCenter.Subscribe<ReservationInfo, string>(this, Constants._loadRemarksInformation, (sender, arg) =>
			{
				//LoadRemarks
				this.RemarkDetails();
				MessagingCenter.Unsubscribe<ReservationInfo, string>(this, Constants._loadRemarksInformation);
            });


            //Display Notices at the begining
            GetResNoticesList();

		}
		public async void RemarkDetails()
		{
			
			MainGuestInformation mainGuestInformation = new MainGuestInformation();

			var resRemList = await GetResRemarksList();
			remarksList = resRemList;


			RemarkDetails remarkDetails = await mainGuestInformation.remarkInformation();
			ReservationIDText.Text = Constants._reservation_id;
			ClientNameText.Text = Constants._clientName;
           
			if (string.IsNullOrEmpty(remarkDetails.MainRemark))
			{
				//remarksUnavailabilityIndicator.IsVisible = true;
			}
			else
			{
				remarksList.Insert(0, new RemarksModel("", "", "", Constants._hotel_code, Constants._reservation_id, "Main", remarkDetails.MainRemark, ""));
			}

			if(resRemList.Count <= 0)
			{
				remarksUnavailabilityIndicator.IsVisible = true;
				AddNreRemark.HorizontalOptions = LayoutOptions.Center;
				AddNreRemark.VerticalOptions = LayoutOptions.Center;
			}

			RemarkDetailsListView.ItemsSource = remarksList;
            
			stopLoading();
		}

        
		async Task<List<RemarksModel>> GetResRemarksList()
        {
			try
            {
                var responce = await checkInManager.GetReservationRemarksAndNotices();

                var output = JObject.Parse(responce);

                if (Enumerable.Count(output["d"]["results"][0]["reserRemarksSet"]["results"]) > 0)
                {

                    var res = Convert.ToString(output["d"]["results"][0]["reserRemarksSet"]["results"]);

                    res = res.Replace("FO", "Front Office")
                             .Replace("HK", "House Keeping")
                             .Replace("POS", "Food & Beverage")
                             .Replace("BILLING", "Billing")
                             .Replace("CONC","Concierge")
                             .Replace("ALG","Allergies")
                             .Replace("C/O","Care of");

                    var resList = JsonConvert.DeserializeObject<List<RemarksModel>>(res);

                    return resList;
                }

                return new List<RemarksModel>();
            }
            catch(Exception)
            {
                Debug.WriteLine("Remarks GET error");
                return new List<RemarksModel>();
            }
            
        }

		async void GetResNoticesList()
        {
            try
            {
                var responce = await checkInManager.GetReservationRemarksAndNotices();

                var output = JObject.Parse(responce);

                if (Enumerable.Count(output["d"]["results"][0]["reserNoticesSet"]["results"]) > 0)
                {

                    var res = Convert.ToString(output["d"]["results"][0]["reserNoticesSet"]["results"]);

                    var resList = JsonConvert.DeserializeObject<List<ResNoticesModel>>(res).Where(x => x.Xaccion == "3");

                    if (resList.FirstOrDefault() != null)
                    {
                        var checkinNotice = resList.FirstOrDefault().Xobservacion;

                        if (!string.IsNullOrEmpty(checkinNotice))
                        {
                            //await DisplayAlert("Notice!", $"{checkinNotice}", "OK");

                            await Navigation.PushPopupAsync(new PopupAlert("Notice for Reception!", $"{checkinNotice}", "OK"));
                        }
                    }
                }
            }
            catch(Exception)
            {
                Debug.WriteLine("Notices GET error");
            }

        }

        async void RemarkItemSelected(object sen, SelectedItemChangedEventArgs e)
		{
			RemarksModel remarksModel = (RemarksModel)e.SelectedItem;

			if (remarksModel.XtipoObserv != "Main")
			{
				string XtipoObservType = "";

				switch (remarksModel.XtipoObserv)
				{
					case "Front Office":
						XtipoObservType = "FO";
						break;

					case "House Keeping":
						XtipoObservType = "HK";
						break;

					case "Food & Beverage":
						XtipoObservType = "POS";
						break;

					case "Billing":
						XtipoObservType = "BILLING";
						break;

                    case "Concierge":
                        XtipoObservType = "CONC";
                        break;

                    case "Care of":
                        XtipoObservType = "C/O";
                        break;
                }

				await Navigation.PushPopupAsync(new PopupInputView(remarksModel.Xobservacion));

				MessagingCenter.Subscribe<PopupInputView, string>(this, "popup", (sender, arg) =>
				{
					Debug.WriteLine(arg);

					RemarkDetailsLayout.IsVisible = false;
					pageLoading();
                    
					UpdateRemarks(XtipoObservType, arg);
				});

				MessagingCenter.Subscribe<PopupInputView>(this,"cancel", (obj) => 
				{
					MessagingCenter.Unsubscribe<PopupInputView>(this, "cancel");

					this.RemarkDetails();

				});
    
			}
            
		}

		async void UpdateRemarks(string obserAc, string arg)
		{
            if (!string.IsNullOrEmpty(obserAc))
            {
                MessagingCenter.Unsubscribe<PopupInputView, string>(this, "popup");

                RemarksPayload remarksPayload = new RemarksPayload(Settings.HotelCode, Constants._reservation_id, "", Settings.HotelCode, Constants._reservation_id, obserAc, arg);

                var res = await new PostServiceManager().SetReservationRemarks(remarksPayload);

                if (res == "Success")
                {
                    RemarkDetailsLayout.IsVisible = true;
                    this.RemarkDetails();
                }
                else
                {
                    RemarkDetailsLayout.IsVisible = true;
                    stopLoading();
                }
			}
            else
            {
                await DisplayAlert("Unauthorized!", "Unable to update the selected remark via Application", "OK");
                RemarkDetailsLayout.IsVisible = true;
                stopLoading();
            }


        }

		async void AddNewRemark(string [] values)
        {
			MessagingCenter.Unsubscribe<RemarkInputView, string[]>(this, "remark");

			RemarksPayload remarksPayload = new RemarksPayload(Settings.HotelCode, Constants._reservation_id, "", Settings.HotelCode, Constants._reservation_id, values[1], values[0]);

            var res = await new PostServiceManager().SetReservationRemarks(remarksPayload);

            if (res == "Success")
            {
				RemarkDetailsLayout.IsVisible = true;
                RemarkDetailsListView.HeightRequest = RemarkDetailsListView.Height + 100;
                this.RemarkDetails();
            }
			else
			{
				RemarkDetailsLayout.IsVisible = true;
                stopLoading();
			}

            
        }

		async void NewRemarkClicked(object sender, EventArgs e)
		{
			MessagingCenter.Subscribe<RemarkInputView, string []>(this, "remark", (senderRem, arg) =>
            {
                RemarkDetailsLayout.IsVisible = false;
                pageLoading();

				AddNewRemark(arg);
            });

			await Navigation.PushPopupAsync(new NewRemarkPopup());
		}

		//Page Loading

		void pageLoading()
		{
			
			Device.BeginInvokeOnMainThread(() =>
						{
							remarksIndicator.IsVisible = true;
							remarksIndicator.IsRunning = true;
							remarksUnavailabilityIndicator.IsVisible = false;
							headerImage.IsVisible = false;
						});
		}

		void stopLoading()

		{
			remarksIndicator.IsVisible = false;
			remarksIndicator.IsRunning = false;
			headerImage.IsVisible = true;
            RemarksBaseLayout.VerticalOptions = LayoutOptions.Start;

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
	}
}

