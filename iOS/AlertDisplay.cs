using Checkin.iOS;
using UIKit;
using checkin;

[assembly: Xamarin.Forms.Dependency(typeof(AlertDisplay))]
namespace Checkin.iOS
{
	class AlertDisplay : DisplayAlert
	{
		public void DisplayAlert(string title, string message, string button)
		{
			UIAlertView alert = new UIAlertView()
			{
				Title = title,
				Message = message
			};
			alert.AddButton(button);
			alert.Show();
		}
	}
}