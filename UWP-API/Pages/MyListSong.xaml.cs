using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
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
    public sealed partial class MyListSong : Page
    {
        private Song currentSong;
        private SongService _songService;
        private bool _isPlaying = false;
        public MyListSong()
        {
            this.InitializeComponent();
        }
        private async void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            _songService = new SongService();
            Songs.ItemsSource =  await _songService.GetMyListSong();
        }

        private void Songs_OnItemClick(object sender, ItemClickEventArgs e)
        {
            currentSong = e.ClickedItem as Song;
            MyPlayer.Source = MediaSource.CreateFromUri(new Uri(currentSong.link));
            When_Pause();
        }

        private void PlayButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (Songs.ItemsSource == null) return;
            if (currentSong == null)
            {
                currentSong = _songService.LoadSongs().FirstOrDefault();
                MyPlayer.Source = MediaSource.CreateFromUri(new Uri(currentSong.link));
                Songs.SelectedIndex = 0;
            }

            if (_isPlaying)
            {
                When_Play();
            }
            else
            {
                When_Pause();
            }
        }

        private void Next_OnClick(object sender, RoutedEventArgs e)
        {
            var currentIndex = Songs.SelectedIndex;
            currentIndex++;
            if (currentIndex >= Songs.Items.Count)
            {
                currentIndex = 0;
            }
            currentSong = Songs.Items[currentIndex] as Song;
            Songs.SelectedIndex = currentIndex;
            MyPlayer.Source = MediaSource.CreateFromUri(new Uri(currentSong.link));
            When_Pause();
        }

        private void Previous_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void When_Play()
        {
            MyPlayer.MediaPlayer.Pause();
            PlayButton.Icon = new SymbolIcon(Symbol.Play);
            StatusText.Text = "Paused";
            _isPlaying = false;
        }

        private void When_Pause()
        {
            MyPlayer.MediaPlayer.Play();
            PlayButton.Icon = new SymbolIcon(Symbol.Pause);
            StatusText.Text = "Now Playing: " + currentSong.name;
            _isPlaying = true;
        }
    }
}

