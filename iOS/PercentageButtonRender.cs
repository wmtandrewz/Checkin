using Checkin;
using Checkin.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PercentageButton), typeof(PercentageButtonRender))]
namespace Checkin.iOS
{
	class PercentageButtonRender : ButtonRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged(e);

			Control.TitleEdgeInsets = new UIEdgeInsets(40, 0, 40, 0);
			Control.ClipsToBounds = true;
		}
	}
}

