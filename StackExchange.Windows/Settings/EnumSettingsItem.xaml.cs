using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Subjects;
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
using StackExchange.Windows.BindingConverters;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace StackExchange.Windows.Settings
{
    public sealed partial class EnumSettingsItem : UserControl, IViewFor<EnumSettingsItemViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            nameof(ViewModel),
            typeof(EnumSettingsItemViewModel),
            typeof(EnumSettingsItem),
            new PropertyMetadata(null));

        public EnumSettingsItem()
        {
            this.InitializeComponent();
            this.WhenActivated(d =>
            {
                this.OneWayBind(ViewModel, vm => vm.NameResource, view => view.SettingName.Text, vmToViewConverterOverride: StringResourceConverter.App)
                    .DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.DescriptionResource, view => view.SettingDescription.Text, vmToViewConverterOverride: StringResourceConverter.App)
                    .DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.HasDescription, view => view.SettingDescription.Visibility)
                    .DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.Values, view => view.EnumValues.ItemsSource)
                    .DisposeWith(d);
            });
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (EnumSettingsItemViewModel)value;
        }

        public EnumSettingsItemViewModel ViewModel
        {
            get => (EnumSettingsItemViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}
