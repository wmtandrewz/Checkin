using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;
using Checkin.Models.ModelClasses;
using SkiaSharp;
using Checkin.Models.ModelClasses.Payloads;
using Rg.Plugins.Popup.Services;
using Checkin.Views;
using System.Diagnostics;

namespace Checkin
{
	public partial class HomeDefault : ContentPage
	{
		userLogout userLogout = new userLogout();


		public HomeDefault()
		{
			InitializeComponent();
			//Setting hotelcodeand hotel logo
			if (Constants._hotel_code == "3015")
			{
				CinnamonLogo.Source = "Hotel_" + Constants._hotel_code + ".jpg";
			}
			else {
				CinnamonLogo.Source = "Hotel_" + Constants._hotel_code + ".bmp";
			}

			MessagingCenter.Subscribe<ReservationsList>(this, "chart", (sender) =>
    		{
				DisplayCharts();

			});
		}

		//		Setting time befire elements load
		public static async Task Sleep(int ms)
		{
			await Task.Delay(ms);
		}

		//Logout button clicked
		 void LogoutButtonClickedEvt(object sender, EventArgs e)
		{

			userLogout.logout();

		}
		protected override bool OnBackButtonPressed()
		{
			return true;
		}

        //Charts
        
        void DisplayCharts()
		{
			List<Microcharts.Entry> checkinEntries = new List<Microcharts.Entry>
			{
				new Microcharts.Entry(ChartModel.resCount - ChartModel.pendingCount)
				{
					Color = SKColor.Parse("#0000FF")
				},

				new Microcharts.Entry(ChartModel.resCount - ChartModel.checkedinCount)
                {
					Color = SKColor.Parse("#a5c7ff")
                }
			};

			checkinChart.Chart = new Microcharts.DonutChart { Entries = checkinEntries };

			List<Microcharts.Entry> pendingEntries = new List<Microcharts.Entry>
            {
                new Microcharts.Entry(ChartModel.resCount - ChartModel.checkedinCount)
                {
					Color = SKColor.Parse("#008206")
                },

				new Microcharts.Entry(ChartModel.resCount - ChartModel.pendingCount)
                {
					Color = SKColor.Parse("#dbffdc")
                }
            };

			pendingChart.Chart = new Microcharts.DonutChart { Entries = pendingEntries };

			var resTot = ChartModel.resCount;
			var checkinTot = ChartModel.checkedinCount;
			var pendingTot = ChartModel.pendingCount;

			double checkinPercentage = ((resTot - pendingTot)*100/resTot);

			checkinTotLabel.Text = checkinPercentage.ToString();

			double pendingPercentage = ((resTot - checkinTot) * 100 / resTot);

			pendingTotLabel.Text = pendingPercentage.ToString();
		}

		protected async override void OnAppearing()
		{

			//await PopupNavigation.PushAsync(new PopupInputView());

			//MessagingCenter.Subscribe<PopupInputView, string>(this, "popup", (sender, arg) => {
			//	Debug.WriteLine(arg);
   //         });

			//ResNoticesModel resNoticesModel = new ResNoticesModel("3000", "0000027687", "", "3000", "0000027687", "3", "haaai thimizzzzzzz");
   //         //resNoticesModel.ImHotelId = "3000";
   //         //resNoticesModel.ImReservationId = "27703";
   //         //resNoticesModel.Mandt = "500";
   //         //resNoticesModel.XhotelId = "3000";
   //         //resNoticesModel.XreservaId = "27703";
   //         //resNoticesModel.Xaccion = "3";
   //         //resNoticesModel.Xobservacion = "Haai Thimz";

   //         await new PostServiceManager().SetReservationNotices(resNoticesModel);


			//RemarksPayload remarksModel = new RemarksPayload("3000","0000027687","","3000","0000027687","FO","Front Office new");

			//await new PostServiceManager().SetReservationRemarks(remarksModel);

			base.OnAppearing();
		}
	}

}

