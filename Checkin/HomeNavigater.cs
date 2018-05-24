using System;

using Xamarin.Forms;

namespace Checkin
{
    public class HomeNavigater
    {
        public HomeNavigater()
        {
            
        }

		public async void GoHome()
		{
			MessagingCenter.Send<HomeNavigater>(this, "home");
		}
    }
}

