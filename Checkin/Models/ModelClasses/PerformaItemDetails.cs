using System;
namespace Checkin
{
	public class PerformaItemDetails
	{
		//public string guide { get; private set; }

		public string startDate { get; private set; }

		public string endDate { get; private set; }

		public string descriptiopn { get; private set; }

		public string roomType { get; private set; }

		public string mealPlan { get; private set; }

		public string occu { get; private set; }

		public string nos { get; private set; }

		public string roomNights { get; private set; }

		public string rate { get; private set; }

		public string currency { get; private set; }

		public string amount { get; private set; }


		public PerformaItemDetails(string StartDate, string EndDate,
							   string Descriptiopn, string RoomType, string MealPlan,
							   string Occu, string Nos, string RoomNights, string Rate,
							   string Currency, string Amount)

		{
			//guide = Guide;
			startDate = StartDate;
			endDate = EndDate;
			descriptiopn = Descriptiopn;
			roomType = RoomType;
			mealPlan = MealPlan;
			occu = Occu;
			nos = Nos;
			roomNights = RoomNights;
			rate = Rate;
			currency = Currency;
			amount = Amount;
		}
	}
}

