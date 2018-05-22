using System;
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
                            "\"CreatedBy\":\"iPad\"}";
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
    }
}
