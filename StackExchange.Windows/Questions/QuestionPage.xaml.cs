using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
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
using StackExchange.Windows.Api.Models;
using StackExchange.Windows.Application;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace StackExchange.Windows.Questions
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class QuestionPage : Page, IViewFor<QuestionPageViewModel>
    {
        public QuestionPage()
        {
            this.InitializeComponent();
            this.WhenActivated(d =>
            {
                d(this.Bind(ViewModel, vm => vm.Question.Title, view => view.QuestionTitle.Text));
                d(this.Bind(ViewModel, vm => vm.AnswersTitle, view => view.AnswersTitle.Text));
                d(this.OneWayBind(ViewModel, vm => vm.Question, view => view.Question.ViewModel));
                d(this.OneWayBind(ViewModel, vm => vm.Answers, view => view.Answers.ItemsSource));

                d(ViewModel.Load.IsExecuting.BindTo(this, view => view.LoadingRing.IsActive));
                d(ViewModel.Load.Execute().Subscribe());

                var app = Locator.Current.GetService<IApplicationViewModel>();
                app.OpenUri.RegisterHandler(ctx =>
                {
                    // TODO: Allow the user to specify whether to open in a real browser
                    //       or our pseudo-browser.
                    if (!ctx.IsHandled)
                    {
                        SplitContent.IsPaneOpen = true;
                        WebResults.Navigate(ctx.Input);
                        ctx.SetOutput(Unit.Default);
                    }
                })
                .DisposeWith(d);
            });
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel = new QuestionPageViewModel(((Question)e.Parameter).QuestionId);
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (QuestionPageViewModel)value; }
        }

        public QuestionPageViewModel ViewModel { get; set; }

        private void WebResults_OnNavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            
        }
    }
}
