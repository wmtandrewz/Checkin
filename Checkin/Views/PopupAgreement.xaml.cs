using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Checkin.Views
{
    public partial class PopupAgreement : Rg.Plugins.Popup.Pages.PopupPage
    {
        
        public PopupAgreement()
        {
            InitializeComponent();


           string text = Constants._agreementInformation;
           text = text.Replace("@", " " + Environment.NewLine);

            label1.Text = text;

        }

		async void OKClicked(object sender, EventArgs e)
		{
			await PopupNavigation.PopAsync(true);

		}


		protected override bool OnBackgroundClicked()
		{
			return true;
		}

	}
}
