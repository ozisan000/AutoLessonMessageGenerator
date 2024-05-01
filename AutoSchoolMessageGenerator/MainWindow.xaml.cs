using AutoSchoolMessageGenerator.Controller;
using AutoSchoolMessageGenerator.Logic;
using AutoSchoolMessageGenerator.View;
using System.Windows;
using System.Windows.Media;

namespace AutoSchoolMessageGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LessonFeeBox _lessonFeeBox;
        private SchedulePicker _startPicker;
        private SchedulePicker _endPicker;

        private ReservationController _contloller;

        public MainWindow()
        {
            InitializeComponent();
            _contloller = new ReservationController();

            _lessonFeeBox = new LessonFeeBox(moneyInputBox);
            var startTime = new TimeCombo(startHourCombo, startMinuteCombo);
            var endTime = new TimeCombo(endHourCombo, endMinuteCombo);
            _startPicker = new SchedulePicker(startDatePicker, startTime);
            _endPicker = new SchedulePicker(startDatePicker, endTime);

            _lessonFeeBox.LessonFeeChanged += (s, e) => _contloller.UpdateLessonFee(e);

            addButton.Click += ClickAddButton;

            //TestCode
            for (int i = 0; i < 10; i++)
            {
                ScheduleControl control;
                control = new ScheduleControl(i.ToString());
                control.ClickDeleteButton += (s, a) =>
                {
                    scheduleStackPanel.Children.Remove(control);
                    ReDrawScheduleBackground();
                };
                scheduleStackPanel.Children.Add(control);
            }
            ReDrawScheduleBackground();
        }

        private void AddScheduleView(string scheduleText)
        {
            ScheduleControl control;
            control = new ScheduleControl(scheduleText);
            control.ClickDeleteButton += (s, a) =>
            {
                scheduleStackPanel.Children.Remove(control);
                ReDrawScheduleBackground();
            };

            scheduleStackPanel.Children.Add(control);
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

        private void ClickAddButton(object s, RoutedEventArgs e)
        {
            string scheduleText = _contloller.AddSchedule(_startPicker.GetDateTime(), _endPicker.GetDateTime());
            AddScheduleView(scheduleText);
        }

        private void ClickDeleteButton(object s, RoutedEventArgs e)
        {

        }

        private void ClickGenerateButton()
        {

        }
    }
}