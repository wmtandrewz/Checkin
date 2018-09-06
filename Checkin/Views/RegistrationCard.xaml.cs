using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.IO;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Globalization;
using Checkin.Data.Posting;
using System.Diagnostics;
using Rg.Plugins.Popup.Services;
using Checkin.Views;

namespace Checkin
{
    public partial class RegistrationCard : ContentPage
    {
        //List Collection
        List<guestDetails> guestdetails;
        List<guestSignature> guestsignature;

        userLogout userLogout = new userLogout();

        //Data Source
        CheckInManager checkInManager = new CheckInManager();
        MainGuestInformation mainGuestInformation = new MainGuestInformation();

        //Setting stackLoyout Heights for signatures and guest details
        int layoutHeightGuestDetails, layoutHeightSignatures, lasyoutHeighSignatureInitial, savedSignatureCount;

        //Setting global variables
        int guestNumberCount;
        int guestNumber;
        string guestEmail = string.Empty;

        //Creating obervable collection
        ItemList items;

        public RegistrationCard()
        {
            initialPageLoading();

            //Privacy statement

            //Signature Added
            MessagingCenter.Subscribe<Signature, List<guestSignature>>(this, Constants._signatureAddedMessage, (sender, arg) =>
            {
                checkinAndSaveButton.IsVisible = true;
                if (Constants._reservationStatus == Constants._reservationStatusCheckedIn)
                {
                    //saveSignatureButton.IsVisible = true;//false
                    checkinButton.IsVisible = false;
                    PerformaButton.IsVisible = true;


                }
                else
                {
                    //saveSignatureButton.IsVisible = false;//false
                    checkinButton.IsVisible = true;
                    PerformaButton.IsVisible = false;
                }

                GuestSignatureList.ItemsSource = null;
                guestsignature = arg;
                GuestSignatureList.ItemsSource = guestsignature;
                //MessagingCenter.Unsubscribe<Signature, List<guestSignature>>(this, Constants._signatureAddedMessage);
            });

            //Guest Edited
            MessagingCenter.Subscribe<GuestEdit, List<guestDetails>>(this, Constants._guestEdited, (sender, arg) =>
            {
                GuestNameList.ItemsSource = null;
                guestdetails = arg;
                GuestNameList.ItemsSource = guestdetails;

                var obj = guestdetails.FirstOrDefault(x => x.guestNumber == Int32.Parse(Constants._guestNumber));
                if (obj != null)
                {
                    var obj1 = guestsignature.FirstOrDefault(x => x.guestNumber == Int32.Parse(Constants._guestNumber));
                    if (obj1 == null)
                    {
                        guestSignature newGuestSignature = new guestSignature("SignatureImage.jpg", obj.guestName, "Purple", obj.guestNumber, Constants._notAvailable, "White", "");
                        GuestSignatureList.ItemsSource = null;
                        guestsignature.Add(newGuestSignature);
                        GuestSignatureList.ItemsSource = guestsignature;
                        GuestSignatureView.HeightRequest = 40 + (157 * guestsignature.Count);
                    }
                    else
                    {
                        obj1.guestName = obj.guestName;
                        obj1.guestNumber = obj.guestNumber;
                        GuestSignatureList.ItemsSource = null;
                        GuestSignatureList.ItemsSource = guestsignature;
                    }
                    if (obj.guestNumber == 1)
                    {

                        string title = obj.salutation;
                        //List of salutations from dictiona
                        var nameToSalutation = CountryDictionary.listofSalutation();

                        if (title != "" || title != "")
                        {
                            //Item Value in dictionar
                            string name = nameToSalutation.FirstOrDefault(x => x.Value == title).Key;
                            titleName.Text = name;
                            titleName.TextColor = serviceDataValidation.validationColor(name);
                        }

                        fname.Text = serviceDataValidation.validation(obj.guestFirstName);
                        fname.TextColor = serviceDataValidation.validationColor(obj.guestFirstName);
                        lname.Text = serviceDataValidation.validation(obj.guestLastName);
                        lname.TextColor = serviceDataValidation.validationColor(obj.guestLastName);
                        emailaddress.Text = serviceDataValidation.validation(obj.email);
                        emailaddress.TextColor = serviceDataValidation.validationColor(obj.email);
                        ContactNumber.Text = serviceDataValidation.validation(obj.contactNumber);
                        ContactNumber.TextColor = serviceDataValidation.validationColor(obj.contactNumber);
                        dob.Text = serviceDataValidation.validation(obj.dateOfBirth);
                        dob.TextColor = serviceDataValidation.validationColor(obj.dateOfBirth);
                        NICPASS.Text = serviceDataValidation.validation(obj.passportIdNumber);
                        NICPASS.TextColor = serviceDataValidation.validationColor(obj.passportIdNumber);
                        houseno.Text = serviceDataValidation.validation(obj.houseNumber);
                        houseno.TextColor = serviceDataValidation.validationColor(obj.houseNumber);
                        street.Text = serviceDataValidation.validation(obj.street);
                        street.TextColor = serviceDataValidation.validationColor(obj.street);
                        city.Text = serviceDataValidation.validation(obj.city);
                        city.TextColor = serviceDataValidation.validationColor(obj.city);




                        country.Text = serviceDataValidation.validation(serviceDataValidation.titleCountry(obj.country));
                        country.TextColor = serviceDataValidation.validationColor(obj.country);
                        nationality.Text = serviceDataValidation.validation(serviceDataValidation.titleNationality(obj.nationality));
                        nationality.TextColor = serviceDataValidation.validationColor(obj.nationality);
                    }
                }
                //MessagingCenter.Unsubscribe<GuestEdit, List<guestDetails>>(this, Constants._guestEdited);
            });

            //Signature Saved
            MessagingCenter.Subscribe<RegistrationCard, string>(this, Constants._signatureSuccessfullySaved, (sender, arg) =>
            {
                //checkinAndSaveButton.IsVisible = false;//false
                stopPageLoading();
                MessagingCenter.Unsubscribe<RegistrationCard, string>(this, Constants._signatureSuccessfullySaved);
            });
            //Checked-In
            MessagingCenter.Subscribe<RegistrationCard, string>(this, Constants._reservationStatusCheckedIn, (sender, arg) =>
            {
                //checkinAndSaveButton.IsVisible = false;//false

                Device.BeginInvokeOnMainThread(() =>
                {
                    checkinButton.IsVisible = false;
                });

                checkinButton.IsVisible = false;
                stopPageLoading();
                MessagingCenter.Unsubscribe<RegistrationCard, string>(this, Constants._reservationStatusCheckedIn);
            });
        }

