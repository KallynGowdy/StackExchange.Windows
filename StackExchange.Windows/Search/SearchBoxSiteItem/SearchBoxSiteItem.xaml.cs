using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
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
using StackExchange.Windows.Search.SearchBox;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace StackExchange.Windows.Search.SearchBoxSiteItem
{
    public sealed partial class SearchBoxSiteItem : UserControl, IViewFor<SiteViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            nameof(ViewModel),
            typeof(SiteViewModel),
            typeof(SearchBoxSiteItem),
            new PropertyMetadata(null));

        public SearchBoxSiteItem()
        {
            this.InitializeComponent();
            if (!DesignMode.DesignModeEnabled)
            {
                this.WhenActivated(d =>
                {
                    d(this.Bind(ViewModel, vm => vm.Name, view => view.SiteName.Text));
                    d(this.Bind(ViewModel, vm => vm.Audience, view => view.SiteDescription.Text));
                    d(this.OneWayBind(ViewModel, vm => vm.HighResIconUrlOrFallback, view => view.SiteIcon.Source));
                });
            }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (SiteViewModel)value; }
        }

        public SiteViewModel ViewModel
        {
            get { return (SiteViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
    }
}
