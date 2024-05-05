using AutoSchoolMessageGenerator.Controller;
using AutoSchoolMessageGenerator.Logic;
using AutoSchoolMessageGenerator.View;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;

namespace AutoSchoolMessageGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LessonFeeBox _lessonFeeBox;
        private DateTimePicker _startPicker;
        private DateTimePicker _endPicker;

        private ReservationController _contloller;

        public MainWindow()
        {
            InitializeComponent();

            _contloller = new ReservationController();

            _lessonFeeBox = new LessonFeeBox(moneyInputBox);
            var startTime = new TimeCombo(startHourCombo, startMinuteCombo);
            var endTime = new TimeCombo(endHourCombo, endMinuteCombo);
            _startPicker = new DateTimePicker(startDatePicker, startTime);
            _endPicker = new DateTimePicker(startDatePicker, endTime);

            _lessonFeeBox.LessonFeeChanged += _contloller.UpdateLessonFee;
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
            string scheduleText = _contloller.AddSchedule(_startPicker.GetDateTime(), _endPicker.GetDateTime());
            AddScheduleView(scheduleText);
        }

        private void ClickDeleteButton(ScheduleControl deleteSchedule)
        {
            int deleteIndex = deleteSchedule.CheckThisIndex();
            _contloller.RemoveSchedule(deleteIndex);
            scheduleStackPanel.Children.RemoveAt(deleteIndex);
            ReDrawScheduleBackground();
        }

        private void ClickGenerateButton(object s, RoutedEventArgs e)
        {
            string generated = _contloller.GenerateGuideMessage();
        }

        //HelperMethod
        private void ReDrawScheduleBackground()
        {
            for (int i = 0; i < scheduleStackPanel.Children.Count; i++)
            {
                var control = scheduleStackPanel.Children[i] as ScheduleControl;
                if (control == null) throw new Exception();
                if (i % 2 == 0)
                    control.Background = Brushes.Gainsboro;
                else
                    control.Background = Brushes.LightGray;
            }
        }
    }
}