using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
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
using ReactiveUI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace StackExchange.Windows.Login
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page, IViewFor<LoginViewModel>
    {
        public LoginPage()
        {
            this.InitializeComponent();
            this.WhenActivated(d =>
            {
                var navigationStarting = Observable
                    .FromEventPattern<TypedEventHandler<WebView, WebViewNavigationStartingEventArgs>, WebViewNavigationStartingEventArgs>
                    (h => OAuthWebView.NavigationStarting += h, h => OAuthWebView.NavigationStarting -= h);

                d(ViewModel.Authentication.RedirectToLogin
                    .RegisterHandler(async c =>
                    {
                        OAuthWebView.Navigate(c.Input);
                        var url = await navigationStarting.Select(args => args.EventArgs.Uri)
                            .FirstAsync(ViewModel.Authentication.IsSuccessUrl);

                        c.SetOutput(url);
                    }));
                
                d(ViewModel.Authentication.Login.Execute().Subscribe());
            });
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (LoginViewModel)value; }
        }

        public LoginViewModel ViewModel { get; set; } = new LoginViewModel();
    }
}
