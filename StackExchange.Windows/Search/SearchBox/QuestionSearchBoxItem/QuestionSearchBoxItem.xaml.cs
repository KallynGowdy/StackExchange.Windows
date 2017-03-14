using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ReactiveUI;
using StackExchange.Windows.BindingConverters;
using StackExchange.Windows.Questions;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace StackExchange.Windows.Search.SearchBox.QuestionSearchBoxItem
{
    public sealed partial class QuestionSearchBoxItem : UserControl, IViewFor<QuestionViewModel>
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
                    d(this.OneWayBind(ViewModel, vm => vm.Tags, view => view.Tags.ItemsSource));
                });
            }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (QuestionViewModel)value; }
        }

        public QuestionViewModel ViewModel
        {
            get { return (QuestionViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
    }
}
