using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Checkin.Data.Posting
{
    public static class APIPostService
    {
        public static async Task<string> SaveTimeTrackToAPI()
        {

			DateTime currDateTime = DateTime.Now;

            string json = "{" +
				            "\"HotelCode\":\"" + Constants._hotel_code + "\"," +
                            "\"RoomNo\":\""+ Constants.result.RoomNumber +"\"," +
				            "\"ReservationNumber\":\"" + Constants._reservation_id + "\"," +
                            "\"CheckinDate\":\""+ currDateTime + "\"," +
                            "\"StartTime\":\"" + Constants._checkinStartTime + "\"," +
                            "\"EndTime\":\"" + currDateTime.ToString() + "\"," +
                            "\"CreatedBy\":\"" + Settings.Username + "\"}";
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri("http://chml.keells.lk/CinnamonCheckin/api/"),
                };
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsync($"Checkin/Insert",new StringContent(json, Encoding.UTF8, "application/json"));

                return response.ToString();

            }

            catch (Exception)
            {
                return null;
            }
           
        }

        public static async void UploadImageHttpPost(byte[] imageBytes, string hotelCode, string resNo, string guestId)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri("http://chml.keells.lk/CinnamonCheckin/api/"),
                };
                MultipartFormDataContent form = new MultipartFormDataContent();

                string timeStamp = "";

                if (string.IsNullOrEmpty(guestId))
                {
                    timeStamp = DateTime.Now.Ticks.ToString();
                }
                else
                {
                    timeStamp = guestId;
                }


                form.Add(new ByteArrayContent(imageBytes, 0, imageBytes.Length), "profile_pic", $"{hotelCode}_{resNo}_{timeStamp}.jpg");
                HttpResponseMessage response = await client.PostAsync($"Image/UploadGuestSignature", form).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();
                client.Dispose();
                string sd = response.Content.ReadAsStringAsync().Result;
                Debug.WriteLine(sd);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error uploading image" + ex.StackTrace);
            }
        }

        

    }
}
