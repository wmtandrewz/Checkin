using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Checkin.Views
{
	public partial class RemarkInputView : Rg.Plugins.Popup.Pages.PopupPage
    {
		private string remarkCatType = "";

		public RemarkInputView(string area, string type)
        {
            InitializeComponent();

			this.remarkCatType = type;
 
			titleLabel.Text = $"Enter your remark for {area}";

        }

		async void OKClicked(object sender, EventArgs e)
		{
			string [] values = {popupEntry.Text,remarkCatType};
            await Navigation.PopPopupAsync(true);
            MessagingCenter.Send<RemarkInputView, string[]>(this, "remark", values);

		}

		void ClearClicked(object sender, EventArgs e)
		{
			popupEntry.Text = "";
			popupEntry.Focus();
		}

		async void CancelClicked(object sender, EventArgs e)
		{
            await Navigation.PopPopupAsync(true);
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
