using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace Checkin
{
	public partial class Performa : ContentPage
	{

        CheckInManager checkinManager = new CheckInManager();

		PerformaInformation performaInformaion = new PerformaInformation();
		string emailAddress = "";
		public Performa(string reservationID, string mainGuestEmail)
		{
			InitializeComponent();
			PerformaIndicator.IsRunning = true;
			PerformaIndicator.IsVisible = true;
			PerformaLayout.IsVisible = false;

			emailAddress = mainGuestEmail;
			performaDetails(reservationID);

			MessagingCenter.Subscribe<PerformaInformation, string>(this, Constants._proformaGeneratError, async (sender, arg) =>
			{
				var output = JObject.Parse(arg);
				try
				{
					string errormessage = Convert.ToString(output["error"]["innererror"]["errordetails"][1]["message"]);

					var reload = await DisplayAlert(Constants._headerMessageProforma, errormessage, Constants._buttonOkay, Constants._buttonClose);
					if (reload)
					{
						this.Navigation.RemovePage(this);
					}
					else {
						this.Navigation.RemovePage(this);
					}
				}
				catch (Exception)
				{
					var reload = await DisplayAlert(Constants._headerMessageProforma, "Error Generating Proforma from service", Constants._buttonOkay, Constants._buttonClose);
					if (reload)
					{
						this.Navigation.RemovePage(this);
					}
					else {
						this.Navigation.RemovePage(this);
					}
				}

			});
			MessagingCenter.Subscribe<PerformaInformation, int>(this, Constants._performaListHeight, (sender, arg) =>
			{
				performaItemListView.HeightRequest = arg;
			});
		}
		async void performaDetails(string reservationID)
		{
			PerformaDetails result = await performaInformaion.performaInfo(reservationID);
			//advancedValues
            BindingContext = result;

			if (result != null)
			{

				if (result.advancedReceivedNegative != "" || result.advancedReceivedPositive != "")
				{
					advancedValues.IsVisible = true;
				}
				else
				{
					advancedValues.IsVisible = false;
				}
				performaItemDetails.ItemsSource = null;
                var res = performaInformaion.performaItemInformation();
                performaItemDetails.ItemsSource = res;

                PerformaItemDetails details = res.FirstOrDefault();
                if (details!=null)
                {
                    roomTypeLabel.Text = details.roomType;
                    mealPlanLabel.Text = details.mealPlan;
                    occuLabel.Text = details.occu;
                    nosLabel.Text = details.nos;
                    roomNightsLabel.Text = details.roomNights;
                    rateLabel.Text = details.rate;
                    currLabel.Text = details.currency;
                }

			}
			PerformaIndicator.IsRunning = false;
			PerformaIndicator.IsVisible = false;
			PerformaLayout.IsVisible = true;

		}
		async void sendPerforma(object sender, EventArgs e)
		{
			sendPerformaButton.IsEnabled = false;
			if (emailAddress == Constants._notAvailable)
			{

				var popup = new EntryPopup("Please Enter Email Address Below", string.Empty, Constants._buttonOkay, Constants._buttonClose);
				popup.PopupClosed += async (o, closedArgs) =>
				{
					if (closedArgs.Button == "Ok")
					{
						emailAddress = closedArgs.Text;
						await emailAddressENtered();
					}
					else {
						sendPerformaButton.IsEnabled = true;
					}
				};
				popup.Show();
			}
			else
			{
				string result = await checkinManager.SendPerformaEmailAvailable(Constants._reservation_id);
				var output = JObject.Parse(result);
				if (Enumerable.Count(output["d"]["results"]) == 0)
				{
					await DisplayAlert(Constants._headerMessage, Constants._emailSent, Constants._buttonOkay);
					sendPerformaButton.IsEnabled = true;
				}
				else
				{
					await DisplayAlert(Constants._headerMessage, Constants._emailNotSent, Constants._buttonOkay);
					sendPerformaButton.IsEnabled = true;
				}
			}

		}

		async System.Threading.Tasks.Task emailAddressENtered()
		{
			string result = await checkinManager.SendPerformaEmailNotAvailable(Constants._reservation_id, emailAddress);
			var output = JObject.Parse(result);
			if (Enumerable.Count(output["d"]["results"]) == 0)
			{
				await DisplayAlert(Constants._headerMessage, Constants._emailSent, Constants._buttonOkay);
				sendPerformaButton.IsEnabled = true;
			}
			else
			{
				await DisplayAlert(Constants._headerMessage, Constants._emailNotSent, Constants._buttonOkay);
				sendPerformaButton.IsEnabled = true;
			}
		}
		//on page load
		protected override async void OnAppearing()
		{
			double scale = this.Scale;
			sendPerformaButton.Scale = (scale * 90 / 100); ;

			base.OnAppearing();
		}
		async void performatListItemSelected(object sender, ItemTappedEventArgs e)
		{
			PerformaItemDetails performaItemDetails = (PerformaItemDetails)e.Item;
			string SelectedItemDetails = "StartDate :" + "" + performaItemDetails.startDate.ToString() + "\n" +
										 "End Date :" + "" + performaItemDetails.endDate.ToString() + "\n" +
																				 "Description :" + "" + performaItemDetails.descriptiopn.ToString() + "\n" +
																				 "Room Type :" + "" + performaItemDetails.roomType.ToString() + "\n" +
																				 "Meal Plan :" + "" + performaItemDetails.mealPlan.ToString() + "\n" +
																				 "Occu :" + "" + performaItemDetails.occu.ToString() + "\n" +
																				 "Nos :" + "" + performaItemDetails.nos.ToString() + "\n" +
																				 "Room Nights :" + "" + performaItemDetails.roomNights.ToString() + "\n" +
																				 "Rate :" + "" + performaItemDetails.rate.ToString() + "\n" +
																				 "Currency :" + "" + performaItemDetails.currency.ToString() + "\n" +
																				 "Amount(LKR) :" + "" + performaItemDetails.amount.ToString() + "\n";
			await DisplayAlert("Performa Items", SelectedItemDetails, Constants._buttonOkay);
		}
	}
}

