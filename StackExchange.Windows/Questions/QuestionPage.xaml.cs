using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using StackExchange.Windows.Api.Models;

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
                d(this.Bind(ViewModel, vm => vm.Title, view => view.QuestionTitle.Text));
                d(this.Bind(ViewModel, vm => vm.Question, view => view.Question.ViewModel));
                d(this.OneWayBind(ViewModel, vm => vm.Answers, view => view.Answers.ItemsSource));

                d(ViewModel.LoadAnswers.Execute().Subscribe());
            });
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel = new QuestionPageViewModel((Question)e.Parameter);
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (QuestionPageViewModel)value; }
        }

        public QuestionPageViewModel ViewModel { get; set; }
    }
}
