using System;
using System.Windows.Forms;

namespace RPNCalculator
{
    public partial class CalculatorView : Form
    {
        private ICalculatorPresenter _presenter;

        internal ICalculatorPresenter Presenter
        {
            get => _presenter;
            set
            {
                if (_presenter == value)
                    return;

                if (_presenter != null)
                {
                    _presenter.ValueChanged -= OnPresenterChanged;
                    ClearButton.Click -= _presenter.OnClarButtonClicked;
                    FormulaTextBox.KeyPress -= _presenter.OnKeyPress;
                }

                _presenter = value;

                if (_presenter != null)
                {
                    _presenter.ValueChanged += OnPresenterChanged;
                    ClearButton.Click += _presenter.OnClarButtonClicked;
                    FormulaTextBox.KeyPress += _presenter.OnKeyPress;
                }
            }
        }

        public CalculatorView()
        {
            InitializeComponent();
        }

        private void OnPresenterChanged(object sender, EventArgs e)
        {
            var text = _presenter.Text;
            FormulaTextBox.Text = text;
            FormulaTextBox.SelectionLength = 0;
            FormulaTextBox.SelectionStart = text.Length;
        }
    }
}