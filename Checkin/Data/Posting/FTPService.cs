using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Checkin.Interface;
using Checkin.Models.ModelClasses.Payloads;
using Xamarin.Forms;

namespace Checkin.Data.Posting
{
    public static class FTPService
    {

        public static async Task UploadImageHttpPost(byte[] imageBytes, string hotelCode, string resNo, string guestId)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
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
                HttpResponseMessage response = await httpClient.PostAsync($"http://chml.keells.lk/GuestFBAPIDev/api/Feedback/UploadGuestImage", form).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();
                httpClient.Dispose();
                string sd = response.Content.ReadAsStringAsync().Result;
                Debug.WriteLine(sd);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error uploading image" + ex.StackTrace);
            }
        }

        public static async Task UploadPassportCopy(ImageSource PassportCopy,string name)
        {
            //passportImage.Source = Constants.PassportCopy;
            Debug.WriteLine("Attachment has been set");

            StreamImageSource streamImageSource = (StreamImageSource)PassportCopy;
            System.Threading.CancellationToken cancellationToken = System.Threading.CancellationToken.None;
            Task<Stream> task = streamImageSource.Stream(cancellationToken);
            Stream stream = task.Result;

            ////Getting Stream as a Memorystream
            var pptMemoryStream = stream as MemoryStream;

            if (pptMemoryStream == null)
            {
                pptMemoryStream = new MemoryStream();
                stream.CopyTo(pptMemoryStream);
            }

            //Adding memorystream into a byte array
            var byteArray = pptMemoryStream.ToArray();

            Constants.ImageBytes = byteArray;



            /***********************************/

            var resizedImage = DependencyService.Get<IImageConverter>().ResizeImageIOS(byteArray, 800, 600);

            //Converting byte array into Base64 string
            var base64 = Convert.ToBase64String(resizedImage);

            //await FTPService.UploadImageHttpPost(byteArray, "CNG","0001","0001");
            /***********************************/

            AttachmentsPayload ppPayload = new AttachmentsPayload();
            ppPayload.IXhotelId = Constants._hotel_code;
            ppPayload.IXreservaId = Constants._reservation_id;
            ppPayload.IXtype = "JPG";
            ppPayload.IXfileName = $"{name}'s Passport Copy";

            //Converting byte array into Base64 string
            ppPayload.IXattach = base64;

            var ppResponce = await new PostServiceManager().SetAttachments(ppPayload);
            Debug.WriteLine(ppResponce);

        }
    }
}
