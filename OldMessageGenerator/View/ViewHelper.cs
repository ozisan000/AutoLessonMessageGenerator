using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
                var timeLabel = new Label();
                timeLabel.Content = i.ToString(format);
                comboBox.Items.Add(timeLabel);
            }
            comboBox.SelectedIndex = 0;
        }
    }
}
