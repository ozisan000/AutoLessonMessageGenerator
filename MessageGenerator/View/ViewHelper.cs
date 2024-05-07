using Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel.DataTransfer;

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
    }
}
