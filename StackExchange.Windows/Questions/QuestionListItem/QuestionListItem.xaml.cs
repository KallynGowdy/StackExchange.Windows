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
            get => (QuestionItemViewModel)GetValue(QuestionProperty);
            set => SetValue(QuestionProperty, value);
        }

        public QuestionListItem()
        {
            this.InitializeComponent();
            if (!DesignMode.DesignModeEnabled)
            {
                this.WhenActivated(d =>
                {
                    this.Bind(ViewModel, vm => vm.Title, view => view.Title.Text)
                        .DisposeWith(d);
                    this.Bind(ViewModel, vm => vm.Score, view => view.Score.Text)
                        .DisposeWith(d);
                    this.Bind(ViewModel, vm => vm.Views, view => view.NumViews.Text)
                        .DisposeWith(d);
                    this.Bind(ViewModel, vm => vm.Answers, view => view.NumAnswers.Text)
                        .DisposeWith(d);
                    this.OneWayBind(ViewModel, vm => vm.IsAnswered, view => view.AnswersPanel.Background, vmToViewConverterOverride: BooleanToBrushBindingTypeConverter.Create(@true: Colors.Aquamarine, @false: Colors.Transparent))
                        .DisposeWith(d);
                    this.Bind(ViewModel, vm => vm.User, view => view.UserCard.ViewModel)
                        .DisposeWith(d);
                    this.Bind(ViewModel, vm => vm.User, view => view.UserCard.ViewModel)
                        .DisposeWith(d);
                    this.Bind(ViewModel, vm => vm.Tags, view => view.Tags.ViewModel)
                        .DisposeWith(d);
                });
            }
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (QuestionItemViewModel)value;
        }

        public QuestionItemViewModel ViewModel
        {
            get => Question;
            set => Question = value;
        }
    }
}
