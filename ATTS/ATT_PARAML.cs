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
using UI.CONS;
using Grasshopper.GUI;
using Grasshopper.GUI.Canvas.Interaction;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Special;

using Rhino.DocObjects;
using Grasshopper.Kernel.Graphs;
using Grasshopper.Kernel.Attributes;
using UI.GEOS;
namespace UI.ATTS
{
    internal class ATT_PARAML : GH_LinkedParamAttributes,I_ATT
    {
        public GH_DocumentObject OWNER => this.Owner as GH_DocumentObject;
        protected bool m_p_ui;
        protected bool m_p_menu;
        protected bool m_p_distag;
        protected bool m_p_gum;
        public bool ATT
        {
            get => m_p_ui;
            set
            {
                m_p_ui = value;
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
            get => m_p_distag;
            set
            {
                m_p_distag = value;
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
        public ATT_PARAML(IGH_Param param, IGH_Attributes patt) : base(param, patt)
        {
            this.m_p_menu = P_OBJECT<bool, object>.DOC_GETVALUE("GetValue", param as GH_DocumentObject, "MENU", true);
        }
        private void Menu_SelectClicked(object sender, EventArgs e)
        {
            this.Owner.RecordUndoEvent("Select");
            if (this.slected_guids.Count > 0)
            {
                    Rhino.RhinoDoc.ActiveDoc.Objects.Select(this.slected_guids, true);
            }
        }
        private void Menu_AppendClicked(object sender, EventArgs e)
        {
            ATT_CLASS.APPEND(this.Owner);
        }

        private List<Guid> slected_guids
        {
            get
            {
                List<Guid> guids = new List<Guid>();
                try
                {
                    int num = this.Owner.VolatileDataCount;
                    for (int i = 0; i < num; i++)
                    {
                        IGH_GeometricGoo t = this.Owner.VolatileData.AllData(false).ElementAt(i) as IGH_GeometricGoo;
                        if (t != null)
                        {
                            if (t.IsReferencedGeometry)
                            {
                                guids.Add(t.ReferenceID);
                            }
                        }
                    }
                }
                finally
                {
                }
                return guids;
            }
        }
        public override GH_ObjectResponse RespondToMouseUp(GH_Canvas sender, Grasshopper.GUI.GH_CanvasMouseEvent e)
        {
            if (e.Button != MouseButtons.Right)
                return base.RespondToMouseUp(sender, e);
            try
            {
                ATT_MENUSTRIP menu = new ATT_MENUSTRIP(this.DocObject as IGH_Component);
                menu.BackColor = Color.DarkGray;
                base.DocObject.AppendMenuItems(menu);
                if (MENU)
                {
                    if (this.Owner.Kind == GH_ParamKind.input && this.Owner.SourceCount == 0)
                    {
                        ATT_PARAMN.EVENT_MENU<Param_Colour, CON_COLOR>(menu, this.Owner);
                        //ATT_PARAMN.EVENT_MENU<PARAM_COLOR, CON_COLOR>(menu, this.Owner);
                        ATT_PARAMN.EVENT_MENU<Param_Number, CON_SLIDER_UI>(menu, this.Owner);
                        ATT_PARAMN.EVENT_MENU<Param_Integer, CON_SLIDER_UI>(menu, this.Owner);
                        ATT_PARAMN.EVENT_MENU<GH_GraphMapper, CON_GRAPH_UI>(menu, this.Owner);
                        //ATT_PARAMN.EVENT_MENU<PARAM_GRAPH_MAPPER, CON_GRAPH_UI>(menu, base.Owner);
                        // ATT_PARAMN.EVENT_MENU<PARAM_GRADIENT_CONTROL, CON_GRADIENT_UI>(menu, base.Owner);
                        try
                        {
                            if (ATT_CLASS.IS_GEO_PARAM(this.Owner))
                                ATT_CLASS.APPEND_MENUA(menu, this.Owner);
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("ASR error:"+ex.Message);
                        }

                    }
                    ATT_CLASS.ADD_CONMENU(menu, this);
                }
                ATT_NORMAL.CHANGE_MODE(menu);
                if (menu.Items.Count > 0)
                {
                    sender.ActiveInteraction = null;
                    Point location = Instances.DocumentEditor.PointToClient(Cursor.Position);
                    menu.Show(Instances.DocumentEditor, location); //new System.Drawing.Point((int) e..X, (int) e.CanvasLocation.Y));
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return GH_ObjectResponse.Handled;
        }
        private void menu_example_clicked(GH_MenuCustomControl sender, KeyEventArgs e)
        {
            this.Owner.RecordUndoEvent("Example");
            GH_Slider s = (GH_Slider)sender.Control;
            Param_Number p = this.Owner as Param_Number;
            p.PersistentData.ClearData();
            p.PersistentData.Append(new GH_Number((double)s.Value));
            p.ExpireSolution(true);

        }
        public override GH_ObjectResponse RespondToMouseMove(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            return base.RespondToMouseMove(sender, e);
        }
        public override GH_ObjectResponse RespondToMouseDown(GH_Canvas sender, GH_CanvasMouseEvent e)
        {

            return base.RespondToMouseDown(sender, e);
        }
        public override GH_ObjectResponse RespondToMouseDoubleClick(Grasshopper.GUI.Canvas.GH_Canvas sender, Grasshopper.GUI.GH_CanvasMouseEvent e)
        {
            if (this.Owner is Param_Boolean && this.Owner.Kind == GH_ParamKind.input&&(MENU))
            {
                Param_Boolean p = this.Owner as Param_Boolean;
                p.Invert = !p.Invert;
                p.ExpireSolution(true);
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
