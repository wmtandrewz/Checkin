using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Plugin.Connectivity;

using checkin;
using Checkin.Models.ModelClasses;

namespace Checkin
{
	public partial class ReservationsList : ContentPage
	{
		void Handle_DateSelected(object sender, Xamarin.Forms.DateChangedEventArgs e)
		{
			ReservationsSearchBar.Text = reservationDatePicker.Date.ToString("dd-MM-yyyy");
		}
		void searchDate(object sender, Xamarin.Forms.DateChangedEventArgs e)
		{
			pageLoading();
			detailsThroughSearchBarValue();
		}

		userLogout userLogout = new userLogout();
		//Navigation Drawer Tabbed Page
		public Action<TabbedPage> selectedItemPending
		{
			get;
			set;
		}
		//Navigation Drawer Content Page

		public Action<ContentPage> selectedItemCheckedIn
		{
			get;
			set;
		}

		//List Collection
		List<ReservationHeader> reservationHeader = new List<ReservationHeader>();

		//Data Source
		CheckInManager checkInManager = new CheckInManager();

		//Creating obervable collection
		ItemList items;

		public ReservationsList()
		{
			InitializeComponent();

			//Refresh page on chekin
			MessagingCenter.Subscribe<CheckInManager, string>(this, "ErrorRefreshingToken", (sender, arg) =>
		   {
			   userLogout.logout();
			   Navigation.PushModalAsync(new LoginPage());
			   DependencyService.Get<DisplayAlert>().DisplayAlert(Constants._headerConnectionLost, Constants._TokenRefreshError, Constants._buttonOkay);
		   });

			MessagingCenter.Subscribe<RegistrationCard, string>(this, Constants._reservationStatusCheckedIn, (sender, arg) =>
			{
				//pageLoading();
				var obj = reservationHeader.FirstOrDefault(x => x.ReservationID == Constants._reservation_id);
				obj.Status = Constants._reservationStatusCheckedIn;
				obj.StatusColor = Color.Green;
				obj.reservationImage = "CheckedIN.png";

				Device.BeginInvokeOnMainThread(() =>
					{
						ReservationsListView.ItemsSource = null;
						items = new ItemList(reservationHeader);
						ReservationsListView.ItemsSource = items.Items;
					});
				//hideLoading();
				//detailsThroughSearchBarValue();
				//MessagingCenter.Unsubscribe<RegistrationCard, string>(this, Constants._reservationStatusCheckedIn);
			});

			//guestRoomUpdated
			MessagingCenter.Subscribe<ReservationInfo, ReservationDetails>(this, Constants._roomAssignedUpdateReservationList, (sender, arg) =>
			{
				var obj = reservationHeader.FirstOrDefault(x => x.ReservationID == Constants._reservation_id);
				obj.roomNumber = arg.RoomNumber;
				obj.roomStatusImageText = serviceDataValidation.roomImageValidationReservationList(arg.RoomStatus);



				Device.BeginInvokeOnMainThread(() =>
					{
						ReservationsListView.ItemsSource = null;
						items = new ItemList(reservationHeader);
						ReservationsListView.ItemsSource = items.Items;
					});
				//MessagingCenter.Unsubscribe<ReservationInfo, string>(this, Constants._roomAssignedUpdateReservationList);
			});

			;
			pageLoading();
			this.DisplayList();
		}

		//Display Reservations List

		async void DisplayList()
		{
			try
			{

				//Retrieving Hotel Name and Hotel Date
				string result2 = await checkInManager.HotelDateAsync();
				if (result2.Contains("Enter your user ID in the format"))
				{
					await DisplayAlert(Constants._headerMessage, Constants._unableToGetHotelDate, Constants._buttonOkay);

				}
				else {
					if (result2 != null)
					{
						var jObj = JObject.Parse(result2);

						//Setting Hotel Nam
						Constants._hotel_name = jObj["d"]["results"][0]["Xnombre"].ToString();

						string temporary_date_holder = jObj["d"]["results"][0]["XfechaHotel"].ToString();
						temporary_date_holder = temporary_date_holder.Split('(', ')')[0];

						//Converting retrieved date to date time forma
						DateTime dt = DateTime.Parse(temporary_date_holder);

						//Setting Hotel Dat
						Constants._hotel_date = dt.ToString("dd-MM-yyyy");

						//Set Hotel Name and Dat
						ReservationsHotelName.Text = Constants._hotel_name + " (" + Constants._hotel_code + ")";
						ReservationsHotelDate.Text = "Date : " + Constants._hotel_date;


						UserName.Text = "User: " + Settings.Username.Replace("@jkintranet.com", "");


						//Retreiving reservation lis
						this.RetrieveReservationList(Constants._hotel_date);


					}
					else
					{
						var reload = await DisplayAlert(Constants._headerMessage, Constants._networkerror, Constants._buttonTryAgain, Constants._buttonClose);
						if (reload)
						{
							this.DisplayList();
						}
					}
				}
			}
			catch (Exception)
			{

				var reload = await DisplayAlert(Constants._headerMessage, Constants._networkerror, Constants._buttonTryAgain, Constants._buttonClose);
				if (reload)
				{
					this.DisplayList();
				}
			}
		}

