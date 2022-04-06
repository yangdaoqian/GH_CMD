using GH_IO.Serialization;
using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Attributes;
using Rhino;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using PANDA.CONS.ATT;
using System.IO;
using PANDA.COMS;
using PANDA.CONS;
namespace PANDA.ATTS
{
    public class ATT_WIDGET : ATT_RESIZABLE
    {
        public int offset;
        public int currentOffset = 15;
        private float _minWidth;
        //private CON_WIDGET _activeToolTip;
        public  CON_MENU collection;
        public float MinWidth
        {
            get
            {
                return this._minWidth;
            }
            set
            {
                this._minWidth = value;
            }
        }

        public ATT_WIDGET(P_COM_NORMAL owner, int width, int height, int left, int top, int right, int bottom) : base(owner, width, height, left, top, right, bottom)
        {
        }
        public virtual void SET_MENU(CON_MENU _menu)
        {
            this.collection=_menu;
        }
        public override bool Write(GH_IWriter writer)
        {
            try
            {
                this.collection.WRITE(writer);
            }
            catch (System.Exception ex)
            {
                RhinoApp.WriteLine("FAIL Write: " + ex.Message + " " + ex.StackTrace);
            }
            return base.Write(writer);
        }
        public override bool Read(GH_IReader reader)
        {
            try
            {
                this.collection.READ(reader);
            }
            catch (System.Exception ex)
            {
                RhinoApp.WriteLine("FAIL Read " + ex.Message + " " + ex.StackTrace);
            }
            return base.Read(reader);
        }
        protected override void PrepareForRender(GH_Canvas canvas)
        {
            base.PrepareForRender(canvas);
            this.LayoutStyle();
        }
        protected override void Layout()
        {
            this.Pivot = GH_Convert.ToPoint(this.Pivot);
            base.Layout();
            this.FixLayout();
            this.LayoutMenu();
        }
        protected void FixLayout()
        {
            float width = this.Bounds.Width;
            if (this._minWidth > width)
            {
                this.Bounds = new System.Drawing.RectangleF(this.Bounds.X, this.Bounds.Y, this._minWidth, this.Bounds.Height);
            }
            float num = this.Bounds.Width - width;

            foreach (IGH_Param current in base.Owner.Params.Output)
            {
                System.Drawing.PointF pivot = current.Attributes.Pivot;
                System.Drawing.RectangleF bounds = current.Attributes.Bounds;
                current.Attributes.Pivot = new System.Drawing.PointF(pivot.X + num, pivot.Y);
                current.Attributes.Bounds = new System.Drawing.RectangleF(bounds.Location.X + num, bounds.Location.Y, bounds.Width, bounds.Height);
            }

        }
        private void LayoutStyle()
        {
            this.collection.PALETTE = GH_CapsuleRenderEngine.GetImpliedPalette(base.Owner);
            this.collection.PALETTE_STYLE = GH_CapsuleRenderEngine.GetImpliedStyle(this.collection.PALETTE, this.Selected, base.Owner.Locked, base.Owner.Hidden);
            this.collection.LAYOUT();
        }
        protected void LayoutMenu()
        {
            GraphicsUnit pixel = GraphicsUnit.Pixel;
            System.Drawing.RectangleF bounds = this.Owner.Icon_24x24.GetBounds(ref pixel);
            int paramin_count = this.Owner.Params.Input.Count;
            int paramout_count = this.Owner.Params.Output.Count;
            float fix_in_x = 0;
            float fix_out_x = 0;
            if (paramin_count > 0)
            {
                fix_in_x = this.Owner.Params.Input[0].Attributes.Bounds.Width;
            }
            if (paramout_count > 0)
            {
                fix_out_x = this.Owner.Params.Output[0].Attributes.Bounds.Width;
            }
            this.collection.PIVOT = new PointF(this.Bounds.X + fix_in_x + 2f, this.Bounds.Y);
            this.collection.WIDTH = this.Bounds.Width - fix_in_x - fix_out_x - 28f;
            this.collection.HEIGHT = this.Bounds.Height;
            this.LayoutStyle();
        }
        protected override void Render(GH_Canvas iCanvas, System.Drawing.Graphics graph, GH_CanvasChannel iChannel)
        {
            base.Render(iCanvas, graph, iChannel);
            if (iChannel == GH_CanvasChannel.Objects)
            {
                this.collection.RENDER(iCanvas, iChannel);
            }
        }
        public override GH_ObjectResponse RespondToMouseUp(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            GH_ObjectResponse gH_ObjectResponse = this.collection.RESPOND_MOUSE_UP(sender, e);
            if (gH_ObjectResponse == GH_ObjectResponse.Capture)
            {
                this.ExpireLayout();
                sender.Invalidate();
                return gH_ObjectResponse;
            }
            if (gH_ObjectResponse != GH_ObjectResponse.Ignore)
            {
                this.ExpireLayout();
                sender.Invalidate();
                return GH_ObjectResponse.Release;
            }
            return base.RespondToMouseUp(sender, e);
        }
        public override GH_ObjectResponse RespondToMouseDoubleClick(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            GH_ObjectResponse gH_ObjectResponse = this.collection.RESPOND_MOUSE_DOUBLE_CLICK(sender, e);
            if (gH_ObjectResponse != GH_ObjectResponse.Ignore)
            {
                this.ExpireLayout();
                sender.Refresh();
                return gH_ObjectResponse;
            }
            return base.RespondToMouseDoubleClick(sender, e);
        }
        public override GH_ObjectResponse RespondToKeyDown(GH_Canvas sender, KeyEventArgs e)
        {
            GH_ObjectResponse gH_ObjectResponse = this.collection.RESPOND_KEY_DOWN(sender, e);
            if (gH_ObjectResponse != GH_ObjectResponse.Ignore)
            {
                this.ExpireLayout();
                sender.Refresh();
                return gH_ObjectResponse;
            }
            return base.RespondToKeyDown(sender, e);
        }
        public override GH_ObjectResponse RespondToMouseMove(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            GH_ObjectResponse gH_ObjectResponse = this.collection.RESPOND_MOUSE_MOVE(sender, e);
            if (gH_ObjectResponse != GH_ObjectResponse.Ignore)
            {
                this.ExpireLayout();
                sender.Refresh();
                return gH_ObjectResponse;
            }
            return base.RespondToMouseMove(sender, e);
        }
        public override GH_ObjectResponse RespondToMouseDown(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            GH_ObjectResponse gH_ObjectResponse = this.collection.RESPOND_MOUSE_DOWN(sender, e);
            if (gH_ObjectResponse != GH_ObjectResponse.Ignore)
            {
                this.ExpireLayout();
                sender.Refresh();
                return gH_ObjectResponse;
            }
            return base.RespondToMouseDown(sender, e);
        }
        public override bool IsTooltipRegion(System.Drawing.PointF pt)
        {
            this._activeToolTip = null;
            bool flag = base.IsTooltipRegion(pt);
            if (flag)
            {
                return flag;
            }
            bool flag2 = this.Bounds.Contains(pt);
            if (flag2)
            {
                CON_WIDGET gH_Attr_Widget = this.collection.IsTtipPoint(pt);
                if (gH_Attr_Widget != null)
                {
                    this._activeToolTip = gH_Attr_Widget;
                    return true;
                }
            }
            return false;
        }
        public bool getActiveTooltip(System.Drawing.PointF pt)
        {
            CON_WIDGET gH_Attr_Widget = this.collection.IsTtipPoint(pt);
            if (gH_Attr_Widget != null)
            {
                this._activeToolTip = gH_Attr_Widget;
                return true;
            }
            return false;
        }
        public override void SetupTooltip(System.Drawing.PointF canvasPoint, GH_TooltipDisplayEventArgs e)
        {
            this.getActiveTooltip(canvasPoint);
            if (this.collection.IS_TOOL_TIP(canvasPoint))
            {
                this.TOOL_TIP_SET_UP(canvasPoint, e);
                return;
            }
            e.Title = this.PathName;
            e.Text = base.Owner.Description;
            e.Description = base.Owner.InstanceDescription;
            e.Icon = base.Owner.Icon_24x24;
            if (base.Owner is IGH_Param)
            {
                IGH_Param iGH_Param = (IGH_Param)base.Owner;
                string text = iGH_Param.TypeName;
                if (iGH_Param.Access == GH_ParamAccess.list)
                {
                    text += "[…]";
                }
                if (iGH_Param.Access == GH_ParamAccess.tree)
                {
                    text += "{…;…;…}";
                }
                e.Title = string.Format("{0} ({1})", this.PathName, text);
            }
        }
    }
}
