using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Checkin.Views
{
	public partial class PopupInputView : Rg.Plugins.Popup.Pages.PopupPage
    {

        public PopupInputView(string obseAction)
        {
            InitializeComponent();

			popupEntry.Text = obseAction;
        }

		async void OKClicked(object sender, EventArgs e)
		{
			var text = popupEntry.Text;
			await Navigation.PopPopupAsync(true);
			MessagingCenter.Send<PopupInputView,string>(this, "popup", text);


		}

		void ClearClicked(object sender, EventArgs e)
		{
			popupEntry.Text = "";
			popupEntry.Focus();
		}

		async void CancelClicked(object sender, EventArgs e)
		{
            await Navigation.PopPopupAsync(true);
            MessagingCenter.Send<PopupInputView>(this, "cancel");
		}

		protected override bool OnBackgroundClicked()
		{
			return false;
		}

		protected override void OnAppearing()
		{
			popupEntry.Focus();
			base.OnAppearing();
		}
	}
}
