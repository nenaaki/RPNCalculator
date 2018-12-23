using System;
using System.Windows.Input;
using RPNCalculatorModel;

namespace RPNCalculatorWPF
{
    public class CalculatorViewModel : ViewModelBase
    {
        private Calculator _model;

        public Calculator Model
        {
            get => _model;

            set
            {
                if (_model == value)
                    return;

                if (_model != null)
                {
                    _model.ValuesChanged -= OnModelChanged;
                }

                _model = value;

                if (_model != null)
                {
                    _model.ValuesChanged += OnModelChanged;
                }
            }
        }

        private string _text;

        public string Text
        {
            get => _text;

            set => SetProperty(ref _text, value);
        }

        public DelegateCommand InputTextCommand { get; }

        public DelegateCommand ClearCommand { get; }

        public CalculatorViewModel()
        {
            InputTextCommand = new DelegateCommand(param =>
            {
                char code = (char)param;
                switch (code)
                {
                    case (char)Key.Back:
                        _model.RemoveCode();
                        break;

                    default:
                        _model.AddCode(code);
                        break;
                }
            });

            ClearCommand = new DelegateCommand(param => Model.Clear());
        }

        private void OnModelChanged(object sender, EventArgs e) => Text = _model.Text;
    }
}