namespace UI.SETTINGS
{
    using Grasshopper;
    using Grasshopper.GUI;
    using Grasshopper.GUI.Ribbon;
    using Grasshopper.GUI.Canvas;
    using Grasshopper.GUI.Canvas.Interaction;
    using Grasshopper.Kernel;
    using Grasshopper.Kernel.Attributes;
    using Grasshopper.Kernel.Special;
    using ALIEN_DLL.GEOS;
    //using ALIEN_DLL.ATTS;
    using Rhino;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Reflection;
    using System.Threading;
    using System.Windows.Forms;
    using GH_IO.Serialization;
    using System.IO;
    using UI;
    public class UI_SETTING
    {
        internal delegate void ValueChangedEventHandler(string name,bool value);
        #region field
        private GH_SettingsServer m_setting;
        private ToolStripMenuItem m_item;
        private static UI_SETTING m_ins;
        private Dictionary<string, IUI_SETTING_ITEM> m_values;
        //private bool m_snap;
        //private bool m_sc;
        //private Pen m_scp;

        //private int m_scpw;

        //private bool m_se;
        //private Pen m_sep;
        //private int m_sepw;

        //private bool m_sw;
        //private Pen m_swp;
        //private int m_swpw;

        //private double m_sr;


        //private bool m_ssf;
        //private bool m_ui;
        //private bool m_att;
        //private bool m_menu;
        //private bool m_tag;
        //private bool m_gum;
        //private bool m_save;
        //private bool m_vs;
        //private bool m_addp;
        //private bool m_tutorial;
        //private bool m_thread;
        private string ap;
        private MODE m_mode;
        private Type m_ui_class;

        internal event PropertyChangedEventHandler VALUE_CHANGED;
        public event PropertyChangedEventHandler PROPERTY_CHANGED;
        #endregion
        #region construct
        internal UI_SETTING()
        {
            RhinoApp.Closing += rhcloseEventHandler;
            PROPERTY_CHANGED -= new PropertyChangedEventHandler(property_changed);
            PROPERTY_CHANGED += new PropertyChangedEventHandler(property_changed);
            if (Instances.ActiveCanvas == null)
                Instances.CanvasCreated += new Instances.CanvasCreatedEventHandler(canvas_created);
            else
            {
                canvas_created();
                //if (UI)
                //{
                //    Instances.ActiveCanvas.DocumentChanged += new GH_Canvas.DocumentChangedEventHandler(uiDocumentAddedEventHandler);
                //    Instances.ActiveCanvas.Document_ObjectsAdded += new GH_Canvas.Document_ObjectsAddedEventHandler(uiObjectsAddedEventHandler);
                //}
                //if (THREAD)
                //{
                //    Instances.ActiveCanvas.DocumentChanged += new GH_Canvas.DocumentChangedEventHandler(threadDocumentAddedEventHandler);
                //    Instances.ActiveCanvas.Document_ObjectsAdded += new GH_Canvas.Document_ObjectsAddedEventHandler(threadObjectsAddedEventHandler);
                //}
                //if (SNAP)
                //{
                //    GH_Canvas c = Instances.ActiveCanvas;
                //    if (c != null)
                //        c.MouseDown += new MouseEventHandler(Canvas_MouseDown);
                //}
            }

        }
        #endregion
        #region method
        private void init()
        {
            m_values = new Dictionary<string, IUI_SETTING_ITEM>();
            ADD_BOOL("SSF",true);
            ADD_BOOL("UI", true);
            ADD_BOOL("ATT", true);
            ADD_BOOL("MENU", true);
            ADD_BOOL("TAG", true);
            ADD_BOOL("GUM", true);

            ADD_INT("MODE", 1);

            ADD_BOOL("SANP", true);
            ADD_BOOL("SC", true);
            ADD_COLOR("SCPC", Color.Red);
            ADD_DOUBLE("SCPW", 0.5);
            ADD_INT("SCPD", 5);
            ADD_BOOL("SE", true);
            ADD_COLOR("SEPC", Color.Red);
            ADD_DOUBLE("SEPW", 0.5);
            ADD_INT("SEPD", 5);
            ADD_BOOL("SW", true);
            ADD_COLOR("SWPC", Color.Red);
            ADD_DOUBLE("SWPW", 0.5);
            ADD_INT("SWPD", 5);

        }
        private void add_item(IUI_SETTING_ITEM _ITEM)
        {
            _ITEM.ADD_TO_SETTING(this);
        }
   
