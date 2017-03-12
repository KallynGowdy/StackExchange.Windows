using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Store.LicenseManagement;
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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace StackExchange.Windows.User.UserCard
{
    public sealed partial class UserCard : UserControl, IViewFor<UserCardViewModel>
    {
        public static readonly DependencyProperty UserProperty = DependencyProperty.Register(
            nameof(User),
            typeof(UserCardViewModel),
            typeof(UserCard),
            new PropertyMetadata(null));

        public UserCardViewModel User
        {
            get
            {
                return (UserCardViewModel)GetValue(UserProperty);
            }
            set
            {
                SetValue(UserProperty, value);
            }
        }

        public UserCard()
        {
            this.InitializeComponent();
            if (!DesignMode.DesignModeEnabled)
            {
                this.WhenActivated(d =>
                {
                    d(this.OneWayBind(ViewModel, vm => vm.ImageUrl, view => view.OwnerImage.Source));
                    d(this.Bind(ViewModel, vm => vm.Owner, view => view.Owner.Text));
                    d(this.Bind(ViewModel, vm => vm.PostedOn, view => view.PostedOn.Text));
                });
            }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (UserCardViewModel)value; }
        }

        public UserCardViewModel ViewModel
        {
            get { return User; }
            set { User = value; }
        }
    }
}
