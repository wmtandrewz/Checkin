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

namespace Checkin
{
	public partial class Rooms : ContentPage
	{
		//Data source
		RoomInformation roomInformation = new RoomInformation();

		//List
		List<roomDetails> roomdetails = new List<roomDetails>();

		public Rooms()
		{
			InitializeComponent();
			Constants._roomdetails.Clear();
			this.DisplayRooms();
		}

		//Display Inspected And Clean room details
		public void DisplayRooms()
		{

			try
			{
				RoomsListView.ItemsSource = null;
				string status = "X";
				roomDetails(status);
			}
			catch (Exception)
			{
			}

		}

		//Dirty Rooms
		void getDirtyRooms(object sender, EventArgs e)
		{
			try
			{
				string status = "";
				this.roomDetails(status);
				DirtyRoomsButton.IsVisible = false;
			}
			catch (Exception)
			{
			}

		}

		//Cleaned and Inspected Rooms
		async void roomDetails(string roomStatus)
		{
			pageLoading();

			roomdetails = await roomInformation.roomInformation(roomStatus);

			Device.BeginInvokeOnMainThread(() =>
			{
				if (roomdetails != null)
				{
					RoomsListView.ItemsSource = roomdetails;
				}
			});
			stopLoading();
		}

		//Room Selected
		async void RoomsListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			pageLoading();
			roomDetails roomDetailsObject = (roomDetails)e.SelectedItem;

			//Add details to payload
			StatusChangeRoom statusChangedRoom = new StatusChangeRoom(Constants._hotel_code, Constants._reservation_id, roomDetailsObject.roomNumber);

			//Data Service
			PostServiceManager postServiceManager = new PostServiceManager();

			//Post Details
			String result = await postServiceManager.StatusChangeRoomAsync(statusChangedRoom);

            if(result == "No updatable data")
            {
                result = "The room is already selected.";
            }

			//SetRoomDetails
			Constants.result.RoomNumber = roomDetailsObject.roomNumber;

			stopLoading();

			await DisplayAlert(Constants._headerMessage, result, Constants._buttonOkay);

			if (string.Equals(result,Constants._roomSccuessfullyassigned)  == true)
			{
				//Room Assigned Idicator
				MessagingCenter.Send<Rooms, string>(this, Constants._roomAssigned, "");
				this.Navigation.RemovePage(this);
			}
		}

		//Page Loadin
		void pageLoading()
		{
			RoomsIndicator.IsVisible = true;
			RoomsIndicator.IsRunning = true;
			RoomDetails.IsVisible = false;
		}

		//Stop page loading
		void stopLoading()
		{
			RoomsIndicator.IsVisible = false;
			RoomsIndicator.IsRunning = false;
			RoomDetails.IsVisible = true;
		}

	}
}


