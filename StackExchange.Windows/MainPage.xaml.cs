using System;
using System.Collections.Generic;
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
using ReactiveUI;
using Splat;
using StackExchange.Windows.Application;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace StackExchange.Windows
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, IViewFor<IApplicationViewModel>
    {

        public MainPage()
        {
            this.InitializeComponent();
            ViewModel = Locator.Current.GetService<IApplicationViewModel>();
            this.WhenActivated(d =>
            {
                d(this.OneWayBind(ViewModel, vm => vm.Authentication.Token, view => view.Hello.Text));
            });
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (ApplicationViewModel)value; }
        }

        public IApplicationViewModel ViewModel { get; set; }
    }
}
