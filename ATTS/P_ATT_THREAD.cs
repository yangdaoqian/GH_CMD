using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel;
using System.Drawing;
using Grasshopper.Kernel.Attributes;
using PANDA.COMS;
using System.Windows.Forms;
namespace PANDA.ATTS
{
    class P_ATT_THREAD : GH_ComponentAttributes
    {
        public P_ATT_THREAD(SET_THREADC com) : base(com)
        {
        }
        public override Grasshopper.GUI.Canvas.GH_ObjectResponse RespondToMouseDoubleClick(Grasshopper.GUI.Canvas.GH_Canvas sender, Grasshopper.GUI.GH_CanvasMouseEvent e)
        {
            SET_THREADC com = this.Owner as SET_THREADC;         
            if (com.COM == null)
            {
                IGH_Component m_com = this.Owner.OnPingDocument().FindComponent(new System.Drawing.Point((int)(this.Bounds.X + this.Bounds.Width / 2), (int)(this.Bounds.Y - this.Bounds.Height - 12)));
                if (m_com != null)
                {
                    com.COM = m_com;                
                    com.COVER();
                }
            }
            else
            {
                com.COM.Attributes?.RespondToMouseDoubleClick(sender,e);
                com.ExpireSolution(true);
            }
            return base.RespondToMouseDoubleClick(sender, e);
        }
        protected override void Render(Grasshopper.GUI.Canvas.GH_Canvas canvas, Graphics graphics, Grasshopper.GUI.Canvas.GH_CanvasChannel channel)
        {
            base.Render(canvas, graphics, channel);

            if (channel == Grasshopper.GUI.Canvas.GH_CanvasChannel.Objects)
            {
                bool m_render_rec = this.Owner.Params.Input.Count == 0 && this.Owner.Params.Output.Count == 0;
                if (m_render_rec)
                {
                    string str = "Put Component here";
                    if (this.Owner.Message != "Double Click")
                        this.Owner.Message = "Double Click";
                    Font f = new System.Drawing.Font("Arial", 6f, FontStyle.Regular);
                    Size size = GH_FontServer.MeasureString(str, f);
                    Rectangle rec = new Rectangle((int)(this.Bounds.X + this.Bounds.Width / 2 - size.Width / 2), (int)(this.Bounds.Y - this.Bounds.Height - 12), size.Width, (int)(this.Bounds.Height));
                    graphics.DrawRectangle(new Pen(Color.Red), rec);
                    graphics.DrawString(str, f, new SolidBrush(Color.Red), this.Bounds.X + this.Bounds.Width / 2 - size.Width / 2, this.Bounds.Y - this.Bounds.Height / 2 - 12 - size.Height / 2);
                }
                else
                {
                    graphics.DrawEllipse(new Pen(Color.Red), this.Bounds.X+2, this.Bounds.Y+2,2,2);
                    graphics.DrawEllipse(new Pen(Color.Red), this.Bounds.X+6, this.Bounds.Y+2,2,2);
                }
            }
        }

    }
}