		//Reservation Status
		String temporary_status = "";

		//Binding all reservations into the Reservations Listview
		public async void BindDataToListView(String result)
		{
			try
			{
				reservationHeader = new List<ReservationHeader>();

				//Clear the ListView
				ReservationsListView.ItemsSource = null;
				if (result.Contains("Enter your user ID in the format"))
				{
					await DisplayAlert(Constants._headerMessage, Constants._unableToGetHotelReservations, Constants._buttonOkay);
				}
				else {
					if (result != null)
					{
						//Solving Date Ticks erro
						result = result.Replace("Date(-", "Date(");
						var output = JObject.Parse(result);

						int cellColourCount = 1;

						//Setting initial colo
						Color temporary_status_color = Color.Gray;
						Color cellColour;
						Color textColor;
						string reservationImage = "";
						string roomStatusImage = "";
						string numberOfVisits = "";
						string totalNumberOfVisits = "";

						//Looping all Dat
						for (int i = 0; i < Enumerable.Count(output["d"]["results"]); i++)
						{
							//Checking reservation availabilit
							if (Enumerable.Count(output["d"]["results"][i]["ReservationNaviSapGuests"]["results"]) > 0)
							{
								numberOfVisits = Convert.ToString(output["d"]["results"][i]["ReservationNaviSapGuests"]["results"][0]["Visitperhotel"]).TrimStart(new Char[] { '0' }).Trim();
								totalNumberOfVisits = Convert.ToString(output["d"]["results"][i]["ReservationNaviSapGuests"]["results"][0]["Totalvisit"]).TrimStart(new Char[] { '0' }).Trim();
								reservationImage = reservationNaviAndNaviSapGuestDetails(output, cellColourCount, out temporary_status_color, out cellColour, out textColor, reservationImage, i);
                                if (temporary_status != Constants._reservationStatusCancelled)
                                {
                                    reservationHeader.Add(new ReservationHeader(
                                                                Convert.ToString(output["d"]["results"][i]["XreservaId"]).TrimStart(new Char[] { '0' }),
                                                                Convert.ToString(output["d"]["results"][i]["ReservationNaviSapGuests"]["results"][0]["McName1"]),
                                                                Convert.ToString(output["d"]["results"][i]["Xclientname"]),
                                                                temporary_status,
                                                                temporary_status_color,
                                                                cellColour,
                                                                textColor,
                                                                reservationImage,
                                                                serviceDataValidation.roomNumberValidation(Convert.ToString(output["d"]["results"][i]["XhabitacionId"])),
                                                                serviceDataValidation.roomImageValidation(Convert.ToString(output["d"]["results"][i]["XstdoLimpia"])),
                                                                numberOfVisits,
                                                                totalNumberOfVisits
                                                            ));
								}
							}
							else if (Enumerable.Count(output["d"]["results"][i]["ReservationNaviGuest"]["results"]) > 0)
							{
								numberOfVisits = "0";
								totalNumberOfVisits = "0";
								reservationImage = reservationNaviAndNaviSapGuestDetails(output, cellColourCount, out temporary_status_color, out cellColour, out textColor, reservationImage, i);

                                if (temporary_status != Constants._reservationStatusCancelled)
                                {
                                    reservationHeader.Add(new ReservationHeader(
                                                                Convert.ToString(output["d"]["results"][i]["XreservaId"]).TrimStart(new Char[] { '0' }),
                                                                Convert.ToString(output["d"]["results"][i]["ReservationNaviGuest"]["results"][0]["Xnombre"]),
                                                                Convert.ToString(output["d"]["results"][i]["Xclientname"]),
                                                                temporary_status,
                                                                temporary_status_color,
                                                                cellColour,
                                                                textColor,
                                                                reservationImage,
                                                                serviceDataValidation.roomNumberValidation(Convert.ToString(output["d"]["results"][i]["XhabitacionId"])),
                                                                serviceDataValidation.roomImageValidation(Convert.ToString(output["d"]["results"][i]["XstdoLimpia"])),
                                                                numberOfVisits,
                                                                totalNumberOfVisits
                                                            ));
								}
							}
							cellColourCount = cellColourCount + 1;
						}

						//Bind the Data to ListVie
						Device.BeginInvokeOnMainThread(() =>
						{

							items = new ItemList(reservationHeader);
							ReservationsListView.ItemsSource = items.Items;

							ReservationsListViewNoItems.IsVisible = false;
							ListViewManualRefreshIcon.IsVisible = false;
							ReservationsListView.IsVisible = true;

							ChartModel.resCount = reservationHeader.Count;
							ChartModel.checkedinCount = reservationHeader.Count(x => x.Status == "Checked-in");
							ChartModel.pendingCount = reservationHeader.Count(x => x.Status == "Pending");

							MessagingCenter.Send<ReservationsList>(this, "chart");
								

						});
						hideLoading();
					}
					else {

						//Reservation details not availabl
						ReservationsListViewNoItems.IsVisible = true;
						ListViewManualRefreshIcon.IsVisible = true;
						hideReservationList();
						hideLoading();
					}
				}


				//Hide Loading Message
				Device.BeginInvokeOnMainThread(() =>
				{
					hideLoading();
				});

				//Finish Refreshing
				if (ReservationsListView.IsRefreshing)
				{
					ReservationsListView.EndRefresh();
				}
			}
			catch (Exception e)
			{
				ReservationsListViewNoItems.IsVisible = true;
				ListViewManualRefreshIcon.IsVisible = true;
				hideReservationList();
				hideLoading();

				//Finish Refreshing
				if (ReservationsListView.IsRefreshing)
				{
					ReservationsListView.EndRefresh();
				}
			}
		}

