﻿using System.Windows.Input;

namespace CosmosDBStudio.Dialogs
{
    public class DialogButton
    {
        public string Text { get; set; }
        public bool? DialogResult { get; set; }
        public bool IsDefault { get; set; }
        public bool IsCancel { get; set; }
        public ICommand Command { get; set; }
        public object CommandParameter { get; set; }
    }
}
