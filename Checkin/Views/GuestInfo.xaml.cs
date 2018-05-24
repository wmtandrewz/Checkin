using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.ComponentModel;
using System.Collections.ObjectModel;


namespace Checkin
{
	public partial class GuestInfo : ContentPage
	{
		
		//Data SourceCheckInManager checkInManager = new CheckInManager();

		//List Collection
		List<guestDetails> guestdetails = new List<guestDetails>();

		//Creating obervable collection
		//ItemList items;

		userLogout userLogout = new userLogout();

		public GuestInfo()
		{
			InitializeComponent();
			startLoading();
			//Enable navigation back button
			NavigationPage.SetHasNavigationBar(this, false);

			//refresh page on guest edit
			MessagingCenter.Subscribe<GuestEdit, List<guestDetails>>(this, Constants._guestEdited, (sender, arg) =>
			{
				GuestListView.ItemsSource = null;
				guestdetails = null;
				guestdetails = arg;
				GuestListView.ItemsSource = guestdetails;
				//MessagingCenter.Unsubscribe<GuestEdit, List<guestDetails>>(this, Constants._guestEdited);
			});

			MessagingCenter.Subscribe<ReservationInfo, string>(this, Constants._loadGuestInformation, (sender, arg) =>
			{
				startLoading();
				DisplayGuestDetails();
				//MessagingCenter.Unsubscribe<ReservationInfo, string>(this, Constants._loadGuestInformation);
			});
		}


		//Display Guest Details
		public async void DisplayGuestDetails()
		{
			MainGuestInformation mainGuestInformation = new MainGuestInformation();
			List<guestDetails> guesInformation = await mainGuestInformation.guestInformation();

			Device.BeginInvokeOnMainThread(() =>
			{
				ReservationIDText.Text = Constants._reservation_id;
				ClientNameText.Text = Constants._clientName;
				if (guesInformation != null)
				{
					GuestListView.ItemsSource = guesInformation;
					guestdetails = guesInformation;
				}
				stopLoading();
				if (GuestListView.IsRefreshing)
				{
					GuestListView.EndRefresh();
				}
			});
		}

		//Guest Selected
		async void SelectedGuest(object sender, ItemTappedEventArgs e)
		{
			GuestListView.SelectedItem = Color.Transparent; //use the color of your background of the listView
			guestDetails guestDetailsObject = (guestDetails)e.Item;
			await Navigation.PushAsync(new GuestEdit(guestdetails, guestDetailsObject));
		}

		public void RetrieveReservationList()
		{
			//Show Loading Message
			startLoading();
			//Read Reservations Data
			this.DisplayGuestDetails();
		}


		//Refreshing list on pulldown
		void refreshingGuest(object sender, EventArgs e)
		{
			//Refresh Guest Detail List
			this.RetrieveReservationList();
		}
		protected override bool OnBackButtonPressed()
		{
			return true;
		}

		//Logout button clicke
		 void LogoutButtonClickedEvt(object sender, EventArgs e)
		{
			userLogout.logout();
			//Navigate to Login Pa
		}

		void startLoading()
		{
			Device.BeginInvokeOnMainThread(() =>
						{
							GuestListIndicator.IsVisible = true;
							GuestListIndicator.IsRunning = true;
							headerImage.IsVisible = false;
							guestListnformation.IsVisible = false;
						});
		}

		void stopLoading()
		{
			GuestListIndicator.IsVisible = false;
			GuestListIndicator.IsRunning = false;
			headerImage.IsVisible = true;
			guestListnformation.IsVisible = true;
		}

        void HomeClicked(object sender, EventArgs e)
		{
			new HomeNavigater().GoHome();
		}
	}

	public class ItemList : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public ObservableCollection<guestDetails> _items;

		public ObservableCollection<guestDetails> Items
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

		public ItemList(List<guestDetails> itemList)
		{
			Items = new ObservableCollection<guestDetails>();
			foreach (guestDetails itm in itemList)
			{
				Items.Add(itm);
			}
		}

		public static implicit operator ItemList(RegistrationCard.ItemList v)
		{
			throw new NotImplementedException();
		}
	}


}

