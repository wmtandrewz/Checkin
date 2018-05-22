using System;
namespace Checkin.Models.ModelClasses
{
    public class FlightsModel
    {
		public string ArrFlight { get; set; }
		public string DepFlight { get; set; }
		public string Airport { get; set; }
		public string AirpArrTime { get; set; }
		public string HotelDepTime { get; set; }
		public string HotelArrTime { get; set; }
		public string AirpDepTime { get; set; }

		public FlightsModel(string arrFlight, string depFlight,string airport, string airArrTime, string hotelDepTime, string hotelArrTime, string airDepTime)
        {
			this.ArrFlight = arrFlight;
			this.DepFlight = depFlight;
			this.Airport = airport;
			this.AirpArrTime = airArrTime;
			this.HotelDepTime = hotelDepTime;
			this.HotelArrTime = hotelArrTime;
			this.AirpDepTime = airDepTime;
        }
    }

}
