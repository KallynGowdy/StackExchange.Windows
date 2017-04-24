﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
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
using ReactiveUI;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace StackExchange.Windows.Common.PostDetail
{
    public sealed partial class PostDetail : UserControl, IViewFor<PostViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            nameof(ViewModel),
            typeof(PostViewModel),
            typeof(PostDetail),
            new PropertyMetadata(null));

        public PostDetail()
        {
            this.InitializeComponent();
            if (!DesignMode.DesignModeEnabled)
            {
                this.WhenActivated(d =>
                {
                    this.Bind(ViewModel, vm => vm.Score, view => view.Score.Text)
                        .DisposeWith(d);
                    this.OneWayBind(ViewModel, vm => vm.Poster, view => view.Poster.ViewModel)
                        .DisposeWith(d);
                    this.OneWayBind(ViewModel, vm => vm.Comments, view => view.Comments.ItemsSource)
                        .DisposeWith(d);
                    this.WhenAnyValue(view => view.ViewModel.Body)
                        .ObserveOn(RxApp.MainThreadScheduler)
                        .Where(body => !string.IsNullOrEmpty(body))
                        .Do(body => Body.NavigateToString(body))
                        .Subscribe()
                        .DisposeWith(d);

                    this.BindCommand(ViewModel, vm => vm.CopyLink, view => view.Link)
                        .DisposeWith(d);
                });
            }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (PostViewModel)value; }
        }

        public PostViewModel ViewModel
        {
            get { return (PostViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        private async void Body_OnDOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
        {
            await sender.ResizeHeightToContentAsync();
        }

        private async void Body_OnNavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            // Redirect all navigation to a real web browser
            if (args.Uri != null)
            {
                args.Cancel = true;
                await Launcher.LaunchUriAsync(args.Uri);
            }
        }
    }
}
