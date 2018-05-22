using System;

using Xamarin.Forms;

namespace Checkin.Models.ModelClasses.Payloads
{
    public class FlightPayload
    {
		public string ImHotelId { get; set; }
		public string ImReservationId { get; set; }
		public string Mandt { get; set; }
		public string XhotelId { get; set; }
		public string XreservaId { get; set; }
		public string XvueloId { get; set; }
		public string XhLlgda { get; set; }
		public string XvueloSalida { get; set; }
		public string XhSlida { get; set; }
		public string XnumTarjeta { get; set; }
		public string XtitularTarj { get; set; }
		public string XclaseTarj { get; set; }
		public string XfCaducTarj { get; set; }

		public FlightPayload(string _ImHotelId, string _ImReservationId, string _Mandt, string _XhotelId, string _XreservaId, string _XvueloId, string _XhLlgda, string _XvueloSalida, string _XhSlida, string _XnumTarjeta, string _XtitularTarj, string _XclaseTarj, string _XfCaducTarj)
		{
			ImHotelId = _ImHotelId;
			ImReservationId = _ImReservationId;
			Mandt = _Mandt;
			XhotelId = _XhotelId;
			XreservaId = _XreservaId;
			XvueloId = _XvueloId;
			XhLlgda = _XhLlgda;
			XvueloSalida = _XvueloSalida;
			XhSlida = _XhSlida;
			XnumTarjeta = _XnumTarjeta;
			XtitularTarj = _XtitularTarj;
			XclaseTarj = _XclaseTarj;
			XfCaducTarj = _XfCaducTarj;
		}

    }
}

