using System.Reactive.Disposables;
using System.Reactive.Linq;
using Windows.ApplicationModel;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using ReactiveUI;
using StackExchange.Windows.BindingConverters;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace StackExchange.Windows.Questions.QuestionListItem
{
    public sealed partial class QuestionListItem : UserControl, IViewFor<QuestionItemViewModel>
    {
        public static readonly DependencyProperty QuestionProperty = DependencyProperty.Register(
            nameof(Question),
            typeof(QuestionItemViewModel),
            typeof(QuestionListItem),
            new PropertyMetadata(null));

        public QuestionItemViewModel Question
        {
            get
            {
                return (QuestionItemViewModel)GetValue(QuestionProperty);
            }
            set
            {
                SetValue(QuestionProperty, value);
            }
        }

        public QuestionListItem()
        {
            this.InitializeComponent();
            if (!DesignMode.DesignModeEnabled)
            {
                this.WhenActivated(d =>
                {
                    d(this.Bind(ViewModel, vm => vm.Title, view => view.Title.Text));
                    d(this.Bind(ViewModel, vm => vm.Score, view => view.Score.Text));
                    d(this.Bind(ViewModel, vm => vm.Views, view => view.NumViews.Text));
                    d(this.Bind(ViewModel, vm => vm.Answers, view => view.NumAnswers.Text));
                    d(this.OneWayBind(ViewModel, vm => vm.IsAnswered, view => view.AnswersPanel.Background, vmToViewConverterOverride: BooleanToBrushBindingTypeConverter.Create(@true: Colors.Aquamarine, @false: Colors.Transparent)));
                    d(this.Bind(ViewModel, vm => vm.User, view => view.UserCard.User));
                    d(this.Bind(ViewModel, vm => vm.Tags, view => view.Tags.ItemsSource));
                });
            }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (QuestionItemViewModel) value; }
        }

        public QuestionItemViewModel ViewModel
        {
            get { return Question; }
            set { Question = value; }
        }
    }
}
