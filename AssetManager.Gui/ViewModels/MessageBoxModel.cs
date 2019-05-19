//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManager.Gui.ViewModels
{
    public class MessageBoxModel : ViewModelBase
    {
        // ---------------- Constructor ----------------

        public MessageBoxModel()
        {
            this.Title = string.Empty;
            this.Message = string.Empty;
            this.Type = MessageBoxType.Information;
        }

        // ---------------- Properties ----------------

        public string Title { get; set; }

        public string Message { get; set; }

        public MessageBoxType Type { get; set; }

        // ---------------- Functions ----------------

        // ---------------- Enums ----------------

        public enum MessageBoxType
        {
            Information,
            Warning,
            Error
        }
    }
}
