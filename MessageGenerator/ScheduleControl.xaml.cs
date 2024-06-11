using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml.Media;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MessageGenerator
{
    public sealed partial class ScheduleControl : UserControl
    {
        public event EventHandler<RoutedEventArgs> ClickDeleteButton;
        public Grid ScheduleElement => scheduleElement;

        public string Text => scheduleLabel.Text;

        public ScheduleControl(string scheduleText)
        {
            InitializeComponent();
            scheduleLabel.Text = scheduleText;
            scheduleDeleteButton.Click += Click_DeleteButton;
        }

        private void Click_DeleteButton(object sender, RoutedEventArgs e)
        {
            ClickDeleteButton?.Invoke(sender, e);
        }
    }
}
