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
using Checkin.Data.Posting;

namespace Checkin
{

    public class PostServiceManager
    {
        userLogout userLogout = new userLogout();
        public async Task<String> StatusChangeAsync(StatusChange statusChange)
        {
            var startTime = DateTime.Now;

            string url = "/sap/opu/odata/sap/ZTMS_GUEST_UPDATE_SRV/UpdateGuestSet";
            String result = await this.GetODataService(url, JsonConvert.SerializeObject(statusChange));

            var endTime = DateTime.Now;

            //Logger
            new APILogger().Logger($"Guest POST start :{startTime} end : {endTime}");

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
            else
            {
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
            else
            {
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
            else
            {
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

        public async Task<string> SetPerformaInvoice(string responsible,string folio)
        {

            string url = "/sap/opu/odata/sap/ZTMS_GEN_PROFORMA_INVOICE_SRV/proformaSet";
            string result = await this.GetProformaInvoiceNumber(url, JsonConvert.SerializeObject(new Perfoma(Constants._hotel_code, Constants._reservation_id, responsible, folio)));

            //If result is success
            if (result != null)
            {
                return result;
            }

            else
            {
                return "error";
            }


        }

        public async Task<String> SetAttachments(AttachmentsPayload attachmentsPayload)
        {

            //string url = "/sap/opu/odata/sap/ZTMS_RESERVATION_ATTACHMENTS_SRV/setReservationAttachmentSet";

            //String result = await this.GetODataService(url, JsonConvert.SerializeObject(attachmentsPayload));

            ////If result is success
            //if (result == "success")
            //{
            //    return "success";
            //}

            //else
            //{
            //    return "Sorry. Unable to update attachement!";
            //}

            string url = "getattachments/setReservationAttachmentSet";

            string result = await this.GetODataAzureService(url, JsonConvert.SerializeObject(attachmentsPayload));

            //If result is success
            if (result == "success")
            {
                return "success";
            }

            else
            {
                return "Sorry. Unable to update attachement!";
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

                        new APILogger().Logger($"Access Token :{Constants._access_token} ***** X-CSRF-Token :{xcsrf_token}");

                        //Post json content
                        var response = client_post.PostAsync(Constants._gatewayURL + url, new StringContent(postBody, Encoding.UTF8, "application/json")).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            Debug.WriteLine(response);
                            var perRes = response.Content.ReadAsStringAsync();
                            Debug.WriteLine(perRes.Result);
                            new APILogger().Logger($"Post Responce Success :{perRes.Result}");
                            return "success";
                        }
                        else
                        {
                            var perRes2 = await response.Content.ReadAsStringAsync();
                            new APILogger().Logger($"Post Responce Error :{perRes2}");
                            return perRes2;
                        }
                    }
                }
                else
                {
                    return "Token or cookie is not available";
                }
            }
            catch (Exception e)
            {
                new APILogger().Logger($"Post Method Exception :{e.StackTrace} ***** Post Method Exception Msg :{e.Message}");
                return "Error";
            }
        }

        public async Task<string> GetODataAzureService(string url, string postBody)
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

                    string access_token = await oauthlogin.LoginUserAsync(Constants._user);
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
                    //client.BaseAddress = new Uri(Constants._azureAPIMDEVBase + url);//DEV
                    client.BaseAddress = new Uri(Constants._azureAPIMPRDBase + url);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants._access_token);
                    client.DefaultRequestHeaders.Add("X-CSRF-Token", "fetch");
                    //client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "7ab76f2c1d7b41c3b6c07a0d2ee492c3");//DEV
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "dbaee796a5fe47bf8f4c467cd7cf9d4d");

                    //GET data
                    //HttpResponseMessage response = await client.GetAsync(Constants._azureAPIMDEVBase + url);//DEV
                    HttpResponseMessage response = await client.GetAsync(Constants._azureAPIMPRDBase + url);
                    xcsrf_token = response.Headers.GetValues("X-CSRF-Token").FirstOrDefault();

                    //Uri uri = new Uri(Constants._azureAPIMDEVBase + url);//DEV
                    Uri uri = new Uri(Constants._azureAPIMPRDBase + url);
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
                    //Uri baseUri = new Uri(Constants._azureAPIMDEVBase + url);//DEV
                    Uri baseUri = new Uri(Constants._azureAPIMPRDBase + url);
                    HttpClientHandler clientHandler = new HttpClientHandler();
                    //Set Cookie in Post
                    clientHandler.CookieContainer.Add(baseUri, new Cookie(Constants._cookie, cookie_value));

                    using (var client_post = new HttpClient(clientHandler))
                    {
                        //client_post.BaseAddress = new Uri(Constants._azureAPIMDEVBase + url);//DEV
                        client_post.BaseAddress = new Uri(Constants._azureAPIMPRDBase + url);
                        client_post.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants._access_token);
                        client_post.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        //Set Token in Post
                        client_post.DefaultRequestHeaders.Add("X-CSRF-Token", xcsrf_token);
                        //client_post.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "7ab76f2c1d7b41c3b6c07a0d2ee492c3");//DEv
                        client_post.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "dbaee796a5fe47bf8f4c467cd7cf9d4d");

                        //Post json content
                        //var response = client_post.PostAsync(Constants._azureAPIMDEVBase + url, new StringContent(postBody, Encoding.UTF8, "application/json")).Result;//DEV
                        var response = client_post.PostAsync(Constants._azureAPIMPRDBase + url, new StringContent(postBody, Encoding.UTF8, "application/json")).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            Debug.WriteLine(response);
                            return "success";
                        }
                        else
                        {
                            return await response.Content.ReadAsStringAsync();
                        }
                    }
                }
                else
                {
                    return "Token or cookie is not available";
                }
            }
            catch (Exception e)
            {
                return "Error";
            }
        }

        public async Task<string> GetProformaInvoiceNumber(String url, String postBody)
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
                            var perRes = response.Content.ReadAsStringAsync();
                            Debug.WriteLine(perRes.Result);
                            return perRes.Result;
                        }
                        else
                        {
                            return await response.Content.ReadAsStringAsync();
                        }
                    }
                }
                else
                {
                    return "Token or cookie is not available";
                }
            }
            catch (Exception e)
            {
                return "Error";
            }
        }
    }


    public class Perfoma
    {
        public Perfoma(string imHotelId, string imReservationId, string imResponsible, string imFolio)
        {
            ImHotelId = imHotelId;
            ImReservationId = imReservationId;
            ImResponsible = imResponsible;
            ImFolio = imFolio;
        }

        public string ImHotelId { get; set; }
        public string ImReservationId { get; set; }
        public string ImResponsible { get; set; }
        public string ImFolio { get; set; }
    }
}

