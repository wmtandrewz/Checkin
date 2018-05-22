using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Collections.ObjectModel;
using checkin;

namespace Checkin
{
	public class MainGuestInformation
	{

		//Data sources
		CheckInManager checkInManager = new CheckInManager();
		MainGuestDetails maingGuestDetails = new MainGuestDetails();

		ReservationDetails reservationDetails = new ReservationDetails();
		RoomStatus roomStatus = new Checkin.RoomStatus();
		roomStatus roomStatusObject = new roomStatus();

		//List Collection
		List<guestDetails> guestdetails = new List<guestDetails>();

		//Creating obervable collection
		ItemList items;


		RemarkDetails remarkDetails = new RemarkDetails();

		public async Task<MainGuestDetails> mainGuestInformation(string reservationID)
		{
			try
			{
				Constants.resultMain = "";
				Constants.resultMain = await checkInManager.GetRemarksDetails(reservationID);
				string tem_resultMain = Constants.resultMain; // + Vinoch 25082017
				if (Constants.resultMain == null || Constants.resultMain == "Error")
				{
					MessagingCenter.Send<MainGuestInformation, string>(this, "nodetailsAvailable", "");
				}
				else
				{
					Constants.resultMain = Constants.resultMain.Replace("Date(-", "Date(");
					var output = JObject.Parse(Constants.resultMain);

					if (Enumerable.Count(output["d"]["results"][0]["ReservationNaviGuest"]["results"]) > 0)
					{
						string guestSalutationKey = string.Empty;
						maingGuestDetails.ReservationID = Constants._reservation_id;
						maingGuestDetails.ClientName = serviceDataValidation.validation(Convert.ToString(output["d"]["results"][0]["Xclientname"]));
						maingGuestDetails.HotelName = serviceDataValidation.validation(Convert.ToString(output["d"]["results"][0]["XhotelName"]));

						string titleName = Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][0]["Title"]);
						guestSalutationKey = serviceDataValidation.titleName(titleName);

						maingGuestDetails.Title = serviceDataValidation.validation(guestSalutationKey);
						maingGuestDetails.FirstName = serviceDataValidation.validation(Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][0]["Name1"]));
						maingGuestDetails.LastName = serviceDataValidation.validation(Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][0]["Name2"]));
						maingGuestDetails.Email = serviceDataValidation.validation(Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][0]["SmtpAddr"]));
						maingGuestDetails.ContactNumber = serviceDataValidation.validation(Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][0]["MobileNo"]));

						//string dateOfBirthWithTime = serviceDataValidation.validation(Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][0]["Gbdat"]));
						string dateOfBirth = Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][0]["Gbdat"]);
						if (dateOfBirth != Constants._notAvailable)
						{
							//DateTime dt = DateTime.Parse(dateOfBirthWithTime);  // - Vinoch 25082017
							if (dateOfBirth != "")
							{
								//Begin of + Vinoch 25082017
								DateTime date = DateTime.Now;
								try
								{
									DateTime dt = DateTime.Now;
									string temp_substring = tem_resultMain.Substring(0, tem_resultMain.IndexOf("Gbdat") + 20);
									temp_substring = temp_substring.Substring(temp_substring.IndexOf("Gbdat"));
									//Begin of + Vinoch 1512206
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
										if (dateOfBirth != "")//- Vinoch 151220
											dt = DateTime.Parse(dateOfBirth); // Vinoch 1512201
									}

									//DateTime date = serviceDataValidation.dateOfBirthValidation(dateOfBirth);
									date = dt;
									maingGuestDetails.DateOfBirth = date.ToString("dd-MM-yyyy");
								}
								catch (Exception)
								{
									maingGuestDetails.DateOfBirth = Constants._notAvailable;
								}
							}
							else
							{
								maingGuestDetails.DateOfBirth = Constants._notAvailable;
							}
							//End of + Vinoch 2508201
						}
						else
						{
							maingGuestDetails.DateOfBirth = Constants._notAvailable;
						}

						maingGuestDetails.NicPassNumber = serviceDataValidation.validation(Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][0]["XnumeroDoc"]));
						maingGuestDetails.HouseNumber = serviceDataValidation.validation(Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][0]["HouseNum1"]));
						maingGuestDetails.Street = serviceDataValidation.validation(Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][0]["Street"]));
						maingGuestDetails.City = serviceDataValidation.validation(Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][0]["City1"]));
						maingGuestDetails.Country = serviceDataValidation.validation(Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][0]["Landx"]));
						maingGuestDetails.Nationality = serviceDataValidation.validation(Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][0]["Natio50"]));
					}
				}
			}
			catch (Exception e)
			{
				MessagingCenter.Send<MainGuestInformation, string>(this, "nodetailsAvailable", "");
			}
			return maingGuestDetails;
		}

		public async Task<ReservationDetails> reservationInformation()
		{

			try
			{
				var output = JObject.Parse(Constants.resultMain);

				reservationDetails.ClientName = serviceDataValidation.validation(Convert.ToString(output["d"]["results"][0]["Xclientname"]));

				string temporary_date_holder = output["d"]["results"][0]["XfLlgda"].ToString();
				temporary_date_holder = temporary_date_holder.Split('(', ')')[0];
				DateTime dt1 = DateTime.Parse(temporary_date_holder);
				int arrivalDateDay = int.Parse(dt1.Day.ToString());
				reservationDetails.ArrivalDate = dt1.ToString("dd-MM-yyyy");


				string temporary_date_holder2 = output["d"]["results"][0]["XfSlida"].ToString();
				temporary_date_holder2 = temporary_date_holder2.Split('(', ')')[0];
				DateTime dt2 = DateTime.Parse(temporary_date_holder2);
				int departureDateDay = int.Parse(dt2.Day.ToString());
				reservationDetails.DepartureDate = dt2.ToString("dd-MM-yyyy");

				reservationDetails.NoOfNights = Convert.ToString((dt2 - dt1).TotalDays);

				//number of guestsy
				string adult = "", child = "", guide = "", freeChild = "";

				if (serviceDataValidation.paxValidation(Convert.ToString(output["d"]["results"][0]["XnumPax1"])) != "")
				{
					adult = "Adults (" + serviceDataValidation.paxValidation(Convert.ToString(output["d"]["results"][0]["XnumPax1"])) + ")";
				}
				if (serviceDataValidation.paxValidation(Convert.ToString(output["d"]["results"][0]["XnumPax2"])) != "")
				{
					child = "Child (" + serviceDataValidation.paxValidation(Convert.ToString(output["d"]["results"][0]["XnumPax2"])) + ")";
				}
				if (serviceDataValidation.paxValidation(Convert.ToString(output["d"]["results"][0]["XnumPax3"])) != "")
				{
					guide = "Guide (" + serviceDataValidation.paxValidation(Convert.ToString(output["d"]["results"][0]["XnumPax3"])) + ")";
				}
				if (serviceDataValidation.paxValidation(Convert.ToString(output["d"]["results"][0]["XnumPax4"])) != "")
				{
					freeChild = "Child Free (" + serviceDataValidation.paxValidation(Convert.ToString(output["d"]["results"][0]["XnumPax4"])) + ")";
				}

				reservationDetails.PaxCount = adult + child + guide + freeChild;
				reservationDetails.RoomType = serviceDataValidation.validation(Convert.ToString(output["d"]["results"][0]["XtipoHabDes"]));
				reservationDetails.UpgradedRoomType = serviceDataValidation.validation(Convert.ToString(output["d"]["results"][0]["XtipoHabUpgDes"]));
				reservationDetails.UpgradedRoomTypeColor = serviceDataValidation.validationColor(reservationDetails.UpgradedRoomType);
				reservationDetails.RoomNumber = serviceDataValidation.validation(Convert.ToString(output["d"]["results"][0]["XhabitacionId"]));
				reservationDetails.RoomColor = serviceDataValidation.validationColor(reservationDetails.RoomNumber);
				reservationDetails.MealPlan = serviceDataValidation.validation(Convert.ToString(output["d"]["results"][0]["XregimenDes"]));

				if (reservationDetails.RoomNumber != Constants._notAvailable)
				{
					roomStatus roomStatusResult = await roomStatus.roomStatusDetails(reservationDetails.RoomNumber);

					if (roomStatusResult != null)
					{
						reservationDetails.RoomStatus = roomStatusResult.RoomstatusDetail;
						reservationDetails.RoomStatusColor = roomStatusResult.RoomStatusColor;
						Constants._roomStatus = roomStatusResult.RoomstatusDetail;
					}
				}
				else
				{
					reservationDetails.RoomStatus = Constants._notAvailable;
					reservationDetails.RoomStatusColor = Color.Red;
				}
				if (Enumerable.Count(output["d"]["results"][0]["ReservationFlightSet01"]["results"]) != 0)
				{
					if (Convert.ToString(output["d"]["results"][0]["ReservationFlightSet01"]["results"][0]["ArrFlightNo"]) != "")
					{

						reservationDetails.ArrivalFlight = Convert.ToString(output["d"]["results"][0]["ReservationFlightSet01"]["results"][0]["ArrFlightNo"]) + "  " +
							Convert.ToString(output["d"]["results"][0]["ReservationFlightSet01"]["results"][0]["ArrFlightTi"]);
                            

						if (!string.IsNullOrEmpty(reservationDetails.ArrivalFlight) && reservationDetails.ArrivalFlight.Contains(" "))
						{
							var splitted = reservationDetails.ArrivalFlight.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
							Constants._arrFlight = splitted[0];
							Constants._arrFlightTIme = GeneratePayloadDate(splitted[1]);
						}
						else
						{
							Constants._arrFlight = "";
							Constants._arrFlightTIme = "";
						}

					}
					else
					{
						reservationDetails.ArrivalFlight = Constants._notAvailable;
						reservationDetails.ArrivalFlightColor = Color.Red;
					}
					if (Convert.ToString(output["d"]["results"][0]["ReservationFlightSet01"]["results"][0]["DepFlightNo"]) != "")
					{
						reservationDetails.DepartureFlight = Convert.ToString(output["d"]["results"][0]["ReservationFlightSet01"]["results"][0]["DepFlightNo"]) + "  " +
							Convert.ToString(output["d"]["results"][0]["ReservationFlightSet01"]["results"][0]["DepFlightTi"]);

						if (!string.IsNullOrEmpty(reservationDetails.DepartureFlight) && reservationDetails.DepartureFlight.Contains(" "))
                        {
							var splitted = reservationDetails.DepartureFlight.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                            Constants._depFlight = splitted[0];
							Constants._depFlightTIme = GeneratePayloadDate(splitted[1]);
                        }
                        else
                        {
                            Constants._depFlight = "";
                            Constants._depFlightTIme = "";
                        }
					}
					else
					{
						reservationDetails.DepartureFlight = Constants._notAvailable;
						reservationDetails.DepartureFlightColor = Color.Red;
					}
				}

			}
			catch (Exception)
			{

			}
			return reservationDetails;
		}

		private string GeneratePayloadDate(string date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                var splited = date.Split(':');
                string payloadDate = $"PT{splited[0]}H{splited[1]}M{splited[2]}S";
                return payloadDate;
            }
            else
            {
                return "PT00H00M00S";
            }
        }

		//public async Task<ItemList> guestInformation()
		public async Task<List<guestDetails>> guestInformation()
		{
			int guestNumber = 1;


			try
			{
				var output = JObject.Parse(Constants.resultMain);
				if (Enumerable.Count(output["d"]["results"][0]["ReservationNaviGuest"]["results"]) > 0)
				{
					for (int i = 0; i < Enumerable.Count(output["d"]["results"][0]["ReservationNaviGuest"]["results"]); i++)
					{
						string numberofTotalVisits = "0";
						string numberofVisits = "0";
						string totalRevenue = "0.00";
						string revenueRoom = "0.00";
						string revenueFnB = "0.00";
						string revenueOther = "0.00";
						string guestCountryKeyValue = string.Empty;
						string guestSalutationKeyValue = string.Empty;

						Constants._clientName = Convert.ToString(output["d"]["results"][0]["Xclientname"]);
						string dateOfBirth = Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][i]["Gbdat"]);
						DateTime date = serviceDataValidation.dateOfBirthValidation(dateOfBirth);
						try
						{
							if (Enumerable.Count(output["d"]["results"][0]["ReservationNaviSapGuests"]["results"]) != 0)
							{
								numberofTotalVisits = serviceDataValidation.hotelVisitValidation(Convert.ToString(output["d"]["results"][0]["ReservationNaviSapGuests"]["results"][i]["Totalvisit"]).TrimStart(new Char[] { '0' }).Trim());
								numberofVisits = serviceDataValidation.hotelVisitValidation(Convert.ToString(output["d"]["results"][0]["ReservationNaviSapGuests"]["results"][i]["Visitperhotel"]).TrimStart(new Char[] { '0' }).Trim());
								totalRevenue = serviceDataValidation.hotelrRevenueValidation(Convert.ToString(output["d"]["results"][0]["ReservationNaviSapGuests"]["results"][i]["RevenueTotal"]).TrimStart(new Char[] { '0' }).Trim());
								revenueRoom = serviceDataValidation.hotelrRevenueValidation(Convert.ToString(output["d"]["results"][0]["ReservationNaviSapGuests"]["results"][i]["RevenueRoom"]).TrimStart(new Char[] { '0' }).Trim());
								revenueFnB = serviceDataValidation.hotelrRevenueValidation(Convert.ToString(output["d"]["results"][0]["ReservationNaviSapGuests"]["results"][i]["RevenueFnb"]).TrimStart(new Char[] { '0' }).Trim());
								revenueOther = serviceDataValidation.hotelrRevenueValidation(Convert.ToString(output["d"]["results"][0]["ReservationNaviSapGuests"]["results"][i]["RevenueOther"]).TrimStart(new Char[] { '0' }).Trim());
							}
						}
						catch (Exception e)
						{
						}
						string country = Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][i]["Country"]);
						//List of Countries from dictionary
						var nameToCountry = listOfCountries();




						if (country != "" || country != "")
						{
							//Item Value in dictionary
							string name = nameToCountry.FirstOrDefault(x => x.Value == country).Key;
							guestCountryKeyValue = name;

						}

						guestdetails.Add(new guestDetails(guestNumber,
														  Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][i]["XtipoDocId"]),
														  Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][i]["XnumeroDoc"]),
														  Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][i]["Title"]),
														  Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][i]["McName1"]),
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
														  date.ToString(),
						                                  "",
														  Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][i]["Langu"]),
														  Convert.ToString(output["d"]["results"][0]["ReservationNaviGuest"]["results"][i]["Kunnr"]).TrimStart(new Char[] { '0' }),
														  numberofTotalVisits,
														  numberofVisits,
														  totalRevenue,
														  revenueRoom,
														  revenueFnB,
														  revenueOther));
						guestNumber = guestNumber + 1;
					}
				}
			}
			catch (Exception e)
			{
			}
			return guestdetails;
		}

		public async Task<RemarkDetails> remarkInformation()
		{
			try
			{
				var output = JObject.Parse(Constants.resultMain);

				remarkDetails.MainRemark = Convert.ToString(output["d"]["results"][0]["Xobserv1"]);
				//Main remarkv
				Constants._clientName = Convert.ToString(output["d"]["results"][0]["Xclientname"]);
				if (Enumerable.Count(output["d"]["results"][0]["ReservationNaviRemarks"]["results"]) > 0)
				{
					remarkDetails.SubRemark = Convert.ToString(output["d"]["results"][0]["ReservationNaviRemarks"]["results"][0]["Xobservacion"]);
				}
			}
			catch (Exception)
			{

			}
			return remarkDetails;
		}
		static Dictionary<string, string> listOfCountries()
		{

			//Add values and keys to dictionary
			var nameToCountry = CountryDictionary.listOfCountries();
			return nameToCountry;
		}


	}
}

