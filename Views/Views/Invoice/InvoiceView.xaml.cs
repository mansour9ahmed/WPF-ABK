using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Views
{
    /// <summary>
    /// Interaction logic for InvoiceView.xaml
    /// </summary>
    public partial class InvoiceView : UserControl
    {
        public InvoiceView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SendEmaillView v = new SendEmaillView();
            v.Show();
        }
    }
}
