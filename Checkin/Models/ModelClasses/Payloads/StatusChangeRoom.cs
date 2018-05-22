using System;

namespace Checkin
{
	public class StatusChangeRoom
	{
		public string ImHotelId { get; private set; }

		public string ImReservaId { get; private set; }

		public string ImHabitacionId { get; private set; }


		public StatusChangeRoom (string HotelID, string ReservationID, string RoomID)
		{
			ImHotelId = HotelID;
			ImReservaId = ReservationID;
			ImHabitacionId = RoomID;
		}
	}


}