        //internal void SET_BOOL(string name,bool value)
        //{
        //    if (m_values.ContainsKey(name))
        //    {
        //        m_values[name].VALUE = value;
        //        SETTINGS.SetValue(name, value);
        //        OnValueChanged(m_values[name],name);
        //        OnPropertyChanged(null,name);
        //    }
        //    else
        //    {
        //        m_values.Add(name, value);
        //        m_values[name] = SETTINGS.GetValue(name, value);
        //    }
        //}
        //internal void ADD_INT(string name, int value)
        //{
        //    if (m_values.ContainsKey(name))
        //    {
        //        m_values[name] = value;
        //        SETTINGS.SetValue(name, value);
        //        OnValueChanged(m_values[name], name);
        //        OnPropertyChanged(null, name);
        //    }
        //    else
        //    {
        //        m_values.Add(name, value);
        //        m_values[name] = SETTINGS.GetValue(name, value);
        //    }
        //}
        //internal void ADD_DOUBLE(string name, double value)
        //{
        //    if (m_values.ContainsKey(name))
        //    {
        //        m_values[name] = value;
        //        SETTINGS.SetValue(name, value);
        //        OnValueChanged(m_values[name], name);
        //        OnPropertyChanged(null, name);
        //    }
        //    else
        //    {
        //        m_values.Add(name, value);
        //        m_values[name] = SETTINGS.GetValue(name, value);
        //    }
        //}
        //internal void ADD_COLOR(string name, Color value)
        //{
        //    if (m_values.ContainsKey(name))
        //    {
        //        m_values[name] = value;
        //        SETTINGS.SetValue(name, value);
        //        OnValueChanged(m_values[name], name);
        //        OnPropertyChanged(null, name);
        //    }
        //    else
        //    {
        //        m_values.Add(name, value);
        //        m_values[name] = SETTINGS.GetValue(name, value);
        //    }
        //}
        internal void OnPropertyChanged(object o,string name)
        {
            PROPERTY_CHANGED(o, new PropertyChangedEventArgs(name));
        }
        internal void OnValueChanged(object o, string name)
        {
            VALUE_CHANGED(o, new PropertyChangedEventArgs(name));
        }

