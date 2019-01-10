using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Checkin.Views
{
    public partial class ProformaSelectorView : ContentPage
    {

        private string Responsible = "1";
        private string Folio = "2";

        private bool IsToggledF1 = false;
        private bool IsToggledF2 = true;
        private bool IsToggledF3 = false;
        private bool IsToggledF4 = false;

        public ProformaSelectorView()
        {
            InitializeComponent();

            mainGuestSwitch.IsToggled = true;
            folio2Switch.IsToggled = true;

            mainClientSwitch.Toggled+=delegate {

                mainGuestSwitch.IsToggled = !mainClientSwitch.IsToggled;
                if(mainClientSwitch.IsToggled)
                {
                    Responsible = "2";
                }
            };

            mainGuestSwitch.Toggled += delegate {

                mainClientSwitch.IsToggled = !mainGuestSwitch.IsToggled;
                if (mainGuestSwitch.IsToggled)
                {
                    Responsible = "1";
                }

            };

            folio1Switch.Toggled += delegate {

                if (!IsToggledF1)
                {
                    folio2Switch.IsToggled = !folio1Switch.IsToggled;
                    folio3Switch.IsToggled = !folio1Switch.IsToggled;
                    folio4Switch.IsToggled = !folio1Switch.IsToggled;
                    IsToggledF1 = true;
                    IsToggledF2 = false;
                    IsToggledF3 = false;
                    IsToggledF4 = false;
                }

                if (folio1Switch.IsToggled)
                {
                    Folio = "1";
                }
                else
                {
                    Folio = "";
                }
            };

            folio2Switch.Toggled += delegate {

                if (!IsToggledF2)
                {
                    folio1Switch.IsToggled = !folio2Switch.IsToggled;
                    folio3Switch.IsToggled = !folio2Switch.IsToggled;
                    folio4Switch.IsToggled = !folio2Switch.IsToggled;
                    IsToggledF1 = false;
                    IsToggledF2 = true;
                    IsToggledF3 = false;
                    IsToggledF4 = false;
                }

                if (folio2Switch.IsToggled)
                {
                    Folio = "2";
                }
                else
                {
                    Folio = "";
                }
            };

            folio3Switch.Toggled += delegate {

                if (!IsToggledF3)
                {
                    folio2Switch.IsToggled = !folio3Switch.IsToggled;
                    folio1Switch.IsToggled = !folio3Switch.IsToggled;
                    folio4Switch.IsToggled = !folio3Switch.IsToggled;
                    IsToggledF1 = false;
                    IsToggledF2 = false;
                    IsToggledF3 = true;
                    IsToggledF4 = false;
                }

                if (folio3Switch.IsToggled)
                {
                    Folio = "3";
                }
                else
                {
                    Folio = "";
                }
            };

            folio4Switch.Toggled += delegate {

                if (!IsToggledF4)
                {
                    folio1Switch.IsToggled = !folio4Switch.IsToggled;
                    folio2Switch.IsToggled = !folio4Switch.IsToggled;
                    folio3Switch.IsToggled = !folio4Switch.IsToggled;
                    IsToggledF1 = false;
                    IsToggledF2 = false;
                    IsToggledF3 = false;
                    IsToggledF4 = true;
                }

                if (folio4Switch.IsToggled)
                {
                    Folio = "4";
                }
                else
                {
                    Folio = "";
                }
            };

            ProformaButton.Clicked+= async delegate {

                if (!string.IsNullOrEmpty(Folio) && !string.IsNullOrEmpty(Responsible))
                {
                    if ((mainGuestSwitch.IsToggled || mainClientSwitch.IsToggled) && (IsToggledF1 || IsToggledF2 || IsToggledF3 || IsToggledF4))
                    {
                        await Navigation.PushAsync(new ProformaInvoice(Folio,Responsible,sendEmailSwitch.IsToggled ? "X" : "N", guestEmailText.Text));
                    }
                }
            };
        }
    }
}
