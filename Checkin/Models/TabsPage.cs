using System;
using Xamarin.Forms;

namespace Checkin
{
	public class TabsPage : TabbedPage
	{
		public TabsPage()
		{
			this.Title = "";
			this.Children.Add(new ReservationInfo() { Title = "Reservation Info",Icon="Planner-30.png" });
			this.Children.Add(new GuestInfo() { Title = "Guest Info",Icon="TabGuests.png" });
            this.Children.Add(new Remarks() { Title = "Remarks",Icon="TabComment.png" });
			this.Children.Add(new Attachments() { Title = "Attachments",Icon="attachments.png" });

		}
	}
}

