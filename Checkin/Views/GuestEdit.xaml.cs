﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using System.Linq;
using Xamarin.Forms.Xaml;
using System.Text.RegularExpressions;
using System.Globalization;
using Checkin.Models.ModelClasses.Payloads;
using System.Diagnostics;
using Checkin.Data.Posting;
using Newtonsoft.Json;
using Microblink.Forms.Core;
using Microblink.Forms.Core.Recognizers;
using Microblink.Forms.Core.Overlays;
using static Microblink.Forms.Core.Messages;
using Plugin.Media.Abstractions;
using System.Threading.Tasks;
using System.IO;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Checkin
{
    public partial class GuestEdit : ContentPage
    {

        /// <summary>
        /// Microblink scanner is used for scanning the identity documents.
        /// </summary>
        IMicroblinkScanner blinkID;

        /// <summary>
        /// MRTD recognizer will be used for scanning Machine Readable Travel Documents (MRTDs), such as IDs and passports.
        /// </summary>
        IMrtdRecognizer mrtdRecognizer;

        /// <summary>
        /// This success frame grabber recognizer will wrap mrtdRecognizer and will contain camera frame of the moment
        /// when wrapped recognizer finished its recognition.
        /// </summary>
        ISuccessFrameGrabberRecognizer mrtdSuccessFrameGrabberRecognizer;

        //Data Source
        CheckInManager checkInManager = new CheckInManager();


        //Global Varialbles
        string guestIdentification = string.Empty, guestGender = string.Empty, guestCountry = string.Empty, guestLanguage = string.Empty, guestNationality = string.Empty, guestSalutation = string.Empty;

        //Information from guest search for guest profile
        string guestCodeFromSearch = string.Empty, Visitperhotel = string.Empty, Totalvisit = string.Empty, RevenueTotal = string.Empty, RevenueRoom = string.Empty, RevenueFnb = string.Empty, RevenueOther = string.Empty;

        string fname = string.Empty, lname = string.Empty, nationality = string.Empty, gender = string.Empty, PassportNumber = string.Empty, dateOfBirthPass = string.Empty, dateOfExpiry = string.Empty;
        List<guestDetails> guestdetails = new List<guestDetails>();

        public GuestEdit(List<guestDetails> guestDetailsList, guestDetails Object)
        {
            //Passing values value on page load to fields and pickers
            InitializeComponent();

            //Initialize Blink ID
            InitBlinkID();

            //Setting limits for datetime.
            DateTime date = new DateTime(1900, 1, 1);
            DateOfBirth.MaximumDate = DateTime.Today;
            DateOfBirth.MinimumDate = date;

            //setting guest details retrieved from guestifo page
            guestdetails = guestDetailsList;
            guestCodeFromSearch = Object.guestCode;
            Visitperhotel = Object.noOfVisitsHotel;
            Totalvisit = Object.noOfVisits;
            RevenueTotal = Object.revenueTotal;
            RevenueRoom = Object.revenueRoom;
            RevenueFnb = Object.reveneuFB;
            RevenueOther = Object.revenueOther;

            //countryFromSearch = Object.country;
            if (Object.passportIdNumber == "")
            {
                hideUntilSearched.IsVisible = false;
                viewUntilSearched.IsVisible = true;
            }
            else
            {
                hideUntilSearched.IsVisible = true;
                viewUntilSearched.IsVisible = false;
            }

            pageLoading();

            guestDetailsAvailableOnLoad((Object.guestNumber).ToString(), Object.identificationMethod, Object.passportIdNumber, Object.salutation, Object.guestFirstName, Object.guestLastName, Object.gender, Object.email, Object.contactNumber, Object.houseNumber, Object.street, Object.city, Object.country, Object.nationality, Object.language, Object.guestCode, Object.dateOfBirth, Object.dateOfExpiry);

            //Getting results after passport successfully scanned
            MessagingCenter.Subscribe<Messages.BlinkIDResults>(this, Messages.BlinkIDResultsMessage, async (sender) =>
            {
                pageLoading();
                try
                {
                    //Values from passport
                    foreach (var result in sender.Results)
                    {
                        //Seperating values from array
                        result.TryGetValue("SecondaryId", out fname);
                        result.TryGetValue("PrimaryId", out lname);
                        result.TryGetValue("Nationality", out nationality);
                        result.TryGetValue("Sex", out gender);
                        result.TryGetValue("DocumentNumber", out PassportNumber);
                        result.TryGetValue("DateOfExpiry", out dateOfExpiry);
                        result.TryGetValue("DateOfBirth", out dateOfBirthPass);

                        PassportNumber = Regex.Replace(PassportNumber, "[^A-Za-z0-9 _]", "");
                        //List of identification methods from dictionary
                        var nameToAlpha2FromAlpha3 = CountryDictionary.listAlpha3To();
                        //Getting Key Value from Dictoinary by passing the THREE Letter code
                        nationality = nameToAlpha2FromAlpha3.FirstOrDefault(x => x.Key == nationality).Value;
                        //string guestNumberPassport = guestNumber.Text;
                    }

                    new APILogger().Logger($"PP Scan Result : FName {fname},LName {lname},Nationality {nationality},Sex {gender},DocumentNumber {PassportNumber},DateOfExpiry {dateOfExpiry},DateOfBirth {dateOfBirthPass}");

                    await existingGuestDetailsFromDatabses(2, PassportNumber);
                    Device.BeginInvokeOnMainThread(() =>
                            {
                                GuestFisrtName.Text = fname;
                                GuestLastName.Text = lname;
                                guestIdentificationDetailsPicker("2");
                                guestNationalityDetailsPicker(nationality);
                                guestContryDetailsPicker(nationality);
                                guestLanguageDetailsPicker("E");
                                PassportExpiry.Date = FormatChanges.PassScanDateFormat(dateOfExpiry);
                                guestGenderDetailsPicker(serviceDataValidation.guestEditGenderValidation(gender));
                                DateOfBirth.Date = FormatChanges.PassScanDateFormat(dateOfBirthPass);
                                stopPageLoading();
                            });

                }
                catch (Exception)
                {
                    Device.BeginInvokeOnMainThread(() =>
                            {
                                stopPageLoading();
                                guestEntryDetails(guestNumber.Text, PassportNumber, fname, lname, "", "", "", "", "");
                                guestIdentificationDetailsPicker("2");
                                guestNationalityDetailsPicker(nationality);
                                guestContryDetailsPicker(nationality);
                                guestLanguageDetailsPicker("E");
                                guestSalutationPicker("");
                                guestGenderDetailsPicker(serviceDataValidation.guestEditGenderValidation(gender));
                                PassportExpiry.Date = FormatChanges.PassScanDateFormat(dateOfExpiry);
                                DateOfBirth.Date = FormatChanges.PassScanDateFormat(dateOfBirthPass);
                                Visitperhotel = "00";
                                Totalvisit = "00";
                                RevenueTotal = "0.00";
                                RevenueRoom = "0.00";
                                RevenueFnb = "0.00";
                                RevenueOther = "0.00";
                                stopPageLoading();
                                DisplayAlert(Constants._headerMessage, Constants._noDetails, Constants._buttonOkay);
                            });
                }
                //MessagingCenter.Unsubscribe<Messages.BlinkIDResults>(this, Messages.BlinkIDResultsMessage);
            });

        }

        void guestDetailsPassportScanner()
        {

        }

        public void HouseNumberTextChanged(object sender, EventArgs e)
        {
            int _limit = 10;
            var entry = (Entry)sender;

            string _text = entry.Text;      //Get Current Text
            if (_text.Length > _limit)       //If it is more than your character restriction
            {
                _text = _text.Remove(_text.Length - 1);  // Remove Last character
                entry.Text = _text;        //Set the Old value
            }


        }

        async void ScanPassport(object sender, EventArgs e)
        {
            //Detail Page Configuration
            try
            {
                StartScan();
                hideUntilSearched.IsVisible = true;
                viewUntilSearched.IsVisible = false;
            }
            catch (Exception)
            {
                await DisplayAlert(Constants._headerMessage, Constants._scannerError, Constants._buttonOkay);
            }
        }

        //Search button clieced event
        async void SearchButtonClicked(object sender, EventArgs e)
        {
            pageLoading();
            //Setting identification method and numbers as constants
            int identificationMethod = (IdentificationMethod.SelectedIndex) + 1;
            if (identificationMethod == 0)
            {
                stopPageLoading();
                await DisplayAlert(Constants._headerMessage, Constants._selectIdentificationMethod, Constants._buttonOkay);

            }
            else
            {
                hideUntilSearched.IsVisible = true;
                viewUntilSearched.IsVisible = false;
                string indetificationMethodNumber = IdentificationMethodNumber.Text;
                try
                {
                    await existingGuestDetailsFromDatabses(identificationMethod, indetificationMethodNumber);
                    stopPageLoading();
                }
                catch (Exception)
                {
                    await DisplayAlert(Constants._headerMessage, Constants._noDetails, Constants._buttonOkay);
                    stopPageLoading();
                }
            }
        }

        async System.Threading.Tasks.Task existingGuestDetailsFromDatabses(int identificationMethod, string indetificationMethodNumber)
        {
            string result = await checkInManager.GetGuestDetailsThroughPassportNumber(identificationMethod.ToString(), indetificationMethodNumber);
            if (result != null || result != "Error")
            {
                var jObj = JObject.Parse(result);
                string ID = jObj["d"]["results"][0]["XtipoDocId"].ToString();
                String guestNumberSearch = guestNumber.Text;


                string pNumber = jObj["d"]["results"][0]["XnumeroDoc"].ToString();
                string fname = jObj["d"]["results"][0]["Name1"].ToString();
                string lname = jObj["d"]["results"][0]["Name2"].ToString();
                string email = jObj["d"]["results"][0]["SmtpAddr"].ToString();
                string cnumber = jObj["d"]["results"][0]["MobileNo"].ToString();
                string hnumber = jObj["d"]["results"][0]["HouseNum1"].ToString(); ;
                string street = jObj["d"]["results"][0]["Street"].ToString();
                string city = jObj["d"]["results"][0]["City1"].ToString();
                string country = jObj["d"]["results"][0]["Country"].ToString();
                string gender = jObj["d"]["results"][0]["Parge"].ToString();
                string nationality = jObj["d"]["results"][0]["Xnacionalidad"].ToString();
                //string language = jObj["d"]["results"][0]["Langu"].ToString();
                string guestcode = jObj["d"]["results"][0]["Kunnr"].ToString().TrimStart(new Char[] { '0' });
                string dob = jObj["d"]["results"][0]["Gbdat"].ToString();
                string doe = jObj["d"]["results"][0]["XfechaExpiry"].ToString();
                string salutation = jObj["d"]["results"][0]["Title"].ToString();

                Visitperhotel = jObj["d"]["results"][0]["Visitperhotel"].ToString().TrimStart(new Char[] { '0' }).Trim();
                Totalvisit = jObj["d"]["results"][0]["Totalvisit"].ToString().TrimStart(new Char[] { '0' }).Trim();
                RevenueTotal = jObj["d"]["results"][0]["RevenueTotal"].ToString().Trim();
                RevenueRoom = jObj["d"]["results"][0]["RevenueRoom"].ToString().Trim();
                RevenueFnb = jObj["d"]["results"][0]["RevenueFnb"].ToString().Trim();
                RevenueOther = jObj["d"]["results"][0]["RevenueOther"].ToString().Trim();


                string historyDetails = "Visits to Hotel :" + "" + Visitperhotel + "\n" +
                                         "Total Visits in all hotels :" + "" + Totalvisit + "\n" +
                                                                                 "Revenue Total :" + "" + RevenueTotal + "\n" +
                                                                                 "Revenue Room :" + "" + RevenueRoom + "\n" +
                                                                                 "Revenue F&B :" + "" + RevenueFnb + "\n" +
                                                                                 "Revenue Other :" + "" + RevenueOther + "\n";

                await DisplayAlert(Constants._headerHostoryDetails, historyDetails, Constants._buttonOkay);

                Device.BeginInvokeOnMainThread(() =>
                        {
                            guestEntryDetails(guestNumberSearch, pNumber, fname, lname, email, cnumber, hnumber, street, city);
                            guestIdentificationDetailsPicker(ID);
                            guestContryDetailsPicker(country);
                            guestGenderDetailsPicker(gender);
                            guestNationalityDetailsPicker(nationality);
                            guestLanguageDetailsPicker("");
                            guestSalutationPicker(salutation);
                            guestCodeFromSearch = guestcode;
                            DateOfBirth.Date = serviceDataValidation.dateOfBirthValidation(dob);
                            if (!string.IsNullOrEmpty(doe))
                            {
                                PassportExpiry.Date = serviceDataValidation.dateOfExpiryValidation(doe);
                            }

                        });
            }

            new APILogger().Logger($"Guest Search Result : {result} *** ID Type : {identificationMethod} ***  ID Number : {identificationMethod}");
        }

        //Get Passport expiry

        async System.Threading.Tasks.Task GETPPExpiryFromDatabses(int identificationMethod, string indetificationMethodNumber)
        {
            try
            {
                string result = await checkInManager.GetGuestDetailsThroughPassportNumber(identificationMethod.ToString(), indetificationMethodNumber);
                if (result != null || result != "Error")
                {
                    var jObj = JObject.Parse(result);

                    string doe = jObj["d"]["results"][0]["XfechaExpiry"].ToString();

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if (!string.IsNullOrEmpty(doe))
                        {
                            PassportExpiry.Date = serviceDataValidation.dateOfExpiryValidation(doe);
                            Debug.WriteLine(PassportExpiry.Date);
                        }

                    });
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Passport ex date service error");
            }
        }

        void guestdetailsOnloadAndSearch(string guestNumberCount, string identificationMethod, string passportIdNumber, string salutation, string guestFirstName, string guestLastName, string gender, string email, string contactNumber, string houseNumber, string street, string city, string country, string nationality, string language, string GuestCode, string dateOfBirth, string dateOfExp)
        {
            pageLoading();

            guestDetailsAvailableOnLoad(guestNumberCount, identificationMethod, passportIdNumber, salutation, guestFirstName, guestLastName, gender, email, contactNumber, houseNumber, street, city, country, nationality, language, GuestCode, dateOfBirth, dateOfExp);

        }

        //Load items into content page if already available
        async void guestDetailsAvailableOnLoad(string guestNumberCount, string identificationMethod, string passportIdNumber, string salutation, string guestFirstName, string guestLastName, string gender, string email, string contactNumber, string houseNumber, string street, string city, string country, string nationality, string language, string GuestCode, string dateOfBirth, string dateOfExp)
        {
            guestEntryDetails(guestNumberCount, passportIdNumber, guestFirstName, guestLastName, email, contactNumber, houseNumber, street, city);
            guestIdentificationDetailsPicker(identificationMethod);
            guestNationalityDetailsPicker(nationality);
            guestContryDetailsPicker(country);
            guestGenderDetailsPicker(gender);
            guestLanguageDetailsPicker(language);
            guestSalutationPicker(salutation);
            if (dateOfBirth != "")
                DateOfBirth.Date = DateTime.Parse(dateOfBirth);
            if (dateOfExpiry != "")
                PassportExpiry.Date = DateTime.Parse(dateOfExp);
            guestCodeFromSearch = GuestCode;

            if (!string.IsNullOrEmpty(passportIdNumber))
            {
                await GETPPExpiryFromDatabses(Convert.ToInt32(identificationMethod), passportIdNumber);
            }
        }

        //Pass available values into entry fields if vailable on load
        void guestEntryDetails(string guestNumberCount, string passportIdNumber, string guestFirstName, string guestLastName, string email, string contactNumber, string houseNumber, string street, string city)
        {
            guestNumber.Text = guestNumberCount;
            IdentificationMethodNumber.Text = passportIdNumber;
            GuestFisrtName.Text = guestFirstName;
            GuestLastName.Text = guestLastName;
            GuestEmail.Text = email;
            GuestContact.Text = contactNumber;
            HouseNumber.Text = houseNumber;
            Street.Text = street;
            City.Text = city;
            stopPageLoading();
        }

        void guestNationalityDetailsPicker(string nationlity)
        {
            Nationality.Items.Clear();
            var nameToNationalityList = CountryDictionary.listOfNationalitie();
            foreach (string NationalityName in nameToNationalityList.Keys)
            {
                Nationality.Items.Add(NationalityName);
            }

            if (nationlity != "" || nationlity != "")
            {
                //Item value in dictionary
                string name = nameToNationalityList.FirstOrDefault(x => x.Value == nationlity).Value;

                //Index of item value in dictionary
                int index = nameToNationalityList.Values.ToList().IndexOf(name);

                //Set picker selecteditem index
                Nationality.SelectedIndex = index;

                //Set Identification Method to Update guest
                guestNationality = nationlity;
            }


            Nationality.SelectedIndexChanged += (sender, args) =>
            {
                if (Nationality.SelectedIndex == -1)
                {
                    guestNationality = nationlity;
                }
                else
                {
                    string nationalityName = Nationality.Items[Nationality.SelectedIndex];
                    guestNationality = nameToNationalityList[nationalityName];
                }
            };
        }

        void guestSalutationPicker(string salutation)
        {
            Salutation.Items.Clear();
            var nameToSalutationList = CountryDictionary.listofSalutation();
            foreach (string SalutationName in nameToSalutationList.Keys)
            {
                Salutation.Items.Add(SalutationName);
            }

            if (salutation != "" || salutation != "")
            {
                //Item value in dictionary
                string name = nameToSalutationList.FirstOrDefault(x => x.Value == salutation).Value;

                //Index of item value in dictionary
                int index = nameToSalutationList.Values.ToList().IndexOf(name);

                //Set picker selecteditem index
                Salutation.SelectedIndex = index;

                //Set Identification Method to Update guest
                guestSalutation = salutation;
            }


            Salutation.SelectedIndexChanged += (sender, args) =>
            {
                if (Salutation.SelectedIndex == -1)
                {
                    guestSalutation = salutation;
                }
                else
                {
                    string nationalityName = Salutation.Items[Salutation.SelectedIndex];
                    guestSalutation = nameToSalutationList[nationalityName];
                }
            };
        }

        //Guest Nationality/Passport pickers
        void guestIdentificationDetailsPicker(string identificationMethod)
        {
            IdentificationMethod.Items.Clear();
            //List of identification methods from dictionary
            var nameToIdentification = CountryDictionary.listofIdentificationMethod();

            //Adding all identiication methods into picker
            foreach (string identificationName in nameToIdentification.Keys)
            {
                IdentificationMethod.Items.Add(identificationName);
            }

            //if values are not available on load
            if (identificationMethod != "" || identificationMethod != "")
            {
                //Item value in dictionary
                string name = nameToIdentification.FirstOrDefault(x => x.Value == identificationMethod).Value;

                //Index of item value in dictionary
                int index = nameToIdentification.Values.ToList().IndexOf(name);

                //Set picker selecteditem index
                IdentificationMethod.SelectedIndex = index;

                //Set Identification Method to Update guest
                guestIdentification = identificationMethod;
            }

            //Index of picker changed
            IdentificationMethod.SelectedIndexChanged += (sender, args) =>
            {
                if (IdentificationMethod.SelectedIndex == -1)
                {
                    guestIdentification = identificationMethod;
                }
                else
                {
                    //Get key value of selected item
                    string identificationName = IdentificationMethod.Items[IdentificationMethod.SelectedIndex];

                    //Set picker value 
                    guestIdentification = nameToIdentification[identificationName];
                }
            };
        }

        //Guest Country pickers
        void guestContryDetailsPicker(string country)
        {
            GuestCountry.Items.Clear();
            //List of Countries from dictionary
            var nameToCountry = CountryDictionary.listOfCountrie();

            //Adding all countries into picker
            foreach (string countryName in nameToCountry.Keys)
            {
                GuestCountry.Items.Add(countryName);
            }

            //if values are not available on load
            if (country != "" || country != "")
            {
                //Item Value in dictionary
                string name = nameToCountry.FirstOrDefault(x => x.Value == country).Value;

                //Index of item in dictionary
                int index = nameToCountry.Values.ToList().IndexOf(name);

                //Set picker selected item index
                GuestCountry.SelectedIndex = index;

                //Set country to update guest
                guestCountry = country;
            }

            //Index of picker changed
            GuestCountry.SelectedIndexChanged += (sender, args) =>
            {
                if (GuestCountry.SelectedIndex == -1)
                {
                    guestCountry = country;
                }
                else
                {

                    //Get key value of selected item
                    string countryName = GuestCountry.Items[GuestCountry.SelectedIndex];

                    //Set picker value 
                    guestCountry = nameToCountry[countryName];
                }
            };
        }

        //Guest Gender pickers
        void guestGenderDetailsPicker(string gender)
        {
            Gender.Items.Clear();
            var nameToGender = CountryDictionary.listOfGender();
            foreach (string genderItems in nameToGender.Keys)
            {
                Gender.Items.Add(genderItems);
            }
            if (gender != "" || gender != "")
            {
                string name = nameToGender.FirstOrDefault(x => x.Value == gender).Value;
                int index = nameToGender.Values.ToList().IndexOf(name);
                Gender.SelectedIndex = index;
                guestGender = gender;
            }
            Gender.SelectedIndexChanged += (sender, args) =>
            {
                if (Gender.SelectedIndex == -1)
                {
                    guestGender = gender;
                }
                else
                {
                    string genderItems = Gender.Items[Gender.SelectedIndex];
                    guestGender = nameToGender[genderItems];
                }
            };
        }

        //Guest Language picker
        void guestLanguageDetailsPicker(string language)
        {
            Language.Items.Clear();
            var nameToLanguage = CountryDictionary.listOfLanguage();
            foreach (string LanguageName in nameToLanguage.Keys)
            {
                Language.Items.Add(LanguageName);
            }
            if (language != "" || language != "")
            {
                string name = nameToLanguage.FirstOrDefault(x => x.Value == language).Value;
                int index = nameToLanguage.Values.ToList().IndexOf(name);
                Language.SelectedIndex = index;
                guestLanguage = language;
            }
            Language.SelectedIndexChanged += (sender, args) =>
            {
                if (Language.SelectedIndex == -1)
                {
                    guestLanguage = language;
                }
                else
                {
                    string languageName = Language.Items[Language.SelectedIndex];
                    guestLanguage = nameToLanguage[languageName];
                }
            };
        }
        /*
        //Update Guest Details
        async void saveButton(object sender, EventArgs e)
        {

            string validation = FieldValidation.guestSaveDetailsValidation(IdentificationMethod, IdentificationMethodNumber.Text, GuestFisrtName.Text, GuestLastName.Text, DateOfBirth, GuestContact.Text, GuestEmail.Text, City.Text, Street.Text);

            if (validation != "")
            {
                await DisplayAlert("Warning!", validation, "Ok");
            }
            else
            {
                DateTime date = DateOfBirth.Date;

                DateTime dateOfEx = PassportExpiry.Date;

                //Page loading indicator
                pageLoading();

                //Pass values to payload
                StatusChange statusChange = new StatusChange(Constants._hotel_code, Constants._reservation_id, guestIdentification, IdentificationMethodNumber.Text, guestNumber.Text, guestSalutation, GuestFisrtName.Text, GuestLastName.Text, guestGender, GuestEmail.Text, GuestContact.Text, HouseNumber.Text, Street.Text, City.Text, guestCountry, guestNationality, guestLanguage, date.ToString("s"), dateOfEx.ToString("s"));
                //PPExpiryDate expiryDateChange = new PPExpiryDate(Constants._hotel_code, Constants._reservation_id, guestNumber.Text, guestSalutation, GuestFisrtName.Text, GuestLastName.Text, IdentificationMethodNumber.Text ,dateOfEx.ToString("s"),guestIdentification,guestCountry,guestLanguage);

                //Data Posting source
                //PostServiceManager postServiceManager = new PostServiceManager();
                GuestPostManager guestPostManager = new GuestPostManager();

                string result = await guestPostManager.CreateUpdateGuest(statusChange);

                //String result = await postServiceManager.StatusChangeAsync(statusChange);
                //String result2 = await postServiceManager.SavePPExpiryAsync(expiryDateChange);

                
                if (result != "Reservation is locked" || result.Contains("Updated Successfully"))
                {
                    //Updateing guest details object
                    var obj = guestdetails.FirstOrDefault(x => x.guestNumber == Int32.Parse(guestNumber.Text));
                    obj.guestNumber = Int32.Parse(guestNumber.Text);
                    obj.identificationMethod = guestIdentification;
                    obj.passportIdNumber = IdentificationMethodNumber.Text;
                    obj.salutation = guestSalutation;
                    obj.guestName = GuestFisrtName.Text + " " + GuestLastName.Text;
                    obj.guestFirstName = GuestFisrtName.Text;
                    obj.guestLastName = GuestLastName.Text;
                    obj.gender = guestGender;
                    obj.email = GuestEmail.Text;
                    obj.contactNumber = GuestContact.Text;
                    obj.houseNumber = HouseNumber.Text;
                    obj.street = Street.Text;
                    obj.city = City.Text;
                    obj.nationality = guestNationality;
                    obj.country = guestCountry;
                    obj.language = guestLanguage;
                    obj.guestCode = guestCodeFromSearch;
                    obj.dateOfBirth = date.ToString("s");
                    obj.dateOfExpiry = dateOfEx.ToString("s");
                    obj.noOfVisitsHotel = Visitperhotel;
                    obj.noOfVisits = Totalvisit;
                    obj.revenueTotal = RevenueTotal;
                    obj.revenueRoom = RevenueRoom;
                    obj.reveneuFB = RevenueFnb;
                    obj.revenueOther = RevenueOther;

                    var nameToCountry = CountryDictionary.listOfCountrie();
                    if (guestCountry != "" || guestCountry != "")
                    {
                        //Item Value in dictionary
                        string name = nameToCountry.FirstOrDefault(x => x.Value == guestCountry).Key;
                        obj.countryKeyValue = name;
                    }

                    Constants._guestNumber = "";
                    Constants._guestNumber = guestNumber.Text;
                    stopPageLoading();


                    if (Constants.PassportCopy != null)
                    {
                        await FTPService.UploadPassportCopy(Constants.PassportCopy);
                    }

                    //Guest details updateindicator, Reload content page
                    MessagingCenter.Send<GuestEdit, List<guestDetails>>(this, Constants._guestEdited, guestdetails);
                    await DisplayAlert("Message", result, "OK");

                    ////Logger
                    //new APILogger().Logger("Guest Payload :" + JsonConvert.SerializeObject(statusChange));
                    //new APILogger().Logger("Guest Save Status :" + result);

                    //Close this content page
                    this.Navigation.RemovePage(this);
                }
                else
                {
                    stopPageLoading();
                    await DisplayAlert("Message", result, "OK");
                }
            }
        }

        */

        //for testing
        async void saveButton(object sender, EventArgs e)
        {

            if (Constants.PassportCopy != null)
            {
                await FTPService.UploadPassportCopy(Constants.PassportCopy, GuestFisrtName.Text);
            }

            //Close this content page
            this.Navigation.RemovePage(this);
        }

            //On page loading
            void pageLoading()
        {
            Device.BeginInvokeOnMainThread(() =>
                {
                    GuestLoadIndicator.IsVisible = true;
                    GuestLoadIndicator.IsRunning = true;
                    GuestDetails.IsVisible = false;
                });
        }

        //On page stopped loading
        void stopPageLoading()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                GuestDetails.IsVisible = true;
                GuestLoadIndicator.IsVisible = false;
                GuestLoadIndicator.IsRunning = false;
            });
        }
        protected override void OnAppearing()
        {
            double scale = this.Scale;

            IdentificationMethod.Scale = (scale * 90 / 100);
            IdentificationMethodNumber.Scale = (scale * 90 / 100);
            GuestFisrtName.Scale = (scale * 90 / 100);
            GuestLastName.Scale = (scale * 90 / 100);
            Gender.Scale = (scale * 90 / 100);
            GuestEmail.Scale = (scale * 90 / 100);
            GuestContact.Scale = (scale * 90 / 100);
            HouseNumber.Scale = (scale * 90 / 100);
            Street.Scale = (scale * 90 / 100);
            City.Scale = (scale * 90 / 100);
            Nationality.Scale = (scale * 90 / 100);
            GuestCountry.Scale = (scale * 90 / 100);
            Language.Scale = (scale * 90 / 100);
            scanButton.Scale = (scale * 90 / 100);
            buttonSave.Scale = (scale * 90 / 100);
            DateOfBirth.Scale = (scale * 90 / 100);
            PassportExpiry.Scale = (scale * 90 / 100);
            Salutation.Scale = (scale * 90 / 100);
            base.OnAppearing();
        }

        private void InitBlinkID()
        {
            // before obtaining any of the recognizer's implementations from DependencyService, it is required
            // to obtain instance of IMicroblinkScanner and set the license key.
            // Failure to do so will crash your app.
            var microblinkFactory = DependencyService.Get<IMicroblinkScannerFactory>();

            // license keys are different for iOS and Android and depend on iOS bundleID/Android application ID
            // in your app, you may obtain the correct license key for your platform via DependencyService from
            // your Droid/iOS projects
            string licenseKey;

            // both these license keys are demo license keys for bundleID/applicationID com.microblink.xamarin.blinkid
            if (Device.RuntimePlatform == Device.iOS)
            {
                licenseKey = @"sRwAAAEQY29tLmNobWwuaXQuY2Nmc0iUtoUk/ef1YJ5jX+o5uIzyxuRON+o5AprGt1y0HRXyOS3r7Xn/J8u2qeYccWpDTk0d8mdaDPrcaJjP7EUTBVG7BwPOlZfYAd4bPftj+19de9RS9WPc8U+nSNUgG39oaeL7cG4/J8B6V13gvRYYCDRAQh8EUDUy7R77C7WcuFQ4UMs2c7MkJxwFuxtX0k2W5TvMANrFRd31HhWVGYl8v1QsBgD2qxPBDq1QIXNdmmfHd0PCavSzTUVXE0tuTGjm7ZiY";
            }
            else
            {
                licenseKey = "sRwAAAEQY29tLmNobWwuaXQuY2Nmc0iUtoUk/ef1YJ5jX+o5uIzyxuRON+o5AprGt1y0HRXyOS3r7Xn/J8u2qeYccWpDTk0d8mdaDPrcaJjP7EUTBVG7BwPOlZfYAd4bPftj+19de9RS9WPc8U+nSNUgG39oaeL7cG4/J8B6V13gvRYYCDRAQh8EUDUy7R77C7WcuFQ4UMs2c7MkJxwFuxtX0k2W5TvMANrFRd31HhWVGYl8v1QsBgD2qxPBDq1QIXNdmmfHd0PCavSzTUVXE0tuTGjm7ZiY";
            }

            // since DependencyService requires implementations to have default constructor, a factory is needed
            // to construct implementation of IMicroblinkScanner with given license key
            blinkID = microblinkFactory.CreateMicroblinkScanner(licenseKey);

            // subscribe to scanning done message
            MessagingCenter.Subscribe<ScanningDoneMessage>(this, ScanningDoneMessageId, async (sender) => {

                pageLoading();

                try
                {


                    ImageSource fullDocumentImageSource = null;
                    ImageSource successFrameImageSource = null;

                    string stringResult = "No valid results.";

                    // if user cancelled scanning, sender.ScanningCancelled will be true
                    if (sender.ScanningCancelled)
                    {
                        stringResult = "Scanning cancelled";
                    }
                    else
                    {
                        // otherwise, one or more recognizers used in RecognizerCollection (see StartScan method below)
                        // will contain result

                        // if specific recognizer's result's state is Valid, then it contains data recognized from image
                        if (mrtdRecognizer.Result.ResultState == RecognizerResultState.Valid)
                        {
                            var result = mrtdRecognizer.Result;


                            fname = result.MrzResult.PrimaryId;
                            lname = result.MrzResult.SecondaryId;
                            nationality = result.MrzResult.Nationality;
                            gender = result.MrzResult.Gender;
                            PassportNumber = Regex.Replace(result.MrzResult.DocumentNumber, "[^A-Za-z0-9 _]", "");
                            dateOfExpiry = $"{result.MrzResult.DateOfExpiry.Day}-{result.MrzResult.DateOfExpiry.Month}-{result.MrzResult.DateOfExpiry.Year}";
                            dateOfBirthPass = $"{result.MrzResult.DateOfBirth.Day}-{result.MrzResult.DateOfBirth.Month}-{result.MrzResult.DateOfBirth.Year}";


                            //List of identification methods from dictionary
                            var nameToAlpha2FromAlpha3 = CountryDictionary.listAlpha3To();
                            //Getting Key Value from Dictoinary by passing the THREE Letter code
                            nationality = nameToAlpha2FromAlpha3.FirstOrDefault(x => x.Key == nationality).Value;

                            //Extracting document Images
                            fullDocumentImageSource = result.FullDocumentImage;
                            successFrameImageSource = mrtdSuccessFrameGrabberRecognizer.Result.SuccessFrame;
                            //Set Image to Globals
                            Constants.PassportCopy = fullDocumentImageSource;
                            

                            await existingGuestDetailsFromDatabses(2, PassportNumber);

                            Device.BeginInvokeOnMainThread(() =>
                            {
                                GuestFisrtName.Text = fname;
                                GuestLastName.Text = lname;
                                guestIdentificationDetailsPicker("2");
                                guestNationalityDetailsPicker(nationality);
                                guestContryDetailsPicker(nationality);
                                guestLanguageDetailsPicker("E");
                                PassportExpiry.Date = DateTime.ParseExact(dateOfExpiry, "d-M-yyyy", CultureInfo.CurrentCulture);
                                guestGenderDetailsPicker(serviceDataValidation.guestEditGenderValidation(gender));
                                DateOfBirth.Date = DateTime.ParseExact(dateOfBirthPass, "d-M-yyyy", CultureInfo.CurrentCulture);
                                stopPageLoading();
                                
                                
                            });
                        }

                    }
                }
                catch (Exception)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        stopPageLoading();
                        guestEntryDetails(guestNumber.Text, PassportNumber, fname, lname, "", "", "", "", "");
                        guestIdentificationDetailsPicker("2");
                        guestNationalityDetailsPicker(nationality);
                        guestContryDetailsPicker(nationality);
                        guestLanguageDetailsPicker("E");
                        guestSalutationPicker("");
                        guestGenderDetailsPicker(serviceDataValidation.guestEditGenderValidation(gender));
                        PassportExpiry.Date = DateTime.ParseExact(dateOfExpiry, "d-M-yyyy", CultureInfo.CurrentCulture);
                        DateOfBirth.Date = DateTime.ParseExact(dateOfBirthPass, "d-M-yyyy", CultureInfo.CurrentCulture);
                        Visitperhotel = "00";
                        Totalvisit = "00";
                        RevenueTotal = "0.00";
                        RevenueRoom = "0.00";
                        RevenueFnb = "0.00";
                        RevenueOther = "0.00";
                        stopPageLoading();
                        
                        
                        DisplayAlert(Constants._headerMessage, Constants._noDetails, Constants._buttonOkay);

                    });
                }

            });
        }

        /// <summary>
        /// Button click event handler that will start the scanning procedure.
        /// </summary>
        private void StartScan()
        {
            // license keys must be set before creating Recognizer, othervise InvalidLicenseKeyException will be thrown
            // the following code creates and sets up implementation of MrtdRecognizer
            mrtdRecognizer = DependencyService.Get<IMrtdRecognizer>(DependencyFetchTarget.NewInstance);
            mrtdRecognizer.ReturnFullDocumentImage = true;

            // success frame grabber recognizer must be constructed with reference to its slave recognizer,
            // so we need to use factory to avoid DependencyService's limitations
            mrtdSuccessFrameGrabberRecognizer = DependencyService.Get<ISuccessFrameGrabberRecognizerFactory>(DependencyFetchTarget.NewInstance).CreateSuccessFrameGrabberRecognizer(mrtdRecognizer);


            // first create a recognizer collection from all recognizers that you want to use in recognition
            // if some recognizer is wrapped with SuccessFrameGrabberRecognizer, then you should use only the wrapped one
            var recognizerCollection = DependencyService.Get<IRecognizerCollectionFactory>().CreateRecognizerCollection(mrtdSuccessFrameGrabberRecognizer);

            // using recognizerCollection, create overlay settings that will define the UI that will be used
            // there are several available overlay settings classes in Microblink.Forms.Core.Overlays namespace
            // document overlay settings is best for scanning identity documents
            var documentOverlaySettings = DependencyService.Get<IDocumentOverlaySettingsFactory>().CreateDocumentOverlaySettings(recognizerCollection);

            // start scanning
            blinkID.Scan(documentOverlaySettings);
        }

        
    }

}

