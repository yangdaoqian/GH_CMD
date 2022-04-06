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
    internal class ATT_GRAPH : GH_GraphMapperAttributes
    {

        private IGH_Graph graph => this.Owner.Graph;
        internal ATT_GRAPH(GH_GraphMapper owner)
          : base(owner)
        {
        }
        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {
            base.Render(canvas, graphics, channel);
            if(channel==GH_CanvasChannel.Objects)
              ATT_NORMAL.RENDER_TOOLTIP(graphics);
        }
        public override GH_ObjectResponse RespondToMouseUp(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
         
            if (e.Button != MouseButtons.Right)
                return base.RespondToMouseUp(sender, e);
            ATT_MENUSTRIP menu = new ATT_MENUSTRIP(this.DocObject as IGH_Component);
            this.DocObject.AppendMenuItems(menu);
            menu.BackColor = Color.DarkGray;

            //ICON_GRAPH iPC_GRAPH = null;
            //if (graph != null)
            //    iPC_GRAPH = graph as ICON_GRAPH;
            //if (iPC_GRAPH != null)
            //{
            //    menu.Items.Add(new ToolStripSeparator());
            //    iPC_GRAPH.AppendMenuItems(menu);
            //}
            if ((bool)UI_SETTING.INS["MENU"])
            ATT_NORMAL.CHANGE_MODE(menu);
            if (menu.Items.Count > 0)
            {
                sender.ActiveInteraction = null;
                Point location = Instances.DocumentEditor.PointToClient(Cursor.Position);
                menu.Show(Instances.DocumentEditor, location);
            }
            return GH_ObjectResponse.Handled;
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
