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
        public event EventHandler<RoutedEventArgs>? ClickDeleteButton;
        private readonly IList<UIElement> _controlList;
        public Grid ScheduleElement => scheduleElement;

        public ScheduleControl(IList<UIElement> controlList, string scheduleText)
        {
            InitializeComponent();
            _controlList = controlList;
            scheduleLabel.Text = scheduleText;
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
