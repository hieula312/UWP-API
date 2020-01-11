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
using Windows.UI.Xaml.Documents;
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
            Sanitize_ErrBlock();
            var password = this.password.Password;
            var email = this.email.Text;
            var errors = _service.LogInValidate(password, email);
            if (errors.Count > 0)
            {
                foreach (KeyValuePair<String, String> item in errors)
                {
                    var block = (TextBlock)this.FindName(item.Key);
                    if (block != null)
                    {
                        block.Text = item.Value;
                    }
                }
            }
            else
            {
                Token = await _service.GetToken(email, password);
                LogIn.Token = Token;
                FileHandleService.WriteToFile("token.txt", Token);
                this.Frame.Navigate(typeof(MyListSong));
            }
            Debug.WriteLine(Token);
        }

        private void Sanitize_ErrBlock()
        {
            emailErr.Text = "";
            passwordErr.Text = "";
        }


        private void Register_Navigator(Hyperlink sender, HyperlinkClickEventArgs args)
        {
            this.Frame.Navigate(typeof(Register));
        }
    }
}
