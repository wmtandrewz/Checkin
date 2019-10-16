using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Checkin.Views
{
    public partial class PopupTermsnConditions : Rg.Plugins.Popup.Pages.PopupPage
    {
        
        public PopupTermsnConditions()
        {
            InitializeComponent();


            string text = Constants._termsAndConditionsAPI;

            text = text.Replace("@", " " + Environment.NewLine);

            label1.Text = text;

        }

		async void OKClicked(object sender, EventArgs e)
		{
            await Navigation.PopPopupAsync(true);

        }


		protected override bool OnBackgroundClicked()
		{
			return true;
		}

	}
}
