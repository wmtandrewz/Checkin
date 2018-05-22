using System;
using System.Collections.Generic;

namespace Checkin
{
	public class Messages
	{
		public static string BlinkIDResultsMessage = "BlinkIDResultsMessage";
		public Messages()
		{

		}
		public class BlinkIDResults
		{
			public List<Dictionary<string, string>> Results { get; set; }

		}

		public static string BlinkIDImageMessage = "BlinkIDImageMessage";
		public class BlinkIDImage
		{
			public Xamarin.Forms.ImageSource Image
			{
				get; set;
			}
		}
	}
}

