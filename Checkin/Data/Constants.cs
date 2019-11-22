using System;
using System.Collections.Generic;
using Checkin.Models.ModelClasses;
using Xamarin.Forms;

namespace Checkin
{
	public static class Constants
	{

        public static string _version = "1.70";

        //*************************************** Development Settings ***********************
        //DEV URL
        //public static string _gatewayURL = "https://nwgateway.keells.lk:44300";
        //public static string _cookie = "sap-XSRF_NWG_100";XSRF_NWG_100
        //*************************************** Development Settings ***********************

        ////*************************************** Production settings ***********************
        ////DEV URL
        public static string _gatewayURL = "";
		public static string _cookie = "";
        //public static string _gatewayURL = "https://alastor.keells.lk:44300";
        //public static string _cookie = "sap-XSRF_GWP_100";
        public static string _azureAPIMDEVBase = "https://jkhapimdev.azure-api.net/api/beta/v1/";
        public static string _azureAPIMPRDBase = "https://octopus.keells.lk/api/v1/";
        ////*************************************** Production Settings ***********************

        //Device Related
        public static string _AndroidPackage = "";
		public static string _iOSPackage = "";

		//Access Token for gateway services
		public static string _access_token = "";
		public static string _expires_in = "";

		//User Login Details
		public static User _user;

		//Checkin details
		public static string _checkinStartTime = "";

		//Hotel Information variables
		public static string _hotel_code = "";
		public static string _hotel_name = "";
		public static string _hotel_date = "";
		public static string _hotel_image = "";


		//Reservation Information variables
		public static string _reservation_id = "";
		public static string _clientName = "";

		//Room status for respective reservation
		public static string _room_status = "";
		public static string _room_number = "";


		//Guest number
		public static string _guestNumber;

		//Flight details
		public static string _arrFlight = "";
		public static string _arrFlightTIme = "PT00H00M00S";
		public static string _depFlight = "";
        public static string _depFlightTIme = "PT00H00M00S";
       

		//Guest Number for Imgae saving
		public static int _guestNumberForSignature;

		//Room Status
		public static string _roomStatus;
		public static List<roomDetails> _roomdetails = new List<roomDetails>();

		//Constant Methods
		public static ReservationDetails result;
		public static string resultMain = "";

        //Reservation Status for Checkin or save signature
        public static string _reservationStatus;

		//Imagesource for Updated images
		public static ImageSource _guestSignatureBase64;
        public static byte[] ImageBytes { get; set; }

		public static int _number;


		//Base64 String for Updated images
		public static string _base64Code = "";

		//Guest details from passport/National Card
		public static int _identificationMethod;
		public static string _identificationMethodNumber = "";


        //Passport Image

        public static ImageSource PassportCopy { get; set; }

        //Message Header
        public static string _headerConnectionLost = "Connection Lost";
		public static string _headerUpdateAvailable = "Update Available";
		public static string _headerHowTo = "How to Log In?";
		public static string _headerMessage = "Message";
		public static string _headerMessageProforma = "Error Loading Proforma";
		public static string _headerWrongCredentials = "Wrong Credentials";
		public static string _headerExitApp = "Exit Checkin App";
		public static string _headerPercentageContribution = "Percentage of Contribution";
		public static string _agreement = "Agreement";
		public static string _headerHostoryDetails = "Guest History Details";


