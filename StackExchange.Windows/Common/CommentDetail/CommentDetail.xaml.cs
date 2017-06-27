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
using StackExchange.Windows.Html;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace StackExchange.Windows.Common.CommentDetail
{
    /// <summary>
    /// Defines a user control that is able to display detail for a <see cref="CommentViewModel"/>.
    /// </summary>
    public sealed partial class CommentDetail : UserControl, IViewFor<CommentViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            nameof(ViewModel),
            typeof(CommentViewModel),
            typeof(CommentDetail),
            new PropertyMetadata(null));

        public CommentDetail()
        {
            this.InitializeComponent();
            if (!DesignMode.DesignModeEnabled)
            {
                this.WhenActivated(d =>
                {
                    this.Bind(ViewModel, vm => vm.Score, view => view.Score.Text);

                    this.WhenAnyValue(view => view.ViewModel.Body)
                        .Do(block =>
                        {
                            CommentContent.Blocks.Clear();
                            CommentContent.Blocks.Add(block);
                        })
                        .Subscribe()
                        .DisposeWith(d);
                });
            }
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (CommentViewModel)value;
        }

        public CommentViewModel ViewModel
        {
            get => (CommentViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}
