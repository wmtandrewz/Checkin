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

            //CinnamonLogo.Source = "banners/"+ Constants._hotel_code + ".jpg";

            //if (Constants._hotel_code == "3015")
            //{
            //	CinnamonLogo.Source = "Hotel_" + Constants._hotel_code + ".jpg";
            //}
            //else {
            //	CinnamonLogo.Source = "Hotel_" + Constants._hotel_code + ".bmp";
            //}

			MessagingCenter.Subscribe<ReservationsList>(this, "chart", (sender) =>
    		{
				DisplayCharts();
                messegeBarLayout.IsVisible = true;

                hotelNameLAbel.Text = Constants._hotel_name;
                messegeLabel.Text = "You will be automatically logged-out in 15 min on idle state";
                messegeLabel.TextColor = Color.Red;
                //hotelNameLAbel.Text = "Cinnamon Hakuraa Huraa Maldives";

                Device.StartTimer(TimeSpan.FromSeconds(10),  () =>
                {
                    messegeLabel.Text = "Designed and developed by Cinnamon IT  |  All rights reserved  |  Copyrights © 2018";
                    messegeLabel.TextColor = Color.Gray;
                    return false;
                });
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

            //new PostServiceManager().SetPerformaInvoice();
            //new CheckInManager().GetPerformaInvoiceNew();

			List<Microcharts.Entry> checkinEntries = new List<Microcharts.Entry>
			{
				new Microcharts.Entry(ChartModel.resCount - ChartModel.pendingCount)
				{
                    Color = SKColor.Parse("#211261")
				},

				new Microcharts.Entry(ChartModel.resCount - ChartModel.checkedinCount)
                {
                    Color = SKColor.Parse("#008FBE")
                }
			};

			checkinChart.Chart = new Microcharts.DonutChart { Entries = checkinEntries };

			var resTot = ChartModel.resCount;
			var checkinTot = ChartModel.checkedinCount;
			var pendingTot = ChartModel.pendingCount;

			double checkinPercentage = ((resTot - pendingTot)*100/resTot);

			checkinTotLabel.Text = checkinPercentage.ToString()+"%";

			double pendingPercentage = ((resTot - checkinTot) * 100 / resTot);

            checkedinCountLabel.Text = " - " + checkinTot.ToString();
            expArrCountLabel.Text = " - " + pendingTot.ToString();

			statLayout.IsVisible = true;

		}


	}

}

