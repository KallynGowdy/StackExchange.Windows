using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace StackExchange.Windows.Settings
{
    /// <summary>
    /// Defines a name-value pair representing an enum option.
    /// </summary>
    public class EnumValue : ReactiveObject
    {
        private readonly ObservableAsPropertyHelper<bool> isSelected;

        public string Name { get; }
        public string Key { get; }
        public Enum Value { get; }
        public ReactiveCommand<Unit, Unit> Select { get; }
        public bool IsSelected => isSelected.Value;

        private readonly EnumSettingsItemViewModel viewModel;

        public EnumValue(Enum value, string name, EnumSettingsItemViewModel viewModel)
        {
            this.Value = value;
            this.Name = name;
            this.Key = viewModel.NameResource;
            this.viewModel = viewModel;

            Select = ReactiveCommand.Create(SelectImpl);
            isSelected = this.WhenAnyValue(vm => vm.viewModel.Value)
                .Select(val => Equals(val, Value))
                .ToProperty(this, vm => vm.IsSelected);
        }

        private void SelectImpl()
        {
            viewModel.Value = Value;
        }
    }
}
