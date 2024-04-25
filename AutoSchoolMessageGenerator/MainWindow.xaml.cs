using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoSchoolMessageGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CreateTimeCombo(startHourCombo,24);
            CreateTimeCombo(endHourCombo, 24); 
            CreateTimeCombo(startMiniteCombo, 60);
            CreateTimeCombo(endMiniteCombo, 60);


            for (int i = 0; i <= 100; i++)
            {
                var labe = new Label();
                labe.Content = "aavavava";
                var aaa = new StackPanel();
                aaa.Orientation = Orientation.Horizontal;
                aaa.Children.Add(labe);
                confimStackPanel.Children.Add(aaa);
            }
        }

        /// <summary>
        /// 24時間設定のComboBoxを作成
        /// </summary>
        /// <param name="comboBox"></param>
        private void CreateTimeCombo(ComboBox comboBox,int time)
        {
            comboBox.Items.Clear();
            for (int i = 0; i <= time; i++)
            {
                var timeLabel = new Label();
                timeLabel.Content = i;
                comboBox.Items.Add(timeLabel);
            }
        }
    }
}