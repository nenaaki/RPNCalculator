using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RPNCalculatorModel;

namespace RPNCalculatorWPF
{
    public static class KeyboardCommandBehavior
    {
        public static DelegateCommand GetInputTextCommand(DependencyObject obj) => (DelegateCommand)obj.GetValue(InputTextCommandProperty);

        public static void SetInputTextCommand(DependencyObject obj, DelegateCommand value) => obj.SetValue(InputTextCommandProperty, value);

        public static readonly DependencyProperty InputTextCommandProperty =
            DependencyProperty.RegisterAttached("InputTextCommand", typeof(DelegateCommand), typeof(KeyboardCommandBehavior), new PropertyMetadata(null,
                (s, e) =>
                {
                    if (s is TextBox element)
                    {
                        if (e.NewValue != null)
                        {
                            element.TextInput += OnElementTextInput;
                            element.PreviewKeyDown += OnElementKeyDown;
                            element.TextChanged += OnElementTextChanged;
                        }
                    }
                }));

        private static void OnElementTextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox element)
            {
                element.CaretIndex = element.Text.Length;
            }
        }

        private static void OnElementKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (sender is TextBox element)
            {
                var command = GetInputTextCommand(element);
                if (e.Key == Key.Back)
                    command?.Execute((char)Key.Back);
                else if (e.Key == Key.Space)
                    command?.Execute((char)Key.Space);
            }
        }

        private static void OnElementTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (sender is TextBox element)
            {
                var command = GetInputTextCommand(element);
                command?.Execute(e.Text[0]);
            }
        }
    }
}