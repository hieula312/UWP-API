using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using UWP_API.Model;
using UWP_API.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP_API.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateSong : Page
    {
        ISongService _service = new SongService();
        public CreateSong()
        {
            this.InitializeComponent();
            _service = new SongService();
        }

        private async void UploadSong_Clicked(object sender, RoutedEventArgs e)
        {
            var song = new Song()
            {
                name = name.Text,
                singer = signer.Text,
                author = author.Text,
                thumbnail = thumbnail.Text,
                link = link.Text
            };
//            var errors = song.CheckValidate();
//            if (errors.Count > 0)
//            {
//
//            }
            song = await _service.Create(song);
            this.Frame.Navigate(typeof(MyListSong));
            Debug.Write(song.name);
        }
    }
}