		//Messages
		public static string _userNotExistInNWGateway = "Authentication Successful. Login Unsuccessful due to unavailability of username in SAP system. Please contact your authorization team.";
		public static string _networkerror = "Unable to Connect!\n\n Note: This error may occur continously when the password is changed. Please login again!";
		public static string _networkUnreachable = "Error: ConnectFailure(Network is unreachable)";
		public static string _wentwrong = "Sorry! Something went wrong. \n\ndue to ";
		public static string _help = "Enter your active directory username along with '@jkintranet.com' and password.";
		public static string _providelogincredentials = "Please Enter Login Credetials!";
		public static string _incorrectlogincredentials = "Incorrect Username/Password.";
		public static string _mandotoryfield = "Please specify a reason with minimum 10 characters.";
		//public static string _updateMessage = "Great News! A new version of this app is available, you must update to new version in order to use this application. Press 'Okay' to update this app.";
		public static string _exitMessage = "Are you sure?";
		public static string _beingImplemented = "This function is under development process.";
		public static string _dateFormat = "Date format should be DD-MM-YYYY!";
		public static string _signatureSuccessfullySaved = "Signature Successfully Saved!";
		public static string _termsAndConditionsMessage = "Terms and Conditions!";
		public static string _signatureSaved = "Signature Added!";
		public static string _noDetails = "No Guest Details Available in System!";
		public static string _checkinInspectedValidation = "Check-In Could Be Done Only With Inspected Rooms!";
		public static string _signatureValidation = "Please add a signature";
		public static string _signatureCannotBeUdated = "Sorry! Signature Cannot be Updated";
		public static string _selectIdentificationMethod = "Please select Identification Method";
		public static string _emailNotAvailable = "Please Enter Email Address";
		public static string _emailSent = "Email Sent Successsfully";
		public static string _emailNotSent = "Error in Sending Email";
		public static string _proformaGeneratError = "Error in Generating Proforma";
		public static string _roomSccuessfullyassigned = "Room Assigned Successfully!";
		public static string _scannerError = "Unable to start passport scanner. Please give camera permision in settings, for the checkin applicarion";
		public static string _TokenRefreshError = "Sorry.. Unable to refresh token. This may occur when the Active Directory password is changed. Please login again!";
		public static string _unableToGetHotelCode = "Unable To Access Hotel Code. Please Contact the developers!";
		public static string _unableToCOnnectToInternet = "Unable to Connect to the Internet!";
		public static string _unableToGetHotelDate = "Unable To Access Hotel Date. Please Contact the IT Co-ordinator!";
		public static string _unableToGetHotelReservations = "Unable To Access Reservations. Please Contact the developers!";
		public static string _gatewayUrlError = "Unable to Connect. This error may occur if the gateway URL is incorrect!";



		//Message Button
		public static string _buttonTryAgain = "Try Again";
		public static string _buttonOkay = "Ok";
		public static string _buttonClose = "Close";
		public static string _buttonYes = "Yes";
		public static string _buttonNo = "No";


		//reservation View Messages
		public static string _reservationStatusCheckedIn = "Checked-in";
		public static string _reservationStatusCancelled = "Cancelled";
		public static string _reservationStatusPending = "Expected";//Pending

		//Additional MessageCeter Messages
		public static string _signatureAddedMessage = "Pending";
		public static string _guestEdited = "guestEdited";
		public static string _roomAssigned = "roomAssigned";
		public static string _loadGuestInformation = "loadGuestDetails";
		public static string _loadRemarksInformation = "loadRemarksInformation";
		public static string _roomAssignedUpdateReservationList = "roomAssignedUpdateReservationList";
		public static string _performaListHeight = "PerformaListHeight";




		//Setting Values for conditions

		public static string _notAvailable = "Not Available";
		public static string _available = "Available";
		public static string _notAssigned = "Not Assigned";
		public static string _roomNotAssigned = "Room Not Assigned";
		public static string _cleanedRoom = "CLEAN";
		public static string _inspectedRoom = "INSPECTED";
		public static string _dirtyRoom = "DIRTY";

		//Image Indictor
		public static bool _notAvailableSignatureAdded;
		public static bool _guestEditedStatus;

        //Terms and conditions
        public static string _termsAndConditionsAPI = "";