        //Initial Page loading
        void initialPageLoading()
        {
            InitializeComponent();
            onloadViewingCheckinAndSignatureButtons();
            loadAllMethods();
        }

        void onloadViewingCheckinAndSignatureButtons()
        {
            //on Device Load Loadeders and other operations
            if (Constants._reservationStatus == Constants._reservationStatusCheckedIn)
            {
                checkinAndSaveButton.IsVisible = false;//false
                checkinButton.IsVisible = false;
				saveSignatureButton.IsVisible = true;//false
                PerformaButton.IsVisible = true;
            }
            else
            {
                checkinAndSaveButton.IsVisible = false;//false
                checkinButton.IsVisible = true;
                saveSignatureButton.IsVisible = true; //false
                PerformaButton.IsVisible = false;

                //GuestSignatureView.IsEnabled = false;
            }

        }

        async void getMainGuestDetails()
        {
            pageLoading();

            MainGuestDetails mainGuestDetails = await mainGuestInformation.mainGuestInformation(Constants._reservation_id);
            if (mainGuestDetails != null)
            {
                DisplayMainGuestDetails(mainGuestDetails);
            }
        }

        //Main Guest Details
        public async void DisplayMainGuestDetails(MainGuestDetails mainGuestDetails)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ReservationIDText.Text = Constants._reservation_id;
                ClientNameText.Text = mainGuestDetails.ClientName;
                HotelNameText.Text = mainGuestDetails.HotelName;
                titleName.Text = mainGuestDetails.Title;
                titleName.TextColor = serviceDataValidation.validationColor(mainGuestDetails.Title);
                fname.Text = mainGuestDetails.FirstName;
                fname.TextColor = serviceDataValidation.validationColor(mainGuestDetails.FirstName);
                lname.Text = mainGuestDetails.LastName;
                lname.TextColor = serviceDataValidation.validationColor(mainGuestDetails.LastName);
                emailaddress.Text = mainGuestDetails.Email;
                emailaddress.TextColor = serviceDataValidation.validationColor(mainGuestDetails.Email);
                guestEmail = mainGuestDetails.Email;
                ContactNumber.Text = mainGuestDetails.ContactNumber;
                ContactNumber.TextColor = serviceDataValidation.validationColor(mainGuestDetails.ContactNumber);
                dob.Text = mainGuestDetails.DateOfBirth;
                dob.TextColor = serviceDataValidation.validationColor(mainGuestDetails.DateOfBirth);
                NICPASS.Text = mainGuestDetails.NicPassNumber;
                NICPASS.TextColor = serviceDataValidation.validationColor(mainGuestDetails.NicPassNumber);
                houseno.Text = mainGuestDetails.HouseNumber;
                houseno.TextColor = serviceDataValidation.validationColor(mainGuestDetails.HouseNumber);
                street.Text = mainGuestDetails.Street;
                street.TextColor = serviceDataValidation.validationColor(mainGuestDetails.Street);
                city.Text = mainGuestDetails.City;
                city.TextColor = serviceDataValidation.validationColor(mainGuestDetails.City);
                country.Text = mainGuestDetails.Country;
                country.TextColor = serviceDataValidation.validationColor(mainGuestDetails.Country);
                nationality.Text = mainGuestDetails.Nationality;
                nationality.TextColor = serviceDataValidation.validationColor(mainGuestDetails.Nationality);
            });
            getReservationDetails();
        }

        async void getReservationDetails()
        {
            ReservationDetails result = await mainGuestInformation.reservationInformation();

            if (result != null)
            {
                DisplayReservationDetails(result);
            }
        }

        //Reservation Details
        public void DisplayReservationDetails(ReservationDetails result)
        {
            //Load Page
            Device.BeginInvokeOnMainThread(() =>
            {
                BindingContext = result;
            });
            getRemarksDetails();
        }
        //Guest Details
        public async void DisplayGuestDetails()
        {
            pageLoading();
            layoutHeightGuestDetails = 0;
            layoutHeightSignatures = 0;
            lasyoutHeighSignatureInitial = 1;
            guestNumber = 1;
            guestNumberCount = 1;
            try
            {
                GuestNameList.ItemsSource = null;
                string result = await checkInManager.GetRemarksDetails(Constants._reservation_id);
                guestdetails = new List<guestDetails>();
                if (Constants._notAvailableSignatureAdded == false)
                {
                    guestsignature = new List<guestSignature>();
                }

                GuestSignatureList.ItemsSource = null;

                if (result != null)
                {
                    string guestNameAvailabiliyty = "";
                    string tem_resultMain = result; // 

                    //Removing ticks issue in date
                    result = result.Replace("Date(-", "Date(");
                    var output = JObject.Parse(result);

                    if (Enumerable.Count(output["d"]["results"][0]["ReservationNaviGuest"]["results"]) > 0)
                    {
                        savedSignatureCount = 0;
                        for (int i = 0; i < Enumerable.Count(output["d"]["results"][0]["ReservationNaviGuest"]["results"]); i++)
                        {
                            string guestCountryKeyValue = string.Empty;
                            string guestSalutationKeyValue = string.Empty;
                            //Setting values to global variables
                            guestNameAvailabiliyty = serviceDataValidation.validation(Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][i]["Xnombre"]));
                            layoutHeightGuestDetails = layoutHeightGuestDetails + 40;
                            if (guestNameAvailabiliyty != Constants._notAvailable)
                            {
                                if (lasyoutHeighSignatureInitial == 1)
                                {
                                    layoutHeightSignatures = 187;
                                }
                                else
                                {
                                    layoutHeightSignatures = layoutHeightSignatures + 157;
                                }

                            }
                            //Signaure Manually Not added
                            if (Constants._notAvailableSignatureAdded == false)
                            {
                                if (guestNameAvailabiliyty != Constants._notAvailable)
                                {
                                    try
                                    {
                                        string result1 = await checkInManager.GetGuestSignature(guestNumberCount.ToString());
                                        if (result1 != null)
                                        {

                                            var output1 = JObject.Parse(result1);

                                            //Removing ticks error
                                            result1 = result1.Replace("Date(-", "Date(");

                                            //Converting base64 string to byte array
                                            byte[] data = Convert.FromBase64String(Convert.ToString(output1["d"]["XIMAGE"]));

                                            guestsignature.Add(new guestSignature(ImageSource.FromStream(() => new MemoryStream(data)), guestNameAvailabiliyty, "White", guestNumber, Constants._available, "Purple", ""));
                                            savedSignatureCount++;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        //Guest Signature Not Available
                                        string addSignatureImage = "SignatureImage.jpg";
                                        if (guestNameAvailabiliyty != Constants._notAvailable)
                                        {
                                            guestsignature.Add(new guestSignature(addSignatureImage,
                                                                                  guestNameAvailabiliyty, "Purple",
                                                                                  guestNumber,
                                                                                  Constants._notAvailable, "White", ""));
                                        }
                                    }
                                }
                            }
                            String dateString = "";
							string expiryDateString = "";

                            DateTime date = DateTime.Now;
                            try
                            {
                                string dateOfBirth = Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][i]["Gbdat"]);

                                if (dateOfBirth != "")
                                {

                                    DateTime dt = DateTime.Now;
                                    int index = i + 1;
                                    string temp_substring = tem_resultMain.Substring(0, tem_resultMain.IndexOf("Xorden\":\"" + "0" + index + "\""));
                                    //temp_substring = temp_substring.Substring(0, temp_substring.IndexOf("Gbdat") + 20);
                                    //temp_substring = temp_substring.Substring(temp_substring.IndexOf("Gbdat"));
                                    temp_substring = temp_substring.Substring(temp_substring.LastIndexOf("Gbdat"));
                                    temp_substring = temp_substring.Substring(0, 20);
                                    //Begin of + Vinoch 15122061
                                    if (temp_substring.Contains("-"))
                                    {
                                        DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                                        long ms = (long)(DateTime.Parse(dateOfBirth) - epoch).TotalMilliseconds;
                                        dateOfBirth = ms.ToString();

                                        dateOfBirth = "-" + dateOfBirth;

                                        long milliSec = (long)(Convert.ToDouble(dateOfBirth));
                                        DateTime startTime = new DateTime(1970, 1, 1);

                                        TimeSpan time = TimeSpan.FromMilliseconds(milliSec);
                                        dt = startTime.Add(time);
                                    }
                                    else
                                    {
                                        if (dateOfBirth != "")//- Vinoch 1512201
                                            dt = DateTime.Parse(dateOfBirth); // Vinoch 1512201
                                    }

                                    //DateTime date = serviceDataValidation.dateOfBirthValidation(dateOfBirth);
                                    date = dt;
                                    dateString = date.ToString();
                                }
                                else
                                {
                                    dateString = "";
									expiryDateString = "";
                                }
                            }
                            catch (Exception)
                            {
                                dateString = "";
								expiryDateString = "";
                            }

                            string country = Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][i]["Country"]);

                            var nameToCountry = CountryDictionary.listOfCountrie();
                            if (country != "" || country != "")
                            {
                                string name = nameToCountry.FirstOrDefault(x => x.Value == country).Key;
                                guestCountryKeyValue = country;
                            }
                            guestdetails.Add(new guestDetails(guestNumberCount,
                                                              Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][i]["XtipoDocId"]),
                                                              Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][i]["XnumeroDoc"]),
                                                              Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][i]["Title"]),
                                                              guestNameAvailabiliyty,
                                                              Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][i]["Name1"]),
                                                              Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][i]["Name2"]),
                                                              Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][i]["Parge"]),
                                                              Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][i]["SmtpAddr"]),
                                                              Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][i]["MobileNo"]),
                                                              Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][i]["HouseNum1"]),
                                                              Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][i]["Street"]),
                                                              Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][i]["City1"]),
                                                              Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][i]["Country"]),
                                                              guestCountryKeyValue,
                                                              Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][i]["Xnacionalidad"]),
                                                              dateString,
                                                              expiryDateString,
                                                              Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][i]["Langu"]),
                                                              "",
                                                              "",
                                                              "",
                                                              "",
                                                              "",
                                                              "",
                                                              ""));

                            //Increment guestnumber and guest number count by 1
                            guestNumber = guestNumber + 1;
                            guestNumberCount = guestNumberCount + 1;
                            lasyoutHeighSignatureInitial = 0;
                        }

                        //Setting stack Layput heights according to the number of records
                        GuestDetails.HeightRequest = layoutHeightGuestDetails;
                        GuestSignatureView.HeightRequest = layoutHeightSignatures;

                    }
                    Device.BeginInvokeOnMainThread(() =>
                                {
                                    //Setting guest number in label
                                    GuestNumber.Text = (guestNumberCount - 1).ToString();

                                    //Adding list to observable Collection
                                    items = new ItemList(guestdetails);
                                    GuestNameList.ItemsSource = items.Items;


                                    //Set Guest Signatures. Signatures loaded from service
                                    if (Constants._notAvailableSignatureAdded == false)
                                    {
                                        GuestSignatureList.ItemsSource = guestsignature;
                                    }
                                    else
                                    {

                                        if (Constants._reservationStatus == Constants._reservationStatusCheckedIn)
                                        {
                                            //checkinAndSaveButton.IsVisible = true;
                                            saveSignatureButton.IsVisible = true;//true
                                            checkinButton.IsVisible = false;
                                        }
                                        else if (Constants._reservationStatus == Constants._reservationStatusPending)
                                        {
                                            //checkinAndSaveButton.IsVisible = true;
                                            saveSignatureButton.IsVisible = true;//false
                                            checkinButton.IsVisible = true;
                                        }

                                    }

                                    if (Constants._reservationStatus != Constants._reservationStatusCheckedIn && savedSignatureCount > 0)
                                    {
                                        checkinAndSaveButton.IsVisible = true;
                                    }
                                    else
                                    {
                                        checkinAndSaveButton.IsVisible = false;
                                    }

                                    if (savedSignatureCount == guestsignature.Count)
                                    {
                                        saveSignatureButton.IsVisible = false;
                                    }
                                });
                }
            }
            catch (Exception e)
            {
                this.DisplayGuestDetails();
            }
            stopPageLoading();
        }

        async void getRemarksDetails()
        {
            RemarkDetails remarkResult = await mainGuestInformation.remarkInformation();
            if (remarkResult != null)
            {
                DisplayRemarkDetails(remarkResult);
            }
        }

        //remark details
        public void DisplayRemarkDetails(RemarkDetails remarkResult)
        {
            //mainRemark.Text = remarkResult.MainRemark;
            //generalRemark.Text = remarkResult.SubRemark;

            //if (remarkResult.MainRemark == "" && (remarkResult.SubRemark == "" || remarkResult.SubRemark == null))
            //{
            //	remarksUnavailabilityIndicator.IsVisible = true;
            //}
            //else {
            //	//Main Remark not Available
            //	if (remarkResult.MainRemark == "")
            //	{
            //		mainRemarkName.IsVisible = false;
            //		remarksGrid.IsVisible = true;
            //		mainRemark.IsVisible = false;
            //	}

            //	//Sub Remark not Available
            //	if (remarkResult.SubRemark == "" || remarkResult.SubRemark == null)
            //	{
            //		generalRemarkName.IsVisible = false;
            //		remarksGrid.IsVisible = true;
            //		generalRemark.IsVisible = false;
            //	}
            //	else {
            //		mainRemarkName.IsVisible = true;
            //		generalRemarkName.IsVisible = true;
            //		remarksGrid.IsVisible = true;
            //		mainRemark.IsVisible = true;
            //		generalRemark.IsVisible = true;
            //	}
            //}
        }

        //Set Dep Flight buton selected
		async void SetDepartureFlight(object sender, EventArgs e)
		{
			DepartureFlightView flights = new DepartureFlightView(DepartureFlightText);
			await Navigation.PushAsync(flights);
		}

		//Set Arr Flight buton selected
        async void SetArrivalFlight(object sender, EventArgs e)
        {
			ArrivalFlightView flights = new ArrivalFlightView(ArrivalFlightText);
            await Navigation.PushAsync(flights);
        }

		//Add Signature Selected
        async void SelectedGuestSignature(object sender, ItemTappedEventArgs e)
        {
            var guestSignatureObject = guestsignature;
            var guestSignature = (guestSignature)e.Item;
            GuestSignatureList.SelectedItem = Color.Transparent; //use the color of your background of the listView
                                                                 //Guest Signature not available
            if (guestSignature.imageAvailability == Constants._notAvailable)
            {
                Constants._number = guestSignature.guestNumber;
                await Navigation.PushModalAsync(new Signature(guestSignatureObject, guestSignature));
            }
            else
            {
                await DisplayAlert(Constants._headerMessage, Constants._signatureCannotBeUdated, Constants._buttonOkay);
            }

        }

        //Guest Selected
        async void SelectedGuestName(object sender, ItemTappedEventArgs e)
        {
            var guestDetailsObject = (guestDetails)e.Item;
            await Navigation.PushAsync(new GuestEdit(guestdetails, guestDetailsObject));
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


        //Checking button clicked
        async void reservationCheckin(object sender, EventArgs e)

        {
            //Asssigned Room Inspected
            if (Constants._roomStatus == Constants._inspectedRoom)
            {
                string result = "";
                //get sigatures with image availability
                var signatureAddedGuests = guestsignature.Where(x => x.imageAvailability == "").ToList();
                pageLoading();
                string IntiailGuestDetail = "F";
                //Loop All available images
                foreach (guestSignature sigAddedGuestDetails in signatureAddedGuests)
                {
                    if (sigAddedGuestDetails.base64String != "")
                    {
                        //Data source
                        var postServiceManager = new PostServiceManager();

                        //Add details to Payload
                        StatusChangeCheckin statusChangedCheckin = new StatusChangeCheckin(Constants._reservation_id, Constants._hotel_code, sigAddedGuestDetails.guestNumber.ToString(), sigAddedGuestDetails.base64String, IntiailGuestDetail, "Checkin App");

                        result = await postServiceManager.StatusChangecheckinAsync(statusChangedCheckin);
                    }

                    Constants._base64Code = "";
                    IntiailGuestDetail = "T";
                }

                //save signature if already saved signature
                if (signatureAddedGuests.Count < 1)
                {
                    var postServiceManager = new PostServiceManager();

                    string resSigns = await checkInManager.GetGuestSignature("1");
                    var outputSign = JObject.Parse(resSigns);
                    var retrivedSignBase64 = Convert.ToString(outputSign["d"]["XIMAGE"]);

                    if(retrivedSignBase64 != null)
                    {
                        IntiailGuestDetail = "F";
                        StatusChangeCheckin statusChangedCheckin = new StatusChangeCheckin(Constants._reservation_id, Constants._hotel_code, "1", retrivedSignBase64, IntiailGuestDetail, "Checkin App");
                        result = await postServiceManager.StatusChangecheckinAsync(statusChangedCheckin);
                    }
                    IntiailGuestDetail = "T";

                }


                await DisplayAlert(Constants._headerMessage, result, Constants._buttonOkay);

                if (result == "Checked-In Successfully!")
                {
                    Constants._reservationStatus = Constants._reservationStatusCheckedIn;
                    Constants._notAvailableSignatureAdded = false;

					//Save usage time to API
					var apires = await APIPostService.SaveTimeTrackToAPI();

					Debug.WriteLine("API Res " + apires);

                    //Checked in
                    MessagingCenter.Send<RegistrationCard, string>(this, Constants._reservationStatusCheckedIn, Constants._reservationStatus);
                }
                stopPageLoading();
            }
            else
            {
                await DisplayAlert(Constants._headerMessage, Constants._checkinInspectedValidation, Constants._buttonOkay);
            }
        }

        //Signature save clicked
        async void saveSignature(object sender, EventArgs e)
        {
            string result = "";

            //get details where image is available
            var signatureAddedGuests = guestsignature.Where(x => x.imageAvailability == "").ToList();
            pageLoading();
            //Loop All images
            foreach (guestSignature sigAddedGuestDetails in signatureAddedGuests)
            {
                if (sigAddedGuestDetails.base64String != "")
                {
                    //Data service
                    var postServiceManager = new PostServiceManager();

                    //Add details to payload
                    var statusChangedCheckin = new StatusChangeCheckin(Constants._reservation_id, Constants._hotel_code, sigAddedGuestDetails.guestNumber.ToString(), sigAddedGuestDetails.base64String, "T", "Checkin App");

                    result = await postServiceManager.StatusChangecheckinAsync(statusChangedCheckin);
                }
                Constants._base64Code = "";
                Constants._notAvailableSignatureAdded = false;
            }
            if (result == "Checked-In Successfully!")
            {
                await DisplayAlert(Constants._headerMessage, Constants._signatureSuccessfullySaved, Constants._buttonOkay);

                //Signatuers saved
                MessagingCenter.Send<RegistrationCard, string>(this, Constants._signatureSuccessfullySaved, "");
            }
            else
            {
                await DisplayAlert(Constants._headerMessage, result, Constants._buttonOkay);
            }

        }

        //View Performa Button
        async void viewPrforma(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Performa(Constants._reservation_id, emailaddress.Text));
        }


        void pageLoading()
        {
            ReservationsListIndicator.IsVisible = true;
            ReservationsListIndicator.IsRunning = true;
            registrationCardDetailsIndicator.IsVisible = false;
            //	//remarksGrid.IsVisible = false;                       //Grid to view remarks in	
            //	remarksUnavailabilityIndicator.IsVisible = false;
        }

        void stopPageLoading()
        {
            registrationCardDetailsIndicator.IsVisible = true;
            ReservationsListIndicator.IsVisible = false;
            ReservationsListIndicator.IsRunning = false;
        }

        //Loading All methods
        void loadAllMethods()
        {
            getMainGuestDetails();
            DisplayGuestDetails();
        }

        //Scroll to Guest details List View
        //async void scrollToguestDetails()
        //{
        //	await myscrollview.ScrollToAsync(GuestDetailsSection, ScrollToPosition.Start, true);
        //}

        //Observable Collection Items
        public class ItemList : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            public ObservableCollection<guestDetails> _items;

            public ObservableCollection<guestDetails> Items
            {
                get { return _items; }
                set
                {
                    _items = value;
                    OnPropertyChanged("Items");
                }
            }

            protected virtual void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged == null)
                    return;
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

            public ItemList(List<guestDetails> itemList)
            {
                Items = new ObservableCollection<guestDetails>();
                foreach (guestDetails itm in itemList)
                {
                    Items.Add(itm);
                }
            }
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
		}

        //Logout button clicked
        async void LogoutButtonClickedEvt(object sender, EventArgs e)
        {
            userLogout.logout();
            //Navigate to Login Page
        }

		void homeClicked(object sender, EventArgs e)
		{
			new HomeNavigater().GoHome();
		}

		void guestCommentEvt(object sender, EventArgs e)
        {
            //var nameToURL = CountryDictionary.listOfURL();
            //string name = nameToURL.FirstOrDefault(x => x.Value == Constants._hotel_code).Key;
            //Device.OpenUri(new Uri("http://chml.keells.lk/GuestComments/GCC/GCCeConcierge.aspx?HtlCode=" + name + ""));
            if (guestEmail != "" && guestEmail != Constants._notAvailable)
            {
                Device.OpenUri(new Uri("http://chml.keells.lk/GuestComments/GCC/GuestCommentsCard.aspx?TmsID=" + Constants._hotel_code + "&VNo=" + Constants._reservation_id + "&Email=" + guestEmail + ""));

            }
            else
            {
                Device.OpenUri(new Uri("http://chml.keells.lk/GuestComments/GCC/GuestCommentsCard.aspx?TmsID=" + Constants._hotel_code + "&VNo=" + Constants._reservation_id + ""));

            }
        }

        void switcher_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value == true)
            {
                MessagingCenter.Send<RegistrationCard, string>(this, "agreed", "");
            }
            else
            {
                MessagingCenter.Send<RegistrationCard, string>(this, "notAgreed", "");
            }
        }
    }
}

