﻿#pragma checksum "C:\Users\ADMIN\source\repos\UWP-API\UWP-API\Pages\CreateSong.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6C5C022DF4C2048F58CF3D76836A041A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UWP_API.Pages
{
    partial class CreateSong : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Pages\CreateSong.xaml line 11
                {
                    this.main = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 3: // Pages\CreateSong.xaml line 46
                {
                    this.name = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 4: // Pages\CreateSong.xaml line 52
                {
                    this.nameErr = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 5: // Pages\CreateSong.xaml line 58
                {
                    this.description = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 6: // Pages\CreateSong.xaml line 64
                {
                    this.desErr = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 7: // Pages\CreateSong.xaml line 70
                {
                    this.signer = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 8: // Pages\CreateSong.xaml line 76
                {
                    this.signerErr = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 9: // Pages\CreateSong.xaml line 82
                {
                    this.author = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 10: // Pages\CreateSong.xaml line 88
                {
                    this.authorErr = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 11: // Pages\CreateSong.xaml line 94
                {
                    this.thumbnail = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 12: // Pages\CreateSong.xaml line 100
                {
                    this.thumbnailErr = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 13: // Pages\CreateSong.xaml line 106
                {
                    this.link = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 14: // Pages\CreateSong.xaml line 112
                {
                    this.linkErr = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 15: // Pages\CreateSong.xaml line 120
                {
                    global::Windows.UI.Xaml.Controls.Button element15 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element15).Click += this.UploadSong_Clicked;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

