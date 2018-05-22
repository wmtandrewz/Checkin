using System;

using Xamarin.Forms;

namespace Checkin.Models.ModelClasses.Payloads
{
    public class RemarksPayload
    {

		public string ImHotelId { get; set; }
        public string ImReservationId { get; set; }
        public string Mandt { get; set; }
        public string XhotelId { get; set; }
        public string XreservaId { get; set; }
        public string XtipoObserv { get; set; }
        public string Xobservacion { get; set; }

		public RemarksPayload(string imHotelID, string imReservationId, string mandt, string xHotelId, string xreservaId, string xTipobserv, string xObservaction)
        {
            ImHotelId = imHotelID;
            ImReservationId = imReservationId;
            Mandt = mandt;
            XhotelId = xHotelId;
            XreservaId = xreservaId;
            XtipoObserv = xTipobserv;
            Xobservacion = xObservaction;
        }
    }
}

