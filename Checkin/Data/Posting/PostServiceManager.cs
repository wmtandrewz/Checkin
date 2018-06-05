using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Collections.Generic;
using System.Diagnostics;
using Checkin.Models.ModelClasses.Payloads;
using Checkin.Models.ModelClasses;

namespace Checkin
{

	public class PostServiceManager
	{
		userLogout userLogout = new userLogout();
		public async Task<String> StatusChangeAsync(StatusChange statusChange)
		{
			string url = "/sap/opu/odata/sap/ZTMS_GUEST_UPDATE_SRV/UpdateGuestSet";
			String result = await this.GetODataService(url, JsonConvert.SerializeObject(statusChange));

			//If result is success
			if (result == "success")
			{
				if (statusChange.XhotelId == Constants._hotel_code)
					return "Guest Details Updated Successfully!";
				else
					return "Sorry. Unable to update guest details!";
			}
			else if (result == "Error")
			{
				return "Sorry. Unable to update guest details!";
			}
			else {
				var jObj = JObject.Parse(result);
				return Convert.ToString(jObj["error"]["message"]["value"]);
			}
		}

		public async Task<String> SavePPExpiryAsync(PPExpiryDate statusChange)
        {
            string url = "/sap/opu/odata/sap/ZTMS_GUEST_UPDATE_SRV/UpdateGuestSet";
            String result = await this.GetODataService(url, JsonConvert.SerializeObject(statusChange));

            //If result is success
            if (result == "success")
            {
                if (statusChange.XhotelId == Constants._hotel_code)
					return "expiry Details Updated Successfully!";
                else
                    return "Sorry. Unable to update expiry!";
            }
            else if (result == "Error")
            {
				return "Sorry. Unable to update expiry details!";
            }
            else
            {
                var jObj = JObject.Parse(result);
                return Convert.ToString(jObj["error"]["message"]["value"]);
            }
        }

		public async Task<String> StatusChangeRoomAsync(StatusChangeRoom StatusChangeRoom)
		{
			string url = "/sap/opu/odata/sap/ZTMS_ASSIGN_ROOM_SRV_03/ZTMS_ASSIGN_ROOMSet";
			String result = await this.GetODataService(url, JsonConvert.SerializeObject(StatusChangeRoom));

			//If result is success
			if (result == "success")
			{
				if (StatusChangeRoom.ImHotelId == Constants._hotel_code)
					return "Room Assigned Successfully!";
				else
					return "Sorry. Unable to update room details!";
			}
			else if (result == "Error")
			{
				return "Sorry. Sorry. Unable to update room details!";
			}
			else {
				var jObj = JObject.Parse(result);
				return Convert.ToString(jObj["error"]["message"]["value"]);
			}
		}

		public async Task<String> StatusChangecheckinAsync(StatusChangeCheckin StatusChangeCheckin) 
		{
			string url = "/sap/opu/odata/sap/ZTMS_IMG_SRV/Checkins";
			String result = await this.GetODataService(url, JsonConvert.SerializeObject(StatusChangeCheckin));

			//If result is success
			if (result == "success")
			{
				if (StatusChangeCheckin.XHOTEL_ID == Constants._hotel_code)
					return "Checked-In Successfully!";
				else
					return "Sorry. Unable to Checkin!";
			}
			else if (result == "Error")
			{
				return "Sorry. Unable to Checkin!";
			}
			else {
				var jObj = JObject.Parse(result);
				return Convert.ToString(jObj["error"]["innererror"]["errordetails"][0]["message"]);
			}
		}

		public async Task<String> SetFlightDetails(FlightPayload flightPayload)
        {
			string url = "/sap/opu/odata/sap/ZTMS_MODIFY_RESERVATION_SRV/reserListSet";
			String result = await this.GetODataService(url, JsonConvert.SerializeObject(flightPayload));

            //If result is success
            if (result == "success")
            {
				return "Success";
            }

			else
			{
				return "Sorry. Unable to set flight!";
			}
            
           
        }

		public async Task<String> SetReservationNotices(ResNoticesModel resNoticesModel)
        {

			string url = "/sap/opu/odata/sap/ZTMS_MODIFY_RESERVATION_SRV/reserNoticesSet";
			String result = await this.GetODataService(url, JsonConvert.SerializeObject(resNoticesModel));

            //If result is success
            if (result == "success")
            {
                return "Success";
            }

            else
            {
                return "Sorry. Unable to update notices!";
            }


        }

		public async Task<String> SetReservationRemarks(RemarksPayload remarksModel)
        {

			string url = "/sap/opu/odata/sap/ZTMS_MODIFY_RESERVATION_SRV/reserRemarksSet";
			String result = await this.GetODataService(url, JsonConvert.SerializeObject(remarksModel));

            //If result is success
            if (result == "success")
            {
                return "Success";
            }

            else
            {
                return "Sorry. Unable to update remarks!";
            }


        }

		public async Task<String> GetODataService(String url, String postBody)
		{
			string xcsrf_token = "";
			string cookie_value = "";

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

				CookieContainer cookies = new CookieContainer();
				HttpClientHandler handler = new HttpClientHandler();
				handler.CookieContainer = cookies;

				using (var client = new HttpClient(handler))
				{
					//Get X-CSRF-TOKEN
					client.BaseAddress = new Uri(Constants._gatewayURL + url);
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants._access_token);
					client.DefaultRequestHeaders.Add("x-csrf-token", "fetch");

					//GET data
					HttpResponseMessage response = await client.GetAsync(Constants._gatewayURL + url);
					xcsrf_token = response.Headers.GetValues("X-CSRF-Token").FirstOrDefault();

					Uri uri = new Uri(Constants._gatewayURL + url);
					IEnumerable<Cookie> responseCookies = cookies.GetCookies(uri).Cast<Cookie>();
					foreach (Cookie cookie in responseCookies)
					{
						if (Constants._cookie == cookie.Name)
							cookie_value = cookie.Value;
					}
				}

				if (xcsrf_token != "" && cookie_value != "")
				{
					//Post Method
					Uri baseUri = new Uri(Constants._gatewayURL + url);
					HttpClientHandler clientHandler = new HttpClientHandler();
					//Set Cookie in Post
					clientHandler.CookieContainer.Add(baseUri, new Cookie(Constants._cookie, cookie_value));

					using (var client_post = new HttpClient(clientHandler))
					{
						client_post.BaseAddress = new Uri(Constants._gatewayURL + url);
						client_post.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants._access_token);
						client_post.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
						//Set Token in Post
						client_post.DefaultRequestHeaders.Add("X-CSRF-Token", xcsrf_token);

						//Post json content
						var response = client_post.PostAsync(Constants._gatewayURL + url, new StringContent(postBody, Encoding.UTF8, "application/json")).Result;
						if (response.IsSuccessStatusCode)
						{
							Debug.WriteLine(response);
							return "success";
						}
						else {
							return await response.Content.ReadAsStringAsync();
						}
					}
				}
				else {
					return "Token or cookie is not available";
				}
			}
			catch (Exception e)
			{
				return "Error";
			}
		}
	}
}

