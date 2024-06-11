using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace MessageGenerator.View
{
    internal class ScheduleControlList
    {
        private const string StripeThemeKey1 = "StripeBack1";
        private const string StripeThemeKey2 = "StripeBack2";
        private readonly StackPanel _scheduleStackPanel;
        private readonly List<ScheduleControl> _scheduleControls;
        private readonly SolidColorBrush _stripeTheme1;
        private readonly SolidColorBrush _stripeTheme2;
        public bool IsReDrawBackGround { get; set; }

        public ScheduleControlList(StackPanel panel, ResourceDictionary resources, bool isReDraw = false)
        {
            _scheduleControls = new List<ScheduleControl>();
            _scheduleStackPanel = panel;
            IsReDrawBackGround = isReDraw;
            string stripeTheme1 = ConfigurationManager.AppSettings[StripeThemeKey1];
            string stripeTheme2 = ConfigurationManager.AppSettings[StripeThemeKey2];
            _stripeTheme1 = (SolidColorBrush)resources[stripeTheme1];
            _stripeTheme2 = (SolidColorBrush)resources[stripeTheme2];
        }

        public void AddScheduleControl(ScheduleControl addControl)
        {
            _scheduleControls.Add(addControl);
            _scheduleStackPanel.Children.Add(addControl);
            ReDrawStripeBackground();
        }

        public void ClearScheduleControls()
        {
            _scheduleControls.RemoveAll(x => true);
            _scheduleStackPanel.Children.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="removeControl"></param>
        /// <returns>removeIndex</returns>
        public int RemoveScheduleControl(ScheduleControl removeControl)
        {
            int removeIndex = SearchScheduleControlIndex(removeControl);
            _scheduleControls.Remove(removeControl);
            _scheduleStackPanel.Children.Remove(removeControl);
            ReDrawStripeBackground();
            return removeIndex;
        }

        public int SearchScheduleControlIndex(ScheduleControl searchControl)
        {
            return _scheduleControls
                .Select((control, index) => new { control, index })
                .FirstOrDefault((taple) => taple.control == searchControl).index;
        }

        private void ReDrawStripeBackground()
        {
            if (!IsReDrawBackGround) return;
            for (int i = 0; i < _scheduleControls.Count; i++)
            {
                var control = _scheduleControls[i];
                if (i % 2 == 0)
                    control.ScheduleElement.Background = _stripeTheme1;
                else
                    control.ScheduleElement.Background = _stripeTheme2;
            }
        }
    }
}
