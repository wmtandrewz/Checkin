using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Checkin.Models.ModelClasses;
using Newtonsoft.Json;

namespace Checkin.Data.Retrieving
{
    public static class APIGetService
    {
        public static async Task<List<Terms>> GetTermsAndConditions()
        {

            try
            {

                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://chml.keells.lk/CinnamonCheckin/api/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await client.GetAsync($"checkin/Retrieve?hotel={3000}");

                    var res = await response.Content.ReadAsStringAsync();

                    if(!string.IsNullOrEmpty(res))
                    {
                        var terms = JsonConvert.DeserializeObject<List<Terms>>(res);
                        if(terms!=null)
                        {
                            return terms;
                        }
                    }
                    return null;
                }

            }

            catch (Exception)
            {
                return null;
            }

        }
    }
}
