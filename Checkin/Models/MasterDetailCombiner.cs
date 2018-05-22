using System;
using Xamarin.Forms;

namespace Checkin
{
	public class MasterDetailCombiner : MasterDetailPage
	{
		public MasterDetailCombiner()
		{
			NavigationPage.SetHasNavigationBar(this, false);

			//Master Page Configuration
			var masterPage = new ReservationsList();
			masterPage.selectedItemPending = (categoryPage) =>
			{
				var detail1 = new NavigationPage(categoryPage);
				detail1.BarBackgroundColor = Color.FromHex("#660099");
				detail1.BarTextColor = Color.White;

				if (MasterBehavior == MasterBehavior.SplitOnLandscape)
				{
					IsPresented = true;
				}
				else {
					IsPresented = false;
				}
				this.Detail = detail1;
			};
			masterPage.selectedItemCheckedIn = (categoryPage) =>
			{
				var detail2 = new NavigationPage(categoryPage);
				detail2.BarBackgroundColor = Color.FromHex("#660099");
				detail2.BarTextColor = Color.White;
				if (MasterBehavior == MasterBehavior.SplitOnLandscape)
				{
					IsPresented = true;
				}
				else {
					IsPresented = false;
				}
				this.Detail = detail2;
			};

            
			this.Master = masterPage;

			//Detail Page Configuration
			var detail = new NavigationPage(new HomeDefault());
			detail.BarBackgroundColor = Color.FromHex("#660099");
			detail.BarTextColor = Color.White;
			this.Detail = detail;

		}
	}
	public class GetMainPage : Page
	{
		public GetMainPage()
		{

			if (Device.OS == TargetPlatform.Android)
			{
				Navigation.PushModalAsync(new MasterDetailCombiner());
			}
			else if (Device.OS == TargetPlatform.iOS)
			{
				Navigation.PushAsync(new MasterDetailCombiner());
			}
				
		}
	}
}

