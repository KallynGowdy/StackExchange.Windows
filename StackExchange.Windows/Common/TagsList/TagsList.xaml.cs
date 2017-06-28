using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
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
    public sealed partial class TagsList : UserControl, IViewFor<TagsListViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            name: nameof(ViewModel),
            propertyType: typeof(TagsListViewModel),
            ownerType: typeof(TagsList),
            typeMetadata: new PropertyMetadata(null));

        public static readonly DependencyProperty TagStyleProperty = DependencyProperty.Register(
            name: nameof(TagStyle),
            propertyType: typeof(TagStyle),
            ownerType: typeof(TagsList),
            typeMetadata: new PropertyMetadata(TagStyle.Normal));

        public TagsList()
        {
            this.InitializeComponent();
            if (!DesignMode.DesignModeEnabled)
            {
                this.WhenActivated(d =>
                {
                    this.WhenAnyValue(view => view.ViewModel.Tags)
                        .BindTo(this, view => view.TagsControl.ItemsSource)
                        .DisposeWith(d);
                });
            }
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (TagsListViewModel)value;
        }

        public TagsListViewModel ViewModel
        {
            get => (TagsListViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        public TagStyle TagStyle
        {
            get => (TagStyle)GetValue(TagStyleProperty);
            set => SetValue(TagStyleProperty, value);
        }
    }
}
