using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP_API.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CameraDemo : Page
    {
        private static string CONTENT_TYPE = "application/json";
        private static string LOGIN_API_URL = "https://2-dot-backup-server-002.appspot.com/upload-my-file-handle";
        private StorageFile photo;

        public CameraDemo()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // get upload url
            HttpClient client = new HttpClient();
            var uploadUrl = client.GetAsync("https://2-dot-backup-server-001.appspot.com/get-upload-token").Result.Content.ReadAsStringAsync().Result;
            Debug.WriteLine("Upload url: " + uploadUrl);

            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);

            this.photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (this.photo == null)
            {
                // User cancelled photo capture
                return;
            }

            HttpUploadFile(uploadUrl, "myFile", "image/png");
        }

        public async void HttpUploadFile(string url, string paramName, string contentType)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";

            Stream rs = await wr.GetRequestStreamAsync();
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string header = string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n", paramName, "path_file", contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            // write file.
            Stream fileStream = await this.photo.OpenStreamForReadAsync();
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }
            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            WebResponse wresp = null;
            try
            {
                wresp = await wr.GetResponseAsync();
                Stream stream2 =  wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                string imageUrl = reader2.ReadToEnd();
                Debug.WriteLine("Image URL is: " + imageUrl);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error uploading file", ex.StackTrace);
                Debug.WriteLine("Error uploading file", ex.InnerException);
                if (wresp != null)
                {
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }
        }

        private async void Choose_File(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".mp3");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            await PushFile(file);
            //Stream stream = await file.OpenStreamForReadAsync();
        }

        private async Task PushFile(StorageFile file)
        {
            JObject cloudInfo = new JObject();
            cloudInfo["cloudName"] = "daaycakkk";
            cloudInfo["apiKey"] = "221664986237777";
            cloudInfo["apiSecret"] = "F5q-h6o5IF7BPknxCsSeVnb8p0Y";
            //Chuyen thanh dang JSON
            var cloudJson = JsonConvert.SerializeObject(cloudInfo);
            var formData = new MultipartFormDataContent();
            //Dong goi gui len
            HttpContent contentToSend = new StringContent(cloudJson, Encoding.UTF8, CONTENT_TYPE);
            formData.Add(contentToSend);
            var stream = await file.OpenStreamForReadAsync();
            HttpContent content = new StringContent("myFile");
            content = new StreamContent(stream);
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "myFile",
                FileName = file.Name
            };
            formData.Add(content);
            //Goi shipper
            HttpClient httpClient = new HttpClient();
            //Lay ket qua
            var response = await httpClient.PostAsync(LOGIN_API_URL, formData);
            //Doc ket qua
            var stringcontent = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(response.Content.ReadAsStringAsync().Result);
            Debug.WriteLine("url" + (string)JObject.Parse(stringcontent)["secure_url"]);
        }
    }
}
