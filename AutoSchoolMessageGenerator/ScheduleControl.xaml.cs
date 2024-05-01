using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoSchoolMessageGenerator
{
    /// <summary>
    /// Interaction logic for ScheduleControl.xaml
    /// </summary>
    public partial class ScheduleControl : UserControl
    {
        public event EventHandler<RoutedEventArgs>? ClickDeleteButton;

        public ScheduleControl(string scheduleText)
        {
            InitializeComponent();
            scheduleLabel.Content = scheduleText;
        }

        private void scheduleDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ClickDeleteButton?.Invoke(sender, e);
        }
    }
}
