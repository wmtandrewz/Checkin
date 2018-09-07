using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Checkin.Views
{
    public partial class PopupAlert : Rg.Plugins.Popup.Pages.PopupPage
    {
        
        public PopupAlert(string title, string messege, string ok)
        {
            InitializeComponent();

            TitleLabel.Text = title;
            MessegeLabel.Text = messege;
            okButton.Text = ok;
        }

		async void OKClicked(object sender, EventArgs e)
		{
			await Navigation.PopPopupAsync(true);

		}

		protected override bool OnBackgroundClicked()
		{
			return false;
		}

	}
}
