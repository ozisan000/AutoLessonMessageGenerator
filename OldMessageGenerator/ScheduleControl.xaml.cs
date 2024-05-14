using System;
using System.Collections;
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
using System.Linq;

namespace MessageGenerator
{
    /// <summary>
    /// Interaction logic for ScheduleControl.xaml
    /// </summary>
    public partial class ScheduleControl : UserControl
    {
        public event EventHandler<RoutedEventArgs>? ClickDeleteButton;
        private readonly IList _controlList;

        public ScheduleControl(IList controlList, string scheduleText)
        {
            InitializeComponent();
            _controlList = controlList;
            scheduleLabel.Content = scheduleText;
            scheduleDeleteButton.Click += Click_DeleteButton;
        }

        public int CheckThisIndex()
        {
            var scheduleTaple = _controlList
                .Cast<ScheduleControl>()
                .Select((item, index) => new { item, index })
                .FirstOrDefault((taple) => taple.item == this);
            if (scheduleTaple == null) throw new ArgumentNullException();
            return scheduleTaple.index;
        }

        private void Click_DeleteButton(object sender, RoutedEventArgs e)
        {
            ClickDeleteButton?.Invoke(sender, e);
        }
    }
}