		public static string _termsAndConditionsDefaults = "1. The guest signing this form and  @@accepting these terms " +
												   "and conditions shall be deemed to have so signed and accepted the same " +
												   "for and on behalf of those persons whose names are listed in this form.@@" +
												   "2. If this form is signed by a guest of a tour group, the terms and conditions " +
												   "herein shall apply for all facilities not provided under the tour package and in respect" +
												   "of all other facilities, insofar as these terms and conditions do not conflict with the " +
												   "terms and conditions under which the tour is conducted.@@  " +
												   "3. The guest is to carry the key card holder at all times when on hotel premises" +
												   "and to produce the same each time a request is made for the guest’s room key, mail" +
												   "or any item from reception and when signing bills or cheques.@@ " +
												   "4. Any facilities/services (including the use of recreation centres, Spas, Gyms, etc.) " +
												   "made available or recommended by the hotel and availed of by the guest shall be at the " +
												   "guest’s own expense (if any) and risk. Subject to (7) below, the hotel shall not be" +
												   "liable in respect of any damage or injury suffered by the guest in this regard.@@ " +
												   "5. Any items used at the mini bar in guest rooms will be charged to the guest’s account at" +
												   "the prices and rates indicated at the mini bar.@@  " +
												   "6. All vehicles parked on the hotel premises and valuables kept at the " +
												   "hotel will be at the guest’s risk and expense. the hotel accepts no liability " +
												   "for damage to or loss of any vehicle or valuables kept at the hotel premises. " +
												   "The hotel strongly advises against keeping of valuables in the guest’s room or" +
												   "public areas of the hotel and recommends the use of safety deposit box provided " +
												   "by the hotel in the rooms.@@  " +
												   "7. The hotel disclaims any liability towards guests " +
												   "unless caused by the negligence of the hotel.@@  " +
												   "8. In no event will the hotel be liable for indirect," +
												   "unforeseen or consequential damages, future loss of profits or loss " +
												   "of revenue arising from any loss/damage suffered by the guest.@@  " +
												   "9. Any loss of or damage to hotel property owing/attributable to any act or" +
												   "omission of the guest shall be charged to the guest’s account.@@  " +
												   "10. The guest is required to return the room keys before departure. " +
												   "Failure to do so or damage to the keys shall result in the cost of replacement " +
												   "of locks and other losses incurred by the hotel, being charged to the account" +
												   "of the guest.@@ " +
												   "11. The guest will be required to provide proof of age of " +
												   "children listed in this form if verification is required by the hotel.@@  " +
												   "12. Use of laundry facilities by the guest will be at the guest’s own risk " +
												   "and cost and the liability of the hotel shall be limited to 5 times the cleaning" +
												   "value of the garment in question. @@  " +
												   "13. Complaints regarding any services availed " +
                                                   "of the event giving rise to the complaint." +
												   "No relief (if any) shall be afforded by the hotel on the basis of complaints" +
												   "made after this time period. Any relief to be afforded shall be at the sole and absolute discretion of the hotel. @@" +
												   "14. Whilst the hotel shall always endeavour to provide the guest with the best service possible, " +
												   "it shall not be responsible for failure to perform its obligations caused by war, riots, civil disorder," +
												   "earthquake, fire, explosion, storm, flood, or other adverse weather conditions, strikes, " +
												   "lockouts or other industrial action, confiscation or other action by government agencies or such other event beyond" +
												   "the control of the hotel.@@  " +
												   "15. Guests staying on an all inclusive basis will be required " +
												   "pay for: (i) all imported liquor; (ii) food and beverage from the a` la carte menu, " +
												   "food and beverage from the pool bar menu; (iii) special food and beverage promotions" +
												   "conducted by the hotel at a supplementary charge; (iv) room service; (v) spa charges (vi) bills" +
												   "incurred by any non resident guests visiting the guest; and/or such other charges as the hotel may notify.@@  " +
												   "16. Safe deposit boxes are available in all rooms. The hotel will not be held responsible for the loss of money " +
												   "or valuables left in the room. The deposit of guest’s goods in the safety deposit boxes at the hotel shall not be " +
												   "deemed a deposit with the manager/hotel keeper of goods, safe custody as provided in the hotel keepers liability" +
												   "ordinance, a copy of which ordinance is exhibited in the hotel. the hotel shall not be liable in any manner whatsoever, " +
												   "for any loss of goods deposited in such safety deposit box. @@ " +
												   "17. In the event of the death of the guest or incapacitation while" +
												   "staying at the hotel, the manager may break open the safety deposit box " +
												   "in the room and handover the contents thereof, if any, to any person whom the manager" +
												   "may in his absolute discretion deem fit without any liability whatsoever for such acts." +
												   "neither the guest nor his heirs, executors or administrators shall have any claim a gainst " +
												   "the manager or the hotel in that respect.  @@ " +
												   "18. The guest shall not use the hotel for " +
												   "any illegal purpose or bring in to the hotel, any goods " +
												   "of explosive or dangerous or offensive nature or which may " +
												   "cause nuisance to the hotel or any of its occupants and shall " +
												   "indemnify and hold harmless the hotel from any damage, liability " +
												   "or loss caused by any such action.@@  " +
												   "19. The guest hereby authorises the hotel," +
												   "at its discretion, to debit his credit card (details of which are given overleaf) " +
												   "or to deduct from the amount deposited with the hotel such amounts as may legally " +
												   "accrue to the hotel by virtue of the guest’s stay at the hotel. @@ " +
												   "20. Activities/services provided by third parties operating in the hotel" +
												   "will be availed of by guests at their risk and expense.@@  " +
												   "21. Safety: guests should study the floor plan on the inside of the " +
												   "door to their room and familiarise themselves with the location of the emergency " +
												   "exits and fire fighting equipment nearest their room. In case of a fire or smoke, " +
												   "elevators should not be used and guests should take their room key with them.@@  " +
												   "22. Resident guests shall not be permitted to entertain non resident guests in " +
												   "their rooms. Non resident guests may only use the facilities of the hotel subject " +
												   "to prior written approval of the hotel. @@ " +
												   "23. No pets shall be permitted in the hotel premises.@@" +
												   "24. The guest acknowledges that the legal age for the consumption of alcohol and tobacco in " +
												   "Sri Lanka is twenty one (21) years and shall ensure that this is strictly adhered to by the guest " +
												   "and any person whose names are listed in this form and agrees to indemnify and hold harmless the hotel " +
												   "from any claims, damages resulting therefrom. Guests are advised to only smoke in specified smoking " +
												   "rooms and designated smoking areas.@@ "+
                                                   "25. The client is required to provide full and accurate details in the form required by us in order for " +
                                                   "us to process the booking made by the client.We are not in a position to confirm or process any bookings made by the client in the event " +
                                                   "that the client does not provide such personal data as required.Cinnamon Hotels & Resorts confirms that all personal information provided by the you " +
                                                   "will be used for the purposes of facilitating the booking /s made by you and to promote the products and services of the Company and its brand.We may provide " +
                                                   "and /or transfer such personal data to our service providers, agents and other parties as required for the purpose of facilitating the booking made by you.@@ " +
                                                   " Such personal data will be stored for a period not exceeding 12 months.The client shall have the right to request from us, access to, any rectification or erasure " +
                                                   "of personal data provided to us or to restrict the processing of such data.The client shall also have the right to withdraw his/her consent for us to use the personal " +
                                                   "data at any time in writing, provided that any processing of data prior to the withdrawal of such consent shall not be rendered unlawful. Furthermore, if the client " +
                                                   "is an EU data subject, the client shall have the right to lodge a complaint with a supervisory authority in the event that there is a breach of the relevant data protection regulations.@@ " +
                                                   " Furthermore, the client confirms that any personal information or data he/she is sharing on behalf of another person for the purposes of the booking with Cinnamon Hotels & Resorts has " +
                                                   "been obtained with the prior consent of such person who has apprised himself/herself of the booking terms and conditions and privacy policy of Cinnamon Hotels & Resorts and such person " +
                                                   "is aware of their rights in respect of such data provided to us";

