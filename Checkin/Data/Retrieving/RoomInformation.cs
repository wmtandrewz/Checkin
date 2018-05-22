using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Checkin
{
	public class RoomInformation
	{

		//Date Source
		CheckInManager checkInManager = new CheckInManager();


		public async Task<List<roomDetails>> roomInformation(string roomStatus)
		{
			
			string resultAvailability = "";
			string statusImage = "";

			string result = await checkInManager.GetRoomsDetails(roomStatus);
			if (result != null)
			{
				Color statusColor = Color.Olive;
				result = result.Replace("Date(-", "Date(");
				var output = JObject.Parse(result);

				for (int i = 0; i < Enumerable.Count(output["d"]["results"]); i++)
				{
					string roomStatusDetail = Convert.ToString(output["d"]["results"][i]["XstdoDesc"]);
					statusImage = serviceDataValidation.roomImageValidation(roomStatusDetail);

					Constants._roomdetails.Add(new roomDetails(Convert.ToString(output["d"]["results"][i]["Xhabitacion"]), Convert.ToString(output["d"]["results"][i]["XtipoHabDesc"]), roomStatusDetail, statusColor, statusImage,serviceDataValidation.roomPreferencesValidation( Convert.ToString(output["d"]["results"][i]["Xpreferences"]))));
				}
				resultAvailability = Constants._available;
			}
			return Constants._roomdetails;
		}
	}
}

