namespace UI.ATTS
{
    using Grasshopper;
    using Grasshopper.Kernel;
    using Grasshopper.Kernel.Attributes;
    using Grasshopper.Kernel.Types;
    using PANDA.GEOS;
    using Rhino;
    using Rhino.Display;
    using Rhino.Geometry;
    using Rhino.Geometry.Intersect;
    using Rhino.Input.Custom;
    using System;
    using PANDA.CONS.UI;
    using Rhino.UI;
    using Rhino.UI.Gumball;
    using System.Collections;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using PANDA.CONS;
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
    internal class ATT_SLIDER :GH_NumberSliderAttributes,I_ATT
    {

        protected bool m_att;
        protected bool m_menu;
        protected bool m_tag;

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
        public ATT_SLIDER(GH_NumberSlider param) : base(param)
        {              

            this.m_att = P_OBJECT<bool, object>.DOC_GETVALUE("GetValue", param as GH_DocumentObject, "ATT", true);
            this.m_menu = P_OBJECT<bool, object>.DOC_GETVALUE("GetValue", param as GH_DocumentObject, "MENU", true);
            this.m_tag = P_OBJECT<bool, object>.DOC_GETVALUE("GetValue", param as GH_DocumentObject, "TAG", true);
        }

        public override GH_ObjectResponse RespondToMouseUp(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
        
            if (e.Button != MouseButtons.Right || !(UI_SETTING.SETTINGS.MENU ? m_menu : false))
            {
                ATT_MENUSTRIP menu = new ATT_MENUSTRIP(base.DocObject as IGH_Component);
     
                SizeF sizeF = GH_FontServer.MeasureString(Owner.ImpliedNickName, GH_FontServer.StandardAdjusted);
                sizeF.Width = Math.Max(sizeF.Width + 10f, 20);
                Rectangle m_boundsName = GH_Convert.ToRectangle(new RectangleF(Pivot.X, Pivot.Y, sizeF.Width, 20));
                System.Drawing.Point position = GH_Convert.ToPoint(e.CanvasLocation);
                if (e.Button == MouseButtons.Left && m_boundsName.Contains(position))
                {
                    CON_PLAYER p = new CON_PLAYER();
                    p.SETUP(base.Owner);
                    menu.Height = p.CONTROL.Height;
                    menu.Width = p.CONTROL.Width;
                    menu.Items.Add(new ToolStripSeparator());
                    GH_DocumentObject.Menu_AppendCustomItem(menu, p.CONTROL);
                    if (menu.Items.Count > 0)
                    {
                        sender.ActiveInteraction = null;
                        position = Instances.DocumentEditor.PointToClient(Cursor.Position);
                        menu.Show(Instances.DocumentEditor, position);
                    }
                }             
            }
            return base.RespondToMouseUp(sender, e);
        }
    }
}

