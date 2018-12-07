using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Checkin.Data.Posting
{
    public class GuestPostManager
    {
        userLogout userLogout = new userLogout();

        public async Task<string> CreateUpdateGuest(StatusChange statusChange)
        {
            string url = "/sap/opu/odata/sap/ZTMS_GUEST_UPDATE_SRV/UpdateGuestSet";
            string result = await this.GetODataService(url, JsonConvert.SerializeObject(statusChange));

            new APILogger().Logger(result);

            try
            {
                if (!string.IsNullOrEmpty(result) && result != "Error")
                {
                    if (result != "Token or cookie is not available")
                    {
                        var jresult = JObject.Parse(result);

                        var hasError = jresult["error"];
                        var hasGuestCode = jresult["d"];

                        if (hasError != null)
                        {
                            return Convert.ToString(jresult["error"]["message"]["value"]);
                        }
                        else if (hasGuestCode != null)
                        {
                            var guestCode = Convert.ToInt32(jresult["d"]["Kunnr"]);
                            return guestCode > 0 ? $"Guest #{guestCode} Updated Successfully!" : "Sorry. Unable to update guest details!";
                        }
                        else
                        {
                            return "Unknown Error!";
                        }

                    }
                    return "Token Cookie Missing!. Unable to update guest details!";
                }
            }
            catch(Exception e)
            {
                return $"Unknown Error!. {e.Message}";
            }

            return "Unknown Error!. Unable to update guest details!";

        }

        public async Task<string> GetODataService(string url, string postBody)
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
                            var perRes = response.Content.ReadAsStringAsync();
                            return perRes.Result;
                        }
                        else
                        {
                            var perRes2 = response.Content.ReadAsStringAsync();
                            return perRes2.Result;
                        }
                    }
                }
                else
                {
                    new APILogger().Logger($"Post Method Error ***** Token or cookie is not available");
                    return "Token or cookie is not available";
                }
            }
            catch (Exception e)
            {
                new APILogger().Logger($"Post Method Exception :{e.StackTrace} ***** Post Method Exception Msg :{e.Message}");
                return "Error";
            }
        }
    }
}