        public static string _termsAndConditionsCNL = "1. The guest signing this form and  @@accepting these terms " +
                                                   "and conditions shall be deemed to have so signed and accepted the same " +
                                                   "for and on behalf of those persons whose names are listed in this form.@@" +
                                                   "2. If this form is signed by a guest of a tour group, the terms and conditions " +
                                                   "herein shall apply for all facilities not provided under the tour package and in respect" +
                                                   "of all other facilities, insofar as these terms and conditions do not conflict with the " +
                                                   "terms and conditions under which the tour is conducted.@@ ";
        
        public static string _termsAndConditionsCNG = "1. The guest signing this form and  @@accepting these terms " +
                                                   "and conditions shall be deemed to have so signed and accepted the same " +
                                                   "for and on behalf of those persons whose names are listed in this form.@@" +
                                                   "2. If this form is signed by a guest of a tour group, the terms and conditions " +
                                                   "herein shall apply for all facilities not provided under the tour package and in respect" +
                                                   "of all other facilities, insofar as these terms and conditions do not conflict with the " +
                                                   "terms and conditions under which the tour is conducted.@@ ";

        public static string _termsAndConditionsRED = "1. The guest signing this form and  @@accepting these terms" +
                                                   "and conditions shall be deemed to have so signed and accepted the same " +
                                                   "for and on behalf of those persons whose names are listed in this form.@@" +
                                                   "2. If this form is signed by a guest of a tour group, the terms and conditions " +
                                                   "herein shall apply for all facilities not provided under the tour package and in respect" +
                                                   "of all other facilities, insofar as these terms and conditions do not conflict with the " +
                                                   "terms and conditions under which the tour is conducted.@@  ";

