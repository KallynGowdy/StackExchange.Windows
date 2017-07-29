using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
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
using StackExchange.Windows.Login;

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
                ViewModel.Navigate.RegisterHandler(context =>
                {
                    if (!context.IsHandled)
                    {
                        if (NavigateByParams(context.Input))
                        {
                            context.SetOutput(Unit.Default);
                        }
                    }
                }).DisposeWith(d);
                ViewModel.NavigateBack.RegisterHandler(context =>
                {
                    if (!context.IsHandled)
                    {
                        if (RootFrame.CanGoBack)
                        {
                            RootFrame.GoBack();
                            context.SetOutput(Unit.Default);
                        }
                    }
                }).DisposeWith(d);
                ViewModel.NavigateAndClearStack.RegisterHandler(context =>
                {
                    if (!context.IsHandled)
                    {
                        NavigateByParams(context.Input);
                        RootFrame.BackStack.Clear();
                        CheckCanGoBack();
                        context.SetOutput(Unit.Default);
                    }
                }).DisposeWith(d);

                ViewModel.Navigate.Handle(new NavigationParams(typeof(LoginPage))).Subscribe().DisposeWith(d);
            });
        }

        private void FrameOnNavigated(object sender, NavigationEventArgs navigationEventArgs)
        {
            CheckCanGoBack();
        }

        private void CheckCanGoBack()
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = RootFrame.CanGoBack
                ? AppViewBackButtonVisibility.Visible
                : AppViewBackButtonVisibility.Collapsed;
        }

        private bool NavigateByParams(NavigationParams input)
        {
            return RootFrame.Navigate(input.PageType, input.Parameter);
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (ApplicationViewModel)value; }
        }

        public IApplicationViewModel ViewModel { get; set; }
    }
}
