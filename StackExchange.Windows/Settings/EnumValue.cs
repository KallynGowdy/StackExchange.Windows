using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace StackExchange.Windows.Settings
{
    /// <summary>
    /// Defines a name-value pair representing an enum option.
    /// </summary>
    public class EnumValue
    {
        public string Name { get; }
        public int Value { get; }
        public ReactiveCommand<Unit, Unit> Select { get; }

        private readonly EnumSettingsItemViewModel viewModel;

        public EnumValue(int value, string name, EnumSettingsItemViewModel viewModel)
        {
            this.Value = value;
            this.Name = name;
            this.viewModel = viewModel;

            Select = ReactiveCommand.Create(SelectImpl);
        }

        private void SelectImpl()
        {
            viewModel.Value = Value;
        }
    }
}