        public static string _termsAndConditionsBEY = "1. The guest signing this form and  @@accepting these terms" +
                                                   "and conditions shall be deemed to have so signed and accepted the same " +
                                                   "for and on behalf of those persons whose names are listed in this form.@@" +
                                                   "2. If this form is signed by a guest of a tour group, the terms and conditions " +
                                                   "herein shall apply for all facilities not provided under the tour package and in respect" +
                                                   "of all other facilities, insofar as these terms and conditions do not conflict with the " +
                                                   "terms and conditions under which the tour is conducted.@@  ";

        public static string _termsAndConditionsVIL = "1. The guest signing this form and  @@accepting these terms" +
                                                   "and conditions shall be deemed to have so signed and accepted the same " +
                                                   "for and on behalf of those persons whose names are listed in this form.@@" +
                                                   "2. If this form is signed by a guest of a tour group, the terms and conditions " +
                                                   "herein shall apply for all facilities not provided under the tour package and in respect" +
                                                   "of all other facilities, insofar as these terms and conditions do not conflict with the " +
                                                   "terms and conditions under which the tour is conducted.@@  ";

        public static string _termsAndConditionsBBH = "1. The guest signing this form and  @@accepting these terms" +
                                                   "and conditions shall be deemed to have so signed and accepted the same " +
                                                   "for and on behalf of those persons whose names are listed in this form.@@" +
                                                   "2. If this form is signed by a guest of a tour group, the terms and conditions " +
                                                   "herein shall apply for all facilities not provided under the tour package and in respect" +
                                                   "of all other facilities, insofar as these terms and conditions do not conflict with the " +
                                                   "terms and conditions under which the tour is conducted.@@  ";

        public static string _termsAndConditionsWLD = "1. The guest signing this form and  @@accepting these terms" +
                                                   "and conditions shall be deemed to have so signed and accepted the same " +
                                                   "for and on behalf of those persons whose names are listed in this form.@@" +
                                                   "2. If this form is signed by a guest of a tour group, the terms and conditions " +
                                                   "herein shall apply for all facilities not provided under the tour package and in respect" +
                                                   "of all other facilities, insofar as these terms and conditions do not conflict with the " +
                                                   "terms and conditions under which the tour is conducted.@@  ";

        public static string _termsAndConditionsTRA = "1. The guest signing this form and  @@accepting these terms" +
                                                   "and conditions shall be deemed to have so signed and accepted the same " +
                                                   "for and on behalf of those persons whose names are listed in this form.@@" +
                                                   "2. If this form is signed by a guest of a tour group, the terms and conditions " +
                                                   "herein shall apply for all facilities not provided under the tour package and in respect" +
                                                   "of all other facilities, insofar as these terms and conditions do not conflict with the " +
                                                   "terms and conditions under which the tour is conducted.@@  ";

        public static string _termsAndConditionsLOD = "1. The guest signing this form and  @@accepting these terms" +
                                                   "and conditions shall be deemed to have so signed and accepted the same " +
                                                   "for and on behalf of those persons whose names are listed in this form.@@" +
                                                   "2. If this form is signed by a guest of a tour group, the terms and conditions " +
                                                   "herein shall apply for all facilities not provided under the tour package and in respect" +
                                                   "of all other facilities, insofar as these terms and conditions do not conflict with the " +
                                                   "terms and conditions under which the tour is conducted.@@  ";

        public static string _termsAndConditionsBLU = "1. The guest signing this form and  @@accepting these terms" +
                                                   "and conditions shall be deemed to have so signed and accepted the same " +
                                                   "for and on behalf of those persons whose names are listed in this form.@@" +
                                                   "2. If this form is signed by a guest of a tour group, the terms and conditions " +
                                                   "herein shall apply for all facilities not provided under the tour package and in respect" +
                                                   "of all other facilities, insofar as these terms and conditions do not conflict with the " +
                                                   "terms and conditions under which the tour is conducted.@@  ";

        public static string _termsAndConditionsCIT = "1. The guest signing this form and  @@accepting these terms" +
                                                   "and conditions shall be deemed to have so signed and accepted the same " +
                                                   "for and on behalf of those persons whose names are listed in this form.@@" +
                                                   "2. If this form is signed by a guest of a tour group, the terms and conditions " +
                                                   "herein shall apply for all facilities not provided under the tour package and in respect" +
                                                   "of all other facilities, insofar as these terms and conditions do not conflict with the " +
                                                   "terms and conditions under which the tour is conducted.@@  ";

