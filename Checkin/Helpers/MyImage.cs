using System;
using Xamarin.Forms;

namespace Checkin
{
	public class MyImage : Image
	{
		public Func<byte[]> GetBytes { get; set; }
	}
}

