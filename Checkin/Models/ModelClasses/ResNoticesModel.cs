using System;
namespace Checkin.Models.ModelClasses
{
    public class ResNoticesModel
    {
		public string ImHotelId { get; set; }
        public string ImReservationId { get; set; }
        public string Mandt { get; set; }
        public string XhotelId { get; set; }
        public string XreservaId { get; set; }
		public string Xaccion { get; set; }
        public string Xobservacion { get; set; }

		public ResNoticesModel(string imHotelID,string imReservationId, string mandt, string xHotelId, string xreservaId, string xAccion, string xObservaction )
		{
			ImHotelId = imHotelID;
			ImReservationId = imReservationId;
			Mandt = mandt;
			XhotelId = xHotelId;
			XreservaId = xreservaId;
			Xaccion = xAccion;
			Xobservacion = xObservaction;
		}
    }
}
