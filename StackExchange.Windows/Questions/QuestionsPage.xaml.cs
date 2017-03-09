using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace StackExchange.Windows.Questions
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class QuestionsPage : Page, IViewFor<QuestionsViewModel>
    {
        public QuestionsPage()
        {
            this.InitializeComponent();
            this.WhenActivated(d =>
            {
                d(this.OneWayBind(ViewModel, vm => vm.Questions, view => view.Questions.ItemsSource));
                d(ViewModel.LoadQuestions.IsExecuting.BindTo(this, view => view.LoadingRing.IsActive));
                d(this.BindCommand(ViewModel, vm => vm.Refresh, view => view.Refresh));

                d(ViewModel.LoadQuestions.Execute().Subscribe());
            });
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (QuestionsViewModel) value; }
        }

        public QuestionsViewModel ViewModel { get; set; } = new QuestionsViewModel();
    }
}
