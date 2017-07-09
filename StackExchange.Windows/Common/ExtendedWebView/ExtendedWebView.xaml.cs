using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace StackExchange.Windows.Common.ExtendedWebView
{
    public sealed partial class ExtendedWebView : UserControl
    {
        public ExtendedWebView()
        {
            this.InitializeComponent();
        }

        public Uri Source
        {
            get => WebResults.Source;
            set => WebResults.Source = value;
        }

        public void Navigate(Uri source)
        {
            ResultsUrl.Text = source.ToString();
            WebResults.Navigate(source);
        }

        private void WebResults_OnNavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            ResultsUrl.Text = sender.Source.ToString();
            ResultsPageTitle.Text = sender.DocumentTitle;
            ToolTipService.SetToolTip(ResultsPageTitle, ResultsPageTitle.Text);
        }

        private async void OpenResultsInBrowser_OnClick(object sender, RoutedEventArgs e)
        {
            if (WebResults.Source != null)
            {
                await Launcher.LaunchUriAsync(WebResults.Source);
            }
        }
    }
}
