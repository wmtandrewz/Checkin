using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Newtonsoft.Json;
using System.Globalization;
using System.Linq;
using Checkin.Models.ModelClasses;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

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
				//MessagingCenter.Unsubscribe<ReservationInfo, string>(this, Constants._loadRemarksInformation);
			});

		}
		public async void RemarkDetails()
		{
			//Display Notices at the begining
			GetResNoticesList();
			
			MainGuestInformation mainGuestInformation = new MainGuestInformation();

			var resRemList = await GetResRemarksList();
			remarksList = resRemList;


			RemarkDetails remarkDetails = await mainGuestInformation.remarkInformation();
			ReservationIDText.Text = Constants._reservation_id;
			ClientNameText.Text = Constants._clientName;
           
			if (string.IsNullOrEmpty(remarkDetails.MainRemark))
			{
				remarksUnavailabilityIndicator.IsVisible = true;
			}
			else
			{
				remarksList.Insert(0, new RemarksModel("", "", "", Constants._hotel_code, Constants._reservation_id, "Main", remarkDetails.MainRemark, ""));
			}

			RemarkDetailsListView.ItemsSource = remarksList;

			stopLoading();
		}

        
		async Task<List<RemarksModel>> GetResRemarksList()
        {
			var responce = await checkInManager.GetReservationRemarksAndNotices();

            var output = JObject.Parse(responce);

			if (Enumerable.Count(output["d"]["results"][0]["reserRemarksSet"]["results"]) > 0)
            {
                
				var res = Convert.ToString(output["d"]["results"][0]["reserRemarksSet"]["results"]);

				res = res.Replace("FO", "Front Office").Replace("HK", "House Keeping");

				var resList = JsonConvert.DeserializeObject<List<RemarksModel>>(res);

				return resList;
            }
            
			return new List<RemarksModel>();
            
        }

		async void GetResNoticesList()
        {
            var responce = await checkInManager.GetReservationRemarksAndNotices();

            var output = JObject.Parse(responce);

			if (Enumerable.Count(output["d"]["results"][0]["reserNoticesSet"]["results"]) > 0)
            {

				var res = Convert.ToString(output["d"]["results"][0]["reserNoticesSet"]["results"]);
                
				var resList = JsonConvert.DeserializeObject<List<ResNoticesModel>>(res).Where(x=>x.Xaccion=="3");
                
				if(resList.FirstOrDefault()!=null)
				{
					var checkinNotice = resList.FirstOrDefault().Xobservacion;

					if (!string.IsNullOrEmpty(checkinNotice))
					{
						await DisplayAlert("Notice!", $"{checkinNotice}", "OK");
					}
				}
            }
            
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

