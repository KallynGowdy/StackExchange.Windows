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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace StackExchange.Windows.Common.TagsList
{
    /// <summary>
    /// Defines a user control that is able to display a single tag.
    /// </summary>
    public sealed partial class Tag : UserControl, IViewFor<TagViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            name: nameof(ViewModel),
            propertyType: typeof(TagViewModel),
            ownerType: typeof(TagsList),
            typeMetadata: new PropertyMetadata(null));

        public Tag()
        {
            this.InitializeComponent();

            if (!DesignMode.DesignModeEnabled)
            {
                this.WhenActivated(d =>
                {
                    this.Bind(ViewModel, vm => vm.Tag, view => view.TagButton.Content)
                        .DisposeWith(d);
                    this.BindCommand(ViewModel, vm => vm.SearchTag, view => view.TagButton)
                        .DisposeWith(d);
                });
            }
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (TagViewModel)value;
        }

        public TagViewModel ViewModel
        {
            get => (TagViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}
