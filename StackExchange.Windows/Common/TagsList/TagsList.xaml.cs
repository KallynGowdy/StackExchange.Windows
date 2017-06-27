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
using StackExchange.Windows.Common.PostDetail;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace StackExchange.Windows.Common.TagsList
{
    /// <summary>
    /// A user control that is able to render a list of tags.
    /// Effectively a wrapper for a list of strings.
    /// </summary>
    public sealed partial class TagsList : UserControl, IViewFor<IEnumerable<string>>
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            name: nameof(ViewModel),
            propertyType: typeof(IEnumerable<string>),
            ownerType: typeof(TagsList),
            typeMetadata: new PropertyMetadata(null));

        public TagsList()
        {
            this.InitializeComponent();
            if (!DesignMode.DesignModeEnabled)
            {
                this.WhenActivated(d =>
                {
                    this.WhenAnyValue(view => view.ViewModel)
                        .BindTo(this, view => view.TagsControl.ItemsSource)
                        .DisposeWith(d);
                });
            }
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (IEnumerable<string>)value;
        }

        public IEnumerable<string> ViewModel
        {
            get => (IEnumerable<string>)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}
