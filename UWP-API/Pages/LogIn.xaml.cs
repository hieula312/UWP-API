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
using UWP_API.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP_API.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LogIn : Page
    {
        AuthenticationService _service = new AuthenticationService();
        public static string Token;
        public LogIn()
        {
            this.InitializeComponent();
        }

        private async void Login_Clicked(object sender, RoutedEventArgs e)
        {
            var password = this.password.Password;
            var email = this.email.Text;
            Token = await _service.GetToken(email, password);
            this.Frame.Navigate(typeof(DetailInformation));
            Debug.Write(Token);
            //MkNAapOe38lh0zrIT2hsp4aWbtjZvPk3a4ffHgelth5N7qhdzd50hyfdzEZ0MP7Z

        }
    }
}
