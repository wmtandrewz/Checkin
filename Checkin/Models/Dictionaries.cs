using System;
using System.Collections.Generic;
using Checkin.Models.ModelClasses;

namespace Checkin
{
	public class CountryDictionary
	{

              //Disctionaty List Of Nationalities
        public static Dictionary<string, string> listofIdentificationMethod()
		{
			var nameToIdentification = CountryDictionary.listofIdentificationMethods();
			return nameToIdentification;
		}

		public static Dictionary<string, string> listofSalutation()
		{
			var nameToIdentification = CountryDictionary.listOfSalutations();
			return nameToIdentification;
		}

		//Dictionary for list of genders
		public static Dictionary<string, string> listOfGender()
		{
			var nameToGender = CountryDictionary.listOfGenders();
			return nameToGender;
		}

		//Dictionary to Convert ISO Alpha 3 to Alpha 2 country code
		public static Dictionary<string, string> listAlpha3To()
		{
			var alphaTo32 = CountryDictionary.listAlpha3To2();
			return alphaTo32;
		}

		//Dictionary for list of countries

		public static Dictionary<string, string> listOfCountrie()
		{

			//Add values and keys to dictionary
			var nameToCountry = CountryDictionary.listOfCountries();
			return nameToCountry;
		}

		public static Dictionary<string, string> listOfNationalitie()
		{
			var nameToNationalityList = CountryDictionary.listOfNationalities();
			return nameToNationalityList;
		}

		//Dictionary fo list of languages
		public static Dictionary<string, string> listOfLanguage()
		{
			var nameToLanguage = CountryDictionary.listOfLanguages();
			return nameToLanguage;
		}

		//Dictionary for URLs
		public static Dictionary<string, string> listOfURL()
		{
			var nameToURL = CountryDictionary.listOfURLS();
			return nameToURL;
		}

		public static Dictionary<string, string> listOfURLS()
		{
			Dictionary<string, string> nameToURL = new Dictionary<string, string> { {
					"BBH",
					"3100"
				}, {
					"BEY",
					"3160"
				},{
					"BLU",
					"3165"
				},
				{
					"CIT",
					"3110"
				},{
					"CNG",
					"3000"
				},
				{
					"CNL",
					"3005"
				},{
					"DHO",
					"3310"
				},{
					"ELL",
					"3300"
				},{
					"HAK",
					"3305"
				},{
					"LOD",
					"3115"
				},{
					"RED",
					"3015"
				},{
					"TRA",
					"3170"
				},{
					"VIL",
					"3120"
				},{
					"WLD",
					"3150"
				},
			};
			return nameToURL;
		}


		public static Dictionary<string, string> listofIdentificationMethods()
		{
			Dictionary<string, string> nameToIdentification = new Dictionary<string, string> { {
					"National ID",
					"1"
				}, {
					"Passport",
					"2"
				},
			};
			return nameToIdentification;
		}

		public static Dictionary<string, string> listOfGenders()
		{
			//Adding values and keys to dictionary
			Dictionary<string, string> nameToGender = new Dictionary<string, string> { {
					"Male",
					"1"
				}, {
					"Female",
					"2"
				},
			};
			return nameToGender;
		}

		public static Dictionary<string, string> listOfSalutations()
		{
			//Adding values and keys to dictionary
			Dictionary<string, string> nameToSalutation = new Dictionary<string, string> { {
					"Ms.",
					"0001"
				}, {
					"Mr.",
					"0002"
				}, {
					"Company",
					"0003"
				}, {
					"Mr. and Mrs.",
					"0004"
				}, {
					"Professor",
					"0006"
				}, {
					"Hon.",
					"0007"
				}, {
					"Rev",
					"0008"
				}, {
					"Dr.",
					"0009"
				},
				 {
					"HE",
					"0010"
				}, {
					"Mrs.",
					"0011"
				}, {
					"INF",
					"0012"
				}, {
					"CHD",
					"0013"
				}, {
					"Ven",
					"0014"
				}
			};
			return nameToSalutation;
		}

