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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace StackExchange.Windows.Search.SearchBox
{
    public sealed partial class QuestionSearchBox : UserControl, IViewFor<ISearchViewModel>
    {
        public QuestionSearchBox()
        {
            this.InitializeComponent();
            this.WhenActivated(d =>
            {
                d(this.OneWayBind(ViewModel, vm => vm.AvailableSites, view => view.Site.ItemsSource));
                d(this.Bind(ViewModel, vm => vm.SelectedSite, view => view.Site.SelectedItem));

                d(ViewModel.LoadSites.Execute().Subscribe());
            });
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (SearchViewModel)value; }
        }

        public ISearchViewModel ViewModel { get; set; } = Locator.Current.GetService<ISearchViewModel>();
    }
}
