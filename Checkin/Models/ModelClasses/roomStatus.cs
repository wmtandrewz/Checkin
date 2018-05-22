using System;
using Xamarin.Forms;

namespace Checkin
{
	public class roomStatus
	{
		private string roomstatusDetail;

		private Color roomStatusColor;

		public Color RoomStatusColor
		{
			get
			{
				return roomStatusColor;
			}

			set
			{
				roomStatusColor = value;
			}
		}

		public string RoomstatusDetail
		{
			get
			{
				return roomstatusDetail;
			}

			set
			{
				roomstatusDetail = value;
			}
		}
	}
}

