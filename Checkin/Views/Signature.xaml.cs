using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Acr.XamForms;
using Acr.XamForms.SignaturePad;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Text.RegularExpressions;
using Rg.Plugins.Popup.Services;
using Checkin.Views;

namespace Checkin
{
    public partial class Signature : ContentPage
    {
        int signature;

        //List Collection
        List<guestSignature> guestsignature = new List<guestSignature>();

        public Signature(List<guestSignature> guestSignature, guestSignature guestSignatureNumber)
        {
            InitializeComponent();

            guestsignature = guestSignature;
            signature = Int32.Parse(guestSignatureNumber.guestNumber.ToString());

            string name = Regex.Replace(guestSignatureNumber.guestName.ToString(), @"(?<=(^|[.;:])\s*)[a-z]", (match) => { return match.Value.ToUpper(); });
            GuestNameText.Text = "Dear " + name + " ! Please add your signature below.";

            SignatureLayout.IsVisible = false;
        }

        //Save image button triggered
        async void ImageSaveButton(object sender, EventArgs e)
        {
            try
            {
                string base64String, base64String2;

                //Getting image from signaturepad as a stream
                var inputStream = padView.GetImage(ImageFormatType.Png);

                //Getting Stream as a Memorystream
                var signatureMemoryStream = inputStream as MemoryStream;

                if (signatureMemoryStream == null)
                {
                    signatureMemoryStream = new MemoryStream();
                    inputStream.CopyTo(signatureMemoryStream);
                }

                //Adding memorystream into a byte array
                var byteArray = signatureMemoryStream.ToArray();

                //Converting byte array into Base64 string
                base64String = Convert.ToBase64String(byteArray);

                if (base64String != Constants.addSignatureImage)
                {
                    Constants._notAvailableSignatureAdded = true;

                    //Converting base 64 string into bytes
                    byte[] data = Convert.FromBase64String(base64String);

                    //Adding image new image source to constant
                    //Constants._guestSignatureBase64 = ImageSource.FromStream(() => new MemoryStream(data));

                    var obj = guestsignature.FirstOrDefault(x => x.guestNumber == signature);
                    obj.base64String = base64String;
                    obj.guestSignatureBase64 = ImageSource.FromStream(() => new MemoryStream(data));
                    obj.cellColor = "Purple";
                    obj.guestNameColor = "White";
                    obj.imageAvailability = "";

                    //Passing message (New Signature added)
                    MessagingCenter.Send<Signature, List<guestSignature>>(this, Constants._signatureAddedMessage, guestsignature);

                    await DisplayAlert(Constants._headerMessage, Constants._signatureSaved, Constants._buttonOkay);

                    //close current content page
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert(Constants._headerMessage, Constants._signatureValidation, Constants._buttonOkay);
                }
            }
            catch (Exception)
            {
                await DisplayAlert(Constants._headerMessage, Constants._signatureValidation, Constants._buttonOkay);
            }

        }

        //public string ImageToBase64(Image image)
        //{
        //	using (MemoryStream ms = new MemoryStream(ImageSourceConverter(signatureView.Source)))
        //	{
        //		// Convert Image to byte[]

        //		byte[] imageBytes = ms.ToArray();

        //		// Convert byte[] to Base64 String
        //		string base64String = Convert.ToBase64String(imageBytes);
        //		return base64String;
        //	}
        //}
        //Override Back Button Function
        protected override bool OnBackButtonPressed()
        {
            Constants._notAvailableSignatureAdded = false;
            return true;
        }
        protected override void OnDisappearing()
        {
            Constants._notAvailableSignatureAdded = true;
            base.OnDisappearing();
        }

        //Animation on page load
        protected override async void OnAppearing()
        {
            double scale = this.Scale;
            ImageSave.Scale = (scale * 90 / 100);
            Close.Scale = (scale * 90 / 100);
        }

        async void CloseButtonClickedEvt(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private static async Task<Stream> GetStreamFromImageSourceAsync(StreamImageSource imageSource, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (imageSource.Stream != null)
            {
                return await imageSource.Stream(cancellationToken);
            }
            return null;
        }

        // Agrreement button Clicked
        async void AgreementButton(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new PopupAgreement());
            //agreementInformation();
        }

        //Agrremnt button clicked
        //async void agreementInformation()
        //{
        //    //Agreement text
        //    //string text = Constants._agreementInformation;
        //    //text = text.Replace("@", " " + Environment.NewLine);
        //    //await DisplayAlert(Constants._agreement, text, Constants._buttonOkay);


        //}

        //Terms and Conditions Button clicked
        async void TermsandConditions(object sender, EventArgs e)
        {
            //string text;

            //switch(Constants._hotel_code)
            //{
            //    case "3000": text = Constants._termsAndConditionsCNG;
            //        break;

            //    case "3005":
            //        text = Constants._termsAndConditionsCNL;
            //        break;

            //    case "3015":
            //        text = Constants._termsAndConditionsRED;
            //        break;

            //    case "3100":
            //        text = Constants._termsAndConditionsBBH;
            //        break;

            //    case "3110":
            //        text = Constants._termsAndConditionsCIT;
            //        break;

            //    case "3115":
            //        text = Constants._termsAndConditionsLOD;
            //        break;

            //    case "3120":
            //        text = Constants._termsAndConditionsVIL;
            //        break;

            //    case "3150":
            //        text = Constants._termsAndConditionsWLD;
            //        break;

            //    case "3160":
            //        text = Constants._termsAndConditionsBEY;
            //        break;

            //    case "3165":
            //        text = Constants._termsAndConditionsBLU;
            //        break;

            //    case "3170":
            //        text = Constants._termsAndConditionsTRA;
            //        break;

            //    case "3300":
            //        text = Constants._termsAndConditionsELL;
            //        break;

            //    case "3305":
            //        text = Constants._termsAndConditionsHAK;
            //        break;

            //    case "3310":
            //        text = Constants._termsAndConditionsDHO;
            //        break;

            //    default:
            //        text = Constants._termsAndConditionsDefaults;
            //        break;
            //}

            //text = text.Replace("@", " " + Environment.NewLine);
            //await DisplayAlert(Constants._termsAndConditionsMessage, text, Constants._buttonOkay);

            await PopupNavigation.PushAsync(new PopupTermsnConditions());
        }



        private async void CloseWindow(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        void AgreementTnC_Toggled(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            SignatureLayout.IsVisible = e.Value == true ? true : false;
            CloseWindowBtn.IsVisible = e.Value == true ? false : true;
        }

    }
}

