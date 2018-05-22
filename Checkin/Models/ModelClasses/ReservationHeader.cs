using System;
using Xamarin.Forms;

namespace Checkin
{
	public class ReservationHeader
	{
		public string ReservationID { get; private set; }

		public string GuestName { get; private set; }

		public string MainClientName { get; private set; }

		public string Status { get;  set; }

		public Color StatusColor { get;  set; }

		public Color CellColour { get; private set; }

		public Color TextColor { get; private set; }

		public string reservationImage { get;  set; }

		public string roomNumber { get; set; }

		public string roomStatusImageText { get; set; }

		public string numberOfVisits { get; set; }

		public string toatlNumberOfVisits { get; set; }

		public ReservationHeader(string reservationID, string guestName, string mainClientName, string status, Color statusColor, Color cellColor, Color textColor, string ReservationImage, string RoomNumber, string RoomStatusImageText, string NumberOfVisits, string TotalNumberOfVisits)
		{
			ReservationID = reservationID;
			GuestName = guestName;
			MainClientName = mainClientName;
			Status = status;
			StatusColor = statusColor;
			CellColour = cellColor;
			TextColor = textColor;
			reservationImage = ReservationImage;
			roomNumber = RoomNumber;
			roomStatusImageText = RoomStatusImageText;
			numberOfVisits = NumberOfVisits;
			toatlNumberOfVisits = TotalNumberOfVisits;
		}
	}
}

