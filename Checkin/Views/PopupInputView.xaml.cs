using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Checkin.Views
{
	public partial class PopupInputView : Rg.Plugins.Popup.Pages.PopupPage
    {

        public PopupInputView()
        {
            InitializeComponent();
        }

		void OKClicked(object sender, EventArgs e)
		{

			MessagingCenter.Send<PopupInputView,string>(this, "popup", popupEntry.Text);

			PopupNavigation.PopAsync(true);
		}

		void CancelClicked(object sender, EventArgs e)
		{
			PopupNavigation.PopAsync(true);
		}

		protected override bool OnBackgroundClicked()
		{
			return base.OnBackgroundClicked();
		}
	}
}
