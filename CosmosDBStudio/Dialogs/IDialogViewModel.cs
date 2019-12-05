﻿using System;
using System.Collections.Generic;

namespace CosmosDBStudio.Dialogs
{
    public interface IDialogViewModel
    {
        string Title { get; }
        IEnumerable<DialogButton> Buttons { get; }
        event EventHandler<bool?> CloseRequested;
    }

    public interface ISizableDialog
    {
        double Width { get; set; }
        double Height { get; set; }
        bool IsResizable { get; set; }
    }
}