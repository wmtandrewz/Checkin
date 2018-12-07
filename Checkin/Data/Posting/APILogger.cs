using System;
using System.Net.Http;
using Xamarin.Forms;

namespace Checkin.Data.Posting
{
    public class APILogger
    {
        public async void Logger(string msg)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://chml.keells.lk/CinnamonCheckin/api/");

                try
                {
                    HttpResponseMessage response = await client.GetAsync($"http://chml.keells.lk/CinnamonCheckin/api/Checkin/Logger?msg={msg}");
                    using (HttpContent content = response.Content)
                    {
                        string result = await content.ReadAsStringAsync();
                    }
                }
                catch (Exception)
                {

                }
            }
        }
    }
}

