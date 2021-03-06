﻿using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.UI.Xaml;
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
            if (!DesignMode.DesignModeEnabled)
            {
                this.WhenActivated(d =>
                {
                    this.Bind(ViewModel, vm => vm.Query, view => view.InputBox.Text)
                        .DisposeWith(d);
                    this.Bind(ViewModel, vm => vm.SuggestedQuestions, view => view.InputBox.ItemsSource)
                        .DisposeWith(d);
                    Observable.FromEventPattern<
                            TypedEventHandler<AutoSuggestBox, AutoSuggestBoxQuerySubmittedEventArgs>,
                            AutoSuggestBox,
                            AutoSuggestBoxQuerySubmittedEventArgs>(
                            h => this.InputBox.QuerySubmitted += h,
                            h => this.InputBox.QuerySubmitted -= h)
                        .Select(ep => ep.EventArgs.ChosenSuggestion)
                        .InvokeCommand(ViewModel, vm => vm.DisplayQuestion)
                        .DisposeWith(d);

                    ViewModel.FocusSearchBox.RegisterHandler(ctx =>
                    {
                        if (InputBox.Focus(FocusState.Programmatic))
                        {
                            ctx.SetOutput(Unit.Default);
                        }
                    }).DisposeWith(d);
                });
            }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (SearchViewModel)value; }
        }

        public ISearchViewModel ViewModel { get; set; } = Locator.Current.GetService<ISearchViewModel>();
    }
}