		public static Dictionary<string, string> listAlpha3To2()
		{

			//Adding values and keys to dictionary
			Dictionary<string, string> alphaTo32 = new Dictionary<string, string> {
			   { "AFG", "AF" },    // Afghanistan
               { "ALB", "AL" },    // Albania
               { "ARE", "AE" },    // U.A.E.
               { "ARG", "AR" },    // Argentina
               { "ARM", "AM" },    // Armenia
               { "AUS", "AU" },    // Australia
               { "AUT", "AT" },    // Austria
               { "AZE", "AZ" },    // Azerbaijan
               { "BEL", "BE" },    // Belgium
               { "BGD", "BD" },    // Bangladesh
               { "BGR", "BG" },    // Bulgaria`
               { "BHR", "BH" },    // Bahrain
               { "BIH", "BA" },    // Bosnia and Herzegovina
               { "BLR", "BY" },    // Belarus
               { "BLZ", "BZ" },    // Belize
               { "BOL", "BO" },    // Bolivia
               { "BRA", "BR" },    // Brazil
               { "BRN", "BN" },    // Brunei Darussalam
               { "CAN", "CA" },    // Canada
               { "CHE", "CH" },    // Switzerland
               { "CHL", "CL" },    // Chile
               { "CHN", "CN" },    // People's Republic of China
               { "COL", "CO" },    // Colombia
               { "CRI", "CR" },    // Costa Rica
               { "CZE", "CZ" },    // Czech Republic
               { "DEU", "DE" },    // Germany
               { "D<<", "DE" },    // Germany
               { "DNK", "DK" },    // Denmark
               { "DOM", "DO" },    // Dominican Republic
               { "DZA", "DZ" },    // Algeria
               { "ECU", "EC" },    // Ecuador
               { "EGY", "EG" },    // Egypt
               { "ESP", "ES" },    // Spain
               { "EST", "EE" },    // Estonia
               { "ETH", "ET" },    // Ethiopia
               { "FIN", "FI" },    // Finland
               { "FRA", "FR" },    // France
               { "FRO", "FO" },    // Faroe Islands
               { "GBR", "GB" },    // United Kingdom
               { "GEO", "GE" },    // Georgia
               { "GRC", "GR" },    // Greece
               { "GRL", "GL" },    // Greenland
               { "GTM", "GT" },    // Guatemala
               { "HKG", "HK" },    // Hong Kong S.A.R.
               { "HND", "HN" },    // Honduras
               { "HRV", "HR" },    // Croatia
               { "HUN", "HU" },    // Hungary
               { "IDN", "ID" },    // Indonesia
               { "IND", "IN" },    // India
               { "IRL", "IE" },    // Ireland
               { "IRN", "IR" },    // Iran
               { "IRQ", "IQ" },    // Iraq
               { "ISL", "IS" },    // Iceland
               { "ISR", "IL" },    // Israel
               { "ITA", "IT" },    // Italy
               { "JAM", "JM" },    // Jamaica
               { "JOR", "JO" },    // Jordan
               { "JPN", "JP" },    // Japan
               { "KAZ", "KZ" },    // Kazakhstan
               { "KEN", "KE" },    // Kenya
               { "KGZ", "KG" },    // Kyrgyzstan
               { "KHM", "KH" },    // Cambodia
               { "KOR", "KR" },    // Korea
               { "KWT", "KW" },    // Kuwait
               { "LAO", "LA" },    // Lao P.D.R.
               { "LBN", "LB" },    // Lebanon
               { "LBY", "LY" },    // Libya
               { "LIE", "LI" },    // Liechtenstein
               { "LKA", "LK" },    // Sri Lanka
               { "LTU", "LT" },    // Lithuania
               { "LUX", "LU" },    // Luxembourg
               { "LVA", "LV" },    // Latvia
               { "MAC", "MO" },    // Macao S.A.R.
               { "MAR", "MA" },    // Morocco
               { "MCO", "MC" },    // Principality of Monaco
               { "MDV", "MV" },    // Maldives
               { "MEX", "MX" },    // Mexico
               { "MKD", "MK" },    // Macedonia (FYROM)
               { "MLT", "MT" },    // Malta
               { "MNE", "ME" },    // Montenegro
               { "MNG", "MN" },    // Mongolia
               { "MYS", "MY" },    // Malaysia
               { "NGA", "NG" },    // Nigeria
               { "NIC", "NI" },    // Nicaragua
               { "NLD", "NL" },    // Netherlands
               { "NOR", "NO" },    // Norway
               { "NPL", "NP" },    // Nepal
               { "NZL", "NZ" },    // New Zealand
               { "OMN", "OM" },    // Oman
               { "PAK", "PK" },    // Islamic Republic of Pakistan
               { "PAN", "PA" },    // Panama
               { "PER", "PE" },    // Peru
               { "PHL", "PH" },    // Republic of the Philippines
               { "POL", "PL" },    // Poland
               { "PRI", "PR" },    // Puerto Rico
               { "PRT", "PT" },    // Portugal
               { "PRY", "PY" },    // Paraguay
               { "QAT", "QA" },    // Qatar
               { "ROU", "RO" },    // Romania
               { "RUS", "RU" },    // Russia
               { "RWA", "RW" },    // Rwanda
               { "SAU", "SA" },    // Saudi Arabia
               { "SCG", "CS" },    // Serbia and Montenegro (Former)
               { "SEN", "SN" },    // Senegal
               { "SGP", "SG" },    // Singapore
               { "SLV", "SV" },    // El Salvador
               { "SRB", "RS" },    // Serbia
               { "SVK", "SK" },    // Slovakia
               { "SVN", "SI" },    // Slovenia
               { "SWE", "SE" },    // Sweden
               { "SYR", "SY" },    // Syria
               { "TAJ", "TJ" },    // Tajikistan
               { "THA", "TH" },    // Thailand
               { "TKM", "TM" },    // Turkmenistan
               { "TTO", "TT" },    // Trinidad and Tobago
               { "TUN", "TN" },    // Tunisia
               { "TUR", "TR" },    // Turkey
               { "TWN", "TW" },    // Taiwan
               { "UKR", "UA" },    // Ukraine
               { "URY", "UY" },    // Uruguay
               { "USA", "US" },    // United States
               { "UZB", "UZ" },    // Uzbekistan
               { "VEN", "VE" },    // Bolivarian Republic of Venezuela
               { "VNM", "VN" },    // Vietnam
               { "YEM", "YE" },    // Yemen
               { "ZAF", "ZA" },    // South Africa
               { "ZWE", "ZW" },    // Zimbabwe
			};
			return alphaTo32;
		}