        public static string _termsAndConditionsDHO = "1. The guest signing this form and  @@accepting these terms" +
                                                   "and conditions shall be deemed to have so signed and accepted the same " +
                                                   "for and on behalf of those persons whose names are listed in this form.@@" +
                                                   "2. If this form is signed by a guest of a tour group, the terms and conditions " +
                                                   "herein shall apply for all facilities not provided under the tour package and in respect" +
                                                   "of all other facilities, insofar as these terms and conditions do not conflict with the " +
                                                   "terms and conditions under which the tour is conducted.@@  ";

        public static string _termsAndConditionsELL = "1. The guest signing this form and  @@accepting these terms" +
                                                   "and conditions shall be deemed to have so signed and accepted the same " +
                                                   "for and on behalf of those persons whose names are listed in this form.@@" +
                                                   "2. If this form is signed by a guest of a tour group, the terms and conditions " +
                                                   "herein shall apply for all facilities not provided under the tour package and in respect" +
                                                   "of all other facilities, insofar as these terms and conditions do not conflict with the " +
                                                   "terms and conditions under which the tour is conducted.@@  ";

        public static string _termsAndConditionsHAK = "1. The guest signing this form and  @@accepting these terms" +
                                                   "and conditions shall be deemed to have so signed and accepted the same " +
                                                   "for and on behalf of those persons whose names are listed in this form.@@" +
                                                   "2. If this form is signed by a guest of a tour group, the terms and conditions " +
                                                   "herein shall apply for all facilities not provided under the tour package and in respect" +
                                                   "of all other facilities, insofar as these terms and conditions do not conflict with the " +
                                                   "terms and conditions under which the tour is conducted.@@  ";

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //Alert messages
        public static string _agreementInformation = "I agree that liability for my account is not waived and agree to" + " be held personally liable in the event that the indicated person, company, " + "association or credit card company details indicated fails to pay for any part or" + " the full amount of the charges. I agree and confirm that if payment is to be" + " made by credit card, my signature below shall constitute authority to debit the" + " nominated credit card or the deposit paid to the hotel with the total amount" + " due from me to the hotel including delayed and/or amended charges and/or in respect " + "of damages, even in the absence of my signature on the sales slip. @@  Non registered" + " visitors are not permitted to remain in guest rooms/floors between 9pm-6am." + " You are kindly requested to adhere to this hotel regulation in order to avoid " + "any unnecessary inconvenience. As per the regulations, the hotel check in time " + "is 1300hrs and departure time is 1200hrs. Unless otherwise pre agreed late departures" + " are subject to a charge. All charges are inclusive of 10% service charge and other" + " government taxes. @@  I state that the " + "information given by me above is correct and having read and understood the terms and" + " conditions of stay/having been explained to my satisfaction the true meaning and" + " purport of the terms and conditions of stay, I hereby for myself, and for and on" + " behalf of those whose names are listed in this form, consent to the same.";

        public static string _privacyStatement = "I confirm that I will be providing you with my personal data and hereby expressly consent to the use of such data for the purpose of the booking made with you. This includes express permission to share the personal data information with your service providers and agents. I also further expressly consent to the use of my personal data to promote products and services of your company or brand. I confirm that I have read through your data policy and have understood my rights in relation to the personal data which I am providing to you.";

		public static string addSignatureImage = "iVBORw0KGgoAAAANSUhEUgAAAnYAAAKACAYAAAARnzX9AAAAAXNSR0IArs4c6QAAABxpRE9UAAAAAgAAAAAAAAFAAAAAKAAAAUAAAAFAAAAOCcy5Ev8AAA3VSURBVHgB7NABDQAAAMKg909tDwcRKAwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAgAEDBgwYMGDAwN/AAAAA///WNUhsAAAN00lEQVTt0AENAAAAwqD3T20PBxEoDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMDA38AAn+gAAb5ZWj0AAAAASUVORK5CYII=";


