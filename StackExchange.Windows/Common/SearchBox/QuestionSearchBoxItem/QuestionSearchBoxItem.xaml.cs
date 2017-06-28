using Windows.ApplicationModel;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ReactiveUI;
using StackExchange.Windows.BindingConverters;
using StackExchange.Windows.Questions;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace StackExchange.Windows.Common.SearchBox.QuestionSearchBoxItem
{
    public sealed partial class QuestionSearchBoxItem : UserControl, IViewFor<QuestionItemViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            nameof(ViewModel),
            typeof(QuestionsViewModel),
            typeof(QuestionSearchBoxItem),
            new PropertyMetadata(null));

        public QuestionSearchBoxItem()
        {
            this.InitializeComponent();
            if (!DesignMode.DesignModeEnabled)
            {
                this.WhenActivated(d =>
                {
                    d(this.Bind(ViewModel, vm => vm.Title, view => view.QuestionTitle.Text));
                    d(this.Bind(ViewModel, vm => vm.User.PostedOn, view => view.Time.Text));
                    d(this.Bind(ViewModel, vm => vm.Score, view => view.Score.Text));
                    d(this.Bind(ViewModel, vm => vm.User.Owner, view => view.Owner.Text));
                    d(this.OneWayBind(ViewModel, vm => vm.IsAnswered, view => view.ScorePanel.Background,
                        vmToViewConverterOverride: BooleanToBrushBindingTypeConverter.Create(@true: Colors.Aquamarine, @false: Colors.LightGray)));
                    d(this.OneWayBind(ViewModel, vm => vm.Tags, view => view.Tags.ViewModel));
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
            get => (QuestionItemViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}
