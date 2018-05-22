using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Checkin
{
	public class guestSignature
	{
		public ImageSource guestSignatureBase64 { get; set; }

		public string guestName { get;  set; }

		public string guestNameColor { get; set; }

		public int guestNumber { get;  set; }

		public string imageAvailability { get; set; }

		public string cellColor { get; set; }

		public string base64String { get; set; }

		public guestSignature(ImageSource signature, string guestname, string GuestNameColor, 
		                      int guestnumber, string imageavailability,
		                      string CellColor, string Base64String)
		{
			guestSignatureBase64 = signature;
			guestName = guestname;
			guestNameColor = GuestNameColor;
			guestNumber = guestnumber;
			imageAvailability = imageavailability;
			cellColor = CellColor;
			base64String = Base64String;
		}

		public static implicit operator List<object>(guestSignature v)
		{
			throw new NotImplementedException();
		}
	}
}

