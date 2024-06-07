using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGenerator.View
{
    internal class ErrorDialog
    {
        private readonly ContentDialog _dialog = new();

        public ErrorDialog(XamlRoot root,string titleText = "Error", string closeText = "Close")
        {
            _dialog.XamlRoot = root;
            _dialog.Title = titleText;
            _dialog.CloseButtonText = closeText;
        }

        public async void ShowDialog(string message)
        {
            _dialog.Content = message;
            await _dialog.ShowAsync();
        }
    }
}
