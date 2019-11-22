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


        public static async Task<DeviceModel> RegisterDevice(string device,string version)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://chml.keells.lk/CinnamonCheckin/api/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var responce = await client.GetAsync($"Device/Register?device={device}&version={version}");

                    var res = await responce.Content.ReadAsStringAsync();

                    if(res.ToLower().Contains("devive is already registered"))
                    {
                        return new DeviceModel() { Notes = "Device already registered" };
                    }

                    else if (res.ToLower().Contains("no such device in database"))
                    {
                        return new DeviceModel() { Notes = "no such device in database" };
                    }

                    else if (!string.IsNullOrEmpty(res))
                    {
                        var terms = JsonConvert.DeserializeObject<DeviceModel>(res);
                        if (terms != null)
                        {
                            return terms;
                        }
                    }
                    return new DeviceModel() { Notes = "Error" };
                }
            }
            catch (Exception ex)
            {
                return new DeviceModel() { Notes = "error" + ex.InnerException.Message };
            }
        }

        public static async Task<bool> IsDeviceRegistered(string device, string version)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://chml.keells.lk/CinnamonCheckin/api/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var responce = await client.GetAsync($"Device/IsRegistered?device={device}&version={version}");

                    var res = await responce.Content.ReadAsStringAsync();

                    if (res.ToLower().Contains("true"))
                    {
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
