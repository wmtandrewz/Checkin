using System.Threading.Tasks;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Checkin
{
	public class CheckInManager
	{
		userLogout userLogout = new userLogout();
		public async Task<String> HotelCodeAsync()
		{
			string url = "/sap/opu/odata/sap/ZTMS_GET_USER_HOTEL_SRV/EX_XHOTEL_IDSet(' ')?$format=json";
			return await this.GetODataService(url);
		}
		public async Task<String> HotelDateAsync()
		{
			string url = "/sap/opu/odata/sap/ZTMS_HOTEL_DETAILS_SRV/HotelInfoSet?$filter=XhotelId eq '" + Constants._hotel_code + "'&$format=json";
			return await this.GetODataService(url);
		}

		public async Task<String> GetReservationsListAsync(String hotel_date)
		{
			DateTime dt = DateTime.ParseExact(hotel_date, "dd-MM-yyyy",
							  CultureInfo.InvariantCulture);
			string temporary_date_holder = dt.ToString("yyyy-MM-dd");

			string url = "/sap/opu/odata/sap/ZTMS_GET_RESERVATION_N3_SRV_01/ReservationHeaderSet?$filter=XarrivalDt eq datetime'" + temporary_date_holder + "T00:00:00' and HtlId eq '" + Constants._hotel_code + "'&$expand=ReservationNaviGuest,ReservationNaviSapGuests&$format=json";
			return await this.GetODataService(url);
		}

		public async Task<String> GetRemarksDetails(string reservationID)
		{
			string url = "/sap/opu/odata/sap/ZTMS_GET_RESERVATION_N3_SRV_01/ReservationHeaderSet?$filter=HtlId eq '" + Constants._hotel_code + "' and ZresvId eq '" + reservationID + "'&$expand=ReservationNaviGuest/ReservationHotelVisitsSet,ReservationNaviSapGuests,ReservationNaviRemarks,ReservationFlightSet01&$format=json";
			return await this.GetODataService(url);
		}

		public async Task<String> GetReservationsDetails(string reservationId)
		{
			string url = "/sap/opu/odata/sap/ZTMS_GET_RESERVATION_N3_SRV_01/ReservationHeaderSet?$filter=HtlId eq '" + Constants._hotel_code + "' and ZresvId eq '" + reservationId + "'&$expand=ReservationNaviGuest,ReservationNaviSapGuests&$format=json";
			return await this.GetODataService(url);
		}

		public async Task<String> GetReservationDetailsRoomNumber(string roomNumber)
		{
			string url = "/sap/opu/odata/sap/ZTMS_GET_RESERVATION_N3_SRV_01/ReservationHeaderSet?$filter=HtlId eq '" + Constants._hotel_code + "' and XroomNo eq '" + roomNumber + "'&$expand=ReservationNaviGuest,ReservationNaviSapGuests&$format=json";
			return await this.GetODataService(url);
		}

		public async Task<String> GetReservationDetailsDepartureDate()
		{
			string url = "/sap/opu/odata/sap/ZTMS_GET_RESERVATION_N3_SRV_01/ReservationHeaderSet?$filter=HtlId eq '" + Constants._hotel_code + "' and XarrivalDt eq datetime'2014-10-10T00:00:00'";
			return await this.GetODataService(url);
		}

		public async Task<String> GetRoomsDetails(string roomStatus)
		{
			string url = "/sap/opu/odata/sap/ZTMS_GET_ROOMS_LIST_SRV/ZTMS_SEARCH_ROOM001Set?$filter=ImHotelId eq '" + Constants._hotel_code + "' and ImReservaId eq '" + Constants._reservation_id + "' and ImCleanOnly eq '" + roomStatus + "'&$format=json";
			return await this.GetODataService(url);
		}

		public async Task<String> GetRoomsStatus(string roomNumber)
		{
			string url = "/sap/opu/odata/sap/ZTMS_GET_ROOM_STATUS_SRV/GetRoomStatusSet?$filter=XhotelId eq '" + Constants._hotel_code + "' and XroomId eq '" + roomNumber + "'&$format=json";
			return await this.GetODataService(url);
		}

		public async Task<String> GetGuestSignature(string guestNumber)
		{
			string url = "/sap/opu/odata/sap/ZTMS_IMG_SRV/Checkins(XRESERVA_ID='" + Constants._reservation_id + "',XHOTEL_ID='" + Constants._hotel_code + "',XPOSITION='" + guestNumber + "')?$format=json";
			return await this.GetODataService(url);
		}

		public async Task<String> GetGuestDetailsThroughPassportNumber(string indentificationMethod, string identificationNumber)
		{
			string url = "/sap/opu/odata/sap/ZTMS_SEARCH_GUEST_V1_SRV/guestDetailsSet?$filter=IdNumber eq '" + identificationNumber + "' and IdType eq '" + indentificationMethod + "' and HtlId eq '" + Constants._hotel_code + "'&$expand=guestHistorySet&$format=json";
			return await this.GetODataService(url);
		}

		public async Task<String> GetPerformaDetails(string reservationID)
		{
			string url = "/sap/opu/odata/sap/ZTMS_GET_PROFORMA_V1_SRV/profomaHeaderSet?$filter=ImXhotelId eq '" + Constants._hotel_code + "' and ImXreservaId eq '" + reservationID + "'&$expand=advanceLinesSet,profomaLinesSet&$format=json";
			return await this.GetODataService(url);
		}
		public async Task<String> SendPerformaEmailAvailable(string reservationID)
		{
			string url = "/sap/opu/odata/sap/ZTMS_GET_PROFORMA_V1_SRV/profomaHeaderSet?$filter=ImXhotelId eq '" + Constants._hotel_code + "' and ImXreservaId eq '" + reservationID + "' and ImSendEmail eq 'X'";
			return await this.GetODataService(url);
		}

		public async Task<String> SendPerformaEmailNotAvailable(string reservationID, string email)
		{
			string url = "/sap/opu/odata/sap/ZTMS_GET_PROFORMA_V1_SRV/profomaHeaderSet?$filter=ImXhotelId eq '" + Constants._hotel_code + "' and ImXreservaId eq '" + reservationID + "' and ImSendEmail eq 'X' and ImEmail eq '" + email + "'";
			return await this.GetODataService(url);
		}
        
		public async Task<String> GetReservationRemarksAndNotices()
        {
			string url = $"/sap/opu/odata/sap/ZTMS_MODIFY_RESERVATION_SRV/reserListSet?$filter=ImHotelId eq '{Constants._hotel_code}' and ImReservationId eq '{Constants._reservation_id}'&$expand=reserNoticesSet,reserRemarksSet";
            return await this.GetODataService(url);
        }

        public async Task<String> GetFlightDetails(string flightTlype)
        {
			if (flightTlype == "DEP")
			{
				string url = $"/sap/opu/odata/sap/ZTMS_GET_FLIGHT_INFO_SRV/flightDetailsSet?$filter=ImHotelid eq '{Constants._hotel_code}' and ImCheckArr eq 'a' and ImCheckDep eq 'x'";
				return await this.GetODataService(url);
			}
			else
			{
				string url = $"/sap/opu/odata/sap/ZTMS_GET_FLIGHT_INFO_SRV/flightDetailsSet?$filter=ImHotelid eq '{Constants._hotel_code}' and ImCheckArr eq 'x' and ImCheckDep eq 'a'";
                return await this.GetODataService(url);
			}
		}

		public async Task<String> GetODataService(String url)
		{
			try
			{
				//Refresh Token if expires
				if (Convert.ToDateTime(Settings.ExpiresTime) <= DateTime.Now)
				{
					//Authenticate against ADFS and NW Gateway
					oAuthLogin oauthlogin = new oAuthLogin();
					String access_token = await oauthlogin.LoginUserAsync(Constants._user);
					if (access_token == "" && access_token == Constants._userNotExistInNWGateway)
					{
						userLogout.logout();
					}
				}
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(Constants._gatewayURL + url);
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants._access_token);
					try
					{
						HttpResponseMessage response = await client.GetAsync(Constants._gatewayURL + url);
						using (HttpContent content = response.Content)
						{
							string result = await content.ReadAsStringAsync();
							return result;
						}
					}
					catch (Exception e)
					{
						return "Error";
					}
				}
			}
			catch (Exception e)
			{
				return "Error";
			}
		}
	}
}

