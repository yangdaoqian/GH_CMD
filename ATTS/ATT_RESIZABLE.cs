using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Attributes;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Grasshopper;
using System;
using ALIEN_DLL.GEOS;
using UI.GEOS;
namespace UI.ATTS
{
    public class ATT_RESIZABLE : GH_ResizableAttributes<GH_Component>
    {
        protected bool m_p_tag;
        protected bool m_p_gum;
        protected bool m_p_menu;
        protected bool m_p_att;
        private int _width;
        private int _height;
        private int _left;
        private int _top;
        private int _right;
        private int _bottom;
        public string ComponentText = "";
        public Color BGColor = Color.White;
        protected RectangleF m_innerBounds;
        protected override Size MinimumSize
        {
            get
            {
                Size result = new Size(this._width, this._height);
                return result;
            }
        }

        protected override Padding SizingBorders
        {
            get
            {
                Padding result = new Padding(this._left, this._top, this._right, this._bottom);
                return result;
            }
        }
        public override string PathName
        {
            get
            {
                return string.Format("{0} ({1})", this.Owner.NickName, this.Owner.Name);
            }
        }

        public ATT_RESIZABLE(GH_Component owner, int width, int height, int left, int top, int right, int bottom)
            : base(owner)
        {
            this.m_p_att = P_OBJECT<bool, object>.DOC_GETVALUE("GetValue", owner as GH_DocumentObject, "P_ATT", true);
            this.m_p_menu = P_OBJECT<bool, object>.DOC_GETVALUE("GetValue", owner as GH_DocumentObject, "P_MENU", true);
            this.m_p_tag = P_OBJECT<bool, object>.DOC_GETVALUE("GetValue", owner as GH_DocumentObject, "P_TAG", true);
            this._left = left;
            this._top = top;
            this._right = right;
            this._bottom = bottom;
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
            this._width = width + (int)fix_in_x + (int)fix_out_x + 24;
            this._height = height;


        }
        public override void AppendToAttributeTree(List<IGH_Attributes> attributes)
        {
            attributes.Add(this);
            try
            {
                IEnumerator<IGH_Param> enumerator = this.Owner.Params.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    IGH_Param param = enumerator.Current;
                    param.Attributes.AppendToAttributeTree(attributes);
                }
            }
            finally
            {
                IEnumerator<IGH_Param> enumerator = this.Owner.Params.GetEnumerator();
                if (enumerator != null)
                {
                    enumerator.Dispose();
                }
            }
        }

        protected override void Layout()
        {
            Pivot = GH_Convert.ToPoint(Pivot);
            Bounds = new RectangleF(Pivot.X, Pivot.Y, Bounds.Width, Bounds.Height);
            RectangleF bounds = new RectangleF(this.Pivot.X, this.Pivot.Y, this.Bounds.Width, this.Bounds.Height);
            bounds.Inflate(-2f, -2f);
            this.m_innerBounds = bounds;
            System.Drawing.RectangleF bounds2 = new RectangleF(this.Pivot.X, this.Pivot.Y, this.Bounds.Width, this.Bounds.Height);
            bounds2.Inflate(-2f, -2f);
            GH_ComponentAttributes.LayoutInputParams(this.Owner, bounds2);
            float width_in = this.Owner.Params.Input.Count > 0 ? this.Owner.Params.Input[0].Attributes.Bounds.Width : 0;
            bounds2.X += (width_in + 3f);
            GH_ComponentAttributes.LayoutInputParams(this.Owner, bounds2);
            System.Drawing.RectangleF bounds3 = new RectangleF(this.Pivot.X, this.Pivot.Y, this.Bounds.Width, this.Bounds.Height);
            bounds3.Inflate(-2f, -2f);
            GH_ComponentAttributes.LayoutOutputParams(this.Owner, bounds3);
            float width_out = this.Owner.Params.Output.Count > 0 ? this.Owner.Params.Output[0].Attributes.Bounds.Width : 0;
            bounds3.X += -width_out - 3f;
            GH_ComponentAttributes.LayoutOutputParams(this.Owner, bounds3);
        }
        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {
            GH_CanvasChannel channel2 = channel;
            if (channel2 == GH_CanvasChannel.Wires)
            {
                base.Render(canvas, graphics, channel);
            }
            else
            {
                GH_PaletteStyle impliedStyle = GH_CapsuleRenderEngine.GetImpliedStyle(GH_Palette.Black, Selected, base.Owner.Locked, base.Owner.Hidden);
                GH_ComponentAttributes.RenderComponentParameters(canvas, graphics, base.Owner, impliedStyle);
                RectangleF bounds = this.Bounds;
                if (canvas.Viewport.IsVisible(ref bounds, 10f))
                {
                    ATT_NORMAL.RENDER(this, this.m_innerBounds, canvas, graphics, true, true, false, true, true, UI_SETTING.INS.ATT ? m_p_att : false, UI_SETTING.INS.TAG ? m_p_tag : false);
                }
            }
        }
    }
}
