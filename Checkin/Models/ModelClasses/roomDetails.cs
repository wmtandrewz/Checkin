using System;

using Xamarin.Forms;

namespace Checkin
{
	public class roomDetails
	{
		public string roomNumber { get; private set; }

		public string roomType { get; private set; }

		public string status { get; private set; }

		public Color statusColor { get; private set; }

		public string statusImage { get; private set; }

		public string roomPreferences { get; private set; }



		public roomDetails (string roomnumber, string roomtype, string roomstatus, Color statuscolor, string statusimage, string RoomPreferences)
		{
			roomNumber = roomnumber;
			roomType = roomtype;
			status = roomstatus;
			statusColor = statuscolor;
			statusImage = statusimage;
			roomPreferences = RoomPreferences;
		}
	}
}

