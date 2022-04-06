namespace UI.ATTS
{
    using Grasshopper;
    using Grasshopper.GUI;
    using Grasshopper.GUI.Canvas;
    using Grasshopper.Kernel;
    using Grasshopper.Kernel.Attributes;
    using Grasshopper.Kernel.Types;
    using ALIEN_DLL.GEOS;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Reflection;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using UI.GEOS;
    public  class ATT_NORMAL : GH_ComponentAttributes, I_ATT
    {
        protected bool m_drawZui;
        protected bool m_p_tag;
        protected bool m_p_menu;
        protected bool m_p_att;
        public ATT_NORMAL(IGH_Component component) : base(component)
        {
            this.m_drawZui = true;
            this.m_p_att =P_OBJECT<bool, object>.DOC_GETVALUE("GetValue", component as GH_DocumentObject, "P_ATT",true);
            this.m_p_menu= P_OBJECT<bool, object>.DOC_GETVALUE("GetValue", component as GH_DocumentObject, "P_MENU", true);
            this.m_p_tag = P_OBJECT<bool, object>.DOC_GETVALUE("GetValue", component as GH_DocumentObject, "P_TAG", true);         
        }
        internal static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180f, 90f);
            path.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - (cornerRadius * 2), rect.Y);
            path.AddArc((rect.X + rect.Width) - (cornerRadius * 2), rect.Y, cornerRadius * 2, cornerRadius * 2, 270f, 90f);
            path.AddLine(rect.Right, rect.Y + (cornerRadius * 2), rect.Right, (rect.Y + rect.Height) - (cornerRadius * 2));
            path.AddArc((int) ((rect.X + rect.Width) - (cornerRadius * 2)), (int) ((rect.Y + rect.Height) - (cornerRadius * 2)), (int) (cornerRadius * 2), (int) (cornerRadius * 2), 0f, 90f);
            path.AddLine(rect.Right - (cornerRadius * 2), rect.Bottom, rect.X + (cornerRadius * 2), rect.Bottom);
            path.AddArc(rect.X, rect.Bottom - (cornerRadius * 2), cornerRadius * 2, cornerRadius * 2, 90f, 90f);
            path.AddLine(rect.X, rect.Bottom - (cornerRadius * 2), rect.X, rect.Y + (cornerRadius * 2));
            path.CloseFigure();
            return path;
        }
        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {
            GH_CanvasChannel channel2 = channel;
            if (channel2 != GH_CanvasChannel.Wires)
            {
                if (channel2 != GH_CanvasChannel.Objects)
                {
                    return;
                }
            }
            else
            {
                using (List<IGH_Param>.Enumerator enumerator = base.Owner.Params.Input.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        enumerator.Current.Attributes.RenderToCanvas(canvas, GH_CanvasChannel.Wires);
                    }
                    return;
                }
            }
            RENDER(this, base.m_innerBounds, canvas, graphics, true, true, true, true, true, ATT, TAG);
           
            if ((base.Owner is IGH_VariableParameterComponent) && this.m_drawZui)
            {
                base.RenderVariableParameterUI(canvas, graphics);
            }
            if(channel==GH_CanvasChannel.Objects)
                RENDER_TOOLTIP(graphics);
        }
        internal static void RENDER(IGH_Attributes att, RectangleF innerBounds, GH_Canvas canvas, Graphics graphics, bool drawComponentBaseBox, bool drawComponentNameBox, bool drawJaggedEdges, bool drawParameterGrips, bool drawParameterNames, bool pui,bool dis)
        {
            RectangleF bounds = att.Bounds;
            RectangleF box = innerBounds;
            att.Bounds = bounds;
            IGH_Component docObject = att.DocObject as IGH_Component;
      
            if (canvas.Viewport.IsVisible(ref bounds, 10f))
            {
                GH_Palette impliedPalette = GH_CapsuleRenderEngine.GetImpliedPalette(docObject);
                if ((impliedPalette == GH_Palette.Normal) && !docObject.IsPreviewCapable)
                {
                    impliedPalette = GH_Palette.Hidden;
                }
                GH_Capsule capsule = GH_Capsule.CreateCapsule(att.Bounds, impliedPalette);
                bool flag2 = docObject.Params.Input.Count == 0;
                bool flag3 = docObject.Params.Output.Count == 0;
                GH_PaletteStyle style = GH_CapsuleRenderEngine.GetImpliedStyle(impliedPalette, att.Selected, docObject.Locked, docObject.Hidden);
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                canvas.SetSmartTextRenderingHint();
                Color red = Color.Red;
                if (GH_Attributes<IGH_Component>.IsIconMode(docObject.IconDisplayMode))
                {
                    if (drawComponentBaseBox)
                    {
                        if (pui)
                        {
                            RENDER_OUTLINES(att, graphics,dis);
                        }
                        else
                        {
                            RENDER_CAPSULE(att, graphics);
                        }
                    }
                    if (drawComponentNameBox)
                    {
                        if (docObject.Icon_24x24 == null)
                        {
                            capsule.RenderEngine.RenderIcon(graphics, null, box, 0, 0);
                        }
                        else if (docObject.Locked)
                        {
                            capsule.RenderEngine.RenderIcon(graphics, docObject.Icon_24x24_Locked, box, 0, 0);
                        }
                        else
                        {
                            capsule.RenderEngine.RenderIcon(graphics, docObject.Icon_24x24, box, 0, 0);
                        }
                    }
                }
                else
                {
                    if (drawComponentBaseBox)
                    {
                        if (pui)
                        {
                            RENDER_OUTLINES(att, graphics,dis);
                        }
                        else
                        {
                            RENDER_CAPSULE(att, graphics);
                        }
                    }
                    if (drawComponentNameBox)
                    {
#if rh6
                        GH_Capsule capsule2 = GH_Capsule.CreateTextCapsule(box, box, GH_Palette.Black, docObject.NickName, GH_FontServer.LargeAdjusted, GH_Orientation.vertical_center, 3, 6);
#else
                        GH_Capsule capsule2 = GH_Capsule.CreateTextCapsule(box, box, GH_Palette.Black, docObject.NickName, GH_FontServer.Large, GH_Orientation.vertical_center, 3, 6);
#endif
                       
                        capsule2.RenderEngine.RenderText(graphics, red);
                        capsule2.Dispose();
                    }
                }
                if (drawComponentBaseBox)
                {
#if rh6
                    IGH_TaskCapableComponent component2 = docObject as IGH_TaskCapableComponent;
                    if (component2 != null)
                    {
                        if (component2.UseTasks)
                        {
                            capsule.RenderEngine.RenderBoundaryDots(graphics, 2, style);
                        }
                        else
                        {
                            capsule.RenderEngine.RenderBoundaryDots(graphics, 1, style);
                        }
                    }
#endif
                }
                if ((drawComponentNameBox && docObject.Obsolete) && (CentralSettings.CanvasObsoleteTags && (canvas.DrawingMode == GH_CanvasMode.Control)))
                {
                    GH_GraphicsUtil.RenderObjectOverlay(graphics, docObject, box);
                }
                if (drawParameterNames)
                {
                    RENDER_PARAMS(att, canvas, graphics, pui);
                }
                capsule.Dispose();
            }
        }
        internal static void RENDER_CAPSULE(IGH_Attributes att, Graphics graphics)
        {
            IGH_Component docObject = att.DocObject as IGH_Component;
            if (docObject != null)
            {
                PointF inputGrip;
                GH_Palette impliedPalette = GH_CapsuleRenderEngine.GetImpliedPalette(docObject);
                GH_Capsule capsule = GH_Capsule.CreateCapsule(att.Bounds, impliedPalette);
                bool left = docObject.Params.Input.Count == 0;
                bool right = docObject.Params.Output.Count == 0;
                capsule.SetJaggedEdges(left, right);
                capsule.RenderEngine.RenderMessage(graphics, docObject.Message, new GH_PaletteStyle(Color.Transparent));
                GH_PaletteStyle style = GH_CapsuleRenderEngine.GetImpliedStyle(impliedPalette, att.Selected, docObject.Locked, docObject.Hidden);
                foreach (IGH_Param param in docObject.Params.Input)
                {
                    inputGrip = param.Attributes.InputGrip;
                    capsule.AddInputGrip(inputGrip.Y);
                }
                foreach (IGH_Param param2 in docObject.Params.Output)
                {
                    inputGrip = param2.Attributes.OutputGrip;
                    capsule.AddOutputGrip(inputGrip.Y);
                }
                capsule.Render(graphics, style);
            }
        }
        internal static Color RENDER_COLOR(IGH_ActiveObject obj,bool objc)
        {
            bool hidden = true;
            if (obj is IGH_PreviewObject)
            {
                hidden = ((IGH_PreviewObject) obj).Hidden;
            }
            Color color = new Color();
            if (obj.Locked)
            {
                if (obj.Attributes.Selected&&objc)
                {
                    return Color.FromArgb(0x3f, 0x7f, 0x7f, 0x7f);
                }
                return Color.Gray;
            }
            switch (obj.RuntimeMessageLevel)
            {
                case GH_RuntimeMessageLevel.Blank:
                    if (obj.Attributes.Selected&&objc)
                    {
                        if (hidden)
                        {
                            return Color.FromArgb(0x3f, 0, 0, 0);
                        }
                        return Color.FromArgb(0x3f, 0xff, 0xff, 0xff);
                    }
                    else
                    {
                        if (hidden && objc)
                        {
                            return Color.FromArgb(0xdf, 0, 0, 0);
                        }
                        return Color.FromArgb(0xdf, 0xff, 0xff, 0xff);
                    }
                

                case GH_RuntimeMessageLevel.Warning:
                    if (obj.Attributes.Selected&&objc)
                    {
                        return Color.FromArgb(0x3f, 0xff, 0xff, 0);
                    }
                    return Color.Yellow;

                case GH_RuntimeMessageLevel.Error:
                    if (obj.Attributes.Selected&&objc)
                    {
                        return Color.FromArgb(0x3f, 0xff, 0, 0);
                    }
                    return Color.Red;
            }
            return color;
        }
        internal static void RENDER_OUTLINES(IGH_Attributes att, Graphics graphics,bool dis)
        {
            Color green = RENDER_COLOR(att.DocObject as IGH_ActiveObject,true);
            if (att.Selected)
            {
                green = Color.Green;
            }
            SizeF ef = new SizeF(5f, -5f);
            RectangleF bounds = att.Bounds;
            bounds.Inflate(-2f, 0f);
            Rectangle rect = GH_Convert.ToRectangle(bounds);
            Rectangle rect0= GH_Convert.ToRectangle(bounds);
            rect0.X += 6;
            rect0.Y+= 6;
            string nickName = att.DocObject.NickName;
#if rh6
            Font f= GH_FontServer.StandardAdjusted;
#else
            Font f = GH_FontServer.Small;
#endif
            Size size = GH_FontServer.MeasureString(nickName, f);
            RectangleF layoutRectangle = new RectangleF(att.Bounds.X, (att.Bounds.Y - size.Height) - 1f, (float) (size.Width + 2), (float) (size.Height + 2));
           if(dis)
            graphics.DrawString(nickName,f, new SolidBrush(green), layoutRectangle);
            string message = (att.DocObject as IGH_Component).Message;
            Size size2 = GH_FontServer.MeasureString(message, f);
            RectangleF ef4 = new RectangleF(att.Bounds.X, (att.Bounds.Y + att.Bounds.Height) + 1f, (float) (size2.Width + 2), (float) (size2.Height + 2));
            graphics.DrawString(message, f, new SolidBrush(Color.HotPink), ef4);
            GraphicsPath path = CreateRoundedRectanglePath(rect, 4);
            GraphicsPath path0 = CreateRoundedRectanglePath(rect0, 6);
            if (GH_Canvas.ZoomFadeLow < 5)
            {
                graphics.FillPath(new SolidBrush(Color.FromArgb(0xbf, green)), path);
            }
            else
            {

                PathGradientBrush gradientBrush = new PathGradientBrush(path0);
                gradientBrush.CenterColor = Color.FromArgb(127, Color.Black);
                float TT = Math.Min(att.Bounds.Width,att.Bounds.Height);
                gradientBrush.SetSigmaBellShape(10f/TT);
                gradientBrush.SurroundColors=new Color[] { Color.FromArgb(0, Color.Black) };
                graphics.FillPath(gradientBrush, path0);
     
              
                graphics.FillPath(new SolidBrush(Color.FromArgb(255, GH_Skin.canvas_back)), path);
            }
            graphics.DrawPath(new Pen((GH_Canvas.ZoomFadeLow < 5) ? Color.Black : green), path);
         
        }
        internal static void RENDER_PARAMS(IGH_Attributes att, GH_Canvas canvas, Graphics graphics, bool ui)
        {
            if (GH_Canvas.ZoomFadeLow >= 5)
            {
                IGH_Component docObject = att.DocObject as IGH_Component;
                RectangleF @in = new RectangleF();
                StringFormat farCenter = GH_TextRenderingConstants.FarCenter;
                canvas.SetSmartTextRenderingHint();
                RectangleF rect = new RectangleF();
                foreach (IGH_Param param in docObject.Params)
                {
                    GH_StateTagList stateTags = param.StateTags;
                    Color color = RENDER_COLOR(param,false);
                    SolidBrush brush = new SolidBrush(color);
                    int count = stateTags.Count;
                    if (param.Kind == GH_ParamKind.input)
                    {
                        rect = new RectangleF(param.Attributes.InputGrip.X - 2f, param.Attributes.InputGrip.Y - 2f, 4f, 4f);
                        if (ui)
                        {
                            graphics.DrawEllipse(new Pen(color), rect);
                        }
                        if (docObject.Params.Input.Count == 1)
                        {
                            @in = new RectangleF((att.Bounds.X + (count * 20)) + 2f, (att.Bounds.Y + (att.Bounds.Height / 2f)) - 10f, 20f, 20f);
                        }
                        else
                        {
                            @in = new RectangleF(param.Attributes.Bounds.X + (count * 20), param.Attributes.Bounds.Y, 20f, 20f);
                        }
                        if (@in.Width >= 1f)
                        {
#if rh6
                            graphics.DrawString(param.NickName, GH_FontServer.StandardAdjusted, brush, param.Attributes.Bounds, farCenter);
#else
                            graphics.DrawString(param.NickName, GH_FontServer.Standard, brush, param.Attributes.Bounds, farCenter);
#endif
                            stateTags = param.StateTags;
                            stateTags.Layout(GH_Convert.ToRectangle(@in), GH_StateTagLayoutDirection.Left);
                            stateTags.RenderStateTags(graphics);
                        }
                    }
                    if (param.Kind == GH_ParamKind.output)
                    {
                        rect = new RectangleF(param.Attributes.OutputGrip.X - 2f, param.Attributes.OutputGrip.Y - 2f, 4f, 4f);
                        if (ui)
                        {
                            graphics.DrawEllipse(new Pen(color), rect);
                        }
                        farCenter = GH_TextRenderingConstants.NearCenter;
                        float num3 = (docObject.Params.Input.Count > 0) ? docObject.Params.Input[0].Attributes.Bounds.Width : 0f;
                        if (docObject.Params.Output.Count == 1)
                        {
                            @in = new RectangleF(((param.Attributes.Bounds.X + param.Attributes.Bounds.Width) - (count * 20)) - 20f, (att.Bounds.Y + (att.Bounds.Height / 2f)) - 10f, 20f, 20f);
                        }
                        else
                        {
                            @in = new RectangleF(((param.Attributes.Bounds.X + param.Attributes.Bounds.Width) - (count * 20)) - 20f, param.Attributes.Bounds.Y, 20f, 20f);
                        }
                        if (@in.Width >= 1f)
                        {
#if rh6
                            graphics.DrawString(param.NickName, GH_FontServer.StandardAdjusted, brush, param.Attributes.Bounds, farCenter);
#else
                            graphics.DrawString(param.NickName, GH_FontServer.Standard, brush, param.Attributes.Bounds, farCenter);
#endif
                            stateTags = param.StateTags;
                            stateTags.Layout(GH_Convert.ToRectangle(@in), GH_StateTagLayoutDirection.Right);
                            stateTags.RenderStateTags(graphics);
                        }
                    }
                }
            }
        }
        public override GH_ObjectResponse RespondToMouseUp(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return base.RespondToMouseUp(sender, e);
            }
            ATT_MENUSTRIP menu = new ATT_MENUSTRIP(base.DocObject as IGH_Component);
            menu.BackColor = Color.DarkGray;

            base.DocObject.AppendMenuItems(menu);
     if(MENU)
            ATT_CLASS.ADD_CONMENU<ATT_NORMAL>(menu, this);

            CHANGE_MODE(menu);
            if (menu.Items.Count > 0)
            {
                sender.ActiveInteraction = null;
                Point position = Instances.DocumentEditor.PointToClient(Cursor.Position);
                menu.Show(Instances.DocumentEditor, position);
            }
            return GH_ObjectResponse.Handled;
        }
        public override void SetupTooltip(PointF canvasPoint, GH_TooltipDisplayEventArgs e)
        {
            base.SetupTooltip(canvasPoint, e);
            e.Text = e.Text + "\nPanda_UI";
            try
            {
                CHANGE_TOOLTIP();
            }
            catch
            {

            }
        }
        internal static void CHANGE_TOOLTIP()
        {
            GH_Tooltip.TooltipForm.Paint += (s, r) =>
            {
                GH_Tooltip.TooltipForm.BackColor = Color.FromArgb(11, 11, 11);
                GH_Tooltip.TooltipForm.TransparencyKey = GH_Tooltip.TooltipForm.BackColor;
                r.Graphics.Clear(GH_Tooltip.TooltipForm.BackColor);
                Instances.ActiveCanvas.Refresh();
            };
            GH_Tooltip.TooltipForm.Disposed += (s, r) =>
             {
                 Instances.ActiveCanvas.Refresh();
             };
        }
        internal static void RENDER_TOOLTIP(Graphics graphics)
        {
            if (GH_Tooltip.TooltipForm.IsDisposed)
                return;
            if (!GH_Tooltip.TooltipForm.Visible)
                return;

            PointF pf = Instances.ActiveCanvas.CursorCanvasPosition;
            Point p = new Point((int)pf.X, (int)pf.Y);


            Padding _padObj = (Padding)GH_Tooltip.TooltipForm.GetType().GetField("_padObj", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(GH_Tooltip.TooltipForm);

            //GH_Tooltip.TooltipForm.GetType().GetMethod("PaintIcon", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(GH_Tooltip.TooltipForm, new object[] { graphics });
            Bitmap _icon = (Bitmap)GH_Tooltip.TooltipForm.GetType().GetField("_icon", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(GH_Tooltip.TooltipForm);
            Rectangle _recIcon = (Rectangle)GH_Tooltip.TooltipForm.GetType().GetField("_recIcon", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(GH_Tooltip.TooltipForm);
            _recIcon.Location = p;
            if (_icon != null && _recIcon.Width != 0 && _recIcon.Height != 0)
            {
                GH_GraphicsUtil.RenderIcon(graphics, _recIcon, _icon);
            }


            //GH_Tooltip.TooltipForm.GetType().GetMethod("PaintTitle", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(GH_Tooltip.TooltipForm, new object[] { graphics });
            string _title = (string)GH_Tooltip.TooltipForm.GetType().GetField("_title", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(GH_Tooltip.TooltipForm);
            Rectangle _recTitle = (Rectangle)GH_Tooltip.TooltipForm.GetType().GetField("_recTitle", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(GH_Tooltip.TooltipForm);
            _recTitle.Location = new Point(_recIcon.Right, _recIcon.Y);
            if (_title.Length != 0 && _recTitle.Width != 0 && _recTitle.Height != 0)
            {
                StringFormat stringFormat = new StringFormat();
                stringFormat.LineAlignment = StringAlignment.Center;
                stringFormat.Alignment = StringAlignment.Near;
                SolidBrush solidBrush = new SolidBrush(Color.Red);
                Rectangle r = PadRec(_recTitle, _padObj);
                graphics.DrawString(_title, GH_FontServer.NewFont(SystemFonts.CaptionFont.FontFamily, 11f, FontStyle.Italic), solidBrush, r, stringFormat);
                stringFormat.Dispose();
                solidBrush.Dispose();
            }



            //GH_Tooltip.TooltipForm.GetType().GetMethod("PaintText", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(GH_Tooltip.TooltipForm, new object[] { graphics });
            string _text = (string)GH_Tooltip.TooltipForm.GetType().GetField("_text", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(GH_Tooltip.TooltipForm);
            Rectangle _recText = (Rectangle)GH_Tooltip.TooltipForm.GetType().GetField("_recText", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(GH_Tooltip.TooltipForm);
            _recText.Location = new Point(_recIcon.X, _recIcon.Bottom);
            if (_text.Length != 0 && _recText.Width != 0 && _recText.Height != 0)
            {
                StringFormat stringFormat = new StringFormat();
                stringFormat.LineAlignment = StringAlignment.Center;
                stringFormat.Alignment = StringAlignment.Near;
                SolidBrush solidBrush = new SolidBrush(Color.Red);
                Rectangle r = PadRec(_recText, _padObj);
                graphics.DrawString(_text, GH_FontServer.NewFont(SystemFonts.DefaultFont.FontFamily, 8f, FontStyle.Regular), solidBrush, r, stringFormat);
                stringFormat.Dispose();
                solidBrush.Dispose();
            }


            string _desc = (string)GH_Tooltip.TooltipForm.GetType().GetField("_desc", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(GH_Tooltip.TooltipForm);
            Rectangle _recDesc = (Rectangle)GH_Tooltip.TooltipForm.GetType().GetField("_recDesc", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(GH_Tooltip.TooltipForm);
            Bitmap _diagram = (Bitmap)GH_Tooltip.TooltipForm.GetType().GetField("_diagram", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(GH_Tooltip.TooltipForm);
            _recDesc.Location = new Point(_recIcon.X, _recText.Bottom);
            Padding _padDsc = (Padding)GH_Tooltip.TooltipForm.GetType().GetField("_padDsc", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(GH_Tooltip.TooltipForm);


            //GH_Tooltip.TooltipForm.GetType().GetMethod("PaintDiagram", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(GH_Tooltip.TooltipForm, new object[] { graphics });
            if (_diagram != null && _recDesc.Width != 0 && _recDesc.Height != 0)
            {
                Rectangle rect = PadRec(_recDesc, _padObj);
                graphics.DrawImage(_diagram, rect);
                if (_desc.Length == 0)
                {
                    Rectangle rectangle = PadRec(_recDesc, _padObj);
                    Rectangle rr = PadRec(rectangle, _padDsc);
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.LineAlignment = StringAlignment.Center;
                    stringFormat.Alignment = StringAlignment.Near;
                    SolidBrush solidBrush2 = new SolidBrush(Color.FromArgb(255, Color.FromArgb(255, 0, 0)));
                    graphics.DrawString(_desc, (Font)GH_FontServer.ConsoleSmall.Clone(), solidBrush2, rr, stringFormat);
                    solidBrush2.Dispose();
                    stringFormat.Dispose();
                    rectangle = PadRec(_recDesc, _padObj);
                    rectangle.Width--;
                    rectangle.Height--;
                    Pen pen = new Pen(GH_GraphicsUtil.ScaleColour(Color.FromArgb(22, 22, 22), 0.3));
                    graphics.DrawRectangle(pen, rectangle);
                    pen.Dispose();
                }
            }


            //GH_Tooltip.TooltipForm.GetType().GetMethod("PaintDescription", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(GH_Tooltip.TooltipForm, new object[] { r.Graphics });
          if (_desc.Length != 0 && _recDesc.Width != 0 && _recDesc.Height != 0)
            {
                Rectangle rectangle = PadRec(_recDesc, _padObj);
                Rectangle rr = PadRec(rectangle, _padDsc);
                StringFormat stringFormat = new StringFormat();
                stringFormat.LineAlignment = StringAlignment.Center;
                stringFormat.Alignment = StringAlignment.Near;
                SolidBrush solidBrush2 = new SolidBrush(Color.FromArgb(255, Color.FromArgb(255, 0, 0)));
                graphics.DrawString(_desc, (Font)GH_FontServer.ConsoleSmall.Clone(), solidBrush2, rr, stringFormat);
                solidBrush2.Dispose();
                stringFormat.Dispose();
                rectangle = PadRec(_recDesc, _padObj);
                rectangle.Width--;
                rectangle.Height--;
                Pen pen = new Pen(GH_GraphicsUtil.ScaleColour(Color.FromArgb(22, 22, 22), 0.3));
                graphics.DrawRectangle(pen, rectangle);
                pen.Dispose();
            }
        }
        private static  Rectangle PadRec(Rectangle rec, Padding pad)
        {
            rec.X += pad.Left;
            rec.Y += pad.Top;
            rec.Width -= pad.Horizontal;
            rec.Height -= pad.Vertical;
            return rec;
        }
        internal static void CHANGE_MODE(ATT_MENUSTRIP menu)
        {
            try
            {
                Color bc = UI_SETTING.INS.XML.GetValue("PBACKC", Color.FromArgb(255, 205, 205, 205));
                Color fc = UI_SETTING.INS.XML.GetValue("PFOREC", Color.Black);
                if (UI_SETTING.INS.DARK_MODE == UI_SETTING.MODE.DARK)
                {
                    bc = Color.FromArgb(255, 105, 105, 105);
                    fc = Color.Yellow;
                }
                foreach (ToolStripItem control in menu.Items)
                {

                    if (control.ForeColor == Color.Yellow || control.ForeColor == SystemColors.ControlText || control.ForeColor == Color.Black)
                        control.ForeColor = fc;
                    if (!control.Enabled)
                        control.BackColor = Color.FromArgb(127, bc);
                    else
                        control.BackColor = bc;
                    if (control is ToolStripMenuItem)
                        ATT_CLASS.SET_BAKECOLOR(control as ToolStripMenuItem, fc, bc);
                } 
            }
            catch(Exception ex)
            {
               // MessageBox.Show("param CHANGE_MODE" + ex.Message);
            }
        }
        internal static bool TVALUE(GH_Boolean gH_Boolean)
        {
            return ((gH_Boolean == null) ? true : gH_Boolean.Value);
        }

        public GH_DocumentObject OWNER
        {
            get
            {
                return (base.Owner as GH_DocumentObject);
            }
        }

        public bool TAG
        {
            get
            {
                return (bool)UI_SETTING.INS["TAG"] ? m_p_tag : false;
            }
            set
            {
                this.m_p_tag = value;
            }
        }

        public bool MENU
        {
            get
            {
                return (bool)UI_SETTING.INS["MENU"] ? m_p_menu : false;
            }
            set
            {
                this.m_p_menu = value;
            }
        }

        public bool ATT
        {
            get
            {
                return (bool)UI_SETTING.INS["ATT"] ? m_p_att : false;
            }
            set
            {
                this.m_p_att = value;
            }
        }
    }
}

