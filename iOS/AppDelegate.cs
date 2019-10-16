using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace Checkin.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
            Rg.Plugins.Popup.Popup.Init();
            global::Xamarin.Forms.Forms.Init();
            Xamarin.Forms.DependencyService.Register<Microblink.Forms.iOS.MicroblinkScannerFactoryImplementation>();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
		}
	}
}

