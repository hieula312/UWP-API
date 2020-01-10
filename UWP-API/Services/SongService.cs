using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation.Peers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UWP_API.Model;
using UWP_API.Pages;

namespace UWP_API.Services
{
    class SongService : ISongService
    {
        private static string UPLOAD_SONG_URL = "https://2-dot-backup-server-002.appspot.com/_api/v2/songs";
        private static string GET_MY_SONG = "https://2-dot-backup-server-002.appspot.com/_api/v2/songs/get-mine";
        private static string GET_OUR_SONG = "https://2-dot-backup-server-002.appspot.com/_api/v2/songs";
        private static string CONTENT_TYPE = "application/json";
        public Task<Song> Create(Song song)
        {
            return PostSong(song);
        }

        public Task<List<Song>> GetList()
        {
            throw new NotImplementedException();
        }

        public Task<Song> GetDetail(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Song> Update(Song user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(string name)
        {
            throw new NotImplementedException();
        }

        private async Task<Song> PostSong(Song song)
        {
            // chuyển đối tượng member thành định dạng json.
            var songJson = JsonConvert.SerializeObject(song);
            // quá trình đóng gói dữ liệu trước khi gửi đi.
            HttpContent contentToSend = new StringContent(songJson,
                Encoding.UTF8, CONTENT_TYPE);
            // gọi shipper
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + LogIn.Token);
            // gửi đến đây (link), món quà này (contentToSend), chờ quá trình gửi thành công, thì lấy xác nhận từ người nhận.
            var response = await httpClient.PostAsync(UPLOAD_SONG_URL, contentToSend);
            // đọc dữ liệu response từ người nhận.
            var stringContent = await response.Content.ReadAsStringAsync();
            // chuyển định dạng dữ liệu về đối tượng của C#
            var returnSong = JsonConvert.DeserializeObject<Song>(stringContent);
            // in ra một thuộc tính của đối tượng đó.
            Debug.WriteLine(response.Content.ReadAsStringAsync().Result);
            Debug.WriteLine(JObject.Parse(stringContent)["id"]);
            return returnSong;
        }

//        private async Task<Song> PostSongFile(StorageFile file)
//        {
////            var formDataContent = new MultipartFormDataContent();
////            formDataContent.Add();          // file
////            formDataContent.Add(new StringContent("Test Full Name"), "FullName")
////            // chuyển đối tượng member thành định dạng json.
////            var songJson = JsonConvert.SerializeObject(song);
//            // quá trình đóng gói dữ liệu trước khi gửi đi.
//            HttpContent contentToSend = new StringContent(songJson,
//                Encoding.UTF8, CONTENT_TYPE);
//            // gọi shipper
//            HttpClient httpClient = new HttpClient();
//            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + LogIn.Token);
//            // gửi đến đây (link), món quà này (contentToSend), chờ quá trình gửi thành công, thì lấy xác nhận từ người nhận.
//            var response = await httpClient.PostAsync(UPLOAD_SONG_URL, contentToSend);
//            // đọc dữ liệu response từ người nhận.
//            var stringContent = await response.Content.ReadAsStringAsync();
//            // chuyển định dạng dữ liệu về đối tượng của C#
//            var returnSong = JsonConvert.DeserializeObject<Song>(stringContent);
//            // in ra một thuộc tính của đối tượng đó.
//            Debug.WriteLine(response.Content.ReadAsStringAsync().Result);
//            Debug.WriteLine(JObject.Parse(stringContent)["id"]);
//            return returnSong;
//        }

        private async Task<StorageFile> Choose_File(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".mp3");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                return null;
            }

            return file;

        }
        private async Task<Song> GetMySong(Song song)
        {
            //var songJson = 
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + LogIn.Token);
            // gửi đến đây (link), món quà này (contentToSend), chờ quá trình gửi thành công, thì lấy xác nhận từ người nhận.
            var response = await httpClient.GetAsync(GET_MY_SONG);
            // đọc dữ liệu response từ người nhận.
            var stringContent = await response.Content.ReadAsStringAsync();
            // chuyển định dạng dữ liệu về đối tượng của C#
            var returnSong = JsonConvert.DeserializeObject<Song>(stringContent);
            // in ra một thuộc tính của đối tượng đó.
            Debug.WriteLine(JObject.Parse(stringContent)["name"]);
            return returnSong;
        }

        private async Task<Song> GetOurSong(Song song)
        {
            //var songJson = 
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + LogIn.Token);
            // gửi đến đây (link), món quà này (contentToSend), chờ quá trình gửi thành công, thì lấy xác nhận từ người nhận.
            var response = await httpClient.GetAsync(GET_OUR_SONG);
            // đọc dữ liệu response từ người nhận.
            var stringContent = await response.Content.ReadAsStringAsync();
            // chuyển định dạng dữ liệu về đối tượng của C#
            var returnSong = JsonConvert.DeserializeObject<Song>(stringContent);
            // in ra một thuộc tính của đối tượng đó.
            Debug.WriteLine(JObject.Parse(stringContent)["name"]);
            return returnSong;
        }

        public ObservableCollection<Song> LoadSongs()
        {
            ObservableCollection<Song> list = new ObservableCollection<Song>();
            list.Add(new Song()
            {
                name = "Tình thôi xót xa",
                author = "Hiếu Lã",
                singer = "Lam Trường",
                link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui829/TinhThoiXotXa-LamTruong-2453487.mp3",
                description = "Des - Tình thôi xót xa",
                thumbnail = "https://avatar-nct.nixcdn.com/singer/avatar/2018/06/07/7/2/a/a/1528338325576_600.jpg"
            });
            list.Add(new Song()
            {
                name = "Đi đu đưa đi",
                author = "Hiếu Lã",
                singer = "Bích Phương",
                link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui988/DiDuDuaDi-BichPhuong-6059493.mp3",
                description = "Tình chưa xót xa",
                thumbnail = "http://giadinh.mediacdn.vn/thumb_w/640/2019/9/4/bich-phuong-trong-di-du-dua-difgfdgfdgd-1567596484678552798701.jpg"
            });
            return list;
        }

        public async Task<ObservableCollection<Song>> GetMyListSong()
        {
            ObservableCollection<Song> list = new ObservableCollection<Song>();
            // gọi shipper
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + LogIn.Token);
            Debug.WriteLine("Token: " + LogIn.Token);
            // gửi đến đây (link), món quà này (contentToSend), chờ quá trình gửi thành công, thì lấy xác nhận từ người nhận.
            var response = await httpClient.GetAsync(GET_MY_SONG);
            Debug.WriteLine(response.Content.ReadAsStringAsync().Result);
            // đọc dữ liệu response từ người nhận.
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var stringContent = await response.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<ObservableCollection<Song>>(stringContent);
                return list;
            }
            return null;
        }

        public async Task<ObservableCollection<Song>> GetOurListSong()
        {
            ObservableCollection<Song> list = new ObservableCollection<Song>();
            // gọi shipper
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + LogIn.Token);
            // gửi đến đây (link), món quà này (contentToSend), chờ quá trình gửi thành công, thì lấy xác nhận từ người nhận.
            var response = await httpClient.GetAsync(GET_OUR_SONG);
            Debug.WriteLine(response.Content.ReadAsStringAsync().Result);
            // đọc dữ liệu response từ người nhận.
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var stringContent = await response.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<ObservableCollection<Song>>(stringContent);
                return list;
            }
            return null;
        }

    }

    
}