		string reservationNaviAndNaviSapGuestDetails(JObject output, int cellColourCount, out Color temporary_status_color, out Color cellColour, out Color textColor, string reservationImage, int i)
		{
			if (Convert.ToString(output["d"]["results"][i]["XcheckIn"]) == "X")
			{
				temporary_status = Constants._reservationStatusCheckedIn;
				temporary_status_color = Color.Green;
				reservationImage = "CheckedIN.png";
			}
			//Reservation Cancelled
			else if (Convert.ToString(output["d"]["results"][i]["Xcancelada"]) == "X")
			{
				temporary_status = Constants._reservationStatusCancelled;
				temporary_status_color = Color.FromHex("FF8000");
				reservationImage = "";
			}
			//Reservation Pending
			else {
				temporary_status = Constants._reservationStatusPending;
				temporary_status_color = Color.Red;
				reservationImage = "Pending.png";
			}
			if (IsOdd(cellColourCount))
			{
				cellColour = Color.FromHex("f1e8ff");
				textColor = Color.FromHex("443266");
			}
			else
			{
				cellColour = Color.FromHex("e3d6f9");
				textColor = Color.FromHex("443266");
			}

			return reservationImage;
		}

		//======================================= End of Display Reservation Details =============================================

		//======================================= Start of Search/Refresh List =====================================================

		void ReservationsSearchBarTextChanged(object sender, EventArgs e)
		{
			//Filter Reservations List
			FilterReservations(ReservationsSearchBar.Text);
		}

		public void FilterReservations(String filter)
		{
			if (String.IsNullOrWhiteSpace(filter))
			{
				ReservationsListView.ItemsSource = reservationHeader;
			}
			else {
				ReservationsListView.ItemsSource = reservationHeader
					.Where(x => x.ReservationID.ToLower()
					   .Contains(filter.ToLower()) || x.MainClientName.ToLower()
					   .Contains(filter.ToLower()) || x.GuestName.ToLower()
					   .Contains(filter.ToLower()) || x.Status.ToLower()
					   .Contains(filter.ToLower()) || x.roomNumber.ToLower()
					   .Contains(filter.ToLower()));
			}
		}

		//Search button clicked
		void ReservationsSearchBarButtonPressed(object sender, EventArgs e)
		{
			pageLoading();
			detailsThroughSearchBarValue();
		}

		void detailsThroughSearchBarValue()
		{
			var formats = new[] { "dd-MM-yyyy" };
			var formats1 = new[] { "MM-dd-yyyy" };
			//Search Reservations
			ReservationsListViewNoItems.IsVisible = false;
			ListViewManualRefreshIcon.IsVisible = false;
			DateTime dDate;
			if (string.IsNullOrEmpty(ReservationsSearchBar.Text) == false)
			{

				if (DateTime.TryParseExact(ReservationsSearchBar.Text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dDate))
				{
					//var formats = new[] { "dd-MM-yyyy" };
					//Search for Reservation Arrival Date
					if (DateTime.TryParseExact(ReservationsSearchBar.Text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dDate))
					{

						RetrieveReservationList(ReservationsSearchBar.Text);
						//Set Hotel Name and Date
						ReservationsHotelDate.Text = "Searched Date : " + ReservationsSearchBar.Text;
					}
					else {
						// do for invalid date
						DisplayAlert(Constants._headerMessage, Constants._dateFormat, Constants._buttonOkay);
					}
				}
				else if (DateTime.TryParseExact(ReservationsSearchBar.Text, formats1, CultureInfo.InvariantCulture, DateTimeStyles.None, out dDate))
				{
					DisplayAlert(Constants._headerMessage, Constants._dateFormat, Constants._buttonOkay);
					hideLoading();
				}

				else {
					//Search for Reservation ID
					if (Regex.IsMatch(ReservationsSearchBar.Text, "^[0-9]+$", RegexOptions.None))
					{
						if (ReservationsSearchBar.Text.Length > 4)
						{
							this.RetrieveReservationDetailsBasedonResID(ReservationsSearchBar.Text);
							ReservationsHotelDate.Text = "Searched Reservation ID : " + ReservationsSearchBar.Text;
						}
						else
						{
							this.RetrieveReservationDetailsBasedonResRoomNumber(ReservationsSearchBar.Text);
							ReservationsHotelDate.Text = "Searched Room Number : " + ReservationsSearchBar.Text;
						}
					}
				}
			}
			else {


				this.DisplayList();
			}
			hideLoading();
		}

