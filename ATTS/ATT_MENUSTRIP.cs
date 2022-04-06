using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper;
using Grasshopper.Kernel;
using System.IO;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using Grasshopper.GUI.Canvas;
using System.Drawing;
using ALIEN_DLL.GEOS;
using UI.GEOS;
using Grasshopper.GUI;
using Grasshopper.GUI.Canvas.Interaction;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;

using Grasshopper.Kernel.Special;

using Rhino.DocObjects;
using Grasshopper.Kernel.Graphs;

namespace UI.ATTS
{
    internal class ATT_MENUSTRIP : ContextMenuStrip
    {
        public int Message_Count
        {
            get
            {
                int ec = com.RuntimeMessages(GH_RuntimeMessageLevel.Error).Count;
                int wc = com.RuntimeMessages(GH_RuntimeMessageLevel.Warning).Count;
                int rc = com.RuntimeMessages(GH_RuntimeMessageLevel.Remark).Count;
                int tc = (ec > 0 ? 1 : 0) + (wc > 0 ? 1 : 0) + (rc > 0 ? 1 : 0);
                return tc == 0 ? 0 : tc + 1;
            }
        }
        private IGH_Component com;
        public ATT_MENUSTRIP(IGH_Component owner)
        {
            this.com = owner;
        }
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message m, System.Windows.Forms.Keys keyData)
        {
            return (keyData == System.Windows.Forms.Keys.Return && this.RespondToEnter()) || base.ProcessCmdKey(ref m, keyData);
        }
        private bool RespondToEnter()
        {
            try
            {
                System.Collections.IEnumerator enumerator = this.Items.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    System.Windows.Forms.ToolStripItem item = (System.Windows.Forms.ToolStripItem)enumerator.Current;
                    if (this.RespondToEnter(item))
                    {
                        return true;
                    }
                }
            }
            finally
            {
            }
            return false;
        }
        private bool RespondToEnter(System.Windows.Forms.ToolStripItem item)
        {
            if (item is Grasshopper.GUI.IGH_ToolstripItemKeyHandler)
            {
                switch (((Grasshopper.GUI.IGH_ToolstripItemKeyHandler)item).RespondToEnter())
                {
                    case Grasshopper.GUI.GH_ToolstripItemKeyHandlerResult.Ignored:
                        return false;
                    case Grasshopper.GUI.GH_ToolstripItemKeyHandlerResult.CloseMenu:
                        return false;
                    case Grasshopper.GUI.GH_ToolstripItemKeyHandlerResult.MaintainMenu:
                        return true;
                }
            }
            System.Windows.Forms.ToolStripControlHost hostItem = item as System.Windows.Forms.ToolStripControlHost;
            if (hostItem != null && hostItem.Control is Grasshopper.GUI.IGH_ToolstripItemKeyHandler)
            {
                switch (((Grasshopper.GUI.IGH_ToolstripItemKeyHandler)hostItem.Control).RespondToEnter())
                {
                    case Grasshopper.GUI.GH_ToolstripItemKeyHandlerResult.Ignored:
                        return false;
                    case Grasshopper.GUI.GH_ToolstripItemKeyHandlerResult.CloseMenu:
                        return false;
                    case Grasshopper.GUI.GH_ToolstripItemKeyHandlerResult.MaintainMenu:
                        return true;
                }
            }
            System.Windows.Forms.ToolStripDropDownItem dropItem = item as System.Windows.Forms.ToolStripDropDownItem;
            if (dropItem != null && dropItem.DropDown != null && dropItem.DropDown.Visible)
            {
                try
                {
                    System.Collections.IEnumerator enumerator = dropItem.DropDownItems.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        System.Windows.Forms.ToolStripItem subitem = (System.Windows.Forms.ToolStripItem)enumerator.Current;
                        if (this.RespondToEnter(subitem))
                        {
                            return true;
                        }
                    }
                }
                finally
                {
                }
                return false;
            }
            return false;
        }
    }
}
