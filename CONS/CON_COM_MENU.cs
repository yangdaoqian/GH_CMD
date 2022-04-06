namespace UI.CONS
{
    using Grasshopper.GUI;
    using System;
    using System.Collections;
    using System.Windows.Forms;
    using Grasshopper.GUI.Canvas;
    internal class CON_COM_MENU : ContextMenuStrip
    {
        protected override bool ProcessCmdKey(ref Message m, Keys keyData) => 
            (((keyData == Keys.Enter) && this.RespondToEnter()) || base.ProcessCmdKey(ref m, keyData));

        private bool RespondToEnter()
        {
            IEnumerator enumerator = this.Items.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    ToolStripItem current = (ToolStripItem) enumerator.Current;
                    if (this.RespondToEnter(current))
                    {
                        return true;
                    }
                }
            }
            finally
            {
                if (enumerator is IDisposable)
                {
                    (enumerator as IDisposable).Dispose();
                }
            }
            return false;
        }

        private bool RespondToEnter(ToolStripItem item)
        {
            if (item is IGH_ToolstripItemKeyHandler)
            {
                switch (((IGH_ToolstripItemKeyHandler) item).RespondToEnter())
                {
                    case GH_ToolstripItemKeyHandlerResult.Ignored:
                        return false;

                    case GH_ToolstripItemKeyHandlerResult.CloseMenu:
                        return false;

                    case GH_ToolstripItemKeyHandlerResult.MaintainMenu:
                        return true;
                }
            }
            ToolStripControlHost host = item as ToolStripControlHost;
            if ((host != null) && (host.Control is IGH_ToolstripItemKeyHandler))
            {
                switch (((IGH_ToolstripItemKeyHandler) host.Control).RespondToEnter())
                {
                    case GH_ToolstripItemKeyHandlerResult.Ignored:
                        return false;

                    case GH_ToolstripItemKeyHandlerResult.CloseMenu:
                        return false;

                    case GH_ToolstripItemKeyHandlerResult.MaintainMenu:
                        return true;
                }
            }
            ToolStripDropDownItem item2 = item as ToolStripDropDownItem;
            if (((item2 != null) && (item2.DropDown != null)) && item2.DropDown.Visible)
            {
                IEnumerator enumerator= item2.DropDownItems.GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        ToolStripItem current = (ToolStripItem) enumerator.Current;
                        if (this.RespondToEnter(current))
                        {
                            return true;
                        }
                    }
                }
                finally
                {
                    if (enumerator is IDisposable)
                    {
                        (enumerator as IDisposable).Dispose();
                    }
                }
            }
            return false;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CONTROL_MENU
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.DropShadowEnabled = false;
            this.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table;
            this.ShowImageMargin = false;
            this.Size = new System.Drawing.Size(36, 4);
            this.ResumeLayout(false);

        }
    }
}

