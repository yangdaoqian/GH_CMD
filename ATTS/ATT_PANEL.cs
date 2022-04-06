namespace ALIEN_DLL.ATTS
{
    using Grasshopper;
    using Grasshopper.Kernel;
    using Grasshopper.Kernel.Attributes;
    using Grasshopper.Kernel.Types;
    using ALIEN_DLL.GEOS;
    using Rhino;
    using Rhino.Display;
    using Rhino.Geometry;
    using Rhino.Geometry.Intersect;
    using Rhino.Input.Custom;
    using System;
    using Rhino.UI;
    using Rhino.UI.Gumball;
    using System.Collections;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using UI.GEOS;
    using Grasshopper.Kernel.Special; 
    using Grasshopper.GUI;
    using Grasshopper.GUI.Base;
    using Grasshopper.GUI.Canvas;
    using Grasshopper.GUI.StringDisplay;
    using Grasshopper.Kernel.Data;
    using Grasshopper.My.Resources;
    using Microsoft.VisualBasic.CompilerServices;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Text;
    using System.Reflection;
    internal class ATT_PANEL :GH_PanelAttributes,I_ATT
    {
        private class PanelEntryItem : GH_FormattedListItem
        {
            private int m_indexWidth;

            private int m_marginLeft;

            private int m_marginRight;

            private int Margins => m_marginLeft + m_marginRight;

            /// <summary>
            /// Creates a new text panel item for a data entry.
            /// </summary>
            public PanelEntryItem(int index, int indexColumnWidth, int marginLeft, int marginRight)
            {
                base.Type = index;
                m_indexWidth = Math.Max(indexColumnWidth, 0);
                m_marginLeft = Math.Max(marginLeft, 0);
                m_marginRight = Math.Max(marginRight, 0);
            }

            public override Size ComputeSize(int layoutWidth)
            {
                layoutWidth -= m_indexWidth + Margins + ScrollBarWidth;
                Size result;
                if (base.Wrap)
                {
                    result = GH_FontServer.MeasureString(base.Text, base.Font, layoutWidth);
                    return result;
                }
                result = new Size(layoutWidth, GH_FontServer.MeasureString(base.Text, base.Font).Height);
                return result;
            }

            public override void RenderItem(Graphics G, Rectangle destination)
            {
                float num = G.Transform.Elements[0];
                if (!(num < 0.5f))
                {
                    int alpha = GH_GraphicsUtil.BlendInteger(0.5, 0.8, 0, base.Colour.A, num);
                    int alpha2 = GH_GraphicsUtil.BlendInteger(0.5, 0.8, 0, 255, num);
                    int alpha3 = GH_GraphicsUtil.BlendInteger(0.5, 0.6, 0, 10, num);
                    Rectangle rectangle = destination;
                    rectangle.X += m_marginLeft;
                    rectangle.Width -= Margins;
                    if (base.Type > 0 && base.Type % 2 == 1)
                    {
                        SolidBrush solidBrush = new SolidBrush(Color.FromArgb(alpha3, Color.Black));
                        G.FillRectangle(solidBrush, destination);
                        solidBrush.Dispose();
                    }
                    if (m_indexWidth > 0)
                    {
                        Rectangle r = rectangle;
                        r.Width = m_indexWidth - 2;
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Far;
                        stringFormat.LineAlignment = StringAlignment.Center;
                        stringFormat.Trimming = StringTrimming.None;
                        SolidBrush solidBrush2 = new SolidBrush(Color.FromArgb(alpha2, base.Colour));
                        G.DrawString(base.Type.ToString(), base.Font, solidBrush2, r, stringFormat);
                        solidBrush2.Dispose();
                        stringFormat.Dispose();
                    }
                    Rectangle r2 = rectangle;
                    r2.Width -= m_indexWidth + ScrollBarWidth;
                    r2.X += m_indexWidth;
                    StringFormat stringFormat2 = new StringFormat();
                    stringFormat2.Alignment = base.Alignment;
                    stringFormat2.LineAlignment = StringAlignment.Center;
                    stringFormat2.Trimming = StringTrimming.EllipsisCharacter;
                    if (!base.Wrap)
                    {
                        stringFormat2.FormatFlags |= StringFormatFlags.NoWrap;
                    }
                    SolidBrush solidBrush3 = new SolidBrush(Color.FromArgb(alpha, base.Colour));
                    G.DrawString(base.Text, base.Font, solidBrush3, r2, stringFormat2);
                    solidBrush3.Dispose();
                    stringFormat2.Dispose();
                }
            }
        }
        private class PanelFillItem : GH_FormattedListItem
        {
            private int m_marginLeft;

            private int m_marginRight;

            private int Margins => m_marginLeft + m_marginRight;

            /// <summary>
            /// Creates a new text panel item for a data entry.
            /// </summary>
            public PanelFillItem(int marginLeft, int marginRight)
            {
                m_marginLeft = Math.Max(marginLeft, 0);
                m_marginRight = Math.Max(marginRight, 0);
            }

            public override Size ComputeSize(int layoutWidth)
            {
                layoutWidth -= Margins + ScrollBarWidth;
                return GH_FontServer.MeasureString(base.Text, base.Font, layoutWidth);
            }

            public override void RenderItem(Graphics G, Rectangle destination)
            {
                float num = G.Transform.Elements[0];
                if (!(num < 0.5f))
                {
                    int alpha = GH_GraphicsUtil.BlendInteger(0.5, 0.8, 0, base.Colour.A, num);
                    GH_GraphicsUtil.BlendInteger(0.5, 0.8, 0, 255, num);
                    Rectangle rectangle = destination;
                    rectangle.X += m_marginLeft;
                    rectangle.Width -= Margins;
                    if (base.Type > 0 && base.Type % 2 == 1)
                    {
                        int alpha2 = GH_GraphicsUtil.BlendInteger(0.5, 0.6, 0, 10, num);
                        SolidBrush solidBrush = new SolidBrush(Color.FromArgb(alpha2, Color.Black));
                        G.FillRectangle(solidBrush, destination);
                        solidBrush.Dispose();
                    }
                    Rectangle r = rectangle;
                    r.Width -= ScrollBarWidth;
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = base.Alignment;
                    stringFormat.LineAlignment = StringAlignment.Center;
                    stringFormat.Trimming = StringTrimming.EllipsisCharacter;
                    SolidBrush solidBrush2 = new SolidBrush(Color.FromArgb(alpha, base.Colour));
                    G.DrawString(base.Text, base.Font, solidBrush2, r, stringFormat);
                    solidBrush2.Dispose();
                    stringFormat.Dispose();
                }
            }
        }
        private class PanelPathItem : GH_FormattedListItem
        {
            private int m_marginLeft;

            private int m_marginRight;

            private int Margins => m_marginLeft + m_marginRight;

            /// <summary>
            /// Creates a new text panel item for a path entry.
            /// </summary>
            public PanelPathItem(int marginLeft, int marginRight)
            {
                m_marginLeft = Math.Max(marginLeft, 0);
                m_marginRight = Math.Max(marginRight, 0);
            }

            public override Size ComputeSize(int layoutWidth)
            {
                return new Size(layoutWidth, GH_FontServer.MeasureString("M", base.Font, 1000f).Height + 6);
            }

            public override void RenderItem(Graphics G, Rectangle destination)
            {
                float num = G.Transform.Elements[0];
                if (!(num < 0.5f))
                {
                    int alpha = GH_GraphicsUtil.BlendInteger(0.5, 0.8, 0, base.Colour.A, num);
                    GH_GraphicsUtil.BlendInteger(0.5, 0.8, 0, 255, num);
                    int alpha2 = GH_GraphicsUtil.BlendInteger(0.5, 0.8, 0, 80, num);
                    int alpha3 = GH_GraphicsUtil.BlendInteger(0.5, 0.8, 0, 40, num);
                    Rectangle rectangle = destination;
                    rectangle.X += m_marginLeft;
                    rectangle.Width -= Margins;
                    Pen pen = new Pen(Color.FromArgb(alpha2, Color.Black));
                    G.DrawLine(pen, destination.X, destination.Y, destination.Right, destination.Y);
                    pen.Dispose();
                    SolidBrush solidBrush = new SolidBrush(Color.FromArgb(alpha3, Color.Black));
                    G.FillRectangle(solidBrush, destination);
                    solidBrush.Dispose();
                    Rectangle r = rectangle;
                    r.Width -= ScrollBarWidth + 2;
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Far;
                    stringFormat.LineAlignment = StringAlignment.Center;
                    stringFormat.Trimming = StringTrimming.EllipsisCharacter;
                    stringFormat.FormatFlags |= StringFormatFlags.NoWrap;
                    solidBrush = new SolidBrush(Color.FromArgb(alpha, base.Colour));
                    G.DrawString(base.Text, base.Font, solidBrush, r, stringFormat);
                    solidBrush.Dispose();
                    stringFormat.Dispose();
                }
            }
        }
        private static readonly int ScrollBarWidth = 8;

        protected bool m_att;
        protected bool m_menu;
        protected bool m_tag;

        private GH_StringList<GH_FormattedListItem> m_stack;

        public bool ATT
        {
            get => m_att;
            set
            {
                m_att = value;
            }
        }
        public bool MENU
        {
            get => m_menu;
            set
            {
                m_menu = value;
            }
        }
        public bool TAG
        {
            get => m_tag;
            set
            {
                m_tag = value;
            }
        }
        public bool SELECTED
        {
            get => this.Selected;
            set
            {
                this.Selected = value;
            }
        }
        public GH_DocumentObject OWNER => this.Owner as GH_DocumentObject;
        public ATT_PANEL(GH_Panel param) : base(param)
        {              

            this.m_att = P_OBJECT<bool, object>.DOC_GETVALUE("GetValue", param as GH_DocumentObject, "ATT", true);
            this.m_menu = P_OBJECT<bool, object>.DOC_GETVALUE("GetValue", param as GH_DocumentObject, "MENU", true);
            this.m_tag = P_OBJECT<bool, object>.DOC_GETVALUE("GetValue", param as GH_DocumentObject, "TAG", true);
        }
        private StringAlignment DetermineAlignment()
        {
            switch (base.Owner.Properties.Alignment)
            {
                case GH_Panel.Alignment.Default:
                    if (base.Owner.SourceCount == 0)
                    {
                        if (base.Owner.Properties.Multiline)
                        {
                            return StringAlignment.Center;
                        }
                        return StringAlignment.Near;
                    }
                    if (base.Owner.VolatileData.IsEmpty)
                    {
                        return StringAlignment.Center;
                    }
                    return StringAlignment.Near;
                case GH_Panel.Alignment.Left:
                    return StringAlignment.Near;
                case GH_Panel.Alignment.Center:
                    return StringAlignment.Center;
                case GH_Panel.Alignment.Right:
                    return StringAlignment.Far;
                default:
                    return StringAlignment.Near;
            }
        }
        private Color CurrentForeColour
        {
            get
            {
                if (Selected)
                {
                    return base.Owner.Properties.InferredForeColourSelected;
                }
                return base.Owner.Properties.InferredForeColour;
            }
        }
        private void SHOW_GUM()
        {           
            Rectangle contentBounds = ContentBounds;
            m_stack = new GH_StringList<GH_FormattedListItem>();
            m_stack.StackAlignment = StringAlignment.Near;
            int marginLeftActual = MarginLeftActual;
            int marginRightActual = MarginRightActual;
            StringAlignment alignment = DetermineAlignment();
            Color currentForeColour = CurrentForeColour;
            if (base.Owner.SourceCount == 0 && base.Owner.Properties.Multiline)
            {
                PanelFillItem panelFillItem = new PanelFillItem(marginLeftActual, marginRightActual);
                panelFillItem.Text = base.Owner.UserText;
                panelFillItem.Font = base.Owner.Properties.Font;
                panelFillItem.Alignment = alignment;
                panelFillItem.Colour = currentForeColour;
                m_stack.AppendItem(panelFillItem);
            }
            else if (base.Owner.VolatileData.IsEmpty)
            {
                PanelFillItem panelFillItem2 = new PanelFillItem(marginLeftActual, marginRightActual);
                panelFillItem2.Text = "No data was collected…";
                panelFillItem2.Font = base.Owner.Properties.Font;
                panelFillItem2.Alignment = alignment;
                panelFillItem2.Colour = Color.FromArgb(100, currentForeColour);
                m_stack.AppendItem(panelFillItem2);
            }
            else
            {
                GH_Structure<GH_String> gH_Structure = (GH_Structure<GH_String>)base.Owner.VolatileData;
                int num = gH_Structure.PathCount - 1;
                for (int i = 0; i <= num; i++)
                {
                    if (base.Owner.Properties.DrawPaths)
                    {
                        PanelPathItem panelPathItem = new PanelPathItem(marginLeftActual, marginRightActual);
                        panelPathItem.Text = gH_Structure.get_Path(i).ToString();
                        panelPathItem.Font = base.Owner.Properties.Font;
                        panelPathItem.Alignment = StringAlignment.Far;
                        Color c= currentForeColour;         
                        panelPathItem.Colour = c;
                        m_stack.AppendItem(panelPathItem);
                    }
                    List<GH_String> list = gH_Structure.Branches[i];
                    if (list == null)
                    {
                        continue;
                    }
                    int num2 = list.Count - 1;
                    for (int j = 0; j <= num2; j++)
                    {
                        string empty = string.Empty;
                        Color color = currentForeColour;
                        if (list[j] == null || list[j].Value == null)
                        {
                            empty = "<null>";
                            color = Color.FromArgb(100, color);
                        }
                        else if (list[j].Value.Length == 0)
                        {
                            empty = "<empty>";
                            color = Color.FromArgb(100, color);
                        }
                        else
                        {
                            empty = list[j].Value;
                            try
                            {
                                StringToColor(list[j].Value, ref color);
                            }
                            catch
                            {
                                color = currentForeColour;
                            }
                            if (color.A == 0)
                                color = Color.FromArgb(255,color.R,color.G,color.B);
                        }
                        PanelEntryItem panelEntryItem = new PanelEntryItem(j, IndexBounds.Width, marginLeftActual, marginRightActual);
                        panelEntryItem.Text = empty;
                        panelEntryItem.Font = base.Owner.Properties.Font;
                        panelEntryItem.Alignment = alignment;
                        panelEntryItem.Colour = color;
                        panelEntryItem.Wrap = base.Owner.Properties.Wrap;
                        m_stack.AppendItem(panelEntryItem);
                    }
                }
            }
            m_stack.BuildCache(contentBounds);
            if (m_stack.Count == 1 && m_stack[0] is PanelFillItem)
            {
                if (m_stack[0].BoundingBox.Height < contentBounds.Height)
                {
                    m_stack[0].BoundingBox = new Rectangle(0, 0, contentBounds.Width, contentBounds.Height);
                }
                else
                {
                    m_stack[0].BoundingBox = m_stack.BoundingBox;
                }
            }
        }
        private static bool StringToColor(string S, ref Color col)
        {
            if (string.IsNullOrEmpty(S))
            {
                return false;
            }
            if (S.StartsWith("#"))
            {
                try
                {
                    col = ColorTranslator.FromHtml(S);
                    return true;
                }
                catch (Exception ex)
                {
                    ProjectData.SetProjectError(ex);
                    Exception ex2 = ex;
                    ProjectData.ClearProjectError();
                }
            }
            S = S.Replace(";", ",");
            S = S.Replace(":", ",");
            S = S.Replace(".", ",");
            S = S.Replace(")", string.Empty);
            S = S.Replace("(", ",");
            if (S.Contains(","))
            {
                string[] array = S.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                List<int> list = new List<int>(4);
                int num = array.Length - 1;
                for (int i = 0; i <= num; i++)
                {
                    int result;
                    if (int.TryParse(array[i], out result))
                    {
                        result = Math.Max(result, 0);
                        result = Math.Min(result, 255);
                        list.Add(result);
                        continue;
                    }
                    return false;
                }
                if (list.Count == 3)
                {
                    col = Color.FromArgb(list[0], list[1], list[2]);
                    return true;
                }
                if (list.Count == 4)
                {
                    col = Color.FromArgb(list[3], list[0], list[1], list[2]);
                    return true;
                }
                return false;
            }
            Color color = Color.FromName(S);
            if (color.IsNamedColor || color.IsKnownColor || color.IsSystemColor)
            {
                col = color;
                return true;
            }
            return false;
        }
        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {
            SHOW_GUM();
            Type type = this.GetType().BaseType;
            FieldInfo targetMethod = type.GetField("m_stack", BindingFlags.NonPublic | BindingFlags.Instance);
            targetMethod.SetValue(this, m_stack);
            base.Render(canvas, graphics, channel);
        }
    }
}

