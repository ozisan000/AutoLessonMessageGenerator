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
using MessageGenerator.Configuration;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MessageGenerator
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public event TypedEventHandler<NumberBox, NumberBoxValueChangedEventArgs> ChangedLessonFee
        {
            add
            {
                feeInputBox.ValueChanged += value;
            }
            remove
            {
                feeInputBox.ValueChanged -= value;
            }
        }

        public event Action GeneratedMessage;

        public event Action<DateTime, DateTime> AddedShecule;

        public event Action<int> RemovedAtSchedule;

        private readonly DateTimePicker _startPicker;
        private readonly DateTimePicker _endPicker;
        private readonly GenerateControl _generateControl;
        private readonly ScheduleControlList _scheduleControlList;
        private readonly WindowNativeControl _nativeControl;

        public MainWindow()
        {
            this.InitializeComponent();

            _nativeControl = NativeControlFactory.CreateWindowNativeControl(
                this,
                ViewHelper.GetWindowSizeConfig(ViewHelper.CreateKeyWord),
                ViewHelper.GetWindowSizeConfig(ViewHelper.MinKeyWord),
                ViewHelper.GetWindowSizeConfig(ViewHelper.MaxKeyWord)
                );

            _startPicker = new DateTimePicker(startDatePicker, startTimePicker);
            _endPicker = new DateTimePicker(startDatePicker, endTimePicker);
            _generateControl = new GenerateControl();
            _scheduleControlList = new ScheduleControlList(scheduleStackPanel, Application.Current.Resources, true);

            addErrorFlyout.Target = addButton;
            addButton.Click += ClickAddButton;
            generateButton.Click += (s,e) => GeneratedMessage?.Invoke();
        }

        public void AddScheduleElement(string scheduleText)
        {
            var control = new ScheduleControl(scheduleText);
            control.ClickDeleteButton += (s, a) => ClickRemoveScheduleButton(control);
            _scheduleControlList.AddScheduleControl(control);
        }

        public void ShowGenerateDialog(string generated)
        {
            _generateControl.ShowGenerateDialogAsync(this.Content.XamlRoot, generated);
        }

        public void ShowErrorFlyout(string subtitle)
        {
            addErrorFlyout.Subtitle = subtitle;
            addErrorFlyout.IsOpen = true;
        }

        private void ClickAddButton(object s, RoutedEventArgs e)
        {
            (DateTime result, DateTimePickerError error) startResult = _startPicker.GetDateTime();
            (DateTime result, DateTimePickerError error) endResult = _endPicker.GetDateTime();
            if (CheckAddButtonError(startResult.error) || CheckAddButtonError(endResult.error)) return;

            AddedShecule?.Invoke(startResult.result, endResult.result);
        }

        private bool CheckAddButtonError(DateTimePickerError error)
        {
            DateTimePickerErrorHandler check = new();
            if (!check.CheckError(error)) return false;
            IDateTimePickerError errorResource = check.GetIDateTimePickerError(error);
            ShowErrorFlyout(errorResource.ErrorMessage);
            return true;
        }

        private void ClickRemoveScheduleButton(ScheduleControl deleteSchedule)
        {
            int deleteIndex = _scheduleControlList.RemoveScheduleControl(deleteSchedule);
            RemovedAtSchedule?.Invoke(deleteIndex);
        }
    }
}
