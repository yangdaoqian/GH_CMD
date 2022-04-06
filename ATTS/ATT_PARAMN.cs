namespace UI.ATTS
{
    using Grasshopper;
    using Grasshopper.GUI;
    using Grasshopper.GUI.Canvas;
    using Grasshopper.Kernel;
    using Grasshopper.Kernel.Parameters;
    using Grasshopper.Kernel.Special;
    using Grasshopper.Kernel.Types;
    using System.Reflection;
    using UI.CONS;
    using ALIEN_DLL.GEOS;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Windows.Forms;
    internal class ATT_PARAMN : ATT_PARAM
    {
        private GH_StateTagList m_stateTags;
        private Rectangle m_textBounds;

        public ATT_PARAMN(IGH_Param param) : base(param)
        {
  
            //base.P_ATT = PG_OBJECT<bool, object>.DOC_GETVALUE("GetValue", param as GH_DocumentObject, "P_UI", base.P_ATT);
            //base.P_MENU = true;
            //base.P_MENU = PG_OBJECT<bool, object>.DOC_GETVALUE("GetValue", param as GH_DocumentObject, "P_MENU", base.P_MENU);
            //base.PerformLayout();
            //this.Owner.OnPingDocument().ObjectsDeleted += ObjectsDeletedEventHandler;
        }
        internal static void EVENT_MENU<T, C>(ATT_MENUSTRIP objectMenu, IGH_Param pa) where T: class, IGH_Param, new() where C: ICON_MENU, new()
        {
            if ((((pa is Param_Colour)  || ((pa is GH_GraphMapper) || (pa.VolatileDataCount <= 1))) || (pa.Kind != GH_ParamKind.output)))
            {
                C local = Activator.CreateInstance<C>();
                try
                {
                    if (!local.SETUP(pa as T))
                    {
                        return;
                    }
                }
                catch
                {
                    return;
                }
                objectMenu.Height = local.CONTROL.Height;
                objectMenu.Width = local.CONTROL.Width;
                objectMenu.Items.Add(new ToolStripSeparator());
                GH_DocumentObject.Menu_AppendCustomItem(objectMenu, local.CONTROL);
            }
        }

        protected override void Layout()
        {
            if (ATT)
            {
                if (GH_Attributes<IGH_Param>.IsIconMode(base.Owner.IconDisplayMode))
                {
                    this.Bounds = new RectangleF(this.Pivot.X - 25f, this.Pivot.Y - 12f, 50f, 24f);
                    this.Bounds = GH_Convert.ToRectangle(this.Bounds);
                }
                else
                {
#if rh6
                    float width = Math.Max(GH_FontServer.MeasureString(base.Owner.NickName, GH_FontServer.StandardAdjusted).Width + 10, 50);
#else
                    float width = Math.Max(GH_FontServer.MeasureString(base.Owner.NickName, GH_FontServer.Standard).Width + 10, 50);
#endif
                    this.Bounds = new RectangleF(this.Pivot.X - (0.5f * width), this.Pivot.Y - 10f, width, 20f);
                    this.Bounds = GH_Convert.ToRectangle(this.Bounds);
                }
                this.m_textBounds = GH_Convert.ToRectangle(this.Bounds);
                this.m_stateTags = base.Owner.StateTags;
                if (this.m_stateTags.Count == 0)
                {
                    this.m_stateTags = null;
                }
                if (this.m_stateTags !=null)
                {
                    this.m_stateTags.Layout(GH_Convert.ToRectangle(this.Bounds), GH_StateTagLayoutDirection.Left);
                    Rectangle boundingBox = this.m_stateTags.BoundingBox;
                    if (!boundingBox.IsEmpty)
                    {
                        boundingBox.Inflate(3, 0);
                        this.Bounds = RectangleF.Union(this.Bounds, boundingBox);
                    }
                }
            }
            else
            {
                base.Layout();
            }
        }

        private void menu_example_clicked(GH_MenuCustomControl sender, KeyEventArgs e)
        {
            base.Owner.RecordUndoEvent("Example");
            GH_Slider control = (GH_Slider) sender.Control;
            Param_Number owner = base.Owner as Param_Number;
            owner.PersistentData.ClearData();
            owner.PersistentData.Append(new GH_Number((double) control.Value));
            owner.ExpireSolution(true);
        }

        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {
            try
            {
                if (ATT)
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
                        if (base.Owner.SourceCount > 0)
                        {
                            base.RenderIncomingWires(canvas.Painter, base.Owner.Sources, base.Owner.WireDisplay);
                        }
                        return;
                    }
                    RectangleF bounds = this.Bounds;
                    this.Bounds = bounds;
                    if (canvas.Viewport.IsVisible(ref bounds, 10f))
                    {
                        bool hidden = true;
                        if (base.Owner is IGH_PreviewObject)
                        {
                            hidden = ((IGH_PreviewObject)base.Owner).Hidden;
                        }
                        if (TAG)
                        {
                            string nickName = base.DocObject.NickName + ": ";
                            if (base.DocObject is IGH_Param)
                            {
                                int n = ((IGH_Param)base.DocObject).VolatileDataCount;
                                if (n > 0)
                                {
                                    if (n == 1)
                                    {
                                        IGH_Goo goo = null;
                                        try
                                        {
                                            goo = ((IGH_Param)base.DocObject).VolatileData.AllData(false).ElementAt(0);
                                        }
                                        catch
                                        {
                                        }
                                        nickName += (goo == null || !goo.IsValid) ? "null" : goo.ToString();
                                    }
                                    else
                                        nickName += n.ToString() + " " + ((IGH_Param)base.DocObject).TypeName;
                                }


                            }
#if rh6
                            Font f = GH_FontServer.StandardAdjusted;
                            //PARAM_FONT fONT = base.DocObject as PARAM_FONT;
                            //if (fONT != null && fONT.VolatileDataCount == 1 && fONT.VolatileData.AllData(true).Count() > 0)
                            //    f = new Font(((PG_FONT)fONT.VolatileData.AllData(true).ElementAt(0)).Value.FamilyName, 4);
#else
                            Font f = GH_FontServer.Small;
#endif
                            Size size = GH_FontServer.MeasureString(nickName, f);
                            RectangleF layoutRectangle = new RectangleF(this.Bounds.X, (this.Bounds.Y - size.Height) - 2f, (float)(size.Width + 2), (float)(size.Height + 2));
                            graphics.DrawString(nickName, f, new SolidBrush(Color.CadetBlue), layoutRectangle);
                        }
                        if (ATT)
                        {
                            Color green = ATT_NORMAL.RENDER_COLOR(base.Owner, true);
                            if (this.Selected)
                            {
                                green = Color.Green;
                            }
                            SizeF ef3 = new SizeF(5f, -5f);
                            RectangleF @in = this.Bounds;
                            @in.Inflate(0f, 0f);
                            Rectangle rectangle3 = GH_Convert.ToRectangle(@in);
                            Rectangle rectangle4 = GH_Convert.ToRectangle(@in);
                            rectangle4.X += 4;
                            rectangle4.Y += 4;
                            GraphicsPath path = ATT_NORMAL.CreateRoundedRectanglePath(rectangle3, 4);
                            GraphicsPath path0 = ATT_NORMAL.CreateRoundedRectanglePath(rectangle4, 4);
                            if (GH_Canvas.ZoomFadeLow < 5)
                            {
                                graphics.FillPath(new SolidBrush(Color.FromArgb(0xbf, green)), path);
                            }
                            else
                            {
                                PathGradientBrush gradientBrush = new PathGradientBrush(path0);
                                gradientBrush.CenterColor = Color.FromArgb(127, Color.Black);
                                float TT = Math.Min(this.Bounds.Width, this.Bounds.Height);
                                gradientBrush.SetSigmaBellShape(10f / TT);
                                gradientBrush.SurroundColors = new Color[] { Color.FromArgb(0, Color.Black) };
                                graphics.FillPath(gradientBrush, path0);

                                graphics.FillPath(new SolidBrush(Color.FromArgb(255, GH_Skin.canvas_back)), path);
                            }
                            graphics.DrawPath(new Pen((GH_Canvas.ZoomFadeLow < 5) ? Color.Black : green), path);
                            RectangleF rect = new RectangleF();
                            rect = new RectangleF(this.InputGrip.X - 2f, this.InputGrip.Y - 2f, 4f, 4f);
                            graphics.DrawEllipse(new Pen(green), rect);
                            rect = new RectangleF(this.OutputGrip.X - 2f, this.OutputGrip.Y - 2f, 4f, 4f);
                            graphics.DrawEllipse(new Pen(green), rect);
                        }
                        GH_Capsule capsule = null;
                        if (GH_Attributes<IGH_Param>.IsIconMode(base.Owner.IconDisplayMode))
                        {
                            capsule = GH_Capsule.CreateCapsule(this.Bounds, GH_CapsuleRenderEngine.GetImpliedPalette(base.Owner));
                        }
                        else
                        {
                            capsule = GH_Capsule.CreateTextCapsule(this.Bounds, this.m_textBounds, GH_CapsuleRenderEngine.GetImpliedPalette(base.Owner), base.Owner.NickName);
                        }
                        if (this.HasInputGrip)
                        {
                            capsule.AddInputGrip(this.InputGrip.Y);
                        }
                        if (this.HasOutputGrip)
                        {
                            capsule.AddOutputGrip(this.OutputGrip.Y);
                        }
                        if (GH_Attributes<IGH_Param>.IsIconMode(base.Owner.IconDisplayMode))
                        {
                            Rectangle box = capsule.Box_Content;
                            if ((this.m_stateTags != null) && (this.m_stateTags.Count > 0))
                            {
                                box = Rectangle.FromLTRB(this.m_stateTags.BoundingBox.Right, box.Y, box.Right, box.Bottom);
                            }
                            if (base.Owner.Locked)
                            {
                                if (!(ATT))
                                {
                                    capsule.Render(graphics, this.Selected, true, hidden);
                                }
                                capsule.RenderEngine.RenderIcon(graphics, base.Owner.Icon_24x24_Locked, box, 0, 1);
                            }
                            else
                            {
                                if (!(ATT))
                                {
                                    capsule.Render(graphics, this.Selected, false, hidden);
                                }
                                capsule.RenderEngine.RenderIcon(graphics, base.Owner.Icon_24x24, box, 0, 1);
                            }
                            if (base.Owner.Obsolete && CentralSettings.CanvasObsoleteTags)
                            {
                                GH_GraphicsUtil.RenderObjectOverlay(graphics, base.Owner, box);
                            }
                        }
                        else
                        {
                            if (!(ATT))
                            {
                                capsule.Render(graphics, this.Selected, base.Owner.Locked, hidden);
                            }
                            capsule.RenderEngine.RenderText(graphics, Color.Teal);
                        }
                        if (GH_Attributes<IGH_Param>.IsIconMode(base.Owner.IconDisplayMode))
                        {
                            Rectangle box = capsule.Box_Content;
                            if ((this.m_stateTags != null) && (this.m_stateTags.Count > 0))
                            {
                                box = Rectangle.FromLTRB(this.m_stateTags.BoundingBox.Right, box.Y, box.Right, box.Bottom);
                            }
                            if (base.Owner.Locked)
                            {
                                if (!(ATT))
                                {
                                    capsule.Render(graphics, this.Selected, true, hidden);
                                }
                                capsule.RenderEngine.RenderIcon(graphics, base.Owner.Icon_24x24_Locked, box, 0, 1);
                            }
                            else
                            {
                                if (!(ATT))
                                {
                                    capsule.Render(graphics, this.Selected, false, hidden);
                                }
                                capsule.RenderEngine.RenderIcon(graphics, base.Owner.Icon_24x24, box, 0, 1);
                            }
                            if (base.Owner.Obsolete && CentralSettings.CanvasObsoleteTags)
                            {
                                GH_GraphicsUtil.RenderObjectOverlay(graphics, base.Owner, box);
                            }
                        }
                        else
                        {
                            if (!(ATT))
                            {
                                capsule.Render(graphics, this.Selected, base.Owner.Locked, hidden);
                            }
                            capsule.RenderEngine.RenderText(graphics, Color.Teal);
                        }
                        if (this.m_stateTags != null)
                        {
                            this.m_stateTags.RenderStateTags(graphics);
                        }
                        capsule.Dispose();
                    }
                }
                else
                {
                    base.Render(canvas, graphics, channel);
                }
                if (channel == GH_CanvasChannel.Objects)
                    ATT_NORMAL.RENDER_TOOLTIP(graphics);
            }
            catch(Exception ex)
            {
                base.Render(canvas, graphics, channel);
                MessageBox.Show(ex.Message+ex.InnerException.Message+"render");
            }
        
        }


        public override GH_ObjectResponse RespondToMouseDoubleClick(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            if (base.Owner is Param_Boolean)
            {
                Param_Boolean owner = base.Owner as Param_Boolean;
                owner.Invert = !owner.Invert;
                owner.ExpireSolution(true);
            }
            return base.RespondToMouseDoubleClick(sender, e);
        }

        public override GH_ObjectResponse RespondToMouseUp(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
          
            if (e.Button != MouseButtons.Right)
            {
                return base.RespondToMouseUp(sender, e);
            }
            ATT_MENUSTRIP menu = new ATT_MENUSTRIP(base.DocObject as IGH_Component);

            base.DocObject.AppendMenuItems(menu);
            menu.BackColor = Color.DarkGray;
   
            //EVENT_MENU<Param_Colour, CON_COLOR>(menu, base.Owner);
            //EVENT_MENU<PARAM_COLOR, CON_COLOR>(menu, base.Owner);
                EVENT_MENU<Param_Integer, CON_SLIDER_UI>(menu, base.Owner);
                EVENT_MENU<Param_Number, CON_SLIDER_UI>(menu, base.Owner);
                EVENT_MENU<GH_GraphMapper, CON_GRAPH_UI>(menu, base.Owner);
                //EVENT_MENU<PARAM_GRAPH_MAPPER, CON_GRAPH_UI>(menu, base.Owner);
                //EVENT_MENU<PARAM_GRADIENT_CONTROL, CON_GRADIENT_UI>(menu, base.Owner);
             
                if (ATT_CLASS.IS_GEO_PARAM(base.Owner) && (base.Owner.SourceCount == 0))
                {
                    ATT_CLASS.APPEND_MENUA(menu, base.Owner);
                }
         if (MENU)
                ATT_CLASS.ADD_CONMENU<ATT_PARAMN>(menu, this);
            ATT_NORMAL.CHANGE_MODE(menu);
            if (menu.Items.Count > 0)
            {
                sender.ActiveInteraction = null;
                Point position = Instances.DocumentEditor.PointToClient(Cursor.Position);
                menu.Show(Instances.DocumentEditor, position);
            }
            return GH_ObjectResponse.Handled;
        }

        public override void SetupTooltip(PointF point, GH_TooltipDisplayEventArgs e)
        {
            base.SetupTooltip(point, e);
            if (base.Owner is Param_Colour)
            {
                e.Text = e.Text + "\nLeft click to set colors";
            }
            if (base.Owner is Param_Boolean)
            {
                e.Text = e.Text + "\nDouble click to invert the values";
            }
            e.Text = e.Text + "\nPanda_UI";
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

