using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Checkin.Views
{
    public partial class PopupTermsnConditions : Rg.Plugins.Popup.Pages.PopupPage
    {
        
        public PopupTermsnConditions()
        {
            InitializeComponent();


            string text;

            //switch (Constants._hotel_code)
            switch (Constants._termsAndConditionsDefaults)
            {
                case "3000":
                    text = Constants._termsAndConditionsCNG;
                    break;

                case "3005":
                    text = Constants._termsAndConditionsCNL;
                    break;

                case "3015":
                    text = Constants._termsAndConditionsRED;
                    break;

                case "3100":
                    text = Constants._termsAndConditionsBBH;
                    break;

                case "3110":
                    text = Constants._termsAndConditionsCIT;
                    break;

                case "3115":
                    text = Constants._termsAndConditionsLOD;
                    break;

                case "3120":
                    text = Constants._termsAndConditionsVIL;
                    break;

                case "3150":
                    text = Constants._termsAndConditionsWLD;
                    break;

                case "3160":
                    text = Constants._termsAndConditionsBEY;
                    break;

                case "3165":
                    text = Constants._termsAndConditionsBLU;
                    break;

                case "3170":
                    text = Constants._termsAndConditionsTRA;
                    break;

                case "3300":
                    text = Constants._termsAndConditionsELL;
                    break;

                case "3305":
                    text = Constants._termsAndConditionsHAK;
                    break;

                case "3310":
                    text = Constants._termsAndConditionsDHO;
                    break;

                default:
                    text = Constants._termsAndConditionsDefaults;
                    break;
            }

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
