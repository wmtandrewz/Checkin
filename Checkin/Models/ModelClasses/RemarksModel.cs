using System;
namespace Checkin.Models.ModelClasses
{
    public class RemarksModel
    {
		public string ImHotelId { get; set; }
		public string ImReservationId { get; set; }
		public string Mandt { get; set; }
		public string XhotelId { get; set; }
		public string XreservaId { get; set; }
		public string XtipoObserv { get; set; }
		public string Xobservacion { get; set; }
		public string Xexterna { get; set; }

		public RemarksModel(string imHotelID, string imReservationId, string mandt, string xHotelId, string xreservaId,string xTipobserv, string xObservaction, string xExterna)
        {
            ImHotelId = imHotelID;
            ImReservationId = imReservationId;
            Mandt = mandt;
            XhotelId = xHotelId;
            XreservaId = xreservaId;
			XtipoObserv = xTipobserv;
            Xobservacion = xObservaction;
			Xexterna = xExterna;
        }
    }
}
