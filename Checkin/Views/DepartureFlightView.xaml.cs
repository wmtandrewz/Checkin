using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Linq;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Checkin.Models.ModelClasses;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Checkin.Models.ModelClasses.Payloads;

namespace Checkin
{
	public partial class DepartureFlightView : ContentPage
	{
        
		//List
		List<FlightsModel> flighList = new List<FlightsModel>();
		Label DepFlightLabel;

		public DepartureFlightView(Label depFlightText)
		{
			InitializeComponent();

			this.DepFlightLabel = depFlightText;

			GetDepFlightsList();
		}
        
		//Page Loadin
		void pageLoading()
		{
			FlightsIndicator.IsVisible = true;
			FlightsIndicator.IsRunning = true;
			FlightDetails.IsVisible = false;
		}

		//Stop page loading
		void stopLoading()
		{
			FlightsIndicator.IsVisible = false;
			FlightsIndicator.IsRunning = false;
			FlightDetails.IsVisible = true;
		}

        //get details
        
		async void GetDepFlightsList()
		{
			CheckInManager checkInManager = new CheckInManager();
			var responce = await checkInManager.GetFlightDetails("DEP");

			var output = JObject.Parse(responce);

			if (Enumerable.Count(output["d"]["results"]) > 0)
			{

				for (int i = 0; i < Enumerable.Count(output["d"]["results"]); i++)
				{
					flighList.Add(new FlightsModel(
						Convert.ToString(output["d"]["results"][i]["ArrFlight"]),
						Convert.ToString(output["d"]["results"][i]["DepFlight"]),
						Convert.ToString(output["d"]["results"][i]["Airport"]),
						Convert.ToString(output["d"]["results"][i]["AirpArrTime"]),
						GenerateDate(Convert.ToString(output["d"]["results"][i]["HotelDepTime"])),
						Convert.ToString(output["d"]["results"][i]["HotelArrTime"]),
						GenerateDate(Convert.ToString(output["d"]["results"][i]["AirpDepTime"]))
					                              
                    ));
				}
			}

			FlightsListView.ItemsSource = flighList;
		}

        async void FlightListItemSelected(object sender,SelectedItemChangedEventArgs e)
		{
			FlightsModel selectedModel = (FlightsModel)e.SelectedItem;

			var userRes = await DisplayAlert("Departure Flight", $"Do you want to change your departure flight as Flight No. {selectedModel.DepFlight}", "Yes", "No");

			if (userRes)
			{
				DepFlightLabel.TextColor = Color.Black;
				DepFlightLabel.Text = selectedModel.DepFlight;

				var depTime = GeneratePayloadDate(selectedModel.AirpDepTime);

				Constants._depFlight = selectedModel.DepFlight;
				Constants._depFlightTIme = depTime;

                PostServiceManager postServiceManager = new PostServiceManager();
                FlightPayload payload = new FlightPayload(
                    Settings.HotelCode,
                    Constants._reservation_id,
                    "500",
                    Settings.HotelCode,
                    Constants._reservation_id,
					Constants._arrFlight,// arr flight
					Constants. _arrFlightTIme, //arr time
					Constants._depFlight,//dep flight
					Constants._depFlightTIme,//dep time
                    "0000000000",
                    "",
                    "",
                    ""

                );

                var serviceRes = await postServiceManager.SetFlightDetails(payload);

				if(serviceRes == "Success")
				{
					await DisplayAlert("Success!", "Flight is set successfully", "OK");
				}
				else
				{
					await DisplayAlert("Error!", "Flight setting is failed", "OK");
				}

				Debug.WriteLine(selectedModel.DepFlight);
                
				await Navigation.PopAsync();
			}
		}

		void SearchBarTextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
		{
			FilterFlight(FlightSearchBar.Text);
		}

		private string GenerateDate(string date)
		{
			var newDate = date.Replace("PT", "").Replace("H",":").Replace("M",":").Replace("S","");
			return newDate;
		}

		public void FilterFlight(String filter)
        {
			if (!String.IsNullOrWhiteSpace(filter))
			{
				FlightsListView.ItemsSource = flighList.Where(x => x.DepFlight.ToLower().Contains(filter.ToLower()));
			}
        }

		private string GeneratePayloadDate(string date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                var splited = date.Split(':');
                string payloadDate = $"PT{splited[0]}H{splited[1]}M{splited[2]}S";
                return payloadDate;
            }
            else
            {
                return "PT00H00M00S";
            }
        }

	}
}


