using MessageGenerator.Configuration;
using Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel.DataTransfer;
using Windows.Graphics;

namespace MessageGenerator.View
{
    internal class ViewHelper
    {
        /// <summary>
        ///0~num分の項目を設定したComboBoxを作成
        ///例) num = 24 => 0~24の項目が設定される
        /// </summary>
        /// <param name="comboBox"></param>
        public static void ZetoToNumComboBox(ComboBox comboBox, int num ,string format = "")
        {
            for (int i = 0; i <= num; i++)
            {
                var timeLabel = new TextBlock();
                timeLabel.Text = i.ToString(format);
                comboBox.Items.Add(timeLabel);
            }
            comboBox.SelectedIndex = 0;
        }

        public static void CopyTextClipBoard(string generated)
        {
            var package = new DataPackage();
            package.SetText(generated);
            Clipboard.SetContent(package);
        }

        public static SizeInt32 GetWindowSizeConfig(string keyword)
        {
            var size = new SizeInt32();
            size.Width = ConfigHelper.GetConfigValue<int>(keyword + WidthKeyWord);
            size.Height = ConfigHelper.GetConfigValue<int>(keyword + HeightKeyWord);
            return size;
        }

        public const string MinKeyWord = "Min";
        public const string MaxKeyWord = "Max";
        public const string DefaultCreateKeyWord = "DefaultCreate";
        public const string CreateKeyWord = "Create";
        public const string WidthKeyWord = "Width";
        public const string HeightKeyWord = "Height";
    }
}
