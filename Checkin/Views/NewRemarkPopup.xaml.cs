using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Checkin.Views
{
	public partial class NewRemarkPopup : Rg.Plugins.Popup.Pages.PopupPage
    {

		public NewRemarkPopup()
        {
            InitializeComponent();

        }

		async void FrontOfficeClicked(object sender, EventArgs e)
		{
			await Navigation.PopPopupAsync(true);
			await Navigation.PushPopupAsync(new RemarkInputView("Front Office","FO"));
		}

		async void FnBClicked(object sender, EventArgs e)
		{
            await Navigation.PopPopupAsync(true);
            await Navigation.PushPopupAsync(new RemarkInputView("Food & Beverage (POS)","POS"));
		}

        async void HKclicked(object sender, EventArgs e)
		{
            await Navigation.PopPopupAsync(true);
            await Navigation.PushPopupAsync(new RemarkInputView("House Keeping","HK"));
		}

		async void BillingClicked(object sender, EventArgs e)
		{
            await Navigation.PopPopupAsync(true);
            await Navigation.PushPopupAsync(new RemarkInputView("Billing Info.","BILLING"));
		}

        async void CareOfClicked(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync(true);
            await Navigation.PushPopupAsync(new RemarkInputView("Care of", "C/O"));
        }

        async void ConcClicked(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync(true);
            await Navigation.PushPopupAsync(new RemarkInputView("Concierge", "CONC"));
        }

        protected override bool OnBackgroundClicked()
		{
			return true;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
		}
	}
}
