//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using AssetManager.Gui.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AssetManager.Gui.Views
{
    public class MessageBox : Window
    {
        // ---------------- Fields ----------------

        // ---------------- Constructor ----------------

        public MessageBox( MessageBoxModel model )
        {
            this.DataContext = model;
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load( this );
        }

        // ---------------- Functions ----------------

        private void OkayButton_OnClicked()
        {
            this.Close();
        }
    }
}