		public static Dictionary<string, string> listOfLanguages()
		{
			Dictionary<string, string> nameToLanguage = new Dictionary<string, string> { {
					"Afrikaans",
					"a"
				}, {
					"Arabic",
					"A"
				}, {
					"Bulgarian",
					"W"
				}, {
					"Catalan",
					"c"

				}, {
					"Chinese",
					"1"
				}, {
					"Croatian",
					"6"
				}, {
					"Customer",
					"Z"
				}, {
					"Czech",
					"C"
				}, {
					"Danish",
					"K"
				}, {
					"Dutch",
					"N"
				}, {
					"English",
					"E"
				}, {
					"Estonian",
					"9"
				}, {
					"Finnish",
					"U"
				}, {
					"French",
					"F"
				}, {
					"German",
					"D"
				}, {
					"Greek",
					"G"
				}, {
					"Hebrew",
					"B"
				}, {
					"Hungarian",
					"H"
				}, {
					"Icelandic",
					"b"
				}, {
					"Indonesian",
					"i"
				}, {
					"Italian",
					"I"
				}, {
					"Japanese",
					"J"
				}, {
					"Korean",
					"3"
				}, {
					"Latvian",
					"Y"
				}, {
					"Lithuanian",
					"X"
				}, {
					"Malaysian",
					"7"
				}, {
					"Norwegian",
					"O"
				}, {
					"Polish",
					"L"
				}, {
					"Portuguese",
					"P"
				}, {
					"Romanian",
					"4"
				}, {
					"Russian",
					"R"
				}, {
					"Serbian",
					"d"
				}, {
					"Slovakian",
					"Q"
				}, {
					"Spanish",
					"S"
				}, {
					"Slovenian",
					"5 "
				}, {
					"Swedish",
					"V"
				}, {
					"Thai",
					"2"
				}, {
					"Turkish",
					"T"
				}, {
					"Ukrainian",
					"8"
				}
			};
			return nameToLanguage;

		}
		public static Dictionary<string, string> listOfNationalities()
		{
			Dictionary<string, string> nameToNationalityList = new Dictionary<string, string> {
				{ "Afghan", "AF" },
				{ "Albanian", "AL" },
				{ "Algerian", "DZ" },
				{ "American", "GU" },
				{ "USA", "US" },
				{ "Andorran", "AD" },
				{ "Angolan", "AO" },
				{ "Anguilla", "AI" },
				{ "Antarctica", "AQ" },
				{ "Antiguan", "AG" },
				{ "Argentinian", "AR" },
				{ "Armenian", "AM" },
				{ "Arubanic", "AW" },
				{ "Australian", "AU" },
				{ "Austrian", "AT" },
				{ "Azerbaijani", "AZ" },
				{ "Bahaman", "BS" },
				{ "Bangladeshi", "BD" },
				{ "Barbadan", "BB" },
				{ "Belgian", "BE" },
				{ "Belizean", "BZ" },
				{ "Belorussian", "BY" },
				{ "Beninese", "BJ" },
				{ "Bermudan", "BM" },
				{ "Bharaini", "BH" },
				{ "Bhutanese", "BT" },
				{ "Bolivian", "BO" },
				{ "Bosnian", "BA" },
				{ "Botswanan", "BW" },
				{ "Bouvet Islands", "BV" },
				{ "Brazilian", "BR" },
				{ "Brit.Ind.Oc.Ter", "IO" },
				{ "British", "GB" },
				{ "Brunei", "BN" },
				{ "Bulgarian", "BG" },
				{ "Burkina-Faso", "BF" },
				{ "Burundi", "BI" },
				{ "Cambodian", "KH" },
				{ "Cameroonian", "CM" },
				{ "Canadian", "CA" },
				{ "Cape Verdian", "CV" },
				{ "Central African", "CF" },
				{ "Chadian", "TD" },
				{ "Chilean", "CL" },
				{ "Chinese", "CN" },
				{ "Colombian", "CO" },
				{ "Comoran", "KM" },
				{ "Congolese", "CD" },
				{ "Cook Islands", "CK" },
				{ "Costa Rican", "CR" },
				{ "Croatian", "HR" },
				{ "Cuban", "CU" },
				{ "Cypriot", "CY" },
				{ "Czech", "CZ" },
                { "Danish", "DK" },
                { "Germany", "DE" },
				{ "Djiboutian", "DJ" },
				{ "Dominican", "DM" },
				{ "Dutch", "NL" },
				{ "East Timor", "TP" },
				{ "Ecuadorian", "EC" },
				{ "Egyptian", "EG" },
				{ "Equatorial Guin", "GQ" },
				{ "Eritrean", "ER" },
				{ "Estonian", "EE" },
				{ "Ethiopian", "ET" },
				{ "f. St Kitts/Nev", "KN" },
				{ "F. Trinidad & T", "TT" },
				{ "Fijian", "FJ" },
				{ "Filipino", "PH" },
				{ "Finnish", "FI" },
				{ "French", "FR" },
				{ "Gabonese", "GA" },
				{ "Gambian", "GM" },
				{ "Georgian", "GE" },
				{ "German", "D" },
				{ "Ghanian", "GH" },
				{ "Gibraltar", "GI" },
				{ "Greek", "GR" },
				{ "Grenadian", "GD" },
				{ "Guatemalan", "GT" },
				{ "Guinean", "GN" },
				{ "Guyanese", "GY" },
				{ "Haitian", "HT" },
				{ "Heard/McDon.Isl", "HM" },
				{ "Honduran", "HN" },
				{ "Hong Kong", "HK" },
				{ "Hungarian", "HU" },
				{ "Icelandic", "IS" },
				{ "Indian", "IN" },
				{ "Indonesian", "ID" },
				{ "Iranian", "IR" },
				{ "Iraqi", "IQ" },
				{ "Irish", "IE" },
				{ "Israeli", "IL" },
				{ "Italian", "IT" },
				{ "Ivory Coast", "CI" },
				{ "Jamaican", "JM" },
				{ "Japanese", "JP" },
				{ "Jordanian", "JO" },
				{ "Kazakh", "KZ" },
				{ "Kenyan", "KE" },
				{ "Kiribati", "KI" },
				{ "Korean", "KP" },
				{ "Kuwaiti", "KW" },
				{ "Kyrgyzstani", "KG" },
				{ "Laotian", "LA" },
				{ "Latvian", "LV" },
				{ "Lebanese", "LB" },
				{ "Lesothan", "LS" },
				{ "Liberian", "LR" },
				{ "Libyan", "LY" },
				{ "Liechtenstein", "LI" },
				{ "Lithuanian", "LT" },
				{ "Lucian", "LC" },
				{ "Luxembourgian", "LU" },
				{ "Macedonian", "MK" },
				{ "Madagascan", "MG" },
				{ "Malawian", "MW" },
				{ "Malaysian", "MY" },
				{ "Maldivian", "MV" },
				{ "Malian", "ML" },
				{ "Maltese", "MT" },
				{ "Marianian", "MP" },
				{ "Marshall Islnds", "MH" },
				{ "Mauretanian", "MR" },
				{ "Mauritian", "MU" },
				{ "Mexican", "MX" },
				{ "Micronesian", "FM" },
				{ "Minor Outl.Isl.", "UM" },
				{ "Moldavian", "MD" },
				{ "Monegasque", "MC" },
				{ "Mongolian", "MN" },
				{ "Montserrat", "MS" },
				{ "Moroccan", "MA" },
				{ "Mozambican", "MZ" },
				{ "Myanmar", "MM" },
				{ "Namibian", "NA" },
				{ "Nauruian", "NR" },
				{ "Nepalese", "NP" },
				{ "New Zealand", "NZ" },
				{ "Nicaraguan", "NI" },
				{ "Nigerian", "NG" },
				{ "Nigerois", "NE" },
				{ "Niue Islands", "NU" },
				{ "Norfolk Islands", "NF" },
				{ "Norwegian", "NO" },
				{ "Omani", "OM" },
				{ "Pakistani", "PK" },
				{ "Palau", "PW" },
				{ "Panamanian", "PA" },
				{ "Pap.New Guinea", "PG" },
				{ "Paraguayan", "PY" },
				{ "Peruvian", "PE" },
				{ "Polish", "PL" },
				{ "Portuguese", "MO" },
				{ "Qatar", "QA" },
				{ "Rumanian", "RO" },
				{ "Russian", "RU" },
				{ "Rwandan", "RW" },
				{ "Salvdorean", "SV" },
				{ "Samoan", "AS" },
				{ "San Marinese", "SM" },
				{ "Santomeic", "ST" },
				{ "Saudi Arabian", "SA" },
				{ "Senegalese", "SN" },
				{ "Seychellian", "SC" },
				{ "Sierra Leonean", "SL" },
				{ "Singaporean", "SG" },
				{ "Slovakian", "SK" },
				{ "Slovenian", "SI" },
				{ "Solomonese", "SB" },
				{ "Somalian", "SO" },
				{ "South African", "ZA" },
				{ "South Georgia", "GS" },
				{ "Spanish", "ES" },
				{ "Sri Lankan", "LK" },
				{ "St. Helena", "SH" },
				{ "Sudanese", "SD" },
				{ "Surinamese", "SR" },
				{ "Swazi", "SZ" },
				{ "Swedish", "SE" },
				{ "Swiss", "CH" },
				{ "Syrian", "SY" },
				{ "Taiwanese", "TW" },
				{ "Tajik", "TJ" },
				{ "Tanzanian", "TZ" },
				{ "Thai", "TH" },
				{ "Togolese", "TG" },
				{ "Tokelau Islands", "TK" },
				{ "Tongan", "TO" },
				{ "Tunisian", "TN" },
				{ "Turkish", "TR" },
				{ "Turkmenian", "TM" },
				{ "Turksh Caicosin", "TC" },
				{ "Tuvaluese", "TV" },
				{ "Ugandan", "UG" },
				{ "Ukrainian", "UA" },
				{ "Unit.Arab Emir.", "AE" },
				{ "Uruguayan", "UY" },
				{ "Uzbekistani", "UZ" },
				{ "Vanuatese", "VU" },
				{ "Vatican City", "VA" },
				{ "Venezuelan", "VE" },
				{ "Vietnamese", "VN" },
				{ "Vincentian", "VC" },
				{ "Wallis,Futuna", "WF" },
				{ "Yemeni", "YE" },
				{ "Yugoslavian", "YU" },
				{ "Zambian", "ZM" },
				{ "Zimbabwean", "ZW" },
					};
			return nameToNationalityList;
		}

