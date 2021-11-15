using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Components
{
    class YesNoButton : Button
    {
        public string Question { get; set; }

        protected override void OnClick()
        {
            if (string.IsNullOrWhiteSpace(Question))
            {
                base.OnClick();
                return;
            }

            var messageBoxResult = MessageBox.Show(Question, "Confirmation", MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
                base.OnClick();
        }
    }
}
