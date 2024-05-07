using Native;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;
using MessageGenerator.Controller;
using MessageGenerator.View;
using Microsoft.UI.System;
using Windows.UI;
using Microsoft.UI;
using Windows.ApplicationModel.DataTransfer;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MessageGenerator
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private ReservationController _controller;
        private DateTimePicker _startPicker;
        private DateTimePicker _endPicker;
        private GenerateControl _generateControl;
        private WindowNativeControl _nativeControl;
        private SizeInt32 minSize = new(700, 400);
        private SizeInt32 createSize = new(800, 500);
        private SizeInt32 maxSize = new(900, 2000);

        public MainWindow()
        {
            this.InitializeComponent();

            _nativeControl = NativeControlFactory.CreateWindowNativeControl(
                this,
                createSize,
                minSize,
                maxSize
                );

            _controller = new ReservationController();

            _startPicker = new DateTimePicker(startDatePicker, startTimePicker);
            _endPicker = new DateTimePicker(startDatePicker, endTimePicker);
            _generateControl = new GenerateControl();

            moneyInputBox.ValueChanged += _controller.UpdateLessonFee;
            addButton.Click += ClickAddButton;
            generateButton.Click += ClickGenerateButton;
        }

        private void AddScheduleView(string scheduleText)
        {
            ScheduleControl control = new ScheduleControl(scheduleStackPanel.Children, scheduleText);
            control.ClickDeleteButton += (s, a) => ClickDeleteButton(control);
            scheduleStackPanel.Children.Add(control);
            ReDrawScheduleBackground();
        }

        private void ClickAddButton(object s, RoutedEventArgs e)
        {
            string scheduleText = _controller.AddSchedule(_startPicker.GetDateTime(), _endPicker.GetDateTime());
            AddScheduleView(scheduleText);
        }

        private void ClickDeleteButton(ScheduleControl deleteSchedule)
        {
            int deleteIndex = deleteSchedule.CheckThisIndex();
            _controller.RemoveSchedule(deleteIndex);
            scheduleStackPanel.Children.RemoveAt(deleteIndex);
            ReDrawScheduleBackground();
        }

        private void ClickGenerateButton(object s, RoutedEventArgs e)
        {
            string generated = _controller.GenerateGuideMessage();
            if (generated == null) return;
            _generateControl.ShowGenerateDialogAsync(this.Content.XamlRoot,generated);
        }

        //HelperMethod
        private void ReDrawScheduleBackground()
        {
            for (int i = 0; i < scheduleStackPanel.Children.Count; i++)
            {
                var control = (ScheduleControl)scheduleStackPanel.Children[i];
                if (i % 2 == 0)
                    control.ScheduleElement.Background = (SolidColorBrush)Application.Current.Resources["LayerOnAcrylicFillColorDefaultBrush"];
                else
                    control.ScheduleElement.Background = (SolidColorBrush)Application.Current.Resources["DesktopAcrylicTransparentBrush"];
            }
        }
    }
}
