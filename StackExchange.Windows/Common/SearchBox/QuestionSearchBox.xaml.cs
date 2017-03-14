using Windows.UI.Xaml.Controls;
using ReactiveUI;
using Splat;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace StackExchange.Windows.Common.SearchBox
{
    public sealed partial class QuestionSearchBox : UserControl, IViewFor<ISearchViewModel>
    {
        public QuestionSearchBox()
        {
            this.InitializeComponent();
            this.WhenActivated(d =>
            {
                d(this.Bind(ViewModel, vm => vm.Query, view => view.InputBox.Text));
                d(this.Bind(ViewModel, vm => vm.SuggestedQuestions, view => view.InputBox.ItemsSource));
            });
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (SearchViewModel)value; }
        }

        public ISearchViewModel ViewModel { get; set; } = Locator.Current.GetService<ISearchViewModel>();
    }
}
