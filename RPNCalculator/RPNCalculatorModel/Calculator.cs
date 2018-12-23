using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPNCalculatorModel
{
    public class Calculator
    {
        public event EventHandler ValuesChanged;

        public string CurrentValue { get; set; } = string.Empty;

        public Stack<double> Values { get; } = new Stack<double>();

        public string Text
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var value in Values.Reverse())
                {
                    sb.Append(value.ToString() + " ");
                }
                sb.Append(CurrentValue);
                return sb.ToString();
            }
        }

        public bool Add() => CulculateCore((arg1, arg2) => arg1 + arg2);

        public bool Subtract() => CulculateCore((arg1, arg2) => arg1 - arg2);

        public bool Multiply() => CulculateCore((arg1, arg2) => arg1 * arg2);

        public bool Divide() => CulculateCore((arg1, arg2) => arg1 / arg2);

        private bool CulculateCore(Func<double, double, double> func)
        {
            bool result;
            try
            {
                var arg1 = Values.Pop();
                var arg2 = Values.Pop();

                Values.Push(func(arg2, arg1));
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            ValuesChanged?.Invoke(this, EventArgs.Empty);
            return result;
        }

        public void Clear()
        {
            Values.Clear();
            ValuesChanged?.Invoke(this, EventArgs.Empty);
        }

        public void AddCode(char code)
        {
            if ('0' <= code && code <= '9' || code == '.')
            {
                CurrentValue = CurrentValue + code;
            }
            else
            {
                ProcessCurrentValue();
                if (code == '+')
                {
                    Add();
                }
                else if (code == '-')
                {
                    Subtract();
                }
                else if (code == '*')
                {
                    Multiply();
                }
                else if (code == '/')
                {
                    Divide();
                }
            }
            ValuesChanged?.Invoke(this, EventArgs.Empty);

            void ProcessCurrentValue()
            {
                if (string.IsNullOrEmpty(CurrentValue))
                    return;

                if (double.TryParse(CurrentValue, out var value))
                {
                    Values.Push(value);
                    CurrentValue = string.Empty;
                }
            }
        }

        public void RemoveCode()
        {
            if (string.IsNullOrEmpty(CurrentValue))
            {
                if (Values.Count > 0)
                {
                    var value = Values.Pop();
                    CurrentValue = value.ToString();
                }
            }
            else
            {
                CurrentValue = CurrentValue.Substring(0, CurrentValue.Length - 1);
            }
            ValuesChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}