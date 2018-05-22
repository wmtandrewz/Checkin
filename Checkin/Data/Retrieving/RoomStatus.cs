using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Checkin
{
	public class RoomStatus
	{
		//Data Source
		CheckInManager checkInManager = new CheckInManager();
		roomStatus roomStatusObject = new roomStatus();

		public async Task<roomStatus> roomStatusDetails(string roomNumber)
		{
			string resultAvailability = "";
			try
			{
				string result = await checkInManager.GetRoomsStatus(roomNumber);

				if (result != null)
				{
					var output = JObject.Parse(result);

					string roomStatus = Convert.ToString(output["d"]["results"][0]["RoomStatus"]);

					//Inspected Room
					if (roomStatus == "IN")
					{
						roomStatusObject.RoomStatusColor = Color.FromHex("FF7F50");
						roomStatusObject.RoomstatusDetail = Constants._inspectedRoom;
					}

					//Cleaned Room
					else if (roomStatus == "CL")
					{
						roomStatusObject.RoomStatusColor = Color.Green;
						roomStatusObject.RoomstatusDetail = Constants._cleanedRoom;
					}

					//Dirty Room
					else if (roomStatus == "DT")
					{
						roomStatusObject.RoomStatusColor = Color.Red;
						roomStatusObject.RoomstatusDetail = Constants._dirtyRoom;
					}
					//Room not assigned
					else if (roomStatus == "S")
					{
						roomStatusObject.RoomStatusColor = Color.FromHex("FF0000");
						roomStatusObject.RoomstatusDetail = Constants._roomNotAssigned;
					}
					else if (roomStatus == "")
					{
						roomStatusObject.RoomStatusColor = Color.FromHex("FF0000");
						roomStatusObject.RoomstatusDetail = Constants._roomNotAssigned;
					}


				}
			}
			catch (Exception e)
			{
			}
			return roomStatusObject;
		}
	}
}