        public static List<AccountDetailsModel> BankAccountsDetails = new List<AccountDetailsModel> {

                new AccountDetailsModel
                {
                    HotelCode = "3000",
                    AccountHolder = "Asian Hotels & Properties PLC - Cinnamon Grand Colombo",
                    BankName = "CITI Bank NA, Colombo",
                    AccountNumber = "200912004",
                    SwiftCode = "CITILKLX"
                },
                new AccountDetailsModel
                {
                    HotelCode = "3005",
                    AccountHolder = "Trans Asia Hotels PLC",
                    BankName = "CITI Bank NA, Colombo",
                    AccountNumber = "LKR - 5200911018",
                    SwiftCode = "CITILKLX"
                },
                new AccountDetailsModel
                {
                    HotelCode = "3015",
                    AccountHolder = "Sancity Hotels and Properties Limited",
                    BankName = "CITI Bank NA, Colombo",
                    AccountNumber = "201005027",
                    SwiftCode = "CITILKLX"
                },
                new AccountDetailsModel
                {
                    HotelCode = "3100",
                    AccountHolder = "Ceylon Holiday Resorts Ltd.",
                    BankName = "Nations Trust Bank PLC",
                    AccountNumber = "0011000-55341",
                    SwiftCode = "NTBCLKLX"
                },

                new AccountDetailsModel
                {
                    HotelCode = "3110",
                    AccountHolder = "Kandy Walk Inn Ltd.",
                    BankName = "Nations Trust Bank PLC",
                    AccountNumber = "0011000-56850",
                    SwiftCode = "NTBCLKLX"
                },

                new AccountDetailsModel
                {
                    HotelCode = "3115",
                    AccountHolder = "Habarana Lodge Ltd.",
                    BankName = "Nations Trust Bank PLC",
                    AccountNumber = "0011000-56837",
                    SwiftCode = "NTBCLKLX"
                },

                new AccountDetailsModel
                {
                    HotelCode = "3120",
                    AccountHolder = "Habarana Walk Inn Ltd.",
                    BankName = "Nations Trust Bank PLC",
                    AccountNumber = "0011000-56849",
                    SwiftCode = "NTBCLKLX"
                },
                new AccountDetailsModel
                {
                    HotelCode = "3150",
                    AccountHolder = "Yala Village Private Ltd.",
                    BankName = "Nations Trust Bank PLC",
                    AccountNumber = "0062120-96349",
                    SwiftCode = "NTBCLKLX"
                },
                new AccountDetailsModel
                {
                    HotelCode = "3160",
                    AccountHolder = "Beruwala Holiday Resorts (Pvt) Ltd",
                    BankName = "Nations Trust Bank PLC",
                    AccountNumber = "0062120-96350",
                    SwiftCode = "NTBCLKLX"
                },
                new AccountDetailsModel
                {
                    HotelCode = "3165",
                    AccountHolder = "Trinco Holiday Resorts Pvt Ltd.",
                    BankName = "Nations Trust Bank PLC",
                    AccountNumber = "0062120-96337",
                    SwiftCode = "NTBCLKLX"
                },
                new AccountDetailsModel
                {
                    HotelCode = "3170",
                    AccountHolder = "Hikkaduwa Holiday Resorts (Pvt) Ltd.",
                    BankName = "Nations Trust Bank PLC",
                    AccountNumber = "0062120-96362",
                    SwiftCode = "NTBCLKLX"
                },

                new AccountDetailsModel
                {
                    HotelCode = "3300",
                    AccountHolder = "TRAVEL CLUB (PVT) LTD",
                    BankName = "HONGKONG & SHANGHAI BANKING CORPORATION LTD",
                    AccountNumber = "USD 200-007003-104 / GBP 200-007003-102",
                    SwiftCode = "HSBCMVMV"
                },
                new AccountDetailsModel
                {
                    HotelCode = "3305",
                    AccountHolder = "FANTASEA WORLD INVESTMENTS (PVT) LTD",
                    BankName = "HONGKONG & SHANGHAI BANKING CORPORATION LTD",
                    AccountNumber = "USD 200-007029-101 / GBP 200-007029-025",
                    SwiftCode = "HSBCMVMV"
                },

                new AccountDetailsModel
                {
                    HotelCode = "3310",
                    AccountHolder = "TRANQUILITY (PVT) LIMITED",
                    BankName = "HONGKONG & SHANGHAI BANKING CORPORATION LTD",
                    AccountNumber = "USD 200-000016-101 / GBP 200-000016-103",
                    SwiftCode = "HSBCMVMV"
                },
                new AccountDetailsModel
                {
                    HotelCode = "3315",
                    AccountHolder = "TRANQUILITY (PVT) LIMITED",
                    BankName = "HONGKONG & SHANGHAI BANKING CORPORATION LTD",
                    AccountNumber = "USD 200-000016-101 / GBP 200-000016-103",
                    SwiftCode = "HSBCMVMV"
                }
        };

    }
}

