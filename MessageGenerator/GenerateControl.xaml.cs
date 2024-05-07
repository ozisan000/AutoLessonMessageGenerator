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
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using MessageGenerator.View;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MessageGenerator
{
    public sealed partial class GenerateControl : UserControl
    {
        private readonly ContentDialog dialog = new();

        public GenerateControl(
            string titleText = "Generate Message",
            string primaryText = "CopyClipBoard",
            string closeText = "Close")
        {
            this.InitializeComponent();
            dialog.Content = this;
            dialog.Title = titleText;
            dialog.PrimaryButtonText = primaryText;
            dialog.CloseButtonText = closeText;
        }

        public async void ShowGenerateDialogAsync(XamlRoot root,string generated)
        {
            generateText.Text = generated;
            dialog.XamlRoot = root;
            dialog.PrimaryButtonClick += (s, e) => CopyTextClipBoard(s, e, generated);
            await dialog.ShowAsync();
            dialog.PrimaryButtonClick -= (s, e) => CopyTextClipBoard(s, e, generated);
        }

        private void CopyTextClipBoard(ContentDialog s,ContentDialogButtonClickEventArgs e,string generated)
        {
            ViewHelper.CopyTextClipBoard(generated);
        }
    }
}
