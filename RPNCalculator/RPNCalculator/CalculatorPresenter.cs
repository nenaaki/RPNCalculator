using System;
using System.Windows.Forms;
using RPNCalculatorModel;

namespace RPNCalculator
{
    internal interface ICalculatorPresenter
    {
        event EventHandler ValueChanged;

        void OnClarButtonClicked(object sender, EventArgs e);

        void OnKeyPress(object sender, KeyPressEventArgs e);

        string Text { get; set; }
    }

    internal class CalculatorPresenter : ICalculatorPresenter
    {
        private Calculator _model;

        public event EventHandler ValueChanged;

        public string Text { get; set; }

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

        private void OnModelChanged(object sender, EventArgs e)
        {
            Text = _model.Text;
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }

        public void OnClarButtonClicked(object sender, EventArgs e) => Model.Clear();

        public void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Back:
                    _model.RemoveCode();
                    break;

                default:
                    _model.AddCode(e.KeyChar);
                    break;
            }
        }
    }
}