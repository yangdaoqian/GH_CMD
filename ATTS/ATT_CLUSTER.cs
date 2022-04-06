using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper;
using Grasshopper.Kernel;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Grasshopper.GUI.Canvas;
using System.Drawing;
using ALIEN_DLL.GEOS;

using Grasshopper.GUI;
using Grasshopper.GUI.Canvas.Interaction;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Special;
using UI.GEOS;
using Rhino.DocObjects;
using Grasshopper.Kernel.Graphs;

namespace UI.ATTS
{
    internal class ATT_CLUSTER : GH_ClusterAttributes,I_ATT
    {
        public GH_DocumentObject OWNER => this.Owner as GH_DocumentObject;
        protected bool m_p_att;
        protected bool m_p_menu;
        protected bool m_p_tag;
        protected bool m_p_gum;
        public bool ATT
        {
            get => m_p_att;
            set
            {
                m_p_att = value;
            }
        }
        public bool MENU
        {
            get => m_p_menu;
            set
            {
                m_p_menu = value;
            }
        }
        public bool TAG
        {
            get => m_p_tag;
            set
            {
                m_p_tag = value;
            }
        }
        public bool GUM
        {
            get => m_p_gum;
            set
            {
                m_p_gum = value;
            }
        }
 
        internal ATT_CLUSTER(GH_Cluster component)
          : base(component)
        {
            this.m_p_att = P_OBJECT<bool, object>.DOC_GETVALUE("GetValue", component as GH_DocumentObject, "P_ATT", false);
            this.m_p_menu = P_OBJECT<bool, object>.DOC_GETVALUE("GetValue", component as GH_DocumentObject, "P_MENU", false);
            this.m_p_tag = P_OBJECT<bool, object>.DOC_GETVALUE("GetValue", component as GH_DocumentObject, "P_DISTAG", false);
        }

        public override GH_ObjectResponse RespondToMouseUp(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            if (e.Button != MouseButtons.Right)
                return base.RespondToMouseUp(sender, e);
            ATT_MENUSTRIP menu = new ATT_MENUSTRIP(this.DocObject as IGH_Component);
            menu.BackColor = Color.DarkGray;
            base.DocObject.AppendMenuItems(menu);
            if ((bool)UI_SETTING.INS["MENU"] ? m_p_menu : false)
                ATT_CLASS.ADD_CONMENU(menu, this);
          ATT_NORMAL.CHANGE_MODE(menu);
            if (menu.Items.Count > 0)
            {
                sender.ActiveInteraction = null;
                Point location = Instances.DocumentEditor.PointToClient(Cursor.Position);
                menu.Show(Instances.DocumentEditor, location); //new System.Drawing.Point((int) e..X, (int) e.CanvasLocation.Y));
            }
            return GH_ObjectResponse.Handled;
        }
        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {
                if (channel == GH_CanvasChannel.Objects)
                {
                ATT_NORMAL.RENDER(this, this.m_innerBounds, canvas, graphics, true, true, true, true, true,this.m_p_att || (bool)UI_SETTING.INS["ATT"], m_p_tag || (bool)UI_SETTING.INS["TAG"]);
                ATT_NORMAL.RENDER_TOOLTIP(graphics);
                return;
                }
          
            base.Render(canvas, graphics, channel);
            if (channel == GH_CanvasChannel.Objects)
            {
                ATT_NORMAL.RENDER_TOOLTIP(graphics);
            }
        }
        public override void SetupTooltip(PointF point, Grasshopper.GUI.GH_TooltipDisplayEventArgs e)
        {
            base.SetupTooltip(point, e);

            if (this.Owner is Param_Colour)
                e.Text += "\nLeft click to set colors";
            if (this.Owner is Param_Boolean)
                e.Text += "\nDouble click to invert the values";
            e.Text += "\nPanda_UI";
            try
            {
             ATT_NORMAL.CHANGE_TOOLTIP();
            }
            catch
            {

            }
        }
    }

}