		public static Dictionary<string, string> listOfCountries()
		{
			Dictionary<string, string> nameToCountry = new Dictionary<string, string> {
				{ "Afghanistan", "AF" },
				{ "Albania", "AL" },
				{ "Algeria", "DZ" },
				{ "Amer.Virgin", "VI" },
				{ "Andorra", "AD" },
				{ "Angola", "AO" },
				{ "Anguilla", "AI" },
				{ "Antarctica", "AQ" },
				{ "Antigua/Barbuda", "AG" },
				{ "Argentina", "AR" },
				{ "Armenia", "AM" },
				{ "Aruba", "AW" },
				{ "Australia", "AU" },
				{ "Austria", "AT" },
				{ "Azerbaijan", "AZ" },
				{ "Bahamas", "BS" },
				{ "Bahrain", "BH" },
				{ "Bangladesh", "BD" },
				{ "Barbados", "BB" },
				{ "Belarus", "BY" },
				{ "Belgium", "BE" },
				{ "Belize", "BZ" },
				{ "Benin", "BJ" },
				{ "Bermuda", "BM" },
				{ "Bhutan", "BT" },
				{ "Bolivia", "BO" },
				{ "Bosnia-Herz.", "BA" },
				{ "Botswana", "BW" },
				{ "Bouvet", "BV" },
				{ "Brazil", "BR" },
				{ "Brit.Ind.Oc.Ter", "IO" },
				{ "Brit.Virgin", "VG" },
				{ "Brunei", "BN" },
				{ "Bulgaria", "BG" },
				{ "Burkina-Faso", "BF" },
				{ "Burundi", "BI" },
				{ "Cambodia", "KH" },
				{ "Cameroon", "CM" },
				{ "Canada", "CA" },
				//				{ "Canada", "CAN" },
				{ "Cape", "CV" },
				{ "Cayman", "KY" },
				{ "Central", "CF" },
				{ "Chad", "TD" },
				{ "Chile", "CL" },
				{ "China", "CN" },
				{ "Christmas", "CX" },
				{ "Coconut", "CC" },
				{ "Colombia", "CO" },
				{ "Comoros", "KM" },
				{ "Congo", "CD" },
				//				{ "Congo", "CG" },
				{ "Cook", "CK" },
				{ "Costa", "CR" },
				{ "Croatia", "HR" },
				{ "Cuba", "CU" },
				{ "Cyprus", "CY" },
				{ "Czech", "CZ" },
				{ "Denmark", "DK" },
				{ "Djibouti", "DJ" },
				{ "Dominica", "DM" },
				//				{ "Dominican", "DO" },
				{ "Dutch", "AN" },
				{ "East", "TP" },
				{ "Ecuador", "EC" },
				{ "Egypt", "EG" },
				{ "El", "SV" },
				{ "Equatorial", "GQ" },
				{ "Eritrea", "ER" },
				{ "Estonia", "EE" },
				{ "Ethiopia", "ET" },
				{ "Falkland", "FK" },
				{ "Faroe", "FO" },
				{ "Fiji", "FJ" },
				{ "Finland", "FI" },
				{ "France", "FR" },
				{ "Frenc.Polynesia", "PF" },
				{ "French", "GF" },
				//				{ "French", "TF" },
				{ "Gabon", "GA" },
				{ "Gambia", "GM" },
				{ "Georgia", "GE" },
				//				{ "Germany", "D" },
				{ "Germany", "DE" },
				{ "Ghana", "GH" },
				{ "Gibraltar", "GI" },
				{ "Greece", "GR" },
				{ "Greenland", "GL" },
				{ "Grenada", "GD" },
				{ "Guadeloupe", "GP" },
				{ "Guam", "GU" },
				{ "Guatemala", "GT" },
				{ "Guinea", "GN" },
				{ "Guinea-Bissau", "GW" },
				{ "Guyana", "GY" },
				{ "Haiti", "HT" },
				{ "Heard/McDon.Isl", "HM" },
				{ "Honduras", "HN" },
				{ "Hong", "HK" },
				{ "Hungary", "HU" },
				{ "Iceland", "IS" },
				{ "India", "IN" },
				{ "Indonesia", "ID" },
				{ "Iran", "IR" },
				{ "Iraq", "IQ" },
				{ "Ireland", "IE" },
				{ "Israel", "IL" },
				{ "Italy", "IT" },
				{ "Ivory", "CI" },
				{ "Jamaica", "JM" },
				{ "Japan", "JP" },
				{ "Jordan", "JO" },
				{ "Kazakhstan", "KZ" },
				{ "Kenya", "KE" },
				{ "Kiribati", "KI" },
				{ "Kuwait", "KW" },
				{ "Kyrgyzstan", "KG" },
				{ "Laos", "LA" },
				{ "Latvia", "LV" },
				{ "Lebanon", "LB" },
				{ "Lesotho", "LS" },
				{ "Liberia", "LR" },
				{ "Libya", "LY" },
				{ "Liechtenstein", "LI" },
				{ "Lithuania", "LT" },
				{ "Luxembourg", "LU" },
				{ "Macau", "MO" },
				{ "Macedonia", "MK" },
				{ "Madagascar", "MG" },
				{ "Malawi", "MW" },
				{ "Malaysia", "MY" },
				{ "Maldives", "MV" },
				{ "Mali", "ML" },
				{ "Malta", "MT" },
				{ "Marshall", "MH" },
				{ "Martinique", "MQ" },
				{ "Mauretania", "MR" },
				{ "Mauritius", "MU" },
				{ "Mayotte", "YT" },
				{ "Mexico", "MX" },
				{ "Micronesia", "FM" },
				{ "Minor", "UM" },
				{ "Moldavia", "MD" },
				{ "Monaco", "MC" },
				{ "Mongolia", "MN" },
				{ "Montserrat", "MS" },
				{ "Morocco", "MA" },
				{ "Mozambique", "MZ" },
				{ "Myanmar", "MM" },
				{ "N.Mariana", "MP" },
				{ "Namibia", "NA" },
				{ "Nauru", "NR" },
				{ "Nepal", "NP" },
				{ "Netherlands", "NL" },
				//				{ "New", "NC" },
				{ "New", "NZ" },
				{ "Nicaragua", "NI" },
				{ "Niger", "NE" },
				{ "Nigeria", "NG" },
				{ "Niue", "NU" },
				{ "Norfolk", "NF" },
				{ "North", "KP" },
				{ "Norway", "NO" },
				{ "Oman", "OM" },
				{ "Pakistan", "PK" },
				{ "Palau", "PW" },
				{ "Panama", "PA" },
				{ "Pap.", "PG" },
				{ "Paraguay", "PY" },
				{ "Peru", "PE" },
				{ "Philippines", "PH" },
				{ "Pitcairn", "PN" },
				{ "Poland", "PL" },
				{ "Portugal", "PT" },
				{ "Puerto", "PR" },
				{ "Qatar", "QA" },
				{ "Reunion", "RE" },
				{ "Romania", "RO" },
				{ "Ruanda", "RW" },
				{ "Russian", "RU" },
				{ "S.", "GS" },
				{ "S.Tome,Principe", "ST" },
				{ "Samoa,", "AS" },
				{ "San", "SM" },
				{ "Saudi", "SA" },
				{ "Senegal", "SN" },
				{ "Seychelles", "SC" },
				{ "Sierra", "SL" },
				{ "Singapore", "SG" },
				{ "Slovakia", "SK" },
				{ "Slovenia", "SI" },
				{ "Solomon", "SB" },
				{ "Somalia", "SO" },
				{ "South", "ZA" },
				//				{ "South", "KR" },
				{ "Spain", "ES" },
				{ "Sri Lanka", "LK" },
				{ "St", "KN" },
				{ "St.", "SH" },
				//				{ "St.", "LC" },
				//				{ "St.", "VC" },
				{ "St.Pier,Miquel.", "PM" },
				{ "Sudan", "SD" },
				{ "Suriname", "SR" },
				{ "Svalbard", "SJ" },
				{ "Swaziland", "SZ" },
				{ "Sweden", "SE" },
				{ "Switzerland", "CH" },
				{ "Syria", "SY" },
				{ "Taiwan", "TW" },
				{ "Tajikstan", "TJ" },
				{ "Tanzania", "TZ" },
				{ "Thailand", "TH" },
				{ "Togo", "TG" },
				{ "Tokelau", "TK" },
				{ "Tonga", "TO" },
				{ "Trinidad,Tobago", "TT" },
				{ "Tunisia", "TN" },
				{ "Turkey", "TR" },
				{ "Turkmenistan", "TM" },
				{ "Turksh", "TC" },
				{ "Tuvalu", "TV" },
				{ "Uganda", "UG" },
				{ "Ukraine", "UA" },
				{ "United Kingdom", "GB" },
				{ "Uruguay", "UY" },
				{ "USA", "US" },
				{ "Utd.Arab", "AE" },
				{ "Uzbekistan", "UZ" },
				{ "Vanuatu", "VU" },
				{ "Vatican", "VA" },
				{ "Venezuela", "VE" },
				{ "Vietnam", "VN" },
				{ "Wallis,Futuna", "WF" },
				{ "West", "EH" },
				{ "Western", "WS" },
				{ "Yemen", "YE" },
				{ "Yugoslavia", "YU" },
				{ "Zambia", "ZM" },
				{ "Zimbabwe", "ZW" },

			};
			return nameToCountry;
		}
	}
}

