using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Net.Http;
using System.Xml.Linq;

namespace Checkin
{
	public class VersionChecker
	{
		public async Task<bool> isUptoDate()
		{
			//Get Device Current Version
			string systemVersion = DependencyService.Get<CurrentVersion>().GetCurrentVersion();
			//Get New Version from Store
			string newVersion = await VersionFromStore();

			if (newVersion != "" && newVersion != null)
			{
				if (systemVersion != newVersion)
					return false;
				else
					return true;
			}
			else
				return true;
		}

		public async Task<String> VersionFromStore()
		{
			if (Device.OS == TargetPlatform.Android)
			{
				try
				{
					using (var client = new HttpClient())
					{
						client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/soap+xml"));
						client.BaseAddress = new Uri(Constants._AndroidPackage);
						HttpResponseMessage response = await client.GetAsync(Constants._AndroidPackage);
						using (HttpContent content = response.Content)
						{
							string result = await content.ReadAsStringAsync();
							if (result.Contains("softwareVersion"))
							{
								int startIndex = result.IndexOf("itemprop=\"softwareVersion\">", StringComparison.CurrentCulture) + "itemprop=\"softwareVersion\">".Length;
								result = result.Substring(startIndex);
								int endIndex = result.IndexOf("</div>", StringComparison.CurrentCulture);
								result = result.Substring(0, endIndex);
								return result.Trim();
							}
							else {
								return "";
							}
						}
					}
				}
				catch (Exception)
				{
					return "";
				}
			}
			else if (Device.OS == TargetPlatform.iOS)
			{
				try
				{
					using (var client = new HttpClient())
					{
						client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/soap+xml"));
						client.BaseAddress = new Uri(Constants._iOSPackage);
						HttpResponseMessage response = await client.GetAsync(Constants._iOSPackage);
						using (HttpContent content = response.Content)
						{
							string result = await content.ReadAsStringAsync();
							if (result.Contains("softwareVersion"))
							{
								int startIndex = result.IndexOf("itemprop=\"softwareVersion\">", StringComparison.CurrentCulture) + "itemprop=\"softwareVersion\">".Length;
								result = result.Substring(startIndex);
								int endIndex = result.IndexOf("</span>", StringComparison.CurrentCulture);
								result = result.Substring(0, endIndex);
								return result.Trim();
							}
							else {
								return "";
							}
						}
					}
				}
				catch (Exception)
				{
					return "";
				}
			}
			else {
				return "";
			}
		}
	}
}

