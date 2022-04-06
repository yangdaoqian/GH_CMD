
namespace GH_CMD
{
    using Rhino;
    using Grasshopper.Kernel.Data;
    using Grasshopper;
    using Grasshopper.Kernel;
    using System.Windows.Forms;
    using System;
    using System.Reflection;
    using System.Threading;
    using Grasshopper.GUI.Canvas;
    using Grasshopper.GUI;
    using Grasshopper.GUI.Ribbon;
    using Rhino.Display;
    using Rhino.DocObjects;
    using GH_IO.Serialization;
    using Grasshopper.Kernel.Parameters;
    using Rhino.Input;
    using Rhino.Input.Custom;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Text;
    using Rhino.Geometry;
    using Grasshopper.Kernel.Types;
    using System.Collections;
    using System.Collections.Generic;
    public class GH_CMD_AP : GH_AssemblyPriority
    {
        private Guid m_guid;
        internal class DRAW : IGH_PreviewArgs
        {
            private GH_Document m_doc;
            private DisplayPipeline m_pipeline;
            private RhinoViewport m_viewport;
            private int m_thickness;
            private Color m_wire_normal;
            private Color m_wire_selected;
            private DisplayMaterial m_face_normal;
            private DisplayMaterial m_face_selected;
            private MeshingParameters m_mesh_params;
            public GH_Document Document
            {
                get
                {
                    return m_doc;
                }
            }
            public DisplayPipeline Display
            {
                get
                {
                    return m_pipeline;
                }
            }
            public RhinoViewport Viewport
            {
                get
                {
                    return m_viewport;
                }
            }
            public int DefaultCurveThickness
            {
                get
                {
                    return m_thickness;
                }
            }
            public DisplayMaterial ShadeMaterial
            {
                get
                {
                    return m_face_normal;
                }
            }
            public DisplayMaterial ShadeMaterial_Selected
            {
                get
                {
                    return m_face_selected;
                }
            }
            public Color WireColour
            {
                get
                {
                    return m_wire_normal;
                }
            }
            public Color WireColour_Selected
            {
                get
                {
                    return m_wire_selected;
                }
            }
            public MeshingParameters MeshingParameters
            {
                get
                {
                    return m_mesh_params;
                }
            }
            internal DRAW(GH_Document doc, DisplayPipeline pl, RhinoViewport vp, int curve_thickness, Color wire, Color wire_sel, DisplayMaterial face, DisplayMaterial face_sel, MeshingParameters mesh_params)
            {
                m_doc = doc;
                m_pipeline = pl;
                m_viewport = vp;
                m_thickness = curve_thickness;
                m_wire_normal = wire;
                m_wire_selected = wire_sel;
                m_face_normal = face;
                m_face_selected = face_sel;
                m_mesh_params = mesh_params;
            }
        }
        internal class GET_I : GetPoint
        {
            private IGH_Component m_com;
            internal GET_I(IGH_Component com)
            {
                m_com = com;
            }
            protected override void OnDynamicDraw(GetPointDrawEventArgs e)
            {
                m_com.ClearData();
                m_com.CollectData();
                m_com.ComputeData();
                int m = m_com.Params.Output.Count;
                for (int i = 0; i < m; i++)
                {
                    if (m_com.Params.Output[i] is IGH_PreviewObject)
                    {
                        Color wire = System.Drawing.Color.FromArgb(255, 0, 191);
                        DisplayMaterial face = GH_Material.CreateStandardMaterial(System.Drawing.Color.FromArgb(131, 255, 0, 191));
                        int defaultCurveThickness = e.Display.DefaultCurveThickness;
                        int curve_thickness = defaultCurveThickness + CentralSettings.PreviewSelectionThickening;
                        DRAW args = new DRAW(null, e.Display, e.Viewport, defaultCurveThickness, wire, wire, face, face, MeshingParameters.DocumentCurrentSetting(RhinoDoc.ActiveDoc));
                        ((IGH_PreviewObject)(m_com.Params.Output[i])).DrawViewportMeshes(args);
                        ((IGH_PreviewObject)(m_com.Params.Output[i])).DrawViewportWires(args);
                    }
                }
            }

        }
        internal List<G> GET_VALUE<G>(string name) where G : IGH_GeometricGoo, new()
        {
            List<G> list = new List<G>();
            OptionToggle t = new OptionToggle(false, "One", "Multipe");
            while (true)
            {
                GetObject val = new GetObject();
                try
                {
                    val.GeometryFilter = (Activator.CreateInstance(typeof(G).BaseType.GenericTypeArguments[0]) as GeometryBase).ObjectType;
                }
                catch
                {

                }
                val.AddOptionToggle("Option", ref t);
                try
                {
                    val.AcceptNothing(false);
                    val.SetCommandPrompt("Set " + name);
                    GetResult val2 = val.Get();
                    if (val2 == GetResult.Option)
                        continue;
                    if ((int)val2 != 2 && (int)val2 != 1)
                    {
                        list.Add(new G() { ReferenceID = val.Object(0).ObjectId });
                    }
                    if (t.CurrentValue && val2 == GetResult.Object)
                        continue;
                    if (val2 == GetResult.Cancel)
                        break;
                }
                finally
                {
                    ((IDisposable)val).Dispose();
                }
                break;
            }
            return list;
        }
        internal List<T> GET_VALUE<T, G>(string name, Func<G, GetResult> get, Func<G, T> getdv, GetResult r) where G : GetBaseClass, new()
        {
            List<T> list = new List<T>();
            OptionToggle t = new OptionToggle(false, "One", "Multipe");
            while (true)
            {
                G val = new G();
                val.AddOptionToggle("Option", ref t);
                try
                {
                    val.AcceptNothing(false);
                    val.SetCommandPrompt("Set " + name);
                    GetResult val2 = get(val);
                    if (val2 == GetResult.Option)
                        continue;
                    if ((int)val2 != 2 && (int)val2 != 1)
                    {
                        list.Add(getdv(val));
                    }
                    if (t.CurrentValue && val2 == r)
                        continue;
                    if (val2 == GetResult.Cancel)
                        break;
                }
                finally
                {
                    ((IDisposable)val).Dispose();
                }
                break;
            }
            return list;
        }
        internal void set_param(IGH_Param pi)
        {
            Type pt = pi.Type;
            IEnumerable listc = null;
            if (pt == typeof(GH_Point))
            {
                listc = GET_VALUE<GH_Point>(pi.Name);
            }
            if (pt == typeof(GH_Curve))
            {
                listc = GET_VALUE<GH_Curve>(pi.Name);
            }
            if (pt == typeof(GH_Surface))
            {
                listc = GET_VALUE<GH_Surface>(pi.Name);
            }
            if (pt == typeof(GH_Brep))
            {
                listc = GET_VALUE<GH_Brep>(pi.Name);
            }
            if (pt == typeof(GH_Mesh))
            {
                listc = GET_VALUE<GH_Mesh>(pi.Name);
            }
            if (pt == typeof(GH_Integer))
            {
                listc = GET_VALUE<int, GetInteger>(pi.Name, i => i.Get(), i => i.Number(), GetResult.Number);
            }
            if (pt == typeof(GH_Number))
            {
                listc = GET_VALUE<double, GetNumber>(pi.Name, i => i.Get(), i => i.Number(), GetResult.Number);
            }
            if (pt == typeof(GH_String))
            {
                listc = GET_VALUE<string, GetString>(pi.Name, i => i.Get(), i => i.StringResult(), GetResult.String);
            }
            pi.ClearData();
            pi.AddVolatileDataList(new GH_Path(0), listc);
        }
        public override GH_LoadingInstruction PriorityLoad()
        {
            Instances.CanvasCreated += CanvasCreatedEventHandler;

            return GH_LoadingInstruction.Proceed;
        }
        private void run_com(IGH_Component com)
        {
            if (com != null)
            {
               // DisplayConduit conduit= DisplayConduit.Rhino.RhinoDoc.ActiveDoc.Views.ActiveView.DisplayPipeline.d
                if (com == null)
                    return;

                int n = com.Params.Input.Count;
                for (int i = 0; i < n; i++)
                {
                    IGH_Param pi = com.Params.Input[i];
                    if(pi.Phase==GH_SolutionPhase.Blank&&!pi.Optional)
                    set_param(pi);
                }
                List<double> list = new List<double>();
                GET_I val = new GET_I(com);
                val.AcceptNothing(false);
                for (int i = 0; i < n; i++)
                {
                    val.AddOption(com.Params.Input[i].Name.Replace(' ', '_'));
                }
                val.SetCommandPrompt("Modify inputs");
                while (true)
                {
                    try
                    {
                        GetResult val2 = val.Get();
                        if (val2 == GetResult.Option)
                        {
                            int mi = val.OptionIndex();
                            if (mi <= n)
                            {
                                IGH_Param pi = null;
                                try
                                {
                                    pi = com.Params.Input[mi - 1];
                                }
                                catch
                                {
                                    com.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, mi.ToString());
                                }
                                if (pi == null)
                                    break;
                                set_param(pi);
                                try
                                {
                                    //com.ClearData();
                                    //com.CollectData();
                                    //com.ComputeData();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                    break;
                                }
                            }
                        }
                        else if (val2 == GetResult.Point)
                        {
                            continue;
                        }
                        else
                            break;
                    }
                    catch
                    {
                        val.Dispose();
                    }
                }
                int m = com.Params.Output.Count;
                GetString valo = new GetString();
                valo.AcceptNothing(false);
                List<int> bp = new List<int>();
                for (int i = 0; i < m; i++)
                {
                    if (com.Params.Output[i] is IGH_BakeAwareObject)
                    {
                        valo.AddOption(com.Params.Output[i].Name);
                        bp.Add(i);
                    }
                }
                valo.SetCommandPrompt("Bake outputs");
                while (true)
                {
                    try
                    {
                        GetResult val2 = valo.Get();
                        if (val2 == GetResult.Option)
                        {
                            int oo = valo.OptionIndex();
                            if (oo <= m)
                            {
                                IGH_Param pi = null;
                                try
                                {
                                    pi = com.Params.Output[bp[m - 1]];
                                }
                                catch
                                {
                                    com.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, oo.ToString());
                                }
                                if (pi == null)
                                    break;
                                try
                                {
                                    ((IGH_BakeAwareObject)pi).BakeGeometry(Rhino.RhinoDoc.ActiveDoc, new List<Guid>());
                                }
                                catch (Exception ex)
                                {
                                    com.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, ex.Message);
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    catch
                    {
                        valo.Dispose();
                    }
                }
                bp = null;
                val.Dispose();
                valo.Dispose();
            }
        }
        void CanvasCreatedEventHandler(GH_Canvas canvas)
        {
            try
            {
                canvas.KeyDown += new KeyEventHandler((o,e)=>
                {
                    if(e.KeyCode==Keys.Enter)
                    {
                        IGH_ObjectProxy proxy = null;
                        if (m_guid!=Guid.Empty)
                        proxy = Instances.ComponentServer.EmitObjectProxy(m_guid);
                        if (proxy != null)
                        {
                            IGH_Component com = proxy.CreateInstance() as IGH_Component;
                            if (com != null)
                            {
                                Instances.DocumentEditor.FadeOut();
                                run_com(com);
                                Instances.DocumentEditor.FadeIn();
                            }
                        }
                    }
                });
                GH_DocumentEditor editor = Instances.DocumentEditor;
                if (editor != null)
                {
                    GH_Ribbon ribbon = null;
                    foreach (Control control in editor.Controls)
                    {
                        ribbon = control as GH_Ribbon;
                        if (ribbon != null)
                        {
                            break;
                        }
                    }
                    if (ribbon != null)
                    {
                        ribbon.MouseDown += new MouseEventHandler((o, e) =>
                        {
                            bool t = GH_Document.EnableSolutions;
                            if (e.Button==MouseButtons.Middle)
                            {                                                                                
                                ALIEN_REFLECT rEFLECT = new ALIEN_REFLECT(ribbon);
                                GH_RibbonItem item = rEFLECT.GetField<GH_RibbonItem>("m_focus_item", true);
                                if (item != null)
                                {
                                    IGH_ObjectProxy proxy = item.Proxy;
                                    IGH_Component com = proxy.CreateInstance() as IGH_Component;
                                    if (com != null)
                                    {
                                        m_guid = proxy.Guid;
                                        ribbon.ActiveObject = null;
                                        canvas.ActiveInteraction = null;
                                        Instances.CursorServer.ResetCursor(canvas);
                                        editor.FadeOut();
                                        if (!t)
                                            t = true;
                                        run_com(com);
                                    }
                                    GH_Document.EnableSolutions = t;
                                    editor.FadeIn();
                                }
                            }
                        }
                        );
                    }
                    //GH_MenuStrip strip = null;
                    //foreach (Control control in editor.Controls)
                    //{
                    //    strip = control as GH_MenuStrip;
                    //    if (strip != null)
                    //    {
                    //        break;
                    //    }
                    //}
                    //if (strip != null)
                    //{
                    //    //ToolStripMenuItem itema = new ToolStripMenuItem("Mantis");
                    //    //itema.ForeColor = Color.FromArgb(255, 0, 255, 191);
                    //    ////set_my_items(itema);
                    //    //try
                    //    //{
                    //    //    strip.Items.Add(itema);
                    //    //}
                    //    //catch (Exception ex)
                    //    //{
                    //    //    MessageBox.Show(ex.Message + "gggggggg");
                    //    //}
                    // //GH_DocumentEditor.AggregateShortcutMenuItems += new GH_DocumentEditor.AggregateShortcutMenuItemsEventHandler(this.aggregate_menu_items);
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