		//Search by Reservation ID
		public async void RetrieveReservationDetailsBasedonResID(string reservationID)
		{
			//Read Reservations Data
			String result = await checkInManager.GetReservationsDetails(reservationID);
			this.BindDataToListView(result);
		}
		//Search by RoomNumber

		public async void RetrieveReservationDetailsBasedonResRoomNumber(string roomNumber)
		{
			//Read Reservations Data
			String result = await checkInManager.GetReservationDetailsRoomNumber(roomNumber);
			this.BindDataToListView(result);
		}

		//Search by Arrival Date
		public async void RetrieveReservationList(String hotel_date)
		{
			//Check internet connection

			if (CrossConnectivity.Current.IsConnected)
			{
				//Read Reservations Data
				String result = await checkInManager.GetReservationsListAsync(hotel_date);
				this.BindDataToListView(result);
			}
			else
			{
				await DisplayAlert(Constants._headerMessage, Constants._networkerror, Constants._buttonTryAgain);
			}

		}
		void ReservationsListViewRefresh(object sender, EventArgs e)
		{
			//Refresh Reservations List
			this.detailsThroughSearchBarValue();
		}

		void RefreshButtonTappedEvt(object sender, EventArgs e)
		{
			//Refresh Reservations List
			pageLoading();
			this.detailsThroughSearchBarValue();
		}

		//Reservation List Selected
		void ReservationsListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
		{

			ReservationHeader reservationHeaderObject = (ReservationHeader)e.SelectedItem;

			//Clearing Constant values
			Constants.result = null;
			Constants._notAvailableSignatureAdded = false;

			//set usage start time
			Constants._checkinStartTime = DateTime.Now.ToString();
          

			//Set reservation id
			Constants._reservation_id = reservationHeaderObject.ReservationID;

			//Set reservation status
			Constants._reservationStatus = reservationHeaderObject.Status;

			//Reseration not cancelled
			if (reservationHeaderObject.Status == Constants._reservationStatusCheckedIn || reservationHeaderObject.Status == Constants._reservationStatusPending)
			{
				if (reservationHeaderObject.Status == Constants._reservationStatusCheckedIn)
				{
					//navigate to registration card
					selectedItemCheckedIn(new RegistrationCard());
				}
				else {

					//navigate to tabs page
					selectedItemPending(new TabsPage());
				}
			}
		}

		//Page loading Indicator
		void pageLoading()
		{
			//Show Loading Message
			Device.BeginInvokeOnMainThread(() =>
			{
				ReservationsListIndicator.IsVisible = true;
				ReservationsListIndicator.IsRunning = true;
				ReservationsListView.IsVisible = false;
			});
		}

		//Hide page loading Indicator
		void hideLoading()
		{
			ReservationsListIndicator.IsVisible = false;
			ReservationsListIndicator.IsRunning = false;
		}

		//Hide Reservation List
		void hideReservationList()
		{
			ReservationsListView.IsVisible = false;
		}
		public class ItemList : INotifyPropertyChanged
		{
			public event PropertyChangedEventHandler PropertyChanged;

			public ObservableCollection<ReservationHeader> _items;

			public ObservableCollection<ReservationHeader> Items
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

			public ItemList(List<ReservationHeader> itemList)
			{
				Items = new ObservableCollection<ReservationHeader>();
				foreach (ReservationHeader itm in itemList)
				{
					Items.Add(itm);
				}
			}
		}

		//Odd Number
		public static bool IsOdd(int value)
		{
			return value % 2 != 0;
		}

		protected override bool OnBackButtonPressed()
		{
			return true;
		}

		async void OpenCalender(object sender, EventArgs e)
		{
			Device.BeginInvokeOnMainThread(() =>
					{
						if (reservationDatePicker.IsFocused)
						{
							reservationDatePicker.Unfocus();
						}
						reservationDatePicker.Focus();
					});


		}
	}
}
