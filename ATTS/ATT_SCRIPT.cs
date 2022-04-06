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
using Grasshopper.Kernel.Attributes;

namespace UI.ATTS
{
    internal class ATT_SCRIPT : ATT_NORMAL
    {
        internal ATT_SCRIPT(IGH_Component owner)
          : base(owner)
        {
        }
        public override GH_ObjectResponse RespondToMouseDoubleClick(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
                try
                {
                    Type componentType = this.Owner.GetType();
                    MethodInfo method = componentType.GetMethod("ShowScriptEditor");
                    object[] parameters = new object[] { };
                    method.Invoke(this.Owner, parameters);
                    return GH_ObjectResponse.Handled;
                }
                catch (TargetInvocationException targetEx)
                {
                    this.Owner.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, targetEx.InnerException.ToString());
                }
            return base.RespondToMouseDoubleClick(sender, e);
        }
        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {       
            base.Render(canvas, graphics, channel);
            if (channel == GH_CanvasChannel.Objects)
                ATT_NORMAL.RENDER_TOOLTIP(graphics);
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
