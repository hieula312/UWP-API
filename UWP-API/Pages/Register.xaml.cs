using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using UWP_API.Model;
using UWP_API.Services;
using System.Net.Http;
using Windows.Media.Capture;
using Windows.Storage;
using SQLitePCL;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP_API.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Register : Page
    {
        private StorageFile photo;
        private static String imageUrl;
        public static DateTime birthDayVal;
        private IUserService _service;
        private int genderVal = 0;
        public Register()
        {
            this.InitializeComponent();
            _service = new UserService();
        }

        private async void Create_User(object sender, RoutedEventArgs e)
        {
            Sanitize_ErrBlock();
            var member = new User()
            {
                firstName = fisrtName.Text,
                lastName = lastName.Text,
                password = password.Password,
                address = address.Text,
                phone = phone.Text,
                avatar = imageUrl,
                gender = genderVal,
                email = email.Text,
                birthday = String.Format("{0:yyyy-MM-dd}",birthDayVal).ToString()
            };
            Debug.WriteLine(String.Format("{0:yyyy-MM-dd}", birthDayVal).ToString());
            var errors = member.CheckValidate();

            if (errors.Count > 0 && errors.Values != null)
            {
                
                string contentErr = "";
                foreach (KeyValuePair<String, String> item in errors)
                {
                    var block = (TextBlock)this.FindName(item.Key);
                    Debug.WriteLine(block);
                    block.Text = item.Value;
                }

                //addressErr.Text = contentErr;
            }                
            else
            {
                User user = await _service.Create(member);
                Debug.Write("user id: " + user.id);
                this.Frame.Navigate(typeof(LogIn));
            }
        }

        private void Sanitize_ErrBlock()
        {
            addressErr.Text = "";
            birthdayErr.Text = "";
            emailErr.Text = "";
            fisrtNameErr.Text = "";
            lastName.Text = "";
            passwordErr.Text = "";
            birthdayErr.Text = "";
        }
        private void Birthday_OnDateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (sender.Date.HasValue)
            {
                birthDayVal = sender.Date.Value.Date;
            }
        }

        private async void TakePhoto(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            var uploadUrl = client.GetAsync("https://2-dot-backup-server-003.appspot.com/get-upload-token").Result.Content.ReadAsStringAsync().Result;
            Debug.WriteLine("Upload url: " + uploadUrl);

            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);

            this.photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (this.photo == null)
            {
                imageUrl = "https://photo-resize-zmp3.zadn.vn/w240_r1x1_jpeg/cover/2/9/0/6/2906681d4b764cd4677342b66813f25d.jpg";
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
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                imageUrl =  reader2.ReadToEnd();
                Debug.WriteLine(imageUrl);
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

        public void Create_StudentSQLite()
        {
           var conn = new SQLiteConnection("sqlitepcldemo.db");
           var sql = @"Create table if not exists 
                        Customers (Id integer primary key AUTOINCREMENT NOT NULL,
                                   Name VARCHAR(140))";
           var statement = conn.Prepare(sql);
           statement.Step();
           var stt2 = conn.Prepare("Insert into Customers (Name) values ('Hieu')");
        }

        private void Gender_Choose(object sender, RoutedEventArgs e)
        {
            var chooseRadioButton = (RadioButton)sender;
            if (chooseRadioButton == null)
            {
                return;
            }
            switch (chooseRadioButton.Tag)
            {
                case "Male":
                    genderVal = 0;
                    break;
                case "Female":
                    genderVal = 1;
                    break;
                case "Other":
                    genderVal = 2;
                    break;
            }
        }
    }
}
