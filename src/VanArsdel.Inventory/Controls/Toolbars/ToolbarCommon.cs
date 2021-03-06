﻿using System;

namespace VanArsdel.Inventory.Controls
{
    public enum ToolbarButton
    {
        New,
        Edit,
        Delete,
        Cancel,
        Save,
        Select,
        Refresh
    }

    public enum ListToolbarMode
    {
        Default,
        Cancel,
        CancelDelete
    }

    public enum DetailToolbarMode
    {
        Default,
        CancelSave
    }

    public class ToolbarButtonClickEventArgs : EventArgs
    {
        public ToolbarButtonClickEventArgs(ToolbarButton button)
        {
            ClickedButton = button;
        }

        public ToolbarButton ClickedButton { get; }
    }

    public delegate void ToolbarButtonClickEventHandler(object sender, ToolbarButtonClickEventArgs e);
}
