using MessageGenerator.View;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Native;
using System;
using Windows.Foundation;
using Windows.Storage.Pickers;
using WinRT.Interop;

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

        public event RoutedEventHandler LoadedWindow
        {
            add
            {
                _dummyLoadControl.Loaded += value;
            }
            remove
            {
                _dummyLoadControl.Loaded -= value;
            }
        }

        public bool EnabledAddButton
        {
            set
            {
                addButton.IsEnabled = value;
            }
        }

        public bool EnabledGenerateButton
        {
            set
            {
                generateButton.IsEnabled = value;
            }
        }

        public event Action GeneratedMessage;
        public event Action DefaultLoadXml;
        public event Action<string> SelectGuideXml;
        public event Action<DateTime, DateTime> AddedShecule;
        public event Action<int> RemovedAtSchedule;

        private readonly DateTimePicker _startPicker;
        private readonly DateTimePicker _endPicker;
        private readonly ScheduleControlList _scheduleControlList;
        private readonly WindowNativeControl _nativeControl;
        private readonly DummyLoadControl _dummyLoadControl = new();
        private GenerateControl _generateControl;
        private ErrorDialog _errorDialog;

        public MainWindow()
        {
            this.InitializeComponent();
            _nativeControl = NativeControlFactory.CreateWindowNativeControl(
                this,
                ViewHelper.GetWindowSizeConfig(ViewHelper.CreateKeyWord),
                ViewHelper.GetWindowSizeConfig(ViewHelper.MinKeyWord),
                ViewHelper.GetWindowSizeConfig(ViewHelper.MaxKeyWord)
                );

            _dummyLoadControl.Loaded += (s, e) => LoadedInit();

            rootGrid.Children.Add(_dummyLoadControl);

            _startPicker = new DateTimePicker(startDatePicker, startTimePicker);
            _endPicker = new DateTimePicker(startDatePicker, endTimePicker);
            
            _scheduleControlList = new ScheduleControlList(scheduleStackPanel, Application.Current.Resources, true);

            addErrorFlyout.Target = addButton;
            addButton.Click += ClickAddButton;
            generateButton.Click += (s,e) => GeneratedMessage?.Invoke();
            defaultXmlItem.Click += (s, e) => DefaultLoadXml?.Invoke();
            selectGuideItem.Click += (s, e) => ClickSelectGuide();
        }

        public void AddScheduleElement(string scheduleText)
        {
            var control = new ScheduleControl(scheduleText);
            control.ClickDeleteButton += (s, a) => ClickRemoveScheduleButton(control);
            _scheduleControlList.AddScheduleControl(control);
        }

        public void ClearScheduleElements()
        {
            _scheduleControlList.ClearScheduleControls();
        }

        public void ShowGenerateDialog(string generated)
        {
            _generateControl.ShowGenerateDialogAsync(generated);
        }

        public void ShowErrorDialog(string errorMessage)
        {
            _errorDialog.ShowDialog(errorMessage);
        }

        public void ShowErrorFlyout(string subtitle)
        {
            addErrorFlyout.Subtitle = subtitle;
            addErrorFlyout.IsOpen = true;
        }

        private void LoadedInit()
        {
            _generateControl = new GenerateControl(this.Content.XamlRoot);
            _errorDialog = new ErrorDialog(this.Content.XamlRoot);
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

        private async void ClickSelectGuide()
        {
            var dialog = new FileOpenPicker();
            InitializeWithWindow.Initialize(dialog, WindowNative.GetWindowHandle(this));
            dialog.FileTypeFilter.Add(".xml");
            var file = await dialog.PickSingleFileAsync();
            if (file != null)
            {
                SelectGuideXml?.Invoke(file.Path);
            }
        }
    }
}
