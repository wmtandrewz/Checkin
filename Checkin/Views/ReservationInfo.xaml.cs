using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Threading.Tasks;


namespace Checkin
{
	public partial class ReservationInfo : ContentPage
	{
		//Data Source
		CheckInManager checkInManager = new CheckInManager();
		//private static ReservationInformation reservationInforamtion = new ReservationInformation();
		RoomStatus roomStatus = new Checkin.RoomStatus();


		userLogout userLogout = new userLogout();
		MainGuestInformation mainGuestInformation = new MainGuestInformation();

		public ReservationInfo()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			//Room Assigned
			MessagingCenter.Subscribe<Rooms, string>(this, Constants._roomAssigned, (sender, arg) =>
			{
				roomStatusDetails();
				//MessagingCenter.Unsubscribe<Rooms, string>(this, Constants._roomAssigned);
			});

			//If not connected refresh
			MessagingCenter.Subscribe<MainGuestInformation, string>(this, "nodetailsAvailable", (sender, arg) =>
			{
				notAvailableOnLoad();
				//MessagingCenter.Unsubscribe<MainGuestInformation, string>(this, "nodetailsAvailable");
			});
			pageLoading();
			this.getGuestDetails();
		}

		//Internet Not Connected On Load
		async void notAvailableOnLoad()
		{
			var reload = await DisplayAlert(Constants._headerMessage, Constants._networkerror, Constants._buttonTryAgain, Constants._buttonClose);
			if (reload)
			{
				this.getGuestDetails();
			}
		}
		async void roomStatusDetails()
		{
			try
			{
				roomStatus roomStatusResult = await roomStatus.roomStatusDetails(Constants.result.RoomNumber);

				if (roomStatusResult != null)
				{
					Constants.result.RoomStatus = roomStatusResult.RoomstatusDetail;
					Constants.result.RoomStatusColor = roomStatusResult.RoomStatusColor;
					Constants._roomStatus = roomStatusResult.RoomstatusDetail;

					RoomNoText.Text = Constants.result.RoomNumber;
					RoomNoText.TextColor = serviceDataValidation.validationColor(Constants.result.RoomNumber);

					RoomStatus.Text = Constants.result.RoomStatus;
					RoomStatus.TextColor = Constants.result.RoomStatusColor;

					RegistrationButton.IsVisible = true;
					RoomNoText.IsVisible = true;
					//AssignRoomButton.IsVisible = false;

					MessagingCenter.Send<ReservationInfo, ReservationDetails>(this, Constants._roomAssignedUpdateReservationList, Constants.result);
				}
			}
			catch
			{
				//var reload = await DisplayAlert(Constants._headerMessage, "Unable To Assign Room", Constants._buttonTryAgain, Constants._buttonClose);
				//if (reload)
				//{
				//	roomStatusDetails();
				//}
				//else {
				//	roomStatusDetails();
				//}
			}
		}

		async void getGuestDetails()
		{
			pageLoading();
			MainGuestDetails mainGuestResult = null;
			mainGuestResult = await mainGuestInformation.mainGuestInformation(Constants._reservation_id);
			if (mainGuestResult != null)
			{
				Constants.result = await mainGuestInformation.reservationInformation();
				if (Constants.resultMain == null || Constants.resultMain == "Error")
				{

				}
				else
				{
					DisplayReservationDetails();
					MessagingCenter.Send<ReservationInfo, string>(this, Constants._loadGuestInformation, "");
					MessagingCenter.Send<ReservationInfo, string>(this, Constants._loadRemarksInformation, "");
				}
			}
		}

		//Display Reservation Details
		public void DisplayReservationDetails()
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				BindingContext = Constants.result;
				if (Constants.result.RoomNumber == Constants._notAvailable)
				{
					RegistrationButton.IsVisible = false;
					//RoomNoText.IsVisible = false;
					//AssignRoomButton.IsVisible = true;
				}
				else
				{
					RegistrationButton.IsVisible = true;
					RoomNoText.IsVisible = true;
					//AssignRoomButton.IsVisible = false;
				}
				ReservationIDText.Text = Constants._reservation_id;
			});

			//Stop Page loading
			stopLoading();
		}

		//Sleep time
		public static async Task Sleep(int ms)
		{
			await Task.Delay(ms); //Mili Secconds
		}

		//Assign Room
		async void AssignRooms(object sender, EventArgs e)
		{
			Rooms rooms = new Rooms();
			await Navigation.PushAsync(rooms);
		}

		//Reservation Card
		async void ReservationCard(object sender, EventArgs e)
		{
			RegistrationCard registrationCard = new RegistrationCard();
			Constants._notAvailableSignatureAdded = false;

			await Navigation.PushAsync(registrationCard);
		}

		void pageLoading()
		{
			Device.BeginInvokeOnMainThread(() =>
						{
							ReservationsListIndicator.IsVisible = true;
							ReservationsListIndicator.IsRunning = true;
							guestInforDetails.IsVisible = false;
							//reservationInfoDetails.IsVisible = false;
							//AssignRoomButton.IsVisible = false;
							RegistrationButton.IsVisible = false;
							//headerImage.IsVisible = false;
						});
		}

		void stopLoading()
		{
			Device.BeginInvokeOnMainThread(() =>
						{
							ReservationsListIndicator.IsVisible = false;
							ReservationsListIndicator.IsRunning = false;
							guestInforDetails.IsVisible = true;
							//reservationInfoDetails.IsVisible = true;
							//headerImage.IsVisible = true;
						});
		}
		protected override bool OnBackButtonPressed()
		{
			return true;
		}

		void HomeClicked(object sender, EventArgs e)
		{
			new HomeNavigater().GoHome();
		}

		//on page load
		protected override async void OnAppearing()
		{
			double scale = this.Scale;
			RegistrationButton.Scale = (scale * 90 / 100); ;
			AssignRoomButton.Scale = (scale);
			AssignRoomButton.Scale = (scale * 60 / 100);

			base.OnAppearing();
		}

		//Logout button clicked
		async void LogoutButtonClickedEvt(object sender, EventArgs e)
		{
			userLogout.logout();
			//Navigate to Login Pag
		}
	}
}

