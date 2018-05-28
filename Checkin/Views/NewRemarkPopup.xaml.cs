using System;
using System.Collections.Generic;
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
			await PopupNavigation.PopAsync(true);
			await PopupNavigation.PushAsync(new RemarkInputView("Front Office","FO"));
		}

		async void FnBClicked(object sender, EventArgs e)
		{
			await PopupNavigation.PopAsync(true);
			await PopupNavigation.PushAsync(new RemarkInputView("Food & Beverage","F&B"));
		}

		async void HKclicked(object sender, EventArgs e)
		{
			await PopupNavigation.PopAsync(true);
			await PopupNavigation.PushAsync(new RemarkInputView("House Keeping","HK"));
		}

		async void BillingClicked(object sender, EventArgs e)
		{
			await PopupNavigation.PopAsync(true);
			await PopupNavigation.PushAsync(new RemarkInputView("Billing Info.","BILLING"));
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