        private void property_changed(object o, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "S":
                case "PUI":
                 

                    break;
                case "PATT":
                    //  Style(new Action<IGH_ActiveObject, bool>(Change_ATT), m_att);
                    break;
                case "PMENU":
                    //redraw();
                    break;
                case "PSNAP":
                    if (Instances.ActiveCanvas != null)
                    {
                        Instances.ActiveCanvas.MouseDown -= new MouseEventHandler(Canvas_MouseDown);
                        if (m_snap)
                            Instances.ActiveCanvas.MouseDown += new MouseEventHandler(Canvas_MouseDown);
                    }
                    break;

                case "PSAVE":
                    if (Instances.ActiveCanvas == null)
                        break;
                    Instances.ActiveCanvas.DocumentChanged -= new GH_Canvas.DocumentChangedEventHandler(saveDocumentChangedEventHandler);
                    if (m_save)
                        Instances.ActiveCanvas.DocumentChanged += new GH_Canvas.DocumentChangedEventHandler(saveDocumentChangedEventHandler);
                    break;
                case "PTHREAD":
                    if (Instances.ActiveCanvas != null)
                    {
                        Instances.ActiveCanvas.DocumentChanged -= new GH_Canvas.DocumentChangedEventHandler(threadDocumentAddedEventHandler);
                        Instances.ActiveCanvas.Document_ObjectsAdded -= new GH_Canvas.Document_ObjectsAddedEventHandler(threadObjectsAddedEventHandler);
                        if (m_thread)
                        {
                            Instances.ActiveCanvas.DocumentChanged += new GH_Canvas.DocumentChangedEventHandler(threadDocumentAddedEventHandler);
                            Instances.ActiveCanvas.Document_ObjectsAdded += new GH_Canvas.Document_ObjectsAddedEventHandler(threadObjectsAddedEventHandler);
                        }
                        if (Instances.ActiveCanvas.Document != null)
                            CHANGE(new Action<IGH_ActiveObject, bool>(threadChange), m_thread, Instances.ActiveCanvas.Document.Objects.ToList());
                    }
                    break;
                case "PDARK_MODE":
                    if (Instances.DocumentEditor != null)
                    {
                        change_mode();
                    }
                    break;
                case "UPDATE":
                    break;
            }

        }
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            GH_Canvas canvas = sender as GH_Canvas;
            if (((bool)m_values["SNAP"] && (canvas != null)) && (e.Button == MouseButtons.Left))
            {
                if ((canvas.ActiveInteraction != null) && (canvas.ActiveInteraction is GH_DragInteraction))
                {
                    try
                    {
                        GH_CanvasMouseEvent event2 = new GH_CanvasMouseEvent(canvas.Viewport, e);
                        GH_DragInteraction target = new GH_DragInteraction(canvas, event2);
                        PANDA_SNAP wrapper = new PANDA_SNAP(canvas, event2, target);
                        canvas.ActiveInteraction = wrapper;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("SNAP ERROR:" + ex.Message);
                    }


                }
                else if (canvas.ActiveObject != null)
                {
                    //MessageBox.Show("ergrergr55555555555eg");
                    IGH_Attributes resizableAttributes = canvas.ActiveObject.Attributes;
                    if (Utils.IsSubclassOfRawGeneric(typeof(GH_ResizableAttributes<GH_Panel>).GetGenericTypeDefinition(), resizableAttributes.GetType()))
                    {
                        GH_CanvasMouseEvent me = new GH_CanvasMouseEvent(canvas.Viewport, e);
                        if (IsMouseDownOnBorders(resizableAttributes, me))
                        {
                            PANDA_SNAP wrapper2 = new PANDA_SNAP(canvas, me, canvas.ActiveObject.Attributes)
                            {
                                Resizing = true
                            };
                            canvas.ActiveInteraction = wrapper2;
                        }
                    }
                }
            }
        }
        private bool IsMouseDownOnBorders(IGH_Attributes resizableAttributes, GH_CanvasMouseEvent me)
        {
            Padding? nullable = null;
            try
            {
                nullable = resizableAttributes.GetType().BaseType.GetProperty("SizingBorders", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(resizableAttributes) as Padding?;
            }
            catch (Exception)
            {
            }
            finally
            {
                if (!nullable.HasValue)
                {
                    if (resizableAttributes is GH_NumberSliderAttributes)
                    {
                        nullable = new Padding(0, 0, 6, 0);
                    }
                    else
                    {
                        nullable = new Padding(0, 0, 6, 0);
                    }
                }
            }
            using (List<GH_Border>.Enumerator enumerator = GH_Border.CreateBorders(resizableAttributes.Bounds, nullable.Value).GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    if (enumerator.Current.Contains(me.CanvasLocation))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void canvas_created()
        {
            //MessageBox.Show("66666");

            try
            {
                SSF = SETTINGS.GetValue("PSSF", true);
                UI = SETTINGS.GetValue("PUI", true);
                ATT = SETTINGS.GetValue("PATT", false);
                MENU = SETTINGS.GetValue("PMENU", true);
                TAG = SETTINGS.GetValue("PTAG", true);
                GUM = SETTINGS.GetValue("PGUM", true);
                SAVE = SETTINGS.GetValue("PSAVE", true);
                SNAP = SETTINGS.GetValue("PSNAP", true);
                SC = SETTINGS.GetValue("PSNAPC", true);
                SE = SETTINGS.GetValue("PSNAPE", true);
                SW = SETTINGS.GetValue("PSNAPW", true);
                DARK_MODE = (MODE)(SETTINGS.GetValue("PDARK_MODE", 0));
                THREAD = SETTINGS.GetValue("PTHREAD", true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("XML error" + ex.Message);
            }
            EventHandler();
            Instances.ActiveCanvas.Document_ObjectsDeleted += new GH_Canvas.Document_ObjectsDeletedEventHandler(gumObjectsDeletedEventHandler);

            //Instances.CanvasCreated -= new Instances.CanvasCreatedEventHandler(canvas_created);
        }
        private void canvas_created(GH_Canvas canvas)
        {
            try
            {
                SSF = SETTINGS.GetValue("PSSF", true);
                UI = SETTINGS.GetValue("PUI", true);
                ATT = SETTINGS.GetValue("PATT", false);
                MENU = SETTINGS.GetValue("PMENU", true);
                TAG = SETTINGS.GetValue("PTAG", true);
                GUM = SETTINGS.GetValue("PGUM", true);
                SAVE = SETTINGS.GetValue("PSAVE", true);
                SNAP= SETTINGS.GetValue("PSNAP", true);
                SC= SETTINGS.GetValue("PSC", true);
                SCP =new Pen(SETTINGS.GetValue("PSC", Color.Red), (float)SETTINGS.GetValue("PSC", 0.5));
                SCPD = SETTINGS.GetValue("PSCPW", 1);




                DARK_MODE = (MODE)(SETTINGS.GetValue("PDARK_MODE", 0));
                THREAD = SETTINGS.GetValue("PTHREAD", true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("XML error" + ex.Message);
            }
            EventHandler();
            Instances.ActiveCanvas.Document_ObjectsDeleted += new GH_Canvas.Document_ObjectsDeletedEventHandler(gumObjectsDeletedEventHandler);

            // Instances.DocumentEditor.Load += EventHandler;


            //Instances.CanvasCreated -= new Instances.CanvasCreatedEventHandler(canvas_created);
        }
        private PaintEventHandler DrawCanvasGrid(GH_Canvas m_canvas)
        {
            return (o,e)=>
            {
                
                RectangleF bounds = m_canvas.Viewport.VisibleRegion;
                int num = Convert.ToInt32((double)(int)GH_Skin.canvas_grid.A * ((double)GH_Canvas.ZoomFadeLow / 255.0));
                if (num < 2)
                {
                    return;
                }
                Color baseColor = Color.FromArgb(num, Color.Yellow);
                int num2 = GH_Skin.canvas_grid_col;
                int num3 = GH_Skin.canvas_grid_row;
                if (m_canvas.IsDocument && m_canvas.Document.Owner != null)
                {
                    baseColor = Color.FromArgb(num, 0, 0, 100);
                    num2 = Math.Min(num2, num3);
                    num3 = num2;
                }
                Pen pen = new Pen(Color.FromArgb(num, baseColor),20/m_canvas.Viewport.Zoom);
                int num4 = Convert.ToInt32(bounds.Left / (float)num2) - 1;
                Math.Min(Math.Max(0f, bounds.Top), bounds.Bottom);
                for (int i = num4; i <= int.MaxValue; i++)
                {
                    int num5 = num2 * i;
                    if ((float)num5 > bounds.Right)
                    {
                        break;
                    }
                    e.Graphics.DrawLine(pen, num5, bounds.Top, num5, bounds.Bottom);
                }
                int num6 = Convert.ToInt32(bounds.Top / (float)num3) - 1;
                Math.Min(Math.Max(0f, bounds.Left), bounds.Right);
                for (int j = num6; j <= int.MaxValue; j++)
                {
                    int num7 = num3 * j;
                    if ((float)num7 > bounds.Bottom)
                    {
                        break;
                    }
                    e.Graphics.DrawLine(pen, bounds.Left, num7, bounds.Right, num7);
                }
                pen.Dispose();
                m_canvas.Refresh();
            };
        }

        void EventHandler()
        {
            change_mode();
            //Instances.DocumentEditor.Load -= EventHandler;
        }
        void EventHandler(object sender, EventArgs e)
        {
           change_mode();
           Instances.DocumentEditor.Load -= EventHandler;
        }
        private void change_mode()
        {
            GH_DocumentEditor editor = Grasshopper.Instances.DocumentEditor;
            
           Color bc = SETTINGS.GetValue("PBACKC",  Color.FromArgb(255, 205, 205, 205));
            Color fc = SETTINGS.GetValue("PFOREC", Color.Black);
            Color cb= SETTINGS.GetValue("PCBC", Color.FromArgb(255, 205, 205, 205));

            GH_Splitter _Splitter= Grasshopper.Instances.DocumentEditor.Controls[2] as GH_Splitter;

            if (m_mode == MODE.DARK)
            {
                GH_Skin.canvas_back = Color.FromArgb(255, 105, 105, 105);
             
                bc = Color.FromArgb(255, 105, 105, 105);
                fc = Color.Yellow;
                _Splitter.BackColor = Color.FromArgb(255, 106, 106, 106);
                SETTINGS.SetValue("PBACKC", bc);
                SETTINGS.SetValue("PFOREC",fc);
                SETTINGS.SetValue("PCBC", fc);
            }
            if (m_mode == MODE.LIGHT)
            {
                bc = Color.FromArgb(255, 205, 205, 205);
                fc = Color.Black;
                GH_Skin.canvas_back = Color.FromArgb(255, 205, 205, 205);
                SETTINGS.SetValue("PBACKC", bc);
                SETTINGS.SetValue("PFOREC", fc);
                SETTINGS.SetValue("PCBC", fc);
                _Splitter.BackColor = Color.FromArgb(255, 205, 205, 205);
            }
            editor.BackColor= bc;
          
        
            Panel p = editor.Controls[0] as Panel;
            p.BackColor = bc;
            GH_Toolstrip toolstrip = p.Controls[1] as GH_Toolstrip;
            toolstrip.BackColor = bc;
            foreach (ToolStripItem control in toolstrip.Items)
            {
                if (control.ForeColor == Color.Yellow || control.ForeColor == SystemColors.ControlText || control.ForeColor == Color.Black)
                    control.ForeColor = fc;
                control.BackColor = bc;
                if (!control.Enabled)
                {
                    control.ForeColor = Color.FromArgb(127, fc);
                }
                //if (!control.Enabled)
                //{
                //    int i = 0;
                //    if (fc.R < 150)
                //        i = 63;
                //    else
                //        i = -63;
                //    control.BackColor = Color.FromArgb(255, fc.R + i, fc.G + i, fc.B + i);
                //}
                if (control is ToolStripDropDownItem)
                {
                    change_mode(control as ToolStripDropDownItem, bc, fc);
                }
            }


            StatusStrip strip= editor.Controls[1] as StatusStrip;
            strip.BackColor = bc;
            foreach (ToolStripItem control in strip.Items)
            {
                control.ForeColor = fc;
                control.BackColor = bc; 
            }
            

            GH_Ribbon ribbon = Grasshopper.Instances.DocumentEditor.Controls[3] as GH_Ribbon;
            ribbon.ForeColor = fc;
            ribbon.BackColor = bc;
           

            GH_MenuStrip menuStrip=Grasshopper.Instances.DocumentEditor.Controls[4] as GH_MenuStrip;
            menuStrip.BackColor = bc;
            foreach (ToolStripItem control in menuStrip.Items)
            {
                if (control.ForeColor == Color.Yellow || control.ForeColor == SystemColors.ControlText || control.ForeColor == Color.Black)
                    control.ForeColor = fc;
                if (!control.Enabled)
                    control.BackColor = Color.FromArgb(127, bc);
                else
                    control.BackColor = bc;
                if (control is ToolStripDropDownItem)
                {
                    (control as ToolStripDropDownItem).DropDownOpening += (o, e) => 
                    {
                        change_mode(control as ToolStripDropDownItem, bc, fc);
                    };                 
                }
            }
            if (m_mode == MODE.TRANSPARENT)
            {
                GH_Skin.canvas_grid = editor.BackColor;
                GH_Skin.canvas_shade= editor.BackColor;
         
                editor.TransparencyKey = editor.BackColor;
            }
            else
            {
                if (m_mode == MODE.LIGHT)
                {
                    GH_Skin.canvas_grid = Color.FromArgb(255, 127, 127, 127);
                    GH_Skin.canvas_shade = Color.FromArgb(255, 127, 127, 127);
                }
                else
                {
                    GH_Skin.canvas_grid = Color.FromArgb(255,63,63,63);
                    GH_Skin.canvas_shade = Color.FromArgb(255, 63,63, 63);
                }
                editor.TransparencyKey = Color.Empty ;
            }
                editor.Refresh();
        }
        private void mnuHelp_DropDownOpening(object sender, EventArgs e)
        {
  
        }
        private void change_mode(ToolStripDropDownItem cn, Color bc, Color fc)
        {
            foreach (ToolStripItem control in cn.DropDownItems)
            {
                if (control.ForeColor == Color.Yellow || control.ForeColor == SystemColors.ControlText || control.ForeColor == Color.Black)
                    control.ForeColor = fc;
                //if(!control.Enabled)
                //{
                //    control.ForeColor =Color.FromArgb(127, fc);
                //}
                
                if (!control.Enabled)
                    control.BackColor = Color.FromArgb(127, bc);
                else
                    control.BackColor = bc;
                if (control is ToolStripDropDownItem)
                {
                    (control as ToolStripDropDownItem).DropDownOpening += (o, e) =>
                    {
                        change_mode(control as ToolStripDropDownItem, bc, fc);
                    };
                }
            }
        }


        private void threadChange(IGH_ActiveObject com, bool va)
        {
            //try
            //{
            //    if (Instances.ActiveCanvas.Document.Owner != null)
            //        return;
            //    IGH_Component comt = com as IGH_Component;
            //    if (comt != null)
            //    {
            //        if (va)
            //        {            
            //            bool b = comt.Attributes.GetType().ToString() == "UI.ATTS.ATT_NORMAL";
            //            bool c = comt.Attributes.GetType().ToString() == "UI.ATTS.ATTR_SCRIPT";
            //            bool d = comt.Attributes.GetType().ToString() == "UI.ATTS.ATT_CLUSTER";
                    
            //            if (b || c || d)
            //            {
            //                PANDA.COMS.SET_THREADC TCOM = new PANDA.COMS.SET_THREADC() { COM = comt };
            //                TCOM.COVER();
            //            }
            //        }
            //        else
            //        {
            //            if (comt.GetType() == typeof(PANDA.COMS.SET_THREADC))
            //            {
            //                PANDA.COMS.SET_THREADC TCOM = comt as PANDA.COMS.SET_THREADC;
            //                if (TCOM != null)
            //                    if (TCOM.Params.Count() > 0)
            //                        TCOM.RECOVER();
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("threadChange" + ex.Message);
            //}
        }
        private void threadObjectsAddedEventHandler(object sender, GH_DocObjectEventArgs e)
        {
            CHANGE(new Action<IGH_ActiveObject, bool>(threadChange), m_thread, e.Objects.ToList());
        }
        private void threadDocumentAddedEventHandler(GH_Canvas sender, GH_CanvasDocumentChangedEventArgs e)
        {
            GH_Canvas c = sender as GH_Canvas;
            c.Document.Enabled = false;
            CHANGE(new Action<IGH_ActiveObject, bool>(threadChange), m_thread, e.NewDocument.Objects.ToList());
            c.Document.Enabled = true;
        }

        //private void gumballsChanged_Handler()
        //{
        //    foreach (IGH_DocumentObject documentObject in Instances.ActiveCanvas.Document.Objects)
        //    {
        //        I_PATT aTT = documentObject.Attributes as I_PATT;
        //        if (aTT == null)
        //            continue;
        //        if (aTT.SELECTED && (UI_SETTING.SETTINGS.GUM ? aTT.GUM : false))
        //        {
        //            aTT.SHOW_GUM();
        //        }
        //    }
        //}
        private void gumObjectsDeletedEventHandler(object sender, GH_DocObjectEventArgs e)
        {
            foreach (IGH_Attributes attributes in e.Attributes)
            {
                I_PATT aTT = attributes as I_PATT;
                if (aTT == null)
                    continue;
                aTT.HIDE_GUM();
            }
        }

        private void saveDocumentChangedEventHandler(GH_Canvas sender, GH_CanvasDocumentChangedEventArgs e)
        {
            if ((((e.NewDocument != null) && !e.NewDocument.IsFilePathDefined) && (e.NewDocument.Owner == null)) && (MessageBox.Show("Save the new file?", "Panda Save", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                GH_DocumentIO tio = new GH_DocumentIO(e.NewDocument);
                if (tio.Save())
                {
                }
            }
        }


        private void redraw()
        {
            if (Instances.ActiveCanvas.Document == null)
                return;
            Instances.ActiveCanvas.Refresh();
        }

        private void rhcloseEventHandler(object sender, EventArgs e)
        {

            RhinoApp.Closing -= new EventHandler(rhcloseEventHandler);
            try
            {

                GH_Archive archive = new GH_Archive();
                archive.AppendObject(m_setting, "SXML");
                string settingsFolder = Folders.SettingsFolder;
                string path = SXML + ".dat";
                string path2 = Path.Combine(settingsFolder, path);
                if (!Directory.Exists(settingsFolder))
                {
                    Directory.CreateDirectory(settingsFolder);
                }
                File.WriteAllBytes(path2, archive.Serialize_Binary());
            }
            catch
            {

            }

        }

        #endregion
        #region property
        internal object  this[string name]
        {
            get
            {
                return m_values[name];
            }
            set
            {
                object  it = value;
                if (it is IUI_SETTING_ITEM)
                {
                    m_values[name] = value as IUI_SETTING_ITEM;
                }
                if(m_values[name].TYPE==it.GetType())
                {
                    m_values[name].VALUE=it;
                }
            }
        }


        internal ToolStripMenuItem ITEM
        {
            get
            {
                if(m_item==null)
                {
                    GH_DocumentEditor editor = Instances.DocumentEditor;
                    if (editor == null)
                        return null;
                    GH_MenuStrip strip = null;
                    foreach (Control control in editor.Controls)
                    {
                        strip = control as GH_MenuStrip;
                        if (strip != null)
                        {
                            break;
                        }
                    }
                    if (strip != null)
                    {
                        ToolStripMenuItem item = null;
                        ToolStripMenuItem itemU = null;
                        foreach (ToolStripItem t in strip.Items)
                        {
                            if (t.Text == "Panda" && t.ForeColor == Color.Red)
                            {
                                item = t as ToolStripMenuItem;
                                foreach (ToolStripItem ti in item.DropDownItems)
                                {
                                    if (ti.Text == "UI")
                                    {
                                        itemU = ti as ToolStripMenuItem;
                                    }
                                }
                            }
                        }
                        if (item == null)
                        {
                            item = new ToolStripMenuItem
                            {
                                Text = "Panda",
                                ForeColor = Color.Red
                            };
                            strip.Items.Add(item);
                        }
                        if (itemU == null)
                        {
                            itemU = new ToolStripMenuItem
                            {
                                Text = "UI"
                            };
                            item.DropDownItems.Add(itemU);
                        }
                        m_item = itemU;
                    }
                }
                return m_item;
            }
        }




        internal GH_Document DOC
        {
            get
            {
                if (Instances.ActiveCanvas != null)
                {
                    return Instances.ActiveCanvas.Document;
                }
                return null;
            }
        }
        //internal bool SSF
        //{
        //    get
        //    {
        //        try
        //        {
        //            //if(CONTROL_MENU_ITEMS.ITEMS.menu_items.Count>111)
        //            //CONTROL_MENU_ITEMS.ITEMS[1].Checked = m_ssf;
        //        }
        //        catch
        //        {

        //        }
        //        return m_ssf;

        //    }
        //    set
        //    {
        //        bool flag = value;
        //        if (m_ssf != flag)
        //        {
        //            m_ssf = flag;
        //            try
        //            {
        //                //CONTROL_MENU_ITEMS.ITEMS[11].Checked=m_ssf;
        //            }
        //            catch
        //            {

        //            }
        //            XML.SetValue("PSSF", m_ssf);
        //        }
        //    }
        //}
        //public bool UI
        //{
        //    get
        //    {
        //        return m_ui;
        //    }
        //    set
        //    {
        //        bool flag = value;
        //        if (m_ui != flag)
        //        {
        //            m_ui = flag;
        //            XML.SetValue("PUI", m_ui);
        //            OnValueChanged(m_ui, "PUI");
        //            OnPropertyChanged(null,"PUI");
        //        }
        //    }
        //}
        //public bool ATT
        //{
        //    get
        //    {
        //        return m_att;
        //    }
        //    set
        //    {
        //        bool flag = value;
        //        if (m_att != flag)
        //        {
        //            m_att = flag;
        //            XML.SetValue("PATT", m_att);
        //            VALUE_CHANGED?.Invoke("ATT", m_att);
        //            OnPropertyChanged(null, "PATT");
        //        }
        //    }
        //}
        //public bool MENU
        //{
        //    get
        //    {
        //        return m_menu;
        //    }
        //    set
        //    {
        //        bool flag = value;
        //        if (m_menu != flag)
        //        {
        //            m_menu = flag;
        //            XML.SetValue("PMENU", m_menu);
        //            VALUE_CHANGED?.Invoke("MENU", m_menu);
        //            OnPropertyChanged(null, "PMENU");
        //        }
        //    }
        //}
        //public bool TAG
        //{
        //    get
        //    {
        //        return m_tag;
        //    }
        //    set
        //    {
        //        bool flag = value;
        //        if (m_tag != flag)
        //        {
        //            m_tag = flag;
        //            XML.SetValue("PTAG", m_tag);
        //            VALUE_CHANGED?.Invoke("TAG", m_tag);
        //            OnPropertyChanged(null, "PDISTAG");

        //        }
        //    }
        //}
        //public bool GUM
        //{
        //    get
        //    {
        //        return m_gum;
        //    }
        //    set
        //    {
        //        bool flag = value;
        //        if (m_gum != flag)
        //        {
        //            m_gum = flag;
        //            XML.SetValue("PGUM", m_gum);
        //            VALUE_CHANGED?.Invoke("GUM", m_gum);
        //            OnPropertyChanged(null, "PGUM");
        //        }
        //    }
        //}
        //public bool SAVE
        //{
        //    get
        //    {
        //        return m_save;
        //    }
        //    set
        //    {
        //        bool flag = value;
        //        if (m_save != flag)
        //        {
        //            m_save = flag;
        //            XML.SetValue("PSAVE", m_save);
        //            VALUE_CHANGED?.Invoke("SAVE", m_save);
        //            OnPropertyChanged(null, "PSAVE");
        //        }
        //    }
        //}
        //public bool THREAD
        //{
        //    get
        //    {
        //        return m_thread;
        //    }
        //    set
        //    {
        //        bool flag = value;
        //        if (m_thread != flag)
        //        {
        //            m_thread = flag;
        //            XML.SetValue("PTHREAD", m_thread);
        //            VALUE_CHANGED?.Invoke("THREAD", m_thread);
        //            OnPropertyChanged(null, "PTHREAD");
        //        }
        //    }
        //}
        //internal bool SNAP
        //{
        //    get
        //    {
        //        return m_snap && m_ui;
        //    }
        //    set
        //    {
        //        m_snap = value;
        //        XML.SetValue("PSNAP", m_snap);
        
        //        OnPropertyChanged(null, "PSNAP");

        //    }
        //}
        //internal bool SC
        //{
        //    get
        //    {
        //        return m_sc;
        //    }
        //    set
        //    {
        //        m_sc = value;
        //        OnPropertyChanged(null, "SC");
        //    }
        //}
        //internal Pen SCP
        //{
        //    get
        //    {
        //        return m_scp;
        //    }
        //    set
        //    {
        //        m_scp = value;
        //        OnPropertyChanged(null, "SCP");
        //    }
        //}
        //internal int SCPD
        //{
        //    get
        //    {
        //        return m_scpw;
        //    }
        //    set
        //    {
        //        m_scpw = value;
        //        OnPropertyChanged(null, "SCPW");
        //    }
        //}
        //internal bool SE
        //{
        //    get
        //    {
        //        return m_se;
        //    }
        //    set
        //    {
        //        m_se = value;
        //        OnPropertyChanged(null, "SE");
        //    }
        //}
        //internal Pen SEP
        //{
        //    get
        //    {
        //        return m_sep;
        //    }
        //    set
        //    {
        //        m_sep = value;
        //        OnPropertyChanged(null, "SEP");
        //    }
        //}
        //internal int SEPW
        //{
        //    get
        //    {
        //        return m_sepw;
        //    }
        //    set
        //    {
        //        m_sepw = value;
        //        OnPropertyChanged(null, "SEPW" +
        //            "");
        //    }
        //}
        //internal double SR
        //{
        //    get
        //    {
        //        return m_sr;
        //    }
        //    set
        //    {
        //        m_sr = value;
        //        OnPropertyChanged(null, "SR");
        //    }
        //}
        //internal bool SW
        //{
        //    get
        //    {
        //        return m_sw;
        //    }
        //    set
        //    {
        //        m_sw = value;
        //        OnPropertyChanged(null, "SW");
        //    }
        //}
        //internal Pen SWP
        //{
        //    get
        //    {
        //        return m_swp;
        //    }
        //    set
        //    {
        //        m_swp = value;
        //        OnPropertyChanged(null, "SWP");
        //    }
        //}
        //internal int SWPW
        //{
        //    get
        //    {
        //        return m_swpw;
        //    }
        //    set
        //    {
        //        m_swpw = value;
        //        OnPropertyChanged(null, "SWPW");
        //    }
        //}
        public MODE DARK_MODE
        {
            get
            {
                return m_mode;
            }
            set
            {
                MODE flag = value;
                if (m_mode != flag)
                {
                    m_mode = flag;
                    switch(m_mode)
                    {
                        case MODE.DARK:
                            SETTINGS.SetValue("PDARK_MODE", (int)m_mode);
                            VALUE_CHANGED?.Invoke("DARK_MODE", true);
                            break;
                        case MODE.LIGHT:
                            SETTINGS.SetValue("PDARK_MODE", (int)m_mode);
                            VALUE_CHANGED?.Invoke("LIGHT_MODE", true);
                            break;
                        case MODE.TRANSPARENT:
                            SETTINGS.SetValue("PDARK_MODE", (int)m_mode);
                            VALUE_CHANGED?.Invoke("TRANS_MODE", true);
                            break;
                    }

                    OnPropertyChanged(null, "PDARK_MODE");
                }
            }
        }
        internal Type UI_CLASS
        {
            get
            {
                if(m_ui_class==null)
                {
                    m_ui_class = Assembly.Load(ICONS.UI_DLL).GetType("UI_DLL_CLASS");
                }
                return m_ui_class;
            }
        }
        public GH_SettingsServer SETTINGS
        {
            get
            {
                if (m_setting == null)
                {
                    m_setting = new GH_SettingsServer(SXML, false);
                    GH_Archive archive = new GH_Archive();
                    string settingsFolder = Folders.SettingsFolder;
                    string path = SXML + ".dat";
                    string path2 = Path.Combine(settingsFolder, path);
                    try
                    {
                        archive.Deserialize_Binary(File.ReadAllBytes(path2));
                        archive.ExtractObject(m_setting, "SXML");
                    }
                    catch (Exception EX)
                    {
                        //MessageBox.Show(EX.InnerException.Message);
                    }
                }
                return m_setting;
            }
        }
        internal string SXML
        {
            get
            {
#if rh6
                return @"Libraries\UI";
#else
                return @"Libraries\Panda\UI";
#endif
            }
        }
        internal string AP
        {
            get
            {
                if (ap == string.Empty || ap == null || ap == "")
                {

                    string codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                    UriBuilder uri = new UriBuilder(codeBase);
                    ap = Uri.UnescapeDataString(uri.Path);
                }
                return ap;

            }

        }
        internal Dictionary<string, IUI_SETTING_ITEM> VALUES => m_values;
        public static UI_SETTING INS
        {
            get
            {
                if (m_ins == null)
                    m_ins = new UI_SETTING();
                return m_ins;
            }
        }
        #endregion
    }
}

