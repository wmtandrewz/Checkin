using System;

namespace Checkin
{
	public class StatusChangeCheckin
	{

		public string XRESERVA_ID { get; private set; }

		public string XHOTEL_ID { get; private set; }

		public string XPOSITION { get; private set; }

		public string XIMAGE { get; private set; }

		public string XOMIT_CHECKIN { get; private set; }

		public string CI_METHOD { get; private set; }



		public StatusChangeCheckin (string reservationId, string hotelID, string position, string Image, string checkinStatus ,string checkinMethod)
		{
			XRESERVA_ID = reservationId;
			XHOTEL_ID = hotelID;
			XPOSITION = position;
			XIMAGE = Image;
			XOMIT_CHECKIN = checkinStatus;
			CI_METHOD = checkinMethod;
		}
	}
}

